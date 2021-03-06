﻿using System;

namespace EGL.GodotBase.GUI.Text
{
    public class RichTextLabel : INode
    {
        public Action Ready = null;
        public Action ExitTree = null;
        public Action EnterTree = null;

        private Action<float> _RenderProcess = null;
        private Action<float> _PhysicsProcess = null;

        private Action<Godot.InputEvent> _InputProcess = null;
        private Action<Godot.InputEvent> _UnhandledInputProcess = null;
        private Action<Godot.InputEvent> _UnhandledKeyInputProcess = null;
        private Action<Notification> _NotificationProcess = null;

        public Action<float> RenderProcess
        {
            get
            {
                return _RenderProcess;
            }
            set
            {
                _RenderProcess = value;
                if (_RenderProcess == null)
                    Base.SetProcess(false);
                else
                    Base.SetProcess(true);
            }
        }
        public Action<float> PhysicsProcess
        {
            get
            {
                return _PhysicsProcess;
            }
            set
            {
                _PhysicsProcess = value;
                if (_PhysicsProcess == null)
                    Base.SetPhysicsProcess(false);
                else
                    Base.SetPhysicsProcess(true);
            }
        }

        public Action<Godot.InputEvent> InputProcess
        {
            get
            {
                return _InputProcess;
            }
            set
            {
                _InputProcess = value;
                if (_InputProcess == null)
                    Base.SetProcessInput(false);
                else
                    Base.SetProcessInput(true);
            }
        }
        public Action<Godot.InputEvent> UnhandledInputProcess
        {
            get
            {
                return _UnhandledInputProcess;
            }
            set
            {
                _UnhandledInputProcess = value;
                if (_UnhandledInputProcess == null)
                    Base.SetProcessUnhandledInput(false);
                else
                    Base.SetProcessUnhandledInput(true);
            }
        }
        public Action<Godot.InputEvent> UnhandledKeyInputProcess
        {
            get
            {
                return _UnhandledKeyInputProcess;
            }
            set
            {
                _UnhandledKeyInputProcess = value;
                if (_UnhandledKeyInputProcess == null)
                    Base.SetProcessUnhandledKeyInput(false);
                else
                    Base.SetProcessUnhandledKeyInput(true);
            }
        }
        public Action<Notification> NotificationProcess
        {
            get
            {
                return _NotificationProcess;
            }
            set
            {
                _NotificationProcess = value;
            }
        }

        public event Action OnReady;
        public event Action Renamed;
        public event Action OnTreeEntered;
        public event Action OnTreeExited;
        public event Action OnTreeExiting;
        public event Action ScriptChanged;

        //CanvasItem
        public event Action Draw;
        public event Action Hide;
        public event Action ItemRectChanged;
        public event Action VisibilityChanged;

        //Control
        public event Action FocusEntered;
        public event Action FocusExited;
        public event Action<Godot.InputEvent> GUIInput;
        public event Action MinimumSizeChanged;
        public event Action ModalClosed;
        public event Action MouseEntered;
        public event Action MouseExited;
        public event Action Resized;
        public event Action SizeFlagsChanged;

        //RichTextLabel
        public event Action<object> MetaClicked;
        public event Action<object> MetaHoverEnded;
        public event Action<object> MetaHoverStarted;

        public Godot.RichTextLabel Base { get; }

        public RichTextLabel()
        {
            Base = new _RichTextLabel(this);
        }

        protected class _RichTextLabel : Godot.RichTextLabel
        {
            public RichTextLabel ClassOwner;

            public _RichTextLabel(RichTextLabel node)
            {
                ClassOwner = node;

                //Node
                Connect("ready", this, nameof(Ready));
                Connect("renamed", this, nameof(Renamed));
                Connect("tree_entered", this, nameof(TreeEntered));
                Connect("tree_exited", this, nameof(TreeExited));
                Connect("tree_exiting", this, nameof(TreeExiting));
                Connect("script_changed", this, nameof(ScriptChanged));

                //CanvasItem
                Connect("draw", this, nameof(Draw));
                Connect("hide", this, nameof(_Hide));
                Connect("item_rect_changed", this, nameof(ItemRectChanged));
                Connect("visibility_changed", this, nameof(VisibilityChanged));

                //Control
                Connect("focus_entered", this, nameof(FocusEntered));
                Connect("focus_exited", this, nameof(FocusExited));
                Connect("gui_input", this, nameof(GUIInput));
                Connect("minimum_size_changed", this, nameof(_MinimumSizeChanged));
                Connect("modal_closed", this, nameof(ModalClosed));
                Connect("mouse_entered", this, nameof(MouseEntered));
                Connect("mouse_exited", this, nameof(MouseExited));
                Connect("resized", this, nameof(Resized));
                Connect("size_flags_changed", this, nameof(SizeFlagsChanged));

                //RichTextLabel
                Connect("meta_clicked", this, nameof(MetaClicked));
                Connect("meta_hover_ended", this, nameof(MetaHoverEnded));
                Connect("meta_hover_started", this, nameof(MetaHoverStarted));
            }

