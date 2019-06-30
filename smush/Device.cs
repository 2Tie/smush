using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smush
{
    class Device
    {
        Graphics buffer;
        double[] depthBuf;
        int bufWidth;
        int bufHeight;

        public struct ScanLineData
        {
            public int currentY;
            public double ndotl1;
            public double ndotl2;
            public double ndotl3;
            public double ndotl4;
        }

        public Device(Graphics Buffer)
        {
            this.buffer = Buffer;
            bufWidth = (int)buffer.VisibleClipBounds.Width;
            bufHeight = (int)buffer.VisibleClipBounds.Height;
            depthBuf = new double[bufWidth * bufHeight];
        }

        public void Clear()
        {
            for (int i = 0; i < depthBuf.Length; i++)
                depthBuf[i] = double.MaxValue;
        }

        public void drawPoint(double x, double y, double z, Color c)
        {
            int ind = (int)(y * bufWidth + x);
            if (z < depthBuf[ind])
                return;
            depthBuf[ind] = z;
            Blit(x, y, c);
        }

        public void Blit(double x, double y, Color c)
        {
            //if (x > 0 && y > 0)
            buffer.FillRectangle(new SolidBrush(c), (int)x, (int)y, 1, 1);
        }

        public Vertex Project(Vertex vert, Matrix transMat, Matrix world)
        {
            var point2D = Vector3.TransformCoordinate(vert.Coordinates, transMat);
            var point3D = Vector3.TransformCoordinate(point2D, world);
            var normal3D = Vector3.TransformCoordinate(vert.Normal, world);
            return new Vertex {
                Coordinates = new Vector3(point2D.X + bufWidth / 2, -1 * point2D.Y + bufHeight / 2, point2D.Z),
                Normal = normal3D,
                WorldCoordinates = point3D
            };
        }
        public void Render(Camera cam, params Mesh[] meshes)
        {
            for (int i = 0; i < depthBuf.Length; i++)
            {
                depthBuf[i] = 0;
            }
            //produce the view matrix
            Matrix viewmat = Matrix.LookAtRH(cam.Position, cam.TargetPos, Vector3.UnitY);
            //produce the projection matrix
            Matrix projmat = Matrix.PerspectiveFOVRH(0.79f, 4, 4, 0.01f, 1.0f);
            foreach (Mesh mesh in meshes)
            {
                //produce world matrix
                Matrix worldmat = Matrix.DoRotation(mesh.Rotation) * Matrix.DoTranslation(mesh.Position);

                //finish the final transform matrix
                Matrix tf = worldmat * viewmat * projmat;

                /*foreach(var vert in mesh.Vertices)
                {
                    var pt = Project(vert, tf);
                    Blit(pt.X, pt.Y, Color.WhiteSmoke);

                }*/

                int f = 0;
                foreach (var face in mesh.Faces)
                {
                    Vertex pix1 = Project(mesh.Vertices[face.A], tf, worldmat);
                    Vertex pix2 = Project(mesh.Vertices[face.B], tf, worldmat);
                    Vertex pix3 = Project(mesh.Vertices[face.C], tf, worldmat);

                    Color c = f % 2 == 1 ? Color.WhiteSmoke : Color.Turquoise;

                    drawTri(pix1, pix2, pix3, c);

                    /*drawBEdge(pix1, pix2);
                    drawBEdge(pix2, pix3);
                    drawBEdge(pix3, pix1);*/

                    f++;
                }
            }
        }

        double ComputeNDotL(Vector3 vertex, Vector3 normal, Vector3 lightPosition)
        {
            Vector3 lightNormal = lightPosition - vertex;

            normal.Normalize();
            lightNormal.Normalize();

            return Math.Max(0, Vector3.Dot(normal, lightNormal));
        }

        public void drawTri(Vertex v1, Vertex v2, Vertex v3, Color c)
        {
            Vector3 p1 = v1.Coordinates;
            Vector3 p2 = v2.Coordinates;
            Vector3 p3 = v3.Coordinates;

            //sort triangles by y
            if (p1.Y > p2.Y)
            {
                var temp = p2;
                p2 = p1;
                p1 = temp;
            }
            if (p2.Y > p3.Y)
            {
                var temp = p2;
                p2 = p3;
                p3 = temp;
            }
            if (p1.Y > p2.Y)
            {
                var temp = p2;
                p2 = p1;
                p1 = temp;
            }

            v1.Coordinates = p1;
            v2.Coordinates = p2;
            v3.Coordinates = p3;

            //face normal
            Vector3 faceNormal = (v1.Normal + v2.Normal + v3.Normal) / 3;
            Vector3 faceCenter = (v1.WorldCoordinates + v2.WorldCoordinates + v3.WorldCoordinates) / 3;

            Vector3 LightCoords = new Vector3(0, 10, 10);

            double ndotl = ComputeNDotL(faceCenter, faceNormal, LightCoords);

            var data = new ScanLineData { ndotl1 = ndotl };


            //inverse slopes
            double dP1P2, dP1P3;

            if (p2.Y - p1.Y > 0)
                dP1P2 = (p2.X - p1.X) / (p2.Y - p1.Y);
            else
                dP1P2 = 0;

            if (p3.Y - p1.Y > 0)
                dP1P3 = (p3.X - p1.X) / (p3.Y - p1.Y);
            else
                dP1P3 = 0;

            if (dP1P2 > dP1P3)
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    data.currentY = y;
                    if (y < p2.Y)
                    {
                        drawScanLine(data, v1, v3, v1, v2, c);
                    }
                    else
                    {
                        drawScanLine(data, v1, v3, v2, v3, c);
                    }
                }
            }
            else
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    data.currentY = y;
                    if (y < p2.Y)
                    {
                        drawScanLine(data, v1, v2, v1, v3, c);
                    }
                    else
                    {
                        drawScanLine(data, v2, v3, v1, v3, c);
                    }
                }
            }
        }

        //draw scanline of a triangle. p1 and p2 is left line, p3 and p4 is right. y is the vertical slice of it.
        public void drawScanLine(ScanLineData data, Vertex v1, Vertex v2, Vertex v3, Vertex v4, Color c)
        {
            Vector3 p1 = v1.Coordinates;
            Vector3 p2 = v2.Coordinates;
            Vector3 p3 = v3.Coordinates;
            Vector3 p4 = v4.Coordinates;


            var gradient1 = p1.Y != p2.Y ? (data.currentY - p1.Y) / (p2.Y - p1.Y) : 1;
            var gradient2 = p3.Y != p4.Y ? (data.currentY - p3.Y) / (p4.Y - p3.Y) : 1;

            int sx = (int)interpolate(p1.X, p2.X, gradient1);
            int ex = (int)interpolate(p3.X, p4.X, gradient2);

            double sz = interpolate(p1.Z, p2.Z, gradient1);
            double ez = interpolate(p3.Z, p4.Z, gradient2);

            for (var x = sx; x < ex; x++)
            {
                double zg = (x - sx) / (double)(ex - sx);
                double z = interpolate(sz, ez, zg);
                Color shadedColor = Color.FromArgb(c.A, (int)(c.R * data.ndotl1), (int)(c.G * data.ndotl1), (int)(c.B * data.ndotl1));
                drawPoint(x, data.currentY, z, c);
            }
        }

        public double interpolate(double min, double max, double gradient)
        {
            return min + (max - min) * clamp(gradient);
        }

        public double clamp(double v, double min = 0, double max = 1)
        {
            return Math.Max(min, Math.Min(v, max));
        }

        //interpolated recursion line
        public void drawIEdge(Vector2 p1, Vector2 p2)
        {
            if (Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2) < 1) //if small distance, return
                return;
            Vector2 mid = new Vector2(p1.X + (p2.X - p1.X) / 2, p1.Y + (p2.Y - p1.Y) / 2);
            Blit(mid.X, mid.Y, Color.WhiteSmoke);
            drawIEdge(p1, mid);
            drawIEdge(mid, p2);
        }

        //bresenham line
        public void drawBEdge(Vector2 p1, Vector2 p2)
        {
            int x0 = (int)p1.X;
            int y0 = (int)p1.Y;
            int x1 = (int)p2.X;
            int y1 = (int)p2.Y;

            var dx = Math.Abs(x1 - x0);
            var dy = Math.Abs(y1 - y0);
            var sx = (x0 < x1) ? 1 : -1;
            var sy = (y0 < y1) ? 1 : -1;
            var e = dx - dy;

            while(true)
            {
                Blit(x0, y0, Color.WhiteSmoke);

                if ((x0 == x1) && (y0 == y1)) break;
                var e2 = 2 * e;
                if (e2 > -dy) { e -= dy; x0 += sx; }
                if (e2 < dx) { e += dx; y0 += sy; }
            }
        }
    }
}
