using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Models
{
    public class ScenePresenter
    {
        public Matrix<float> ProjectionMatrix { get; set; }
        private int _vPWidth;
        private int _vPHeight;


        public ScenePresenter(int vPHeight, int vPWidth)
        {
            _vPHeight = vPHeight;
            _vPWidth = vPWidth;
        }

        private void CreateProjectionMatrix()
        {
            float FOV = 90;
            float e = (float)(1 / Math.Tan(FOV * Math.PI / 180 / 2));
            int n = 1;
            int f = 100;
            float a = _vPHeight / _vPWidth;

            ProjectionMatrix = MathNetHelper.M.DenseOfArray(new float[4, 4]
            {
                {e, 0, 0, 0},
                {0, e/a, 0, 0},
                {0, 0, -(f+n)/(f-n), -2*f*n/(f-n)},
                {0, 0, -1, 0}
            });
        }
    }
}
