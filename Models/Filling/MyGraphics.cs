using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Accord.IO;
using Accord.Math;
using MathNet.Numerics.LinearAlgebra;
using Models.FillingRectangles;
using Vector3 = Accord.Math.Vector3;
using Vector4 = System.Numerics.Vector4;

namespace Models
{
    public class MyGraphics
    {

        private float[,] _zBuffer;
        private ShadingArguments[,] arguments;
        private int minX, minY, maxX, maxY = 0;
        public DirectBitmap DirectBitmap { get; set; }

        public MyGraphics(DirectBitmap directBitmap)
        {
            DirectBitmap = directBitmap;
            InitializeZBuffer(directBitmap.Width,DirectBitmap.Height);
        }

        public void  InitializeZBuffer(int width, int height)
        {
            _zBuffer = new float[width, height];
            arguments = new ShadingArguments[width, height];
            Parallel.For(0, width, i =>
            {
                Parallel.For(0, height, j =>
                {
                    _zBuffer[i, j] = float.PositiveInfinity;
                    arguments[i, j] = new ShadingArguments(null,Vector4.Zero, Vector4.Zero, Vector4.Zero );
                });
            });

        }

        public void Clear()
        {
            minX = minY = maxX=  maxY = 0;
        }

        private float CountZCoord(Vector3 barycentricCoords, FilledTriangle triangle)
        {
            float z = barycentricCoords.X * triangle.ZA + barycentricCoords.Y * triangle.ZB + barycentricCoords.Z * triangle.ZC;
            return z;
        }

        private Vector4 CalculateNormal(Vector3 barycentricCoords, FilledTriangle triangle)
        {

            Vector4 normal = new Vector4();
            normal.X = barycentricCoords.X * triangle.N1.X+ barycentricCoords.Y * triangle.N2.X + barycentricCoords.Z * triangle.N3.X;
            normal.Y = barycentricCoords.X * triangle.N1.Y + barycentricCoords.Y * triangle.N2.Y + barycentricCoords.Z * triangle.N3.Y;
            normal.Z = barycentricCoords.X * triangle.N1.Z + barycentricCoords.Y * triangle.N2.Z + barycentricCoords.Z * triangle.N3.Z;
            return Vector4.Normalize(normal);
        }

        private Vector4 CalculatePoint(Vector3 barycentricCoords, FilledTriangle triangle)
        {

            Vector4 point = new Vector4();
            point.X = barycentricCoords.X * triangle.P1.X + barycentricCoords.Y * triangle.P2.X + barycentricCoords.Z * triangle.P3.X;
            point.Y = barycentricCoords.X * triangle.P1.Y + barycentricCoords.Y * triangle.P2.Y + barycentricCoords.Z * triangle.P3.Y;
            point.Z = barycentricCoords.X * triangle.P1.Z + barycentricCoords.Y * triangle.P2.Z + barycentricCoords.Z * triangle.P3.Z;
            return point;
        }

        private Vector4 CalculateColor(Vector3 barycentricCoords, FilledTriangle triangle)
        {

            Vector4 point = new Vector4();
            point.X = barycentricCoords.X * triangle.Vertices[0].Color.X + barycentricCoords.Y * triangle.Vertices[1].Color.X + barycentricCoords.Z * triangle.Vertices[2].Color.X;
            point.Y = barycentricCoords.X * triangle.Vertices[0].Color.Y + barycentricCoords.Y * triangle.Vertices[1].Color.Y + barycentricCoords.Z * triangle.Vertices[2].Color.Y;
            point.Z = barycentricCoords.X * triangle.Vertices[0].Color.Z + barycentricCoords.Y * triangle.Vertices[1].Color.Z + barycentricCoords.Z * triangle.Vertices[2].Color.Z;
            return point;
        }

        private Vector3 CalculateBarycentric(int x, int y, FilledTriangle triangle)
        {

            var d = new Vector3(x, y, 1);

            var w = triangle.A.Determinant;
            var wx = Matrix3x3.CreateFromColumns(d, triangle.VB, triangle.VC).Determinant;
            var wy = Matrix3x3.CreateFromColumns(triangle.VA, d, triangle.VC).Determinant;
            var wz = Matrix3x3.CreateFromColumns(triangle.VA, triangle.VB, d).Determinant;

            var xA = wx / w;
            var xB = wy / w;
            var xC = wz / w;

            return  new Vector3(xA,xB,xC);
        }
        
        // ToDo: Zmienić na strukturę
        private class Node
        {
            public int Start { get; }
            public int End { get; }
            public double iM { get; }
            public double X { get; set; }


