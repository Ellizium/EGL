using System;

namespace EGL.GodotBase.Nodes3D.Physics
{
    public class Area : INode
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
        public event Action<Godot.Node,Godot.InputEvent,Godot.Vector3,Godot.Vector3,int> OnInputEvent;
        public event Action MouseEntered;
        public event Action MouseExited;

        //Area
        public event Action<Area> AreaEntered;
        public event Action<Area> AreaExited;
        public event Action<int, Area, int, int> AreaShapeEntered;
        public event Action<int, Area, int, int> AreaShapeExited;
        public event Action<INode> BodyEntered;
        public event Action<INode> BodyExited;
        public event Action<int, INode, int, int> BodyShapeEntered;
        public event Action<int, INode, int, int> BodyShapeExited;

        public Godot.Area Base { get; }

        public Area()
        {
            Base = new _Area(this);
        }

        protected class _Area : Godot.Area, ICollisionBody
        {
            public Area ClassOwner;

            public INode Reference { get => ClassOwner; }

            public _Area(Area node)
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

                //Area
                Connect("area_entered", this, nameof(AreaEntered));
                Connect("area_exited", this, nameof(AreaExited));
                Connect("area_shape_entered", this, nameof(AreaShapeEntered));
                Connect("area_shape_exited", this, nameof(AreaShapeExited));
                Connect("body_entered", this, nameof(BodyEntered));
                Connect("body_exited", this, nameof(BodyExited));
                Connect("body_shape_entered", this, nameof(BodyShapeEntered));
                Connect("body_shape_Exited", this, nameof(BodyShapeExited));
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

            #region Area
            private void AreaEntered(Godot.Area area)
            {
                _Area collision = area as _Area;
                ClassOwner.AreaEntered?.Invoke(collision?.ClassOwner);
            }
            private void AreaExited(Godot.Area area)
            {
                _Area collision = area as _Area;
                ClassOwner.AreaExited?.Invoke(collision?.ClassOwner);
            }
            private void AreaShapeEntered(int areaId, Godot.Area area, int areaShapeId, int selfShapeId)
            {
                _Area collision = area as _Area;
                ClassOwner.AreaShapeEntered?.Invoke(areaId, collision?.ClassOwner, areaShapeId, selfShapeId);
            }
            private void AreaShapeExited(int areaId, Godot.Area area, int areaShapeId, int selfShapeId)
            {
                _Area collision = area as _Area;
                ClassOwner.AreaShapeExited?.Invoke(areaId, collision?.ClassOwner, areaShapeId, selfShapeId);
            }
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
            private void BodyShapeEntered(int areaId, Godot.Node body, int bodyShapeId, int selfShapeId)
            {
                ICollisionBody collision = body as ICollisionBody;
                ClassOwner.BodyShapeEntered?.Invoke(areaId, collision.Reference, bodyShapeId, selfShapeId);
            }
            private void BodyShapeExited(int areaId, Godot.Node body, int bodyShapeId, int selfShapeId)
            {
                ICollisionBody collision = body as ICollisionBody;
                ClassOwner.BodyShapeExited?.Invoke(areaId, collision.Reference, bodyShapeId, selfShapeId);
            }
            #endregion
        }
    }
}
