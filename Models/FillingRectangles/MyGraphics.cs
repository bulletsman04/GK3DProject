using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
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
        public DirectBitmap _directBitmap;

        public MyGraphics(DirectBitmap directBitmap)
        {
            _directBitmap = directBitmap;
            _zBuffer = new float[directBitmap.Width, directBitmap.Height];
        }

        public void InitializeZBuffer()
        {
            for (int i = 0; i < _directBitmap.Width; i++)
            {
                for (int j = 0; j < _directBitmap.Height; j++)
                {
                    _zBuffer[i, j] = float.PositiveInfinity;
                }
            }
        }

        private float CountZCoord(Vector3 barycentricCoords, FilledTriangle triangle)
        {
          

            float z = barycentricCoords.X * triangle.ZA + barycentricCoords.Y * triangle.ZB + barycentricCoords.Z * triangle.ZC;

            return z;
        }

        private Vector4 CalculateNormal(Vector3 barycentricCoords, FilledTriangle triangle)
        {

            Vector4 normal = new Vector4();
            normal.X = barycentricCoords.X * triangle.n1.X+ barycentricCoords.Y * triangle.n2.X + barycentricCoords.Z * triangle.n3.X;
            normal.Y = barycentricCoords.X * triangle.n1.Y + barycentricCoords.Y * triangle.n2.Y + barycentricCoords.Z * triangle.n3.Y;
            normal.Z = barycentricCoords.X * triangle.n1.Z + barycentricCoords.Y * triangle.n2.Z + barycentricCoords.Z * triangle.n3.Z;
            return Vector4.Normalize(normal);
        }

        private Vector4 CalculatePoint(Vector3 barycentricCoords, FilledTriangle triangle)
        {

            Vector4 point = new Vector4();
            point.X = barycentricCoords.X * triangle.p1.X + barycentricCoords.Y * triangle.p1.X + barycentricCoords.Z * triangle.p3.X;
            point.Y = barycentricCoords.X * triangle.p1.Y + barycentricCoords.Y * triangle.p2.Y + barycentricCoords.Z * triangle.p3.Y;
            point.Z = barycentricCoords.X * triangle.p1.Z + barycentricCoords.Y * triangle.p3.Z + barycentricCoords.Z * triangle.p3.Z;
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
        public void FillPolygon(FilledTriangle triangle, Color color, Camera camera)
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
                for (int i = 0; i < AET.Count - 1; i += 2)
                {
                    for (int j = (int)Math.Round(AET[i].X); j < (int)Math.Round(AET[i + 1].X); j++)
                    {
                        if (j < _directBitmap.Width && j >= 0 && (y - 1) < _directBitmap.Height && (y - 1) >= 0)
                        {
                            Vector3 barycentricCoords = CalculateBarycentric(j, y - 1, triangle);
                        
                            float zp = CountZCoord(barycentricCoords, triangle);

                            if (zp < _zBuffer[j, y - 1])
                            {
                                Vector4 normal = CalculateNormal(barycentricCoords, triangle);
                                Vector4 point = CalculatePoint(barycentricCoords, triangle);
                                Color finalColor = Shaders.FragmentShader(camera, point, normal, new Vector4(0, 1, 0, 0),
                                    new List<Vector4>
                                    {
                                        new Vector4(0f, 0f, -4f, 0)
                                    });

                                _directBitmap.SetPixel(j, y - 1, finalColor);

                                _zBuffer[j, y - 1] = zp;
                            }

                        }
                    }

                }
                // ToDo: Przerzucić wyżej
                foreach (var t in AET)
                {
                    t.X += t.iM;
                }
            }

            ;
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


        public void ClearBitmap()
        {

            for (int i = 0; i < _directBitmap.Width; i++)
            {
                for (int j = 0; j < _directBitmap.Height; j++)
                {
                     _directBitmap.SetPixel(i,j, Color.Black);
                }
            }
        }


    }
}
