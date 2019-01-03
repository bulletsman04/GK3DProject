using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace Models
{
    public class WorldObject
    {
        public LocalObject LocalObject { get; set; }
        public Matrix<float> ModelMatrix { get; set; }

        public WorldObject(LocalObject localObject, Matrix<float> modelMatrix)
        {
            LocalObject = localObject;
            ModelMatrix = modelMatrix;
        }
    }
}
