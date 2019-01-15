using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Triangle
    {
        public int A;
        public int B;
        public int C;

        public Color Color { get; set; } = Color.White;
        public Triangle(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }
    }
}
