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
        public Vector4 p1 { get; set; }
        public Vector4 p2 { get; set; }
        public Vector4 p3 { get; set; }
        public Vector4 n1 { get; set; }
        public Vector4 n2 { get; set; }
        public Vector4 n3 { get; set; }

        public Matrix3x3 A { get; set; }
        public float ZA { get; set; }
        public float ZB { get; set; }
        public float ZC { get; set; }
        public Vector3 VA { get; set; }
        public Vector3 VB { get; set; }
        public Vector3 VC { get; set; }
        public Vector4 Color { get; set; }





    }
}
