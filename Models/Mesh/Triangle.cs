using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Triangle
    {
        public int A;
        public int B;
        public int C;

        public Vector4 Color { get; set; } = new Vector4(0,1,0,0);
        public Triangle(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }
    }
}
