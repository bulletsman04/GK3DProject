using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace Models
{
    public class Mesh
    {
        public string Name { get; set; }
        public Vector4[] Vertices { get; set; }
        public Triangle[] Triangles { get; set; }
    }
}
