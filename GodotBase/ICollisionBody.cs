using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGL.GodotBase
{
    internal interface ICollisionBody
    {
        INode Reference { get; }
    }
}
