using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using MathNet.Numerics.LinearAlgebra;
using Vector4 = System.Numerics.Vector4;

namespace Models.FillingRectangles
{
    public struct FilledTriangle
    {
        public List<Vertex> Vertices { get; set; }
        public NVertex Mv1 { get; set; }
        public NVertex Mv2 { get; set; }
        public NVertex Mv3 { get; set; }

        public Matrix3x3 A { get; set; }
        public Vector3 VA { get; set; }
        public Vector3 VB { get; set; }
        public Vector3 VC { get; set; }





    }
}
