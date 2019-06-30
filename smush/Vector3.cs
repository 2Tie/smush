using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smush
{
    public class Vector3
    {
        public double X = 0, Y = 0, Z = 0;

        public static Vector3 UnitX = new Vector3(1, 0, 0);
        public static Vector3 UnitY = new Vector3(0, 1, 0);
        public static Vector3 UnitZ = new Vector3(0, 0, 1);
        public static Vector3 Zero = new Vector3(0, 0, 0);
        public static Vector3 One = new Vector3(1, 1, 1);

        public Vector3(double nx, double ny, double nz)
        {
            X = nx;
            Y = ny;
            Z = nz;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator /(Vector3 a, int b)
        {
            return new Vector3(a.X / b, a.Y / b, a.Z / b);
        }

        public static double Dot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public void Normalize()
        {
            double mag = Math.Sqrt(X * X + Y * Y + Z * Z);
            X /= mag;
            Y /= mag;
            Z /= mag;
        }

        public static Vector3 TransformCoordinate(Vector3 coord, Matrix mat)
        {
            double vx = coord.X * mat[0, 0] + coord.Y * mat[1, 0] + coord.Z * mat[2, 0] + mat[3, 0];
            double vy = coord.X * mat[0, 1] + coord.Y * mat[1, 1] + coord.Z * mat[2, 1] + mat[3, 1];
            double vz = coord.X * mat[0, 2] + coord.Y * mat[1, 2] + coord.Z * mat[2, 2] + mat[3, 2];
            double den = 1f / (coord.X * mat[0, 3] + coord.Y * mat[1, 3] + coord.Z * mat[2, 3] + mat[3, 3]);
            return new Vector3(vx/den, vy/den, vz/den);
        }
    }
}
