using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using Models.FillingRectangles;

namespace Models
{
    public class MyGraphics
    {

        private double[,] _zBuffer;
        public DirectBitmap _directBitmap;

        public MyGraphics(DirectBitmap directBitmap)
        {
            _directBitmap = directBitmap;
            InitializeZBuffer(directBitmap.Width, directBitmap.Height);
        }

        private void InitializeZBuffer(int width, int height)
        {
            _zBuffer = new double[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _zBuffer[i, j] = double.PositiveInfinity;
                }
            }
        }

        private double CountZCoord(float x, float y, FilledTriangle triangle)
        {
            List<Vertex> vertices = triangle.Vertices;
            Vertex p1 = vertices[0];
            Vertex p2 = vertices[1];
            Vertex p3 = vertices[2];

            var A = Matrix<float>.Build.DenseOfArray(new float[,] {
                { p1.X, p2.X, p3.X },
                { p1.Y, p2.Y, p3.Y },
                { 1, 1, 1 }
            });
            var b = Vector<float>.Build.Dense(new float[] { x, y, 1 });
            var X = A.Solve(b);

            float zA = triangle.p1[2] / triangle.p1[3];
            float zB = triangle.p2[2] / triangle.p2[3];
            float zC = triangle.p3[2] / triangle.p3[3];


            float z = X[0] * zA + X[1] * zB + X[2] * zC;

            return z;
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
        public void FillPolygon(FilledTriangle triangle, Color color)
        {

            List<Vertex> vertices = triangle.Vertices;
            int[] ind = Enumerable.Range(0, triangle.Vertices.Count).OrderBy(x => triangle.Vertices[x].Y).ToArray();
            int ymin = vertices[ind[0]].Y;
            int ymax = vertices[ind[vertices.Count - 1]].Y;
            int k = 0;
            List<Node> AET = new List<Node>();
            int counter = 0;
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
                            double zp = CountZCoord(j, y - 1, triangle);

                            if (zp < _zBuffer[j, y - 1])
                            {
                                _directBitmap.SetPixel(j, y - 1, color);
                                counter++;

                                _zBuffer[j, y - 1] = zp;
                            }
                            else
                            {
                                ;
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
                     _directBitmap.SetPixel(i,j, Color.White);
                }
            }
        }


    }
}
