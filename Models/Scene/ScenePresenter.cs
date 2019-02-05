using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using Accord.Math;
using MathNet.Numerics.LinearAlgebra;
using Models.FillingRectangles;
using MvvmFoundation.Wpf;
using Matrix4x4 = System.Numerics.Matrix4x4;
using Timer = System.Timers.Timer;
using Vector3 = System.Numerics.Vector3;
using Vector4 = System.Numerics.Vector4;

namespace Models
{
    public class ScenePresenter : ObservableObject
    {
        public Matrix4x4 ProjectionMatrix { get; set; }
        private int _vPWidth;
        private int _vPHeight;
        private BitmapManager _bitmapManager;
        private Scene _scene;
        private Timer _timer;
        private bool _locked = false;
        private MyGraphics _myGraphics;
        public PropertyObserver<Settings> SettingsObserver { get; set; }
        public Settings Settings { get; set; }

        public ScenePresenter(BitmapManager bitmapManager, Settings settings)
        {
            _vPHeight = bitmapManager.Height;
            _vPWidth = bitmapManager.Width;
            _bitmapManager = bitmapManager;
            _myGraphics = new MyGraphics(_bitmapManager.MainBitmap);
            CreateProjectionMatrix();
            _scene = new Scene(settings);
            Settings = settings;
            Shader.Settings = settings;

            RegisterPropertiesChanged();
            StartScene();
        }
        private void RegisterPropertiesChanged()
        {
            SettingsObserver = new PropertyObserver<Settings>(Settings)
                .RegisterHandler(n => n.Width, BitmapSizeChangedHandler);
        }

        private void BitmapSizeChangedHandler(Settings s)
        {
            _timer.Stop();
            _vPHeight = Settings.Height;
            _vPWidth = Settings.Width;
            CreateProjectionMatrix();
            _myGraphics.InitializeZBuffer(_vPWidth,_vPHeight);
            _bitmapManager.MainBitmap.Dispose();
            _bitmapManager.MainBitmap = new DirectBitmap(_vPWidth, _vPHeight);
            _myGraphics.DirectBitmap = _bitmapManager.MainBitmap;
            _myGraphics.SetMinMaxBorder();
            _timer.Start();
        }
        public void StartScene()
        {
            SetTimer();
        }

