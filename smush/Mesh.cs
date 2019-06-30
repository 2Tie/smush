using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smush
{
    public struct Tri
    {
        public int A;
        public int B;
        public int C;
    }

    public struct Quad
    {
        public int A;
        public int B;
        public int C;
        public int D;
    }

    public struct Vertex
    {
        public Vector3 Normal;
        public Vector3 Coordinates;
        public Vector3 WorldCoordinates;
    }

    class Mesh
    {
        public string Name { get; set; }
        public Vertex[] Vertices { get; private set; }
        public Tri[] Faces { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public Mesh(string name, int verticesCount, int facesCount)
        {
            Vertices = new Vertex[verticesCount];
            Faces = new Tri[facesCount];
            Name = name;
            Position = new Vector3(0, 0, 0);
            Rotation = new Vector3(0, 0, 0);
        }
    }
}
