using System;
using System.Collections.Generic;
using Godot;


namespace EGL.Extension.Render
{
    public class Mesh
    {
        private RID _InstanceRID;
        private RID _MeshRID;
        private RID _MateraialRID;
        
        private List<Vector3> _Vertex = new List<Vector3>();
        
        public Mesh()
        {
            _InstanceRID = VisualServer.InstanceCreate();
            _MeshRID = VisualServer.MeshCreate();
        }

        public Mesh(Vector3[] vertex)
        {
            
        }
        
    }
}