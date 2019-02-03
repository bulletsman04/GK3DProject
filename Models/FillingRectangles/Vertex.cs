using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models.FillingRectangles
{
    public struct Vertex
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vector4 Color { get; set; }

        public Vertex(int x, int y, Vector4 color)
        {
            X = x;
            Y = y;
            Color = color;
        }


    }
}
