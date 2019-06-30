using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smush
{
    public partial class Form1 : Form
    {
        Image imgBuffer;
        Graphics graphics, drawBuffer;

        Stopwatch timer = new Stopwatch();
        long interval, startTime;
        Device dev;
        Mesh cub = new Mesh("Cub", 8, 12);
        Camera cam = new Camera();

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ClientSize = new Size(480, 360);

            interval = (long)TimeSpan.FromSeconds(1.0 / 60).TotalMilliseconds;

            imgBuffer = (Image)new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            graphics = this.CreateGraphics();
            drawBuffer = Graphics.FromImage(imgBuffer);

            dev = new Device(drawBuffer);

            cub.Vertices[0] = new Vertex { Coordinates = new Vector3(-1, 1, 1), Normal = new Vector3(-0.57, 0.57, 0.57) };
            cub.Vertices[1] = new Vertex { Coordinates = new Vector3(1, 1, 1), Normal = new Vector3(0.57, 0.57, 0.57) };
            cub.Vertices[2] = new Vertex { Coordinates = new Vector3(-1, -1, 1), Normal = new Vector3(-0.57, -0.57, 0.57) };
            cub.Vertices[3] = new Vertex { Coordinates = new Vector3(1, -1, 1), Normal = new Vector3(0.57, -0.57, 0.57) };
            cub.Vertices[4] = new Vertex { Coordinates = new Vector3(-1, 1, -1), Normal = new Vector3(-0.57, 0.57, -0.57) };
            cub.Vertices[5] = new Vertex { Coordinates = new Vector3(1, 1, -1), Normal = new Vector3(0.57, 0.57, -0.57) };
            cub.Vertices[6] = new Vertex { Coordinates = new Vector3(1, -1, -1), Normal = new Vector3(0.57, -0.57, -0.57) };
            cub.Vertices[7] = new Vertex { Coordinates = new Vector3(-1, -1, -1), Normal = new Vector3(-0.57, -0.57, -0.57) };

            cub.Faces[0] = new Tri { A = 0, B = 1, C = 2 };
            cub.Faces[1] = new Tri { A = 1, B = 2, C = 3 };
            cub.Faces[2] = new Tri { A = 1, B = 3, C = 6 };
            cub.Faces[3] = new Tri { A = 1, B = 5, C = 6 };
            cub.Faces[4] = new Tri { A = 0, B = 1, C = 4 };
            cub.Faces[5] = new Tri { A = 1, B = 4, C = 5 };

            cub.Faces[6] = new Tri { A = 2, B = 3, C = 7 };
            cub.Faces[7] = new Tri { A = 3, B = 6, C = 7 };
            cub.Faces[8] = new Tri { A = 0, B = 2, C = 7 };
            cub.Faces[9] = new Tri { A = 0, B = 4, C = 7 };
            cub.Faces[10] = new Tri { A = 4, B = 5, C = 6 };
            cub.Faces[11] = new Tri { A = 4, B = 6, C = 7 };

            cam.Position = new Vector3(0, 0, 15.0f);
            cam.TargetPos = Vector3.Zero;
        }

        public void MainLoop()
        {
            timer.Start();
            while (Created)
            {
                startTime = timer.ElapsedMilliseconds;
                //draw temp BG so bleeding doesn't occur
                drawBuffer.FillRectangle(new SolidBrush(Color.Black), this.ClientRectangle);
                dev.Clear();

                cub.Rotation = new Vector3(cub.Rotation.X + 0.01f, cub.Rotation.Y + 0.01f, cub.Rotation.Z);

                

                dev.Render(cam, cub);


                //draw the buffer, then set to refresh
                this.BackgroundImage = imgBuffer;
                this.Invalidate();

                Application.DoEvents();
                while (timer.ElapsedMilliseconds - startTime < interval) ;
            }
        }
    }
}
