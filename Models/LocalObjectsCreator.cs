﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace Models
{
    public static class LocalObjectsCreator
    {
        public static LocalObject CreateCone(string name)
        {
            Mesh coneMesh = new Mesh()
            {
                Name = name,
                Vertices = new[]
                {
                    MathNetHelper.V.DenseOfArray(new float[] {-1, -1, -1f, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {-1, 1, -1f, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {1, -1, -1f, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {1, 1, -1f, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] { 0, 0, 1f, 1})
                },
                Triangles = new[]
                {
                    new Triangle(0,2,1),
                    new Triangle(1,2,3),
                    new Triangle(4,2,3),
                    new Triangle(4,3,1),
                    new Triangle(4,1,0),
                    new Triangle(4,0,2)
                }
            };

            return new LocalObject(coneMesh);
        }

        public static LocalObject CreateCube(string name)
        {
            Mesh coneMesh = new Mesh()
            {
                Name = name,
                Vertices = new[]
                {
                    MathNetHelper.V.DenseOfArray(new float[] {-1, -1, -1f, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {1, -1, -1f, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {-1, 1f, -1f, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {1, 1, -1f, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {1, -1, 1f, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {-1, -1, 1f, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {-1, 1f, 1f, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {1, 1, 1f, 1}),
                },
                Triangles = new[]
                {
                    new Triangle(0,1,2),
                    new Triangle(1,3,2),
                    new Triangle(4,1,3),
                    new Triangle(7,4,3),
                    new Triangle(7,3,2),
                    new Triangle(6,7,2),
                    new Triangle(6,2,0),
                    new Triangle(5,6,0),
                    new Triangle(4,0,1),
                    new Triangle(5,0,4),
                    new Triangle(5,4,7),
                    new Triangle(5,7,6)
                }
            };

            return new LocalObject(coneMesh);
        }

        public static LocalObject CreateSphere(string name)
        {
            int m = 80;
            int n = 40;
            int mn = m * n;
            float r = 1;

            Vector<float>[] vertices = new Vector<float>[mn+2];
            vertices[0] = MathNetHelper.V.DenseOfArray(new float[] {0, r, 0, 1});
            vertices[mn+1] = MathNetHelper.V.DenseOfArray(new float[] { 0, -r, 0, 1 });

            // diff i=0...m-1
            for (int i = 0; i < m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    //float x = (float) (r * Math.Cos(DegreesToRadians((float) (2 * Math.PI * (j - 1) / n)))*Math.Sin(DegreesToRadians((float) Math.PI * i / (m+1))));
                    //float y = (float)(r*Math.Cos(DegreesToRadians((float)Math.PI * j / (m + 1))));
                    //float z = (float)(r * Math.Sin(DegreesToRadians((float)(2 * Math.PI * (j - 1) / n))) * Math.Sin(DegreesToRadians((float)Math.PI * i / (m + 1))));
                    //float w = 1f;

                    float x = (float)(r * Math.Cos((float)(2 * Math.PI * (j - 1) / n)) * Math.Sin((float)Math.PI * i / (m + 1)));
                    // i not j
                    float y = (float)(r * Math.Cos((float)Math.PI * i / (m + 1)));
                    float z = (float)(r * Math.Sin((float)(2 * Math.PI * (j - 1) / n)) * Math.Sin((float)Math.PI * i / (m + 1)));
                    float w = 1f;

                    vertices[i*n + j] = MathNetHelper.V.DenseOfArray(new float[] { x, y, z, w });
                }
            }

            Triangle[] triangles = new Triangle[2*mn];

            triangles[n-1] = new Triangle(0,1,n);
            // diff
            triangles[2*mn  - 1] = new Triangle(mn+1,mn,mn-n+1);
            int c = 2;
            for (int i = 0; i <= n - 2; i++)
            {
                triangles[i] = new Triangle(0, i + 2, i + 1);
                c++;
            }

            for (int i = 0; i <= n - 2; i++)
            {
                c++;
                // diff
                triangles[ (2*m - 1) * n + i] = new Triangle(mn + 1, (m - 1) * n + i + 1, (m - 1) * n + i + 2);
            }

            for (int i = 0; i <= m - 2; i++)
            {
                for (int j = 1; j <= n; j++)
                {

                    if (triangles[(2 * i + 1) * n + j - 1] != null)
                    {
                        ;
                    }
                    if (triangles[(2 * i + 2) * n + j-1] != null)
                    {
                        ;
                    }
                    if (j == n)
                    {

                        triangles[(2 * i + 1) * n + j - 1] = new Triangle((i + 1) * n, i * n + 1, (i + 1) * n + 1);
                        triangles[(2 * i + 2) * n + j - 1] = new Triangle((i + 1) * n, (i + 1) * n + 1, (i + 2) * n);
                    }
                    else
                    {
                        triangles[(2 * i + 1) * n + j - 1] = new Triangle(i * n + j, i * n + j + 1, (i + 1) * n + j + 1);
                        triangles[(2 * i + 2) * n + j - 1] = new Triangle(i * n + j, (i + 1) * n + j + 1, (i + 1) * n + j);
                    }
                    c+=2;

                }
            }

            Mesh cubeMesh = new Mesh()
            {
                Name = name,
                Vertices = vertices,
                Triangles = triangles
            };

            return new LocalObject(cubeMesh);
        }

        private static float DegreesToRadians(float degrees) => (float)(degrees * Math.PI / 180);
    }
}
