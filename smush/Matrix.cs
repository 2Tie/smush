using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smush
{
    public class Matrix
    {
        double[,] points { get; set; }

        public Matrix()
        {
            points = new double[4, 4] { { 1, 0, 0, 0 },
                                        { 0, 1, 0, 0 },
                                        { 0, 0, 1, 0 },
                                        { 0, 0, 0, 1 } };
        }

        public Matrix(Vector4 a, Vector4 b, Vector4 c, Vector4 d)
        {
            points = new double[4, 4] { { a.X, a.Y, a.Z, a.W },
                                        { b.X, b.Y, b.Z, b.W },
                                        { c.X, c.Y, c.Z, c.W },
                                        { d.X, d.Y, d.Z, d.W } };
        }

        public double this[int i1, int i2]
        {
            get
            {
                return points[i1, i2];
            }
            set
            {
                points[i1, i2] = value;
            }
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            return new Matrix(new Vector4(a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0] + a[0, 2] * b[2, 0] + a[0, 3] * b[3, 0], a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1] + a[0, 2] * b[2, 1] + a[0, 3] * b[3, 1], a[0, 0] * b[0, 2] + a[0, 1] * b[1, 2] + a[0, 2] * b[2, 2] + a[0, 3] * b[3, 2], a[0, 0] * b[0, 3] + a[0, 1] * b[1, 3] + a[0, 2] * b[2, 3] + a[0, 3] * b[3, 3]),
                              new Vector4(a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0] + a[1, 2] * b[2, 0] + a[1, 3] * b[3, 0], a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1] + a[1, 2] * b[2, 1] + a[1, 3] * b[3, 1], a[1, 0] * b[0, 2] + a[1, 1] * b[1, 2] + a[1, 2] * b[2, 2] + a[1, 3] * b[3, 2], a[1, 0] * b[0, 3] + a[1, 1] * b[1, 3] + a[1, 2] * b[2, 3] + a[1, 3] * b[3, 3]),
                              new Vector4(a[2, 0] * b[0, 0] + a[2, 1] * b[1, 0] + a[2, 2] * b[2, 0] + a[2, 3] * b[3, 0], a[2, 0] * b[0, 1] + a[2, 1] * b[1, 1] + a[2, 2] * b[2, 1] + a[2, 3] * b[3, 1], a[2, 0] * b[0, 2] + a[2, 1] * b[1, 2] + a[2, 2] * b[2, 2] + a[2, 3] * b[3, 2], a[2, 0] * b[0, 3] + a[2, 1] * b[1, 3] + a[2, 2] * b[2, 3] + a[2, 3] * b[3, 3]),
                              new Vector4(a[3, 0] * b[0, 0] + a[3, 1] * b[1, 0] + a[3, 2] * b[2, 0] + a[3, 3] * b[3, 0], a[3, 0] * b[0, 1] + a[3, 1] * b[1, 1] + a[3, 2] * b[2, 1] + a[3, 3] * b[3, 1], a[3, 0] * b[0, 2] + a[3, 1] * b[1, 2] + a[3, 2] * b[2, 2] + a[3, 3] * b[3, 2], a[3, 0] * b[0, 3] + a[3, 1] * b[1, 3] + a[3, 2] * b[2, 3] + a[3, 3] * b[3, 3]));

            /*return new Matrix(new Vector4(a[0, 0] * b[0, 0] + a[1, 0] * b[0, 1] + a[2, 0] * b[0, 2] + a[3, 0] * b[0, 3], a[0, 1] * b[0, 0] + a[1, 1] * b[0, 1] + a[2, 1] * b[0, 2] + a[3, 1] * b[0, 3], a[0, 2] * b[0, 0] + a[1, 2] * b[0, 1] + a[2, 2] * b[0, 2] + a[3, 2] * b[0, 3], a[0, 3] * b[0, 0] + a[1, 3] * b[0, 1] + a[2, 3] * b[0, 2] + a[3, 3] * b[0, 3]),
                              new Vector4(a[0, 0] * b[1, 0] + a[1, 0] * b[1, 1] + a[2, 0] * b[1, 2] + a[3, 0] * b[1, 3], a[0, 1] * b[1, 0] + a[1, 1] * b[1, 1] + a[2, 1] * b[1, 2] + a[3, 1] * b[1, 3], a[0, 2] * b[1, 0] + a[1, 2] * b[1, 1] + a[2, 2] * b[1, 2] + a[3, 2] * b[1, 3], a[0, 3] * b[1, 0] + a[1, 3] * b[1, 1] + a[2, 3] * b[1, 2] + a[3, 3] * b[1, 3]),
                              new Vector4(a[0, 0] * b[2, 0] + a[1, 0] * b[2, 1] + a[2, 0] * b[2, 2] + a[3, 0] * b[2, 3], a[0, 1] * b[2, 0] + a[1, 1] * b[2, 1] + a[2, 1] * b[2, 2] + a[3, 1] * b[2, 3], a[0, 2] * b[2, 0] + a[1, 2] * b[2, 1] + a[2, 2] * b[2, 2] + a[3, 2] * b[2, 3], a[0, 3] * b[2, 0] + a[1, 3] * b[2, 1] + a[2, 3] * b[2, 2] + a[3, 3] * b[2, 3]),
                              new Vector4(a[0, 0] * b[3, 0] + a[1, 0] * b[3, 1] + a[2, 0] * b[3, 2] + a[3, 0] * b[3, 3], a[0, 1] * b[3, 0] + a[1, 1] * b[3, 1] + a[2, 1] * b[3, 2] + a[3, 1] * b[3, 3], a[0, 2] * b[3, 0] + a[1, 2] * b[3, 1] + a[2, 2] * b[3, 2] + a[3, 2] * b[3, 3], a[0, 3] * b[3, 0] + a[1, 3] * b[3, 1] + a[2, 3] * b[3, 2] + a[3, 3] * b[3, 3]));//a[0,0]*b[0,0] + a[1,0]*b[0,1]..., a[0,1]*b[0,0] + a[1,1]*b[0,1]...,... then copy vector4 adding 1 to the first part of b each time??
*/
        }

        public static Vector3 normal(Vector3 vec)
        {
            double den = Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y + vec.Z * vec.Z);
            return new Vector3(vec.X / den, vec.Y / den, vec.Z / den);
        }

        public static double dot(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static Vector3 cross(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
        }

        public static Matrix LookAtRH(Vector3 eye, Vector3 target, Vector3 up)
        {
            Vector3 Zaxis = normal(target - eye);
            Vector3 Xaxis = normal(cross(up, Zaxis));
            Vector3 Yaxis = cross(Zaxis, Xaxis);

            return new Matrix(new Vector4(Xaxis.X, Yaxis.X, Zaxis.X, 0), new Vector4(Xaxis.Y, Yaxis.Y, Zaxis.Y, 0), new Vector4(Xaxis.Z, Yaxis.Z, Zaxis.Z, 0), new Vector4(-dot(Xaxis, eye), -dot(Yaxis, eye), -dot(Zaxis, eye), 1));
        }

        public static Matrix PerspectiveFOVRH(double fov, double width, double height, double nearclip, double farclip)
        {
            return new Matrix(new Vector4(width, 0, 0, 0), new Vector4(0, height, 0, 0), new Vector4(0, 0, farclip / (nearclip - farclip), -1), new Vector4(0, 0, nearclip * farclip / (nearclip - farclip), 0));
        }

        public static Matrix DoRotation(Vector3 vec)//y x z
        {
            double cY = Math.Cos(-1 * vec.Y);
            double sY = Math.Sin(-1 * vec.Y);
            Matrix rY = new Matrix(new Vector4(cY, 0, sY, 0), new Vector4(0, 1, 0, 0), new Vector4(-1 * sY, 0, cY, 0), new Vector4(0, 0, 0, 1));
            double cX = Math.Cos(-1 * vec.X);
            double sX = Math.Sin(-1 * vec.X);
            Matrix rX = new Matrix(new Vector4(1, 0, 0, 0), new Vector4(0, cX, -1 * sX, 0), new Vector4(0, sX, cX, 0), new Vector4(0, 0, 0, 1));
            double cZ = Math.Cos(-1 * vec.Z);
            double sZ = Math.Sin(-1 * vec.Z);
            Matrix rZ = new Matrix(new Vector4(cZ, -1 * sZ, 0, 0), new Vector4(sZ, cZ, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
            return rY * rX * rZ;
        }

        public static Matrix DoTranslation(Vector3 vec)
        {
            Matrix m = new Matrix();
            m[0, 3] = vec.X;
            m[1, 3] = vec.Y;
            m[2, 3] = vec.Z;
            return m;
        }
    }
}
