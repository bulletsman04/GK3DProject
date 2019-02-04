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
        public Matrix4x4 ViewMatrix { get; set; }
        public Camera(Vector3 cTarget, Vector3 cPos, Vector3 cUp)
        {
            CTarget = cTarget;
            CPos = cPos;
            CUp = cUp;
            CalculateViewMatrix();
        }

        public Camera()
        {
        }

        private void CalculateViewMatrix()
        {
            //Vector3 cZ = (CPos - CTarget) / (CPos - CTarget).Length();
            //Vector3 cX = (MathNetHelper.Cross(CUp, cZ)) / (float)(MathNetHelper.Cross(CUp, cZ)).Length();
            //Vector3 cY = (MathNetHelper.Cross(cZ, cX)) / (float)(MathNetHelper.Cross(cZ, cX)).Length();

            //ViewMatrix = new Matrix4x4
            //(

            //    cX.X, cY.X, cZ.X, CPos.X,
            //    cX.Y, cY.Y, cZ.Y, CPos.Y,
            //    cX.Z, cY.Z, cZ.Z, CPos.Z,

            //    0, 0, 0, 1
            //);
            //Matrix4x4.Invert(ViewMatrix, out var invertedViewMatrix);
            //ViewMatrix = invertedViewMatrix;

            Vector3 f = Vector3.Normalize(CPos - CTarget);
            Vector3 s = Vector3.Normalize(Vector3.Cross(f, CUp));
            Vector3 v = Vector3.Normalize(Vector3.Cross(s, f));

            ViewMatrix = new Matrix4x4(
                s.X, s.Y, s.Z, Vector3.Dot(s, -CPos),
                v.X, v.Y, v.Z, Vector3.Dot(v, -CPos),
                f.X, f.Y, f.Z, Vector3.Dot(f, -CPos),
                0, 0, 0, 1);
        }
        

        public void UpdateCamera()
        {
            CalculateViewMatrix();
        }
    }
}
