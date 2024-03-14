using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karan_Project
{
    /// <summary>
    /// Represents a Circle shape.
    /// </summary>
    public class Circle:Shape
    {

        int radius;

        /// <summary>
        /// Initializes a new instance of the Circle class with default values.
        /// </summary>
        public Circle() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the Circle class with specified color, position, and radius.
        /// </summary>
        /// <param name="colour">Color of the circle.</param>
        /// <param name="x">X-coordinate of the center.</param>
        /// <param name="y">Y-coordinate of the center.</param>
        /// <param name="radius">Radius of the circle.</param>
        public Circle(Color colour, int x, int y, int radius) : base(colour, x, y)
        {

            this.radius = radius; //the only thingthat is different from shape
        }

        /// <summary>
        /// Initializes a new instance of the Circle class with specified color, fill status, position, and radius.
        /// </summary>
        /// <param name="colour">Color of the circle.</param>
        /// <param name="fill">Boolean indicating whether the circle should be filled.</param>
        /// <param name="x">X-coordinate of the center.</param>
        /// <param name="y">Y-coordinate of the center.</param>
        /// <param name="radius">Radius of the circle.</param>

        public Circle(Color colour,bool fill, int x, int y, int radius) : base(colour, x, y)
        {

            this.radius = radius; //the only thingthat is different from shape
        }



        /// <summary>
        /// Sets the properties of the circle with the specified color and parameters.
        /// </summary>
        /// <param name="colour">Color of the circle.</param>
        /// <param name="list">Parameters: x-coordinate, y-coordinate, and radius.</param>

        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is radius
            base.set(colour, list[0], list[1]);
            this.radius = list[2];

        }

        /// <summary>
        /// Sets the properties of the circle with the specified color, fill status, and parameters.
        /// </summary>
        /// <param name="colour">Color of the circle.</param>
        /// <param name="fill">Boolean indicating whether the circle should be filled.</param>
        /// <param name="list">Parameters: x-coordinate, y-coordinate, and radius.</param>

        public override void set(Color colour, bool fill, params int[] list)
        {
            base.set(colour, fill, list[0], list[1]);
            this.radius = list[2];
        }


        /// <summary>
        /// Draws the circle on the provided graphics object.
        /// </summary>
        /// <param name="g">Graphics object to draw the circle.</param>

        public override void draw(Graphics g)
        {
            if (fill)
            {
                SolidBrush b = new SolidBrush(this.colour);
                g.FillEllipse(b, x, y, radius * 2, radius * 2);
            }
            else
            {
                Pen p = new Pen(this.colour, 2);
                g.DrawEllipse(p, x, y, radius * 2, radius * 2);
            }
        }

        /// <summary>
        /// Overrides the ToString method to provide shape information.
        /// </summary>
        /// <returns>A string representation of the circle's properties.</returns>

        public override string ToString() //all classes inherit from object and ToString() is abstract in object
        {
            return base.ToString() + "  " + this.radius;
        }
    }
}
