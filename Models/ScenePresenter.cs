using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using Accord.Math;
using MathNet.Numerics.LinearAlgebra;
using Models.FillingRectangles;
using MvvmFoundation.Wpf;
using Matrix4x4 = System.Numerics.Matrix4x4;
using Vector3 = System.Numerics.Vector3;
using Vector4 = System.Numerics.Vector4;

namespace Models
{
    public class ScenePresenter: ObservableObject
    {
        public Matrix4x4 ProjectionMatrix { get; set; }
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
            //RepaintScene();
            //_bitmapManager.RaiseBitmapChanged();
            ////_scene.WorldObjects.Clear();
            //_scene.Camera.RotateCamera();
            //RepaintScene();
            //_bitmapManager.RaiseBitmapChanged();
        }

        private void SetTimer()
        {
            _timer = new Timer(100);
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
                   // _scene.Camera.RotateCamera();
                    foreach (var sceneWorldObject in _scene.WorldObjects)
                    {
                        
                        sceneWorldObject.Update();
                    }
                    RepaintScene();
                    _bitmapManager.RaiseBitmapChanged();
                    _locked = false;
                });
            }



        }

        private int counter = 0;

        private void RepaintScene()
        {
            // WTF?
            //using (Graphics g = Graphics.FromImage(_bitmapManager.MainBitmap.Bitmap))
            //{
            //    g.Clear(Color.White);
            //}

            counter++;

                MyGraphics myGraphics = new MyGraphics(_bitmapManager.MainBitmap);

                myGraphics.ClearBitmap();




                Pen pen = new Pen(Color.Black);
                foreach (WorldObject worldObject in _scene.WorldObjects)
                {
                    foreach (Triangle triangle in worldObject.LocalObject.Mesh.Triangles)
                    {
                        if (triangle == null)
                            continue;
                        FilledTriangle filledtriangle = new FilledTriangle();

                        Vector4 p1 = worldObject.LocalObject.Mesh.Vertices[triangle.A].Point;
                        Vector4 p2 = worldObject.LocalObject.Mesh.Vertices[triangle.B].Point;
                        Vector4 p3 = worldObject.LocalObject.Mesh.Vertices[triangle.C].Point;

                        Vector4 vshader1 = vectorShader(p1, worldObject.LocalObject.Mesh.Vertices[triangle.A].Normal, worldObject.ModelMatrix, out Vector4 p1M, out  Vector4 n1M);
                        Vector4 vshader2 = vectorShader(p2, worldObject.LocalObject.Mesh.Vertices[triangle.B].Normal, worldObject.ModelMatrix, out Vector4 p2M, out Vector4 n2M);
                        Vector4 vshader3 = vectorShader(p3, worldObject.LocalObject.Mesh.Vertices[triangle.C].Normal, worldObject.ModelMatrix, out Vector4 p3M, out Vector4 n3M);

                        float p1ex = (vshader1.X / vshader1.W + 1) * _vPWidth / 2;
                        float p1ey =  ((vshader1.Y / vshader1.W + 1) * _vPHeight / 2);
                        float p2ex =  ((vshader2.X / vshader2.W + 1) * _vPWidth / 2);
                        float p2ey = ((vshader2.Y / vshader2.W + 1) * _vPHeight / 2);
                        float p3ex =  ((vshader3.X / vshader3.W + 1) * _vPWidth / 2);
                        float p3ey =  ((vshader3.Y / vshader3.W + 1) * _vPHeight / 2);

                        
                        filledtriangle.Vertices = new List<Vertex>();
                        filledtriangle.Vertices.Add(new Vertex((int)p1ex, (int)p1ey));
                        filledtriangle.Vertices.Add(new Vertex((int)p2ex, (int)p2ey));
                        filledtriangle.Vertices.Add(new Vertex((int)p3ex, (int)p3ey));
                        Accord.Math.Vector3 a = new Accord.Math.Vector3(p1ex, p1ey, 1);
                        Accord.Math.Vector3 b = new Accord.Math.Vector3(p2ex, p2ey, 1);
                        Accord.Math.Vector3 c = new Accord.Math.Vector3(p3ex, p3ey, 1);
                        filledtriangle.A = Matrix3x3.CreateFromColumns(a, b, c);
                        filledtriangle.VA = a;
                        filledtriangle.VB = b;
                        filledtriangle.VC = c;
                        filledtriangle.ZA = vshader1.Z / vshader1.W;
                        filledtriangle.ZB = vshader2.Z / vshader2.W;
                        filledtriangle.ZC  = vshader3.Z / vshader3.W;
                        filledtriangle.n1 = n1M;
                        filledtriangle.n2 = n2M;
                        filledtriangle.n3 = n3M;
                        filledtriangle.p1 = p1M;
                        filledtriangle.p2 = p2M;
                        filledtriangle.p3 = p3M;



                    myGraphics.FillPolygon(filledtriangle, triangle.Color,_scene.Camera);

                        //g.DrawLine(pen, p1ex, p1ey, p2ex, p2ey);
                        //g.DrawLine(pen, p1ex, p1ey, p3ex, p3ey);
                        //g.DrawLine(pen, p2ex, p2ey, p3ex, p3ey);
                    
                }
            }

            // _bitmapManager.MainBitmap = myGraphics._directBitmap;

            //Bitmap b = _bitmapManager.MainBitmap.Bitmap;
            //b.RotateFlip(RotateFlipType.Rotate180FlipX);
            //_bitmapManager.MainBitmap.Bitmap = b;
        }

        private Vector4 vectorShader(Vector4 point, Vector4 normal, Matrix4x4 modelMatrix, out Vector4 pM, out Vector4 nM)
        {

            Matrix4x4 modelVertex = modelMatrix.Multiply(point);

            Matrix4x4 result = ProjectionMatrix * _scene.Camera.ViewMatrix * modelVertex;

            pM = MathNetHelper.Matrix4X4ToVector4(modelVertex);
            nM = MathNetHelper.Matrix4X4ToVector4(modelMatrix.Multiply(normal));

            return MathNetHelper.Matrix4X4ToVector4(result);
        }

        private void CreateProjectionMatrix()
        {
            float FOV = 90;
            float e = (float)(1 / Math.Tan(FOV * Math.PI / 180 / 2));
            int n = 1;
            int f = 100;
            float a = _vPHeight / _vPWidth;

            ProjectionMatrix = new Matrix4x4
            (
                e, 0, 0, 0,
                0, e/a, 0, 0,
                0, 0, -(f+n)/(f-n), -2*f*n/(f-n),
                0, 0, -1, 0
            );
        }
    }
}
