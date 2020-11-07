using System;

namespace EGL.GodotBase.Nodes2D.Physics
{
    public class Area2D : INode
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

        //Node
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

        //CollisionObject2D
        public event Action<Godot.Node, Godot.InputEvent, int> InputEvent;
        public event Action MouseEntered;
        public event Action MouseExited;

        //Area2D
        public event Action<Godot.Area2D> AreaEntered;
        public event Action<Godot.Area2D> AreaExited;
        public event Action<int, Godot.Area2D, int, int> AreaShapeEntered;
        public event Action<int, Godot.Area2D, int, int> AreaShapeExited;
        public event Action<Godot.Node> BodyEntered;
        public event Action<Godot.Node> BodyExited;
        public event Action<int, Godot.Node, int, int> BodyShapeEntered;
        public event Action<int, Godot.Node, int, int> BodyShapeExited;

        public Godot.Area2D Base { get; }

        public Area2D()
        {
            Base = new _Area2D(this);
        }

        private class _Area2D : Godot.Area2D
        {
            public Area2D ClassOwner;

            public _Area2D(Area2D node)
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

                //CollisionObject2D
                Connect("input_event", this, nameof(InputEvent));
                Connect("mouse_entered", this, nameof(MouseEntered));
                Connect("mouse_exited", this, nameof(MouseExited));

                //Area2D
                Connect("area_entered", this, nameof(AreaEntered));
                Connect("area_exited", this, nameof(AreaExited));
                Connect("area_shape_entered", this, nameof(AreaShapeEntered));
                Connect("area_shape_exited", this, nameof(AreaShapeExited));
                Connect("body_entered", this, nameof(BodyEntered));
                Connect("body_exited", this, nameof(BodyExited));
                Connect("body_shape_entered", this, nameof(BodyShapeEntered));
                Connect("body_shape_exited", this, nameof(BodyShapeExited));
            }

            #region Node
            public override void _Ready()
            {
                if (ClassOwner._RenderProcess == null)
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
                ClassOwner._RenderProcess?.Invoke(delta);
            }
            public override void _PhysicsProcess(float delta)
            {
                ClassOwner._PhysicsProcess?.Invoke(delta);
            }
            public override void _Input(Godot.InputEvent ev)
            {
                ClassOwner._InputProcess?.Invoke(ev);
            }
            public override void _UnhandledInput(Godot.InputEvent ev)
            {
                ClassOwner._UnhandledInputProcess?.Invoke(ev);
            }
            public override void _UnhandledKeyInput(Godot.InputEventKey ev)
            {
                ClassOwner._UnhandledKeyInputProcess?.Invoke(ev);
            }
            public override void _Notification(int what)
            {
                ClassOwner._NotificationProcess?.Invoke((Notification)what);
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
            #region CollisionObject2D
            private void InputEvent(Godot.Node viewport, Godot.InputEvent ev, int shapeIdx)
            {
                ClassOwner.InputEvent?.Invoke(viewport, ev, shapeIdx);
            }
            private void MouseEntered()
            {
                ClassOwner.MouseEntered?.Invoke();
            }
            private void MouseExited()
            {
                ClassOwner.MouseExited?.Invoke();
            }
            #endregion
            #region Area2D
            private void AreaEntered(Godot.Area2D area)
            {
                ClassOwner.AreaEntered?.Invoke(area);
            }
            private void AreaExited(Godot.Area2D area)
            {
                ClassOwner.AreaExited?.Invoke(area);
            }
            private void AreaShapeEntered(int id, Godot.Area2D area, int areaShape, int selfShape)
            {
                ClassOwner.AreaShapeEntered?.Invoke(id, area, areaShape, selfShape);
            }
            private void AreaShapeExited(int id, Godot.Area2D area, int areaShape, int selfShape)
            {
                ClassOwner.AreaShapeExited?.Invoke(id, area, areaShape, selfShape);
            }
            private void BodyEntered(Godot.Node node)
            {
                ClassOwner.BodyEntered?.Invoke(node);
            }
            private void BodyExited(Godot.Node node)
            {
                ClassOwner.BodyExited?.Invoke(node);
            }
            private void BodyShapeEntered(int id, Godot.Node node, int areaShape, int selfShape)
            {
                ClassOwner.BodyShapeEntered?.Invoke(id, node, areaShape, selfShape);
            }
            private void BodyShapeExited(int id, Godot.Node node, int areaShape, int selfShape)
            {
                ClassOwner.BodyShapeExited?.Invoke(id, node, areaShape, selfShape);
            }
            #endregion
        }
    }
}
