using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karan_Project
{

    /// <summary>
    /// Interface for defining shapes.
    /// </summary>
    interface Shapes
    {

        /// <summary>
        /// Sets the shape's properties with the given color and parameters.
        /// </summary>
        /// <param name="c">Color to set for the shape.</param>
        /// <param name="list">Parameters to define the shape.</param>

        void set(Color c, params int[] list);

        /// <summary>
        /// Sets the shape's properties with the given color, fill status, and parameters.
        /// </summary>
        /// <param name="c">Color to set for the shape.</param>
        /// <param name="fill">Boolean value indicating whether to fill the shape or not.</param>
        /// <param name="list">Parameters to define the shape.</param>

        void set(Color c, bool fill , params int[] list);

        /// <summary>
        /// Draws the shape on the provided graphics object.
        /// </summary>
        /// <param name="g">Graphics object to draw the shape.</param>

        void draw(Graphics g);

    }
}
