using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using MathNet.Numerics.LinearAlgebra;
using MvvmFoundation.Wpf;

namespace Models
{
    public class ScenePresenter: ObservableObject
    {
        public Matrix<float> ProjectionMatrix { get; set; }
        private int _vPWidth;
        private int _vPHeight;
        private BitmapManager _bitmapManager;
        private Scene _scene;
        private Timer _timer;
        private bool _locked = false;
        public ScenePresenter(BitmapManager bitmapManager)
        {
            _vPHeight = bitmapManager.Height;
            _vPWidth = bitmapManager.Width;
            _bitmapManager = bitmapManager;
            CreateProjectionMatrix();
            _scene = new Scene();
            StartScene();
        }

        public void StartScene()
        {
            SetTimer();
        }

        private void SetTimer()
        {
            _timer = new Timer(50);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;

        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            
            if (_locked == false)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _locked = true;
                    RepaintScene();
                    _bitmapManager.RaiseBitmapChanged();
                    _locked = false;
                });
            }



        }



        private void RepaintScene()
        {
            using (Graphics g = Graphics.FromImage(_bitmapManager.MainBitmap.Bitmap))
            {
                g.Clear(Color.White);
                Pen pen = new Pen(Color.Black);
                foreach (WorldObject worldObject in _scene.WorldObjects)
                {
                    foreach (Triangle triangle in worldObject.LocalObject.Mesh.Triangles)
                    {
                        if(triangle==null)
                            continue;

                        Vector<float> p1 = worldObject.LocalObject.Mesh.Vertices[triangle.A];
                        Vector<float> p2 = worldObject.LocalObject.Mesh.Vertices[triangle.B];
                        Vector<float> p3 = worldObject.LocalObject.Mesh.Vertices[triangle.C];

                        Vector<float> vshader1 = vectorShader(p1, worldObject.ModelMatrix);
                        Vector<float> vshader2 = vectorShader(p2, worldObject.ModelMatrix);
                        Vector<float> vshader3 = vectorShader(p3, worldObject.ModelMatrix);

                        float p1ex = (float)((vshader1[0] / vshader1[3] + 1) * _vPWidth / 2);
                        float p1ey = (float)((vshader1[1] / vshader1[3] + 1) * _vPHeight / 2);
                        float p2ex = (float)((vshader2[0] / vshader2[3] + 1) * _vPWidth / 2);
                        float p2ey = (float)((vshader2[1] / vshader2[3] + 1) * _vPHeight / 2);
                        float p3ex = (float)((vshader3[0] / vshader3[3] + 1) * _vPWidth / 2);
                        float p3ey = (float)((vshader3[1] / vshader3[3] + 1) * _vPHeight / 2);

                        g.DrawLine(pen, p1ex, p1ey, p2ex, p2ey);
                        g.DrawLine(pen, p1ex, p1ey, p3ex, p3ey);
                        g.DrawLine(pen, p2ex, p2ey, p3ex, p3ey);

                    }
                }
                
            }
            _bitmapManager.MainBitmap.Bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
        }

        private Vector<float> vectorShader(Vector<float> point, Matrix<float> modelMatrix)
        {
            Vector<float> result = ProjectionMatrix * _scene.Camera.ViewMatrix * modelMatrix * point;
            return result;
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
