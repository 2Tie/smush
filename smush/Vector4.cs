using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smush
{
    public class Vector4
    {
        public double X = 0, Y = 0, Z = 0, W = 0;

        public Vector4(Vector3 vbase, double nw)
        {
            X = vbase.X;
            Y = vbase.Y;
            Z = vbase.Z;
            W = nw;
        }
        public Vector4(double nx, double ny, double nz, double nw)
        {
            X = nx;
            Y = ny;
            Z = nz;
            W = nw;
        }
    }
}
