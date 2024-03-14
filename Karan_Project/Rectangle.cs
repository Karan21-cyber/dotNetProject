using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karan_Project
{
    /// <summary>
    /// Represents a rectangle shape derived from the base Shape class.
    /// </summary>
    public class Rectangle:Shape
    {
        
        int width, height;

        /// <summary>
        /// Default constructor for Rectangle.
        /// </summary>
        public Rectangle() : base()
        {
        }


        /// <summary>
        /// Constructor for Rectangle with specified parameters.
        /// </summary>
        /// <param name="colour">Color of the rectangle.</param>
        /// <param name="x">X-coordinate of the rectangle.</param>
        /// <param name="y">Y-coordinate of the rectangle.</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        public Rectangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {

            this.width = width; //the only thingthat is different from shape
            this.height = height;
        }


        /// <summary>
        /// Constructor for Rectangle with specified parameters including fill.
        /// </summary>
        /// <param name="colour">Color of the rectangle.</param>
        /// <param name="fill">Boolean indicating whether the rectangle is filled.</param>
        /// <param name="x">X-coordinate of the rectangle.</param>
        /// <param name="y">Y-coordinate of the rectangle.</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>

        public Rectangle(Color colour, bool fill, int x, int y , int width , int height) : base(colour, fill, x, y)
        {
            this.width=width;
            this.height=height;
        }

        /// <summary>
        /// Sets the attributes of the Rectangle.
        /// </summary>
        /// <param name="colour">The color of the Rectangle.</param>
        /// <param name="list">List containing x, y, width, and height parameters.</param>

        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.set(colour, list[0], list[1]);
            this.width = list[2];
            this.height = list[3];

        }

        /// <summary>
        /// Sets the attributes of the Rectangle.
        /// </summary>
        /// <param name="colour">The color of the Rectangle.</param>
        /// <param name="fill">Whether the Rectangle should be filled or not.</param>
        /// <param name="list">List containing x, y, width, and height parameters.</param>

        public override void set(Color colour, bool fill, params int[] list)
        {
            base.set(colour, fill, list[0], list[1]);
            this.width = list[2];
            this.height = list[3];
        }


        /// <summary>
        /// Draws the Rectangle using the specified Graphics object.
        /// </summary>
        /// <param name="g">The Graphics object used for drawing.</param>

        public override void draw(Graphics g)
        {

            if (fill)
            {
                SolidBrush b = new SolidBrush(this.colour);
                g.FillRectangle(b, x, y, width, height);
            }
            else
            {
                Pen p = new Pen(this.colour, 2);
                g.DrawRectangle(p, x, y, width, height);
            }
            
        }

    }
}
