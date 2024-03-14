using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karan_Project
{
    /// <summary>
    /// Factory class responsible for creating shapes based on the provided shape type.
    /// </summary>
    public class ShapeFactory {

        /// <summary>
        /// Gets the specific shape based on the provided shape type.
        /// </summary>
        /// <param name="shapeType">Type of the shape to create.</param>
        /// <returns>A shape object based on the provided type.</returns>

        public Shape getShape(String shapeType)
        {
            shapeType = shapeType.ToUpper().Trim(); //yoi could argue that you want a specific word string to create an object but I'm allowing any case combination


            if (shapeType.Equals("CIRCLE"))
            {
                return new Circle();
            }
            else if (shapeType.Equals("RECTANGLE"))
            {
                return new Rectangle();

            }
            else if (shapeType.Equals("TRIANGLE"))
            {
                return new Triangle();
            }
            else if (shapeType.Equals("DRAWTO"))
            {
                return new DrawLine();
            }
            else
            {
                //if we get here then what has been passed in is inkown so throw an appropriate exception
                System.ArgumentException argEx = new System.ArgumentException("Factory error: " + shapeType + " does not exist");
                throw argEx;
            }


        }
    }
}
