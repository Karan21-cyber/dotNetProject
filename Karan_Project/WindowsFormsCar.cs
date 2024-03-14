using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karan_Project
{
    public partial class WindowsFormsCar : Form
    {
        private Timer timer;
        private int carPositionX = 0;
        private int carSpeed = 5;
        public WindowsFormsCar()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 50; // Adjust speed of the car by changing this value
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            carPositionX += carSpeed;
            if (carPositionX > this.Width)
            {
                carPositionX = -250; // Reset car position when it reaches the end of the form
            }
            this.Invalidate(); // Redraw the form

        }

        private void DrawCar(Graphics g)
        {
            // Draw a more detailed car
            g.FillRectangle(Brushes.LightBlue, carPositionX, 150, 200, 50); // Body
            g.FillRectangle(Brushes.LightBlue, carPositionX + 50, 100, 100, 50); // Roof
            g.FillEllipse(Brushes.Black, carPositionX + 20, 175, 50, 50); // Front wheel
            g.FillEllipse(Brushes.Black, carPositionX + 130, 175, 50, 50); // Rear wheel

            // Windshield
            Point[] windshieldPoints =
            {
                new Point(carPositionX + 130, 100),
                new Point(carPositionX + 150, 150),
                new Point(carPositionX + 120, 150)
            };
            g.FillPolygon(Brushes.LightGray, windshieldPoints);

            // Windows
            g.FillRectangle(Brushes.LightGray, carPositionX + 55, 105, 90, 30);
        }
        private void WindowsFormsCar_Load_1(object sender, EventArgs e)
        {
            this.DoubleBuffered = true; // Reduce flickering when redrawing the form

        }
        private void WindowsFormsCar_Paint_1(object sender, PaintEventArgs e)
        {
            DrawCar(e.Graphics);

        }
    }
}
