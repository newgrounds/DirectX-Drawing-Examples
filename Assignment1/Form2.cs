using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Assignment1
{
    public partial class Form2 : Form
    {
        private Device m_device = null;

        public Form Form1Reference
        {
            get;
            set;
        }

        public Form Form3Reference
        {
            get;
            set;
        }

        public Form Form4Reference
        {
            get;
            set;
        }

        public Form2()
        {
            InitializeComponent();
            InitGraphics();
        }

        void InitGraphics()
        {
            PresentParameters present_params = new PresentParameters();
            present_params.Windowed = true;
            present_params.SwapEffect = SwapEffect.Discard;

            m_device = new Device(0, DeviceType.Hardware, this,
                CreateFlags.SoftwareVertexProcessing, present_params);
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            float x = (this.ClientSize.Width - 200) / 2;
            float y = (this.ClientSize.Height - 200) / 2;

            m_device.Clear(ClearFlags.Target, System.Drawing.Color.FromArgb(0, 0, 0).ToArgb(), 1.0f, 0);
            m_device.BeginScene();
            m_device.VertexFormat = CustomVertex.TransformedColored.Format;

            // randomly generate a bunch of vertices
            List<CustomVertex.TransformedColored[]> drawList = new List<CustomVertex.TransformedColored[]>();
            int randX, randY;
            Random autoRand = new Random();
            int randRange = 100;
            for (int b = 0; b < 10; b++)
            {
                int randNumOfVerts = autoRand.Next(5, 15);
                CustomVertex.TransformedColored[] randVerts = new CustomVertex.TransformedColored[randNumOfVerts];
                for (int i = 0; i < randNumOfVerts; i++)
                {
                    randVerts[i].Position = new Vector4(0, 0, 0, 1.0f);
                    randX = autoRand.Next(b * randRange, b * randRange + randRange);// +(b * randRange);
                    randVerts[i].X = randX;
                    randVerts[i].Color = System.Drawing.Color.FromArgb(autoRand.Next(0, 255), autoRand.Next(0, 255), autoRand.Next(0, 255)).ToArgb();
                }

                for (int k = 0; k < randNumOfVerts; k++)
                {
                    randY = autoRand.Next(0, 1000);
                    randVerts[k].Y = randY;
                }
                drawList.Add(randVerts);
            }
            for (int l = 0; l < drawList.Count; l++)
            {
                int len = drawList[l].Length;
                m_device.DrawUserPrimitives(PrimitiveType.LineStrip, len-1, drawList[l]);
            }

            int numOfCurveLines = 10;
            CustomVertex.TransformedColored[] curve = new CustomVertex.TransformedColored[numOfCurveLines];
            int startPosX = 1100;
            int startPosY = 800;
            for (int t = 0; t < numOfCurveLines; t++)
            {
                curve[t].Position = new Vector4(startPosX + (t * 5), startPosY, 0, 1.0f);
                curve[t].Color = System.Drawing.Color.FromArgb(200, 0, 200).ToArgb();
            }
            rotate(3 * (Math.PI / 180), curve);

            //CustomVertex.TransformedColored[] textList = new CustomVertex.TransformedColored[10];

            m_device.DrawUserPrimitives(PrimitiveType.LineStrip, curve.Length-1, curve);

            //m_device.DrawUserPrimitives(PrimitiveType.LineStrip, textList.Length, textList);

            m_device.EndScene();
            m_device.Present();
        }

        private void rotate(double theta, CustomVertex.TransformedColored[] vertList)
        {
            float x, y;
            for (int i = 0; i < vertList.Length; i++)
            {
                x = (float)(vertList[i].Position.X * Math.Cos(theta * i) + vertList[i].Position.Y * Math.Sin(theta * i));
                y = (float)(-vertList[i].Position.X * Math.Sin(theta * i) + vertList[i].Position.Y * Math.Cos(theta * i));
                vertList[i].Position = new Vector4(x, y, 0, 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Form3Reference.Show();
            this.Hide();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1Reference.Close();
        }
    }
}
