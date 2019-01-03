using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Models
{
    public class Camera
    {
        public Vector<float> CTarget { get; set; }
        public Vector<float> CPos{ get; set; }
        public Vector<float> CUp { get; set; }
        // ToDo: Should be there ?
        public Matrix<float> ViewMatrix { get; set; }

        public Camera(Vector<float> cTarget, Vector<float> cPos, Vector<float> cUp)
        {
            CTarget = cTarget;
            CPos = cPos;
            CUp = cUp;
            CalculateViewMatrix();
        }

        private void CalculateViewMatrix()
        {
            Vector<float> cZ = (CPos - CTarget) / (float)(CPos - CTarget).L2Norm();
            Vector<float> cX = (MathNetHelper.Cross(CUp, cZ)) / (float)(MathNetHelper.Cross(CUp, cZ)).L2Norm();
            Vector<float> cY = (MathNetHelper.Cross(cZ, cX)) / (float)(MathNetHelper.Cross(cZ, cX)).L2Norm();

            ViewMatrix = MathNetHelper.M.DenseOfArray(new float[4, 4]
            {

                {cX[0],cY[0],cZ[0],CPos[0]},
                {cX[1],cY[1],cZ[1],CPos[1]},
                {cX[2],cY[2],cZ[2],CPos[2]},

                {0,0,0,1}
            });

            ViewMatrix = ViewMatrix.Inverse();
        }
    }
}
