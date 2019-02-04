using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models.FillingRectangles
{
    public struct ShadingArguments
    {
        public ShadingArguments(Camera camera, Vector4 point, Vector4 normal, Vector4 iO) : this()
        {
            Camera = camera;
            Point = point;
            Normal = normal;
            IO = iO;
        }

        public Camera Camera { get; set; }
        public Vector4 Point { get; set; }
        public Vector4 Normal { get; set; }
        public Vector4 IO { get; set; }

        
    }
}
