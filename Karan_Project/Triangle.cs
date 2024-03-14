using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karan_Project
{
    /// <summary>
    /// Represents a triangle shape.
    /// </summary>
    public class Triangle:Shape
    {

        int x2;
        int y2;
        int x3;
        int y3;


        /// <summary>
        /// Default constructor for Triangle class.
        /// </summary>
        public Triangle() : base()
        {

        }

        /// <summary>
        /// Constructor for Triangle class with specified parameters.
        /// </summary>
        /// <param name="colour">The color of the triangle.</param>
        /// <param name="fill">Determines if the triangle should be filled.</param>
        /// <param name="x">X-coordinate of the first point.</param>
        /// <param name="y">Y-coordinate of the first point.</param>
        /// <param name="x2">X-coordinate of the second point.</param>
        /// <param name="y2">Y-coordinate of the second point.</param>
        /// <param name="x3">X-coordinate of the third point.</param>
        /// <param name="y3">Y-coordinate of the third point.</param>
        public Triangle(Color colour,bool fill, int x, int y, int x2, int y2, int x3, int y3) : base(colour,fill, x, y)
        {
            this.colour = colour;
            this.x2 = x2; //the only thingthat is different from shape
            this.y2 = y2;
            this.x3 = x3;
            this.y3 = y3;
        }


        /// <summary>
        /// Sets the properties of the triangle using specified parameters.
        /// </summary>
        /// <param name="colour">The color of the triangle.</param>
        /// <param name="list">List of integers specifying coordinates.</param>
        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is side1 , list[3] is side2 , list[4] is side3
            base.set(colour, list[0], list[1]);
            this.x2 = list[2]; //the only thingthat is different from shape
            this.y2 = list[3];
            this.x3 = list[4];
            this.y3 = list[5];

        }


        /// <summary>
        /// Sets the properties of the triangle using specified parameters.
        /// </summary>
        /// <param name="colour">The color of the triangle.</param>
        /// <param name="fill">Determines if the triangle should be filled.</param>
        /// <param name="list">List of integers specifying coordinates.</param>

        public override void set(Color colour, bool fill, params int[] list)
        {
            base.set(colour, fill, list[0], list[1]);
            this.x2 = list[2]; //the only thingthat is different from shape
            this.y2 = list[3];
            this.x3 = list[4];
            this.y3 = list[5];
        }


        /// <summary>
        /// Draws the triangle on the specified graphics object.
        /// </summary>
        /// <param name="g">The graphics object used for drawing.</param>

        public override void draw(Graphics g)
        {

            Point[] trianglePoints = { new Point(x, y), new Point(x2, y2), new Point(x3, y3) };

            if (fill)
            {
                SolidBrush b = new SolidBrush(this.colour);
                g.FillPolygon(b, trianglePoints);
            }
            else
            {
                Pen p = new Pen(this.colour);
                g.DrawPolygon(p, trianglePoints);
            }
        }


        /// <summary>
        /// Returns a string representation of the triangle.
        /// </summary>
        /// <returns>A string containing information about the triangle.</returns>

        public override string ToString() //all classes inherit from object and ToString() is abstract in object
        {
            return base.ToString() + "  " + this.x2+ "  " + this.y2 + "  " + this.x3 + "  " + this.y3;
        }
    }
}
