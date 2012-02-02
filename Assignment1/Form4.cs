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
    public partial class Form4 : Form
    {
        private Device m_device = null;

        public Form Form1Reference
        {
            get;
            set;
        }

        public Form Form2Reference
        {
            get;
            set;
        }

        public Form Form3Reference
        {
            get;
            set;
        }

        public Form4()
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

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Form1Reference.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Form1Reference.Show();
            this.Hide();
        }

        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            List<CustomVertex.TransformedColored[]> drawList = new List<CustomVertex.TransformedColored[]>();

            m_device.Clear(ClearFlags.Target, System.Drawing.Color.FromArgb(0, 0, 0).ToArgb(), 1.0f, 0);
            m_device.BeginScene();
            m_device.VertexFormat = CustomVertex.TransformedColored.Format;

            int randX, randY;
            int maxDist = 100;
            Random rand = new Random();
            for (int b = 0; b < 100; b++)
            {
                int randNumOfVerts = rand.Next(5, 15);
                CustomVertex.TransformedColored[] randVerts = new CustomVertex.TransformedColored[randNumOfVerts];
                randVerts[0].Position = new Vector4(rand.Next(0, this.ClientSize.Width), rand.Next(0, this.ClientSize.Height), 0, 1.0f);
                for (int i = 1; i < randNumOfVerts-1; i++)
                {
                    randVerts[i] = randVerts[0];
                    randX = rand.Next((int)randVerts[0].X - maxDist, (int)randVerts[0].X + maxDist);
                    randVerts[i].X = randX;
                    randVerts[i].Color = System.Drawing.Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)).ToArgb();
                }

                for (int k = 1; k < randNumOfVerts-1; k++)
                {
                    randY = rand.Next((int)randVerts[0].Y - maxDist, (int)randVerts[0].Y + maxDist);
                    randVerts[k].Y = randY;
                }
                randVerts[randNumOfVerts - 1] = randVerts[0];
                drawList.Add(randVerts);
            }

            for (int j = 0; j < drawList.Count; j++)
            {
                if (j % 2 == 1)
                    m_device.DrawUserPrimitives(PrimitiveType.LineStrip, drawList[j].Length - 1, drawList[j]);
                else
                    m_device.DrawUserPrimitives(PrimitiveType.TriangleFan, drawList[j].Length - 1, drawList[j]);
            }

            m_device.EndScene();
            m_device.Present();
        }
    }
}
