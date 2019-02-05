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
        public ShadingArguments(Camera camera, Accord.Math.Vector3 barycentricCoords, FilledTriangle triangle) : this()
        {
            Camera = camera;
            BarycentricCoords = barycentricCoords;
            Triangle = triangle;
        }

        public Camera Camera { get; set; }
        public Accord.Math.Vector3 BarycentricCoords { get; set; }
        public FilledTriangle Triangle { get; set; }

        
    }
}
