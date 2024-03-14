using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karan_Project
{
    /// <summary>
    /// Represents a basic shape.
    /// </summary>
    public abstract class Shape:Shapes
    {
        protected Color colour; //shape's colour
        protected int x, y; // not I could use C# properties for this
        protected bool fill;

        /// <summary>
        /// Default constructor for the Shape class.
        /// </summary>
        public Shape()
        {
            colour = Color.Red;
            x = y = 0;
        }


        /// <summary>
        /// Constructor to initialize a Shape with specific attributes.
        /// </summary>
        /// <param name="colour">The color of the Shape.</param>
        /// <param name="x">The X-coordinate of the Shape.</param>
        /// <param name="y">The Y-coordinate of the Shape.</param>
        public Shape(Color colour, int x, int y)
        {

            this.colour = colour; //shape's colour
            this.x = x; //its x pos
            this.y = y; //its y pos
        }

        /// <summary>
        /// Constructor to initialize a Shape with specific attributes.
        /// </summary>
        /// <param name="colour">The color of the Shape.</param>
        /// <param name="fill">Whether the Shape should be filled or not.</param>
        /// <param name="x">The X-coordinate of the Shape.</param>
        /// <param name="y">The Y-coordinate of the Shape.</param>
        public Shape(Color colour, bool fill, int x, int y)
        {
            this.colour = colour;
            this.fill = fill;
            this.x = x;
            this.y = y;

        }

        //the three methods below are from the Shapes interface
        //here we are passing on the obligation to implement them to the derived classes by declaring them as abstract

        /// <summary>
        /// Draws the shape using the specified Graphics object.
        /// </summary>
        /// <param name="g">The Graphics object used for drawing.</param>

        public abstract void draw(Graphics g); //any derrived class must implement this method

        //set is declared as virtual so it can be overridden by a more specific child version
        //but is here so it can be called by that child version to do the generic stuff
        //note the use of the param keyword to provide a variable parameter list to cope with some shapes having more setup information than others

        /// <summary>
        /// Sets the attributes of the shape.
        /// </summary>
        /// <param name="colour">The color of the shape.</param>
        /// <param name="list">List containing x and y parameters.</param>

        public virtual void set(Color colour, params int[] list)
        {
            this.colour = colour;
            this.x = list[0];
            this.y = list[1];
        }


        /// <summary>
        /// Sets the attributes of the shape.
        /// </summary>
        /// <param name="colour">The color of the shape.</param>
        /// <param name="fill">Whether the shape should be filled or not.</param>
        /// <param name="list">List containing x and y parameters.</param>

        public virtual void set(Color colour , bool fill, params int[] list)
        {
            this.colour = colour;
            this.fill = fill;
            this.x= list[0];
            this.y= list[1];
        }


        /// <summary>
        /// Overrides the default ToString() method to display position coordinates.
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }
    }
}
