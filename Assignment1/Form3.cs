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
    public partial class Form3 : Form
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

        public Form Form4Reference
        {
            get;
            set;
        }

        public Form3()
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

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            List<CustomVertex.TransformedColored[]> drawList = new List<CustomVertex.TransformedColored[]>();

            m_device.Clear(ClearFlags.Target, System.Drawing.Color.FromArgb(0, 0, 0).ToArgb(), 1.0f, 0);
            m_device.BeginScene();
            m_device.VertexFormat = CustomVertex.TransformedColored.Format;

            Random rando = new Random();
            for (int i = 0; i < 50; i++)
            {
                int col = System.Drawing.Color.FromArgb(rando.Next(0, 255), rando.Next(0, 255), rando.Next(0, 255)).ToArgb();
                drawList.Add(circleVerts(col, rando.Next(0, this.ClientSize.Width), rando.Next(this.ClientSize.Height), rando.Next(5,50)));
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

        private CustomVertex.TransformedColored[] circleVerts(int col, int x, int y, int r)
        {
            CustomVertex.TransformedColored[] vs = new CustomVertex.TransformedColored[100];
            for (int i = 0; i < 99; i++)
            {
                float angle = (float)(i / 100.0 * Math.PI * 2);
                vs[i].Position = new Vector4((float)x + (float)Math.Cos(angle) * r, y + (float)Math.Sin(angle) * r, 0, 1.0f);
                vs[i].Color = col;
            }
            vs[99] = vs[0];

            return vs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Form4Reference.Show();
            this.Hide();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1Reference.Close();
        }
    }
}
