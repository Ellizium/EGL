using System;

namespace EGL.GodotBase.Nodes3D.Physics
{
    public class VehicleBody : INode
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

        //Spatial
        public event Action VisibilityChanged;

        //CollisionObject
        public event Action<Godot.Node, Godot.InputEvent, Godot.Vector3, Godot.Vector3, int> OnInputEvent;
        public event Action MouseEntered;
        public event Action MouseExited;

        //RigidBody
        public event Action<INode> BodyEntered;
        public event Action<INode> BodyExited;
        public event Action<int, INode, int, int> BodyShapeEntered;
        public event Action<int, INode, int, int> BodyShapeExited;
        public event Action SleepingStateChanged;

        public Godot.VehicleBody Base { get; }

        public VehicleBody()
        {
            Base = new _VehicleBody(this);
        }

        protected class _VehicleBody : Godot.VehicleBody, ICollisionBody
        {
            public VehicleBody ClassOwner;

            public INode Reference { get => ClassOwner; }

            public _VehicleBody(VehicleBody node)
            {
                ClassOwner = node;

                //Node
                Connect("ready", this, nameof(Ready));
                Connect("renamed", this, nameof(Renamed));
                Connect("tree_entered", this, nameof(TreeEntered));
                Connect("tree_exited", this, nameof(TreeExited));
                Connect("tree_exiting", this, nameof(TreeExiting));
                Connect("script_changed", this, nameof(ScriptChanged));

                //Spatial
                Connect("visibility_changed", this, nameof(VisibilityChanged));

                //CollisionObject
                Connect("input_event", this, nameof(OnInputEvent));
                Connect("mouse_entered", this, nameof(MouseEntered));
                Connect("mouse_exited", this, nameof(MouseExited));

                //RigidBody
                Connect("body_entered", this, nameof(BodyEntered));
                Connect("body_exited", this, nameof(BodyExited));
                Connect("body_shape_entered", this, nameof(BodyShapeEntered));
                Connect("body_shape_exited", this, nameof(BodyShapeExited));
                Connect("sleeping_state_changed", this, nameof(SleepingStateChanged));
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

            #region Spatial
            private void VisibilityChanged()
            {
                ClassOwner.VisibilityChanged?.Invoke();
            }
            #endregion

            #region CollisionObject
            private void OnInputEvent(Godot.Node camera, Godot.InputEvent ev, Godot.Vector3 clickPosition, Godot.Vector3 normalPosition, int index)
            {
                ClassOwner.OnInputEvent?.Invoke(camera, ev, clickPosition, normalPosition, index);
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

            #region RigidBody
            private void BodyEntered(Godot.Node body)
            {
                ICollisionBody collision = body as ICollisionBody;
                ClassOwner.BodyEntered?.Invoke(collision.Reference);
            }
            private void BodyExited(Godot.Node body)
            {
                ICollisionBody collision = body as ICollisionBody;
                ClassOwner.BodyExited?.Invoke(collision.Reference);
            }
            private void BodyShapeEntered(int bodyId, Godot.Node body, int bodyShapeId, int localShape)
            {
                ICollisionBody collision = body as ICollisionBody;
                ClassOwner.BodyShapeEntered?.Invoke(bodyId, collision.Reference, bodyShapeId, localShape);
            }
            private void BodyShapeExited(int bodyId, Godot.Node body, int bodyShapeId, int localShape)
            {
                ICollisionBody collision = body as ICollisionBody;
                ClassOwner.BodyShapeExited?.Invoke(bodyId, collision.Reference, bodyShapeId, localShape);
            }
            private void SleepingStateChanged()
            {
                ClassOwner.SleepingStateChanged?.Invoke();
            }
            #endregion
        }
    }
}
