using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karan_Project
{   /// <summary>
    /// Class DrawTo for line inherits from shape
    /// </summary>
   public class DrawLine: Shape
    {
        int x2, y2;
        /// <summary>
        /// base class constructor called
        /// </summary>
        /// 

        public DrawLine() : base()
        {

        }
        /// <summary>
        /// Overloading constructor and call overloaded base constructor
        /// </summary>
        /// <param name="color"></param>
        /// <param name="fill"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public DrawLine(Color colour, bool fill, int x2, int y2) : base(colour, fill, x2, y2)
        {
            this.x2 = x2;
            this.y2 = y2;
        }
        /// <summary>
        /// Override draw method from base class implemented
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            Pen pen = new Pen(this.colour, 2);
            g.DrawLine(pen, x, y, x2, y2);
        }
        /// <summary>
        /// Override set method from base class implemented
        /// </summary>
        /// <param name="color"></param>
        /// <param name="fill"></param>
        /// <param name="list"></param>
        public override void set(Color colour, bool fill, params int[] list)
        {
            base.set(colour, fill, list[0], list[1]);
            this.x2 = list[2];
            this.y2 = list[3];
        }
    }
}
