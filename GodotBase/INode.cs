using System;
using Godot;

namespace EGL.GodotBase
{
    public interface INode 
    {
        Action<float> RenderProcess { get; set; }
        Action<float> PhysicsProcess { get; set; }
        Action<InputEvent> InputProcess { get; set; }
        Action<InputEvent> UnhandledInputProcess { get; set; }
        Action<InputEvent> UnhandledKeyInputProcess { get; set; }
        Action<Notification> NotificationProcess { get; set; }
    }
}
