using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonteCarloMethodDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += (s, e) =>
            {
                g.Clear(SystemColors.ActiveBorder);
                scaleFactor += (float)e.Delta / 1200;
                try
                {
                    g.ScaleTransform(scaleFactor, scaleFactor);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Invalidate();
            };
            g = pictureBox1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Invalidate();
            if (!int.TryParse(textBox1.Text, out int TotalCount)) return;
            innerPointsNum = 0;
            g.Transform = new Matrix(1, 0, 0, -1, pictureBox1.Width / 2, pictureBox1.Height / 2);
            for (int i = 0; i < TotalCount; i++)
            {
                double x1 = random.NextDouble() * 2 - 1;
                double y1 = random.NextDouble() * 2 - 1;
                double distance = x1 * x1 + y1 * y1;
                if (distance <= 1)
                {
                    innerPointsNum++;
                    g.FillEllipse(Brushes.Blue, new RectangleF((float)x1 * 50 - 1, (float)y1 * 50 - 1, 2, 2));
                }
                else
                {
                    g.FillEllipse(Brushes.Red, new RectangleF((float)x1 * 50 - 1, (float)y1 * 50 - 1, 2, 2));
                }
            }
            double res = (double)innerPointsNum / (double)TotalCount * 4;
            textBox2.Text = res.ToString();
            g.ResetTransform();
        }

        private Graphics g;
        private Random random = new Random();
        private int innerPointsNum;
        private float scaleFactor = 1;

        protected override void OnPaint(PaintEventArgs e)
        {
            //g.Clear(SystemColors.ActiveBorder);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawRectangle(Pens.Black, new Rectangle(15, 15, 100, 100));
            g.DrawEllipse(Pens.Black, new Rectangle(15, 15, 100, 100));
            base.OnPaint(e);
        }
    }
}