            #region Node
            public override void _Ready()
            {
                if(ClassOwner._RenderProcess == null)
                    SetProcess(false);

                if (ClassOwner._PhysicsProcess == null)
                    SetPhysicsProcess(false);

                if (ClassOwner._InputProcess == null)
                    SetProcessInput(false);

                if (ClassOwner._UnhandledInputProcess == null)
                    SetProcessUnhandledInput(false);

                if (ClassOwner._UnhandledKeyInputProcess == null)
                    SetProcessUnhandledKeyInput(false);

                ClassOwner.Ready?.Invoke();
            }
            public override void _EnterTree()
            {
                ClassOwner.EnterTree?.Invoke();
            }
            public override void _ExitTree()
            {
                ClassOwner.ExitTree?.Invoke();
            }
            public override void _Process(float delta)
            {
                ClassOwner.RenderProcess?.Invoke(delta);
            }
            public override void _PhysicsProcess(float delta)
            {
                ClassOwner.PhysicsProcess?.Invoke(delta);
            }
            public override void _Input(Godot.InputEvent ev)
            {
                ClassOwner.InputProcess?.Invoke(ev);
            }
            public override void _UnhandledInput(Godot.InputEvent ev)
            {
                ClassOwner.UnhandledInputProcess?.Invoke(ev);
            }
            public override void _UnhandledKeyInput(Godot.InputEventKey ev)
            {
                ClassOwner.UnhandledKeyInputProcess?.Invoke(ev);
            }
            public override void _Notification(int what)
            {
                ClassOwner.NotificationProcess?.Invoke((Notification)what);
            }
            private void ScriptChanged()
            {
                ClassOwner.ScriptChanged?.Invoke();
            }
            private void TreeExiting()
            {
                ClassOwner.OnTreeExiting?.Invoke();
            }
            private void TreeExited()
            {
                ClassOwner.OnTreeExited?.Invoke();
            }
            private void TreeEntered()
            {
                ClassOwner.OnTreeEntered?.Invoke();
            }
            private void Renamed()
            {
                ClassOwner.Renamed?.Invoke();
            }
            private void Ready()
            {
                ClassOwner.OnReady?.Invoke();
            }
            #endregion

            #region CanvasItem
            private void Draw()
            {
                ClassOwner.Draw?.Invoke();
            }
            private void _Hide()
            {
                ClassOwner.Hide?.Invoke();
            }
            private void ItemRectChanged()
            {
                ClassOwner.ItemRectChanged?.Invoke();
            }
            private void VisibilityChanged()
            {
                ClassOwner.VisibilityChanged?.Invoke();
            }
            #endregion

            #region Control
            private void FocusEntered()
            {
                ClassOwner.FocusEntered?.Invoke();
            }
            private void FocusExited()
            {
                ClassOwner.FocusExited?.Invoke();
            }
            private void GUIInput(Godot.InputEvent ev)
            {
                ClassOwner.GUIInput?.Invoke(ev);
            }
            private void _MinimumSizeChanged()
            {
                ClassOwner.MinimumSizeChanged?.Invoke();
            }
            private void ModalClosed()
            {
                ClassOwner.ModalClosed?.Invoke();
            }
            private void MouseEntered()
            {
                ClassOwner.MouseEntered?.Invoke();
            }
            private void MouseExited()
            {
                ClassOwner.MouseExited?.Invoke();
            }
            private void Resized()
            {
                ClassOwner.Resized?.Invoke();
            }
            private void SizeFlagsChanged()
            {
                ClassOwner.SizeFlagsChanged?.Invoke();
            }
            #endregion

            #region RichTextLabel
            private void MetaClicked(object obj)
            {
                ClassOwner.MetaClicked?.Invoke(obj);
            }
            private void MetaHoverEnded(object obj)
            {
                ClassOwner.MetaHoverEnded?.Invoke(obj);
            }
            private void MetaHoverStarted(object obj)
            {
                ClassOwner.MetaHoverStarted?.Invoke(obj);
            }
            #endregion
        }
    }
}
