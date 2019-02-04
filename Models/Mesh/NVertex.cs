using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public struct NVertex
    {
        public Vector4 Point { get; set; }
        public Vector4 Normal { get; set; }

        public NVertex(Vector4 point, Vector4 normal)
        {
            Point = point;
            Normal = normal;
                
        }
    }
}
