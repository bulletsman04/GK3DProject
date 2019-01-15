using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Models.FillingRectangles
{
    public class FilledTriangle
    {
        public List<Vertex> Vertices { get; set; }
        public Vector<float> p1 { get; set; }
        public Vector<float> p2 { get; set; }
        public Vector<float> p3 { get; set; }


        public FilledTriangle()
        {
            Vertices = new List<Vertex>();
        }
    }
}
