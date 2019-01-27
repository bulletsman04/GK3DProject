using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Models
{
    public class Camera
    {
        public Vector3 CTarget { get; set; }
        public Vector3 CPos { get; set; }
        public Vector3 CUp { get; set; }
        // ToDo: Should be there ?
        public Matrix4x4 ViewMatrix { get; set; }
        private float _alpha = 0;
        public Camera(Vector3 cTarget, Vector3 cPos, Vector3 cUp)
        {
            CTarget = cTarget;
            CPos = cPos;
            CUp = cUp;
          // RotateCamera();
            CalculateViewMatrix();
        }

        public Camera()
        {
        }

        private void CalculateViewMatrix()
        {
            Vector3 cZ = (CPos - CTarget) / (CPos - CTarget).Length();
            Vector3 cX = (MathNetHelper.Cross(CUp, cZ)) / (float)(MathNetHelper.Cross(CUp, cZ)).Length();
            Vector3 cY = (MathNetHelper.Cross(cZ, cX)) / (float)(MathNetHelper.Cross(cZ, cX)).Length();

            ViewMatrix = new Matrix4x4
            (

                cX.X,cY.X,cZ.X,CPos.X,
                cX.Y,cY.Y,cZ.Y,CPos.Y,
                cX.Z,cY.Z,cZ.Z,CPos.Z,

                0,0,0,1
            );
            Matrix4x4.Invert(ViewMatrix,out var invertedViewMatrix);
            ViewMatrix = invertedViewMatrix;
        }


        public void RotateCamera()
        {
            _alpha += (float)(3 * Math.PI / 180);
            CPos = new Vector3((float)(0 + 7* Math.Cos(_alpha)), (float)(0 + 7 * Math.Sin(_alpha)),-3f);
            CalculateViewMatrix();
        }
    }
}
