using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Models
{
    public static class MathNetHelper
    {
        public static VectorBuilder<float> V { get; } = Vector<float>.Build;
        public static MatrixBuilder<float> M { get; } = Matrix<float>.Build;
        public static Vector3 Cross(Vector3 left, Vector3 right)
        {

            Vector3 result = new Vector3();
            result.X = left.Y * right.Z - left.Z * right.Y;
            result.Y = -left.X * right.Z + left.Z * right.X;
            result.Z = left.X * right.Y - left.Y * right.X;

            return result;
        }

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
