using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class MathNetHelper
    {
        public static Matrix4x4 Multiply(this Matrix4x4 matrix, Vector4 vector)
        {
          Matrix4x4  calculatedMatrixvector = matrix
                * new Matrix4x4(
                    vector.X, 0, 0, 0,
                    vector.Y, 0, 0, 0,
                    vector.Z, 0, 0, 0,
                    vector.W, 0, 0, 0
                );

            return calculatedMatrixvector;
        }

        public static Vector4 Matrix4X4ToVector4(Matrix4x4 matrix)
        {
            return new Vector4(matrix.M11, matrix.M21, matrix.M31, matrix.M41);
        }
        
        
    }
}
