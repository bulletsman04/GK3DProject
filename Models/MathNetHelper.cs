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
    }
}