        private void SetTimer()
        {
            _timer = new Timer(30);
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (_locked == false)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _locked = true;
                    _scene.Camera.UpdateCamera();
                    Parallel.ForEach(_scene.WorldObjects, sceneWorldObject => { sceneWorldObject.Update(); });
                    RepaintScene();
                    _bitmapManager.RaiseBitmapChanged();
                    _locked = false;
                });
            }
        }

        private void RepaintScene()
        {
           
            _myGraphics.Clear();

                foreach (WorldObject worldObject in _scene.WorldObjects)
                {

                Parallel.ForEach(worldObject.LocalObject.Mesh.Triangles, triangle =>
                {
                    FilledTriangle filledtriangle = new FilledTriangle();

                    // Vertex shading
                    Vector4 vshader1 = VectorShader(worldObject.LocalObject.Mesh.Vertices[triangle.A],
                        worldObject.ModelMatrix, out Vector4 p1M, out Vector4 n1M);
                    Vector4 vshader2 = VectorShader(worldObject.LocalObject.Mesh.Vertices[triangle.B],
                        worldObject.ModelMatrix, out Vector4 p2M, out Vector4 n2M);
                    Vector4 vshader3 = VectorShader(worldObject.LocalObject.Mesh.Vertices[triangle.C],
                        worldObject.ModelMatrix, out Vector4 p3M, out Vector4 n3M);

             
                    // Back face culling
                    float cosNView = CalculateBackFaceCulling(p1M, n1M, n2M, n3M);

                    if (cosNView >= 0)
                        return;

                    // Normalized vertices in Projection
                    Vector4 pV1 = CaclulateNormalizedProjectionVertex(vshader1);
                    Vector4 pV2 = CaclulateNormalizedProjectionVertex(vshader2);
                    Vector4 pV3 = CaclulateNormalizedProjectionVertex(vshader3);

                    // Cutting to cube
                    if (!isInCube(pV1) || !isInCube(pV2) || !isInCube(pV3))
                        return;


                    // Vertices in World Space used to calculate colors
                    filledtriangle.Mv1 = new NVertex(p1M, n1M);
                    filledtriangle.Mv2 = new NVertex(p2M, n2M);
                    filledtriangle.Mv3 = new NVertex(p3M, n3M);


                    Vector4 color1, color2, color3;
                    GetVerticesColors(triangle, p1M, n1M, out color1, out color2, out color3);

                    // Certices ready to be drawn
                    Vertex viewV1 = CaclulateVieportVertex(pV1, color1);
                    Vertex viewV2 = CaclulateVieportVertex(pV2, color2);
                    Vertex viewV3 = CaclulateVieportVertex(pV3, color3);


                    filledtriangle.Vertices = new List<Vertex> {viewV1, viewV2, viewV3};

                    filledtriangle = PrepareBarycentricMatrix(filledtriangle, viewV1, viewV2, viewV3);

                    _myGraphics.FillPolygon(filledtriangle, _scene.Camera);

                });

                }
                _myGraphics.FinalFill();
            }

        private Vector4 VectorShader(NVertex vertex, Matrix4x4 modelMatrix, out Vector4 pM,
            out Vector4 nM)
        {
            Matrix4x4 modelVertex = modelMatrix.Multiply(vertex.Point);

            Matrix4x4 result = ProjectionMatrix * _scene.Camera.ViewMatrix * modelVertex;

            pM = MathNetHelper.Matrix4X4ToVector4(modelVertex);
            nM = MathNetHelper.Matrix4X4ToVector4(modelMatrix.Multiply(vertex.Normal));

            return MathNetHelper.Matrix4X4ToVector4(result);
        }

        private void GetVerticesColors(Triangle triangle, Vector4 p1M, Vector4 n1M, out Vector4 color1, out Vector4 color2, out Vector4 color3)
        {
            if (Settings.IsGouraud)
            {
                color1 = Shader.CalculatePhong(_scene.Camera, p1M, n1M, triangle.Color);
                color2 = Shader.CalculatePhong(_scene.Camera, p1M, n1M, triangle.Color);
                color3 = Shader.CalculatePhong(_scene.Camera, p1M, n1M, triangle.Color);
            }
            else
            {
                color1 = color2 = color3 = triangle.Color;
            }
        }

        private static FilledTriangle PrepareBarycentricMatrix(FilledTriangle filledtriangle, Vertex viewV1, Vertex viewV2, Vertex viewV3)
        {
            Accord.Math.Vector3 a = new Accord.Math.Vector3(viewV1.X, viewV1.Y, 1);
            Accord.Math.Vector3 b = new Accord.Math.Vector3(viewV2.X, viewV2.Y, 1);
            Accord.Math.Vector3 c = new Accord.Math.Vector3(viewV3.X, viewV3.Y, 1);
            filledtriangle.A = Matrix3x3.CreateFromColumns(a, b, c);
            filledtriangle.VA = a;
            filledtriangle.VB = b;
            filledtriangle.VC = c;
            return filledtriangle;
        }

        private Vertex CaclulateVieportVertex(Vector4 normalizedPVertex, Vector4 color)
        {
            int x = (int)((normalizedPVertex.X+ 1) * _vPWidth / 2);
            int y = (int)((normalizedPVertex.Y + 1) * _vPHeight / 2);
            
            return  new Vertex(x,y,normalizedPVertex.Z,color);
        }

        private Vector4 CaclulateNormalizedProjectionVertex(Vector4 vshader)
        {
            var x = vshader.X / vshader.W;
            var y = vshader.Y / vshader.W;
            var z = vshader.Z / vshader.W;
            Vector4 pV1 = new Vector4(x, y, z, 0);

            return pV1;
        }

        private float CalculateBackFaceCulling(Vector4 p1M, Vector4 n1M, Vector4 n2M, Vector4 n3M)
        {
            Vector3 view = new Vector3(p1M.X - _scene.Camera.CPos.X, p1M.Y - _scene.Camera.CPos.Y, p1M.Z - _scene.Camera.CPos.Z);
            Vector4 nAverage = (n1M + n2M + n3M) / 3;
            var cosNView = nAverage.X * view.X + nAverage.Y * view.Y + nAverage.Z * view.Z;
            return cosNView;
        }


        private bool isInCube(Vector4 vertex)
        {
            return !(vertex.X < -1) && !(vertex.X > 1) && !(vertex.Y < -1) && !(vertex.Y > 1) && !(vertex.Z < -1) && !(vertex.Z > 1);
        }
        

        private void CreateProjectionMatrix()
        {
            float FOV = 90;
            float e = (float) (1 / Math.Tan(FOV * Math.PI / 180 / 2));
            int n = 1;
            int f = 100;
            float a = _vPHeight / _vPWidth;

            ProjectionMatrix = new Matrix4x4
            (
                e, 0, 0, 0,
                0, e / a, 0, 0,
                0, 0, -(f + n) / (f - n), -2 * f * n / (f - n),
                0, 0, -1, 0
            );
        }
    }
}