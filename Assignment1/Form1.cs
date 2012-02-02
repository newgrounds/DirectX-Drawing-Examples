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
    public partial class Form1 : Form
    {
        public CustomVertex.TransformedColored[] vertices = new CustomVertex.TransformedColored[5];
        private Device m_device = null;

        public Form1()
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            float x = (this.ClientSize.Width - 200) / 2;
            float y = (this.ClientSize.Height - 200) / 2;

            vertices[0].Position = new Vector4(x, y, 0, 1.0f);
            vertices[0].Color = System.Drawing.Color.FromArgb(0, 0, 255).ToArgb();
            vertices[1].Position = new Vector4(x+200, y, 0, 1.0f);
            vertices[1].Color = System.Drawing.Color.FromArgb(0, 0, 255).ToArgb();
            vertices[2].Position = new Vector4(x+200, y+200, 0, 1.0f);
            vertices[2].Color = System.Drawing.Color.FromArgb(0, 0, 255).ToArgb();
            vertices[3].Position = new Vector4(x, y+200, 0, 1.0f);
            vertices[3].Color = System.Drawing.Color.FromArgb(0, 0, 255).ToArgb();
            vertices[4].Position = new Vector4(x, y, 0, 1.0f);
            vertices[4].Color = System.Drawing.Color.FromArgb(0, 0, 255).ToArgb();

            List<CustomVertex.TransformedColored[]> drawList = new List<CustomVertex.TransformedColored[]>();
            drawList = Generator(vertices, drawList);

            m_device.Clear(ClearFlags.Target, System.Drawing.Color.FromArgb(0, 0, 0).ToArgb(), 1.0f, 0);
            m_device.BeginScene();
            m_device.VertexFormat = CustomVertex.TransformedColored.Format;

            m_device.DrawUserPrimitives(PrimitiveType.TriangleFan, 4, vertices);

            foreach (CustomVertex.TransformedColored[] v in drawList)
            {
                m_device.DrawUserPrimitives(PrimitiveType.LineStrip, 4, v);
            }
            m_device.EndScene();
            m_device.Present();
        }

        public List<CustomVertex.TransformedColored[]> Generator(CustomVertex.TransformedColored[] start, List<CustomVertex.TransformedColored[]> drawList)
        {
            Random rand = new Random();
            for (int i = 0; i < 30; i++)
            {
                CustomVertex.TransformedColored[] newBox = new CustomVertex.TransformedColored[5];
                for (int j = 0; j < newBox.Length; j++)
                {
                    newBox[j].Color = System.Drawing.Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)).ToArgb();
                    newBox[j].X = start[j].X + (10 * i);
                    newBox[j].Y = start[j].Y + (10 * i);
                }

                drawList.Add(newBox);
            }

            for (int k = 0; k < 30; k++)
            {
                CustomVertex.TransformedColored[] newBox = new CustomVertex.TransformedColored[5];
                for (int j = 0; j < newBox.Length; j++)
                {
                    newBox[j].Color = System.Drawing.Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)).ToArgb();
                    newBox[j].X = start[j].X - (10 * k);
                    newBox[j].Y = start[j].Y - (10 * k);
                }

                drawList.Add(newBox);
            }

            for (int m = 0; m < 30; m++)
            {
                CustomVertex.TransformedColored[] newBox = new CustomVertex.TransformedColored[5];
                for (int j = 0; j < newBox.Length; j++)
                {
                    newBox[j].Color = System.Drawing.Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)).ToArgb();
                    newBox[j].X = start[j].X + (10 * m);
                    newBox[j].Y = start[j].Y - (10 * m);
                }

                drawList.Add(newBox);
            }
            
            for (int l = 0; l < 30; l++)
            {
                CustomVertex.TransformedColored[] newBox = new CustomVertex.TransformedColored[5];
                for (int j = 0; j < newBox.Length; j++)
                {
                    newBox[j].Color = System.Drawing.Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)).ToArgb();
                    newBox[j].X = start[j].X - (10 * l);
                    newBox[j].Y = start[j].Y + (10 * l);
                }

                drawList.Add(newBox);
            }

            return drawList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            Form3 form3 = new Form3();
            Form4 form4 = new Form4();
            form2.Form1Reference = this;
            form2.Form3Reference = form3;
            form2.Form4Reference = form4;
            form3.Form1Reference = this;
            form3.Form2Reference = form2;
            form3.Form4Reference = form4;
            form4.Form1Reference = this;
            form4.Form2Reference = form2;
            form4.Form3Reference = form3;
            form2.Show();
            this.Hide();
        }
    }
}
