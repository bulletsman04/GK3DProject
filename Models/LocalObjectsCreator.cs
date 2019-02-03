﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace Models
{
    public static class LocalObjectsCreator
    {
        //public static LocalObject CreateCone(string name,float width2, float height2)
        //{
        //    Mesh coneMesh = new Mesh()
        //    {
        //        Name = name,
        //        Vertices = new[]
        //        {
        //           new Vector4(-width2, -width2, height2, 1),
        //            new Vector4(-width2, width2, height2, 1),
        //            new Vector4(width2, -width2, height2, 1),
        //            new Vector4(width2, width2, height2, 1),
        //            new Vector4( 0, 0, -height2, 1)
        //        },
        //        Triangles = new[]
        //        {
        //            new Triangle(0,2,1),
        //            new Triangle(1,2,3),
        //            new Triangle(4,2,3),
        //            new Triangle(4,3,1),
        //            new Triangle(4,1,0),
        //            new Triangle(4,0,2)
        //        }
        //    };

        //    return new LocalObject(coneMesh);
        //}

        public static LocalObject CreateCube(string name, float width2)
        {
            Mesh coneMesh = new Mesh()
            {
                Name = name,
                Vertices = new[]
                {
                    // 1
                   new NVertex(  new Vector4(-width2, -width2, width2, 1),new Vector4(0,0,1,0) ),
                    new NVertex(  new Vector4(-width2, -width2, width2, 1),new Vector4(-1,0,0,0) ),
                    new NVertex(  new Vector4(-width2, -width2, width2, 1),new Vector4(0,-1,0,0) ),
                    // 2
                    new NVertex(  new Vector4(width2, -width2, width2, 1),new Vector4(0,0,1,0) ),
                    new NVertex(  new Vector4(width2, -width2, width2, 1),new Vector4(1,0,0,0) ),
                    new NVertex(  new Vector4(width2, -width2, width2, 1),new Vector4(0,-1,0,0) ),

                    // 3
                    new NVertex(  new Vector4(-width2, width2, width2, 1),new Vector4(0,0,1,0) ),
                    new NVertex(  new Vector4(-width2, width2, width2, 1),new Vector4(-1,0,0,0) ),
                    new NVertex(  new Vector4(-width2, width2, width2, 1),new Vector4(0,1,0,0) ),
                    
                    // 4
                    new NVertex(  new Vector4(width2, width2, width2, 1),new Vector4(0,0,1,0) ),
                    new NVertex(  new Vector4(width2, width2, width2, 1),new Vector4(1,0,0,0) ),
                    new NVertex(  new Vector4(width2, width2, width2, 1),new Vector4(0,1,0,0) ),

                    // 5
                    new NVertex(  new Vector4(-width2, -width2, -width2, 1),new Vector4(0,0,-1,0) ),
                    new NVertex(  new Vector4(-width2, -width2, -width2, 1),new Vector4(-1,0,0,0) ),
                    new NVertex(  new Vector4(-width2, -width2, -width2, 1),new Vector4(0,-1,0,0) ),

                    // 6
                    new NVertex(  new Vector4(width2, -width2, -width2, 1),new Vector4(0,0,-1,0) ),
                    new NVertex(  new Vector4(width2, -width2, -width2, 1),new Vector4(1,0,0,0) ),
                    new NVertex(  new Vector4(width2, -width2, -width2, 1),new Vector4(0,-1,0,0) ),

                    // 7
                    new NVertex(  new Vector4(-width2, width2, -width2, 1),new Vector4(0,0,-1,0) ),
                    new NVertex(  new Vector4(-width2, width2, -width2, 1),new Vector4(-1,0,0,0) ),
                    new NVertex(  new Vector4(-width2, width2, -width2, 1),new Vector4(0,1,0,0) ),

                    // 8
                    new NVertex(  new Vector4(width2, width2, -width2, 1),new Vector4(0,0,-1,0) ),
                    new NVertex(  new Vector4(width2, width2, -width2, 1),new Vector4(1,0,0,0) ),
                    new NVertex(  new Vector4(width2, width2, -width2, 1),new Vector4(0,1,0,0) )

                },
                Triangles = new[]
                {
                    new Triangle(0,3,6),
                    new Triangle(3,9,6),

                    new Triangle(2,17,5),
                    new Triangle(14,17,2),

                    new Triangle(16,4,10),
                    new Triangle(10,22,16),

                    new Triangle(20,23,11),
                    new Triangle(11,8,20),

                    new Triangle(13,19,7),
                    new Triangle(7,1,13),
                    new Triangle(21,18,12),
                    new Triangle(12,15,21)
                    
                }
            };

            return new LocalObject(coneMesh);
        }

        public static LocalObject CreateCuboid(string name, float width2, float height2, float depth2)
        {
            Mesh cuboidMesh = new Mesh()
            {
                Name = name,
               Vertices = new[]
                {
                    // 1
                   new NVertex(  new Vector4(-height2, -width2, depth2, 1),new Vector4(0,0,1,0) ),
                    new NVertex(  new Vector4(-height2, -width2, depth2, 1),new Vector4(-1,0,0,0) ),
                    new NVertex(  new Vector4(-height2, -width2, depth2, 1),new Vector4(0,-1,0,0) ),
                    // 2
                    new NVertex(  new Vector4(height2, -width2, depth2, 1),new Vector4(0,0,1,0) ),
                    new NVertex(  new Vector4(height2, -width2, depth2, 1),new Vector4(1,0,0,0) ),
                    new NVertex(  new Vector4(height2, -width2, depth2, 1),new Vector4(0,-1,0,0) ),

                    // 3
                    new NVertex(  new Vector4(-height2, width2, depth2, 1),new Vector4(0,0,1,0) ),
                    new NVertex(  new Vector4(-height2, width2, depth2, 1),new Vector4(-1,0,0,0) ),
                    new NVertex(  new Vector4(-height2, width2, depth2, 1),new Vector4(0,1,0,0) ),
                    
                    // 4
                    new NVertex(  new Vector4(height2, width2, depth2, 1),new Vector4(0,0,1,0) ),
                    new NVertex(  new Vector4(height2, width2, depth2, 1),new Vector4(1,0,0,0) ),
                    new NVertex(  new Vector4(height2, width2, depth2, 1),new Vector4(0,1,0,0) ),

                    // 5
                    new NVertex(  new Vector4(-height2, -width2, -depth2, 1),new Vector4(0,0,-1,0) ),
                    new NVertex(  new Vector4(-height2, -width2, -depth2, 1),new Vector4(-1,0,0,0) ),
                    new NVertex(  new Vector4(-height2, -width2, -depth2, 1),new Vector4(0,-1,0,0) ),

                    // 6
                    new NVertex(  new Vector4(height2, -width2, -depth2, 1),new Vector4(0,0,-1,0) ),
                    new NVertex(  new Vector4(height2, -width2, -depth2, 1),new Vector4(1,0,0,0) ),
                    new NVertex(  new Vector4(height2, -width2, -depth2, 1),new Vector4(0,-1,0,0) ),

                    // 7
                    new NVertex(  new Vector4(-height2, width2, -depth2, 1),new Vector4(0,0,-1,0) ),
                    new NVertex(  new Vector4(-height2, width2, -depth2, 1),new Vector4(-1,0,0,0) ),
                    new NVertex(  new Vector4(-height2, width2, -depth2, 1),new Vector4(0,1,0,0) ),

                    // 8
                    new NVertex(  new Vector4(height2, width2, -depth2, 1),new Vector4(0,0,-1,0) ),
                    new NVertex(  new Vector4(height2, width2, -depth2, 1),new Vector4(1,0,0,0) ),
                    new NVertex(  new Vector4(height2, width2, -depth2, 1),new Vector4(0,1,0,0) )

                },
                Triangles = new[]
                {
                    new Triangle(0,3,6),
                    new Triangle(3,9,6),

                    new Triangle(2,17,5),
                    new Triangle(14,17,2),

                    new Triangle(16,4,10),
                    new Triangle(10,22,16),

                    new Triangle(20,23,11),
                    new Triangle(11,8,20),

                    new Triangle(13,19,7),
                    new Triangle(7,1,13),

                    new Triangle(21,15,12),
                    new Triangle(12,18,21)
                }
            };

            return new LocalObject(cuboidMesh);
        }

        public static LocalObject CreateSphere(string name, float radius)
        {
            int m = 30;
            int n =30;
            int mn = m * n;
            float r = radius;

            NVertex[] vertices = new NVertex[mn + 2];
            vertices[0] = new NVertex( new Vector4 (0, r, 0, 1 ),new Vector4(0,1,0,0));
            vertices[mn + 1] = new NVertex(new Vector4(0, -r, 0, 1), new Vector4(0, -1, 0, 0));
            float maxz = 0;
            float minz = int.MaxValue;
            // diff i=0...m-1
            for (int i = 0; i < m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                   
                    float x = (float)(r * Math.Cos((float)(2 * Math.PI * (j - 1) / n)) * Math.Sin((float)Math.PI * (i + 1) / (m + 1)));
                    // i not j
                    float y = (float)(r * Math.Cos((float)Math.PI * (i + 1) / (m + 1)));
                    float z = (float)(r * Math.Sin((float)(2 * Math.PI * (j - 1) / n)) * Math.Sin((float)Math.PI * (i + 1) / (m + 1)));
                    float w = 1f;

                    if (z > maxz)
                    {
                        maxz = z;
                    }

                    if (z < minz)
                    {
                        minz = z;
                    }


                    vertices[i * n + j] = new NVertex(new Vector4(x, y, -z, w), new Vector4(x/r, y/r, -z/r, 0)); 
                }
            }

            Triangle[] triangles = new Triangle[2 * mn];

            triangles[n - 1] = new Triangle(0, 1, n);
            // diff
            triangles[2 * mn - 1] = new Triangle(mn + 1, mn, mn - n + 1);
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
                triangles[(2 * m - 1) * n + i] = new Triangle(mn + 1, (m - 1) * n + i + 1, (m - 1) * n + i + 2);
            }

            for (int i = 0; i <= m - 2; i++)
            {
                for (int j = 1; j <= n; j++)
                {

                    if (triangles[(2 * i + 1) * n + j - 1] != null)
                    {
                        ;
                    }
                    if (triangles[(2 * i + 2) * n + j - 1] != null)
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
                    c += 2;

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