            public Node(int start, int end, double M, int x)
            {
                Start = start;
                End = end;
                this.iM = M != 0 ? 1 / M : 0;
                X = x;
            }
        }
        public void FillPolygon(FilledTriangle triangle, Camera camera)
        {

            
            List<Vertex> vertices = triangle.Vertices;
            int[] ind = Enumerable.Range(0, triangle.Vertices.Count).OrderBy(x => triangle.Vertices[x].Y).ToArray();
            int ymin = vertices[ind[0]].Y;
            int ymax = vertices[ind[vertices.Count - 1]].Y;
            int k = 0;
            List<Node> AET = new List<Node>();
            for (int y = ymin; y <= ymax; y++)
            {
                while (vertices[ind[k]].Y == y - 1)
                {
                    int i = ind[k];
                    int iPrev = (ind[k] - 1) % vertices.Count;
                    if (iPrev < 0)
                        iPrev += vertices.Count;

                    Vertex Pi = vertices[i];
                    Vertex PiPrev = vertices[iPrev];

                    CheckNeighbour(AET, iPrev, Pi, i, PiPrev);

                    int iNext = (ind[k] + 1) % vertices.Count;
                    if (iNext < 0)
                        iNext += vertices.Count;

                    Vertex PiNext = vertices[iNext];

                    CheckNeighbour(AET, i, Pi, iNext, PiNext);
                    k++;
                }

                // AET update
                AET = AET.OrderBy(node => node.X).ToList();
                var aet = AET;
                Parallel.For(0, AET.Count - 1,i =>
                {
                    i *= 2;
                    var y1 = y;
                    Parallel.For((int)Math.Round(aet[i].X), (int)Math.Round(aet[i + 1].X), j =>
                    {

                        Vector3 barycentricCoords = CalculateBarycentric(j, y1 - 1, triangle);

                        float zp = CountZCoord(barycentricCoords, triangle);

                        if (zp <= _zBuffer[j, y1 - 1])
                        {
                            triangle = PrepareForFilling(triangle, camera, j, y, y1, barycentricCoords, zp);
                        }
                    });
                });

                foreach (var t in AET)
                {
                    t.X += t.iM;
                }
            }
        }

        private FilledTriangle PrepareForFilling(FilledTriangle triangle, Camera camera, int j, int y, int y1, Vector3 barycentricCoords, float zp)
        {
            Vector4 normal = Vector4.Zero;
            Vector4 point = Vector4.Zero;
            ;
            Vector4 IO = Vector4.One;
            if (Shaders.Settings.IsPhong == true)
            {
                normal = CalculateNormal(barycentricCoords, triangle);
                point = CalculatePoint(barycentricCoords, triangle);
                IO = triangle.Vertices[0].Color;
            }
            else
            {
                IO = CalculateColor(barycentricCoords, triangle);
            }

            arguments[j, y - 1] = new ShadingArguments(camera, point, normal, IO);

            _zBuffer[j, y1 - 1] = zp;

            if (j <= minX)
                minX = j;
            if (j >= maxX)
                maxX = j;
            if (y - 1 <= minY)
                minY = y - 1;
            if (y - 1 >= maxY)
                maxY = y - 1;
            return triangle;
        }

        public void FinalFill()
        {
            Parallel.For(minX, maxX+1, i =>
            {
                Parallel.For(minY, maxY+1, j =>
                {
                    ShadingArguments shadingArguments = arguments[i, j];
                    if (shadingArguments.Camera != null)
                    {
                        Color finalColor = Shaders.FragmentShader(shadingArguments.Camera, shadingArguments.Point,
                            shadingArguments.Normal, shadingArguments.IO);

                        DirectBitmap.SetPixel(i, j, finalColor);
                        arguments[i, j] = new ShadingArguments(null,Vector4.Zero, Vector4.Zero, Vector4.Zero);
                        _zBuffer[i, j] = float.PositiveInfinity;
                    }
                });
            });
            
        }

        private void CheckNeighbour(List<Node> AET, int i, Vertex Pi, int iNext, Vertex PiNext)
        {
            if (PiNext.Y > Pi.Y)
            {
                double M = PiNext.X != Pi.X ? (double)(Pi.Y - PiNext.Y) / (double)(Pi.X - PiNext.X) : 0;
                AET.Add(new Node(i, iNext, M, Pi.X));
            }
            else
            {
                for (int j = 0; j < AET.Count; j++)
                {
                    if (AET[j].Start != i || AET[j].End != iNext) continue;
                    AET.RemoveAt(j);
                    break;
                }
            }
        }


        
        }


    
}
