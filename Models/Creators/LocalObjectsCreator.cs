using System;
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
        public static LocalObject CreateCone(string name, float width2, float height2)
        {

            Vector4 p1 = new Vector4(-width2, -width2, height2, 1);
            Vector4 p2 = new Vector4(-width2, width2, height2, 1);
            Vector4 p3 = new Vector4(width2, -width2, height2, 1);
            Vector4 p4 = new Vector4(width2, width2, height2, 1);
            Vector4 p5 = new Vector4(0, 0, -height2, 1);

          
            Vector4 n1 = new Vector4(0,0,1,0);
            Vector3 cross2 = -Vector3.Cross(new Vector3(p4.X, p4.Y, p4.Z) - new Vector3(p5.X, p5.Y, p5.Z), new Vector3(p2.X, p2.Y, p2.Z) -  new Vector3(p4.X, p4.Y, p4.Z));
            Vector4 n2 = Vector4.Normalize(new Vector4(cross2.X,cross2.Y,cross2.Z,0));
            Vector3 cross3 = -Vector3.Cross(new Vector3(p2.X, p2.Y, p2.Z) - new Vector3(p5.X, p5.Y, p5.Z), new Vector3(p1.X, p1.Y, p1.Z) - new Vector3(p2.X, p2.Y, p2.Z) );
            Vector4 n3 = Vector4.Normalize(new Vector4(cross3.X, cross3.Y, cross3.Z, 0));
            Vector3 cross4 = -Vector3.Cross(new Vector3(p1.X, p1.Y, p1.Z) -  new Vector3(p5.X, p5.Y, p5.Z) , new Vector3(p3.X, p3.Y, p3.Z) - new Vector3(p1.X, p1.Y, p1.Z) );
            Vector4 n4 = Vector4.Normalize(new Vector4(cross4.X, cross4.Y, cross4.Z, 0));
            Vector3 cross5 = -Vector3.Cross(new Vector3(p3.X, p3.Y, p3.Z) - new Vector3(p5.X, p5.Y, p5.Z), new Vector3(p4.X, p4.Y, p4.Z) - new Vector3(p3.X, p3.Y, p3.Z));
            Vector4 n5 =Vector4.Normalize(new Vector4(cross5.X, cross5.Y, cross5.Z, 0));


            Mesh coneMesh = new Mesh()
            {
                Name = name,
                Vertices = new[]
                {
                    // 1
                    new NVertex(  p1,n1 ),
                    new NVertex( p1,n3),
                    new NVertex( p1,n4 ),
                    // 2
                    new NVertex(  p2,n1 ),
                    new NVertex( p2,n2),
                    new NVertex( p2,n3 ), 
                    // 4
                    new NVertex(  p4,n1 ),
                    new NVertex( p4,n5),
                    new NVertex( p4,n2 ),              
                    
                    // 3
                    new NVertex(  p3,n1 ),
                    new NVertex( p3,n5),
                    new NVertex( p3,n4 ),   
                    
                    // 5
                    new NVertex(  p5,n2 ),
                    new NVertex( p5,n3),
                    new NVertex( p5,n4 ),
                    new NVertex( p5,n5 )


                },
                Triangles = new[]
                {
                    new Triangle(0,3,6),
                    new Triangle(0,9,6),
                    new Triangle(4,8,12),
                    new Triangle(5,1,13),
                    new Triangle(2,11,14),
                    new Triangle(10,7,15)
                }
            };

            return new LocalObject(coneMesh);
        }

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

            float step = 0.5f;
            List<NVertex> vertices = new List<NVertex>() {
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

                };
            List<Triangle> triangles = new List<Triangle>()    {
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
            };
            
            Mesh cuboidMesh = new Mesh()
            {
                Name = name,
               Vertices = vertices.ToArray(),
              Triangles = triangles.ToArray()
            
            };

            return new LocalObject(cuboidMesh);
        }

        public static LocalObject CreateSphere(string name, float radius)
        {
            int m = 20;
            int n =30;
            int mn = m * n;
            float r = radius;

            NVertex[] vertices = new NVertex[mn + 2];
            vertices[0] = new NVertex( new Vector4 (0, r, 0, 1 ),new Vector4(0,1,0,0));
            vertices[mn + 1] = new NVertex(new Vector4(0, -r, 0, 1), new Vector4(0, -1, 0, 0));
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

        public static LocalObject CreateCylinder(string name, float R, float H)
        {
            float step = 0.05f;
            List<NVertex> vertices = new List<NVertex>();
            List<Triangle> triangles = new List<Triangle>();
            int verticesCount = 0;

           
            for (float t = 0; t < 1; t += step)
            {
                var n = new Vector4(0, 0, -1, 0);

                vertices.Add(new NVertex(new Vector4(0, 0, 0, 1), n));
                vertices.Add(new NVertex(FromPolarCoordinates(R, t * 2 * (float)Math.PI, 0), n));
                vertices.Add(new NVertex(FromPolarCoordinates(R, (t + step) * 2 * (float)Math.PI, 0), n));

                triangles.Add(new Triangle(verticesCount,verticesCount+1,verticesCount+2));
                verticesCount += 3;


                var p1 = FromPolarCoordinates(R, t * 2 * (float)Math.PI, 0);
                var p2 = FromPolarCoordinates(R, (t + step) * 2 * (float)Math.PI, 0);
                var p3 = FromPolarCoordinates(R, t * 2 * (float)Math.PI, H);
                var p4 = FromPolarCoordinates(R, (t + step) * 2 * (float)Math.PI, H);

                var n1 = (new Vector4(p1.X, p1.Y, 0, 0));
                var n2 = (new Vector4(p2.X, p2.Y, 0,0));
                var n3 = (new Vector4(p3.X, p3.Y, 0, 0));
                var n4 = (new Vector4(p4.X, p4.Y, 0, 0));


                vertices.Add(new NVertex(p1, n1));
                vertices.Add(new NVertex(p2, n2));
                vertices.Add(new NVertex(p3, n3));

                triangles.Add(new Triangle(verticesCount, verticesCount + 1, verticesCount + 2));
                verticesCount += 3;

                vertices.Add(new NVertex(p2, n2));
                vertices.Add(new NVertex(p4, n4));
                vertices.Add(new NVertex(p3, n3));

                triangles.Add(new Triangle(verticesCount, verticesCount + 1, verticesCount + 2));
                verticesCount += 3;

                n = new Vector4(0, 0, 1, 0);

                vertices.Add(new NVertex(new Vector4(0, 0, H, 1), n));
                vertices.Add(new NVertex(FromPolarCoordinates(R, t * 2 * (float)Math.PI, H), n));
                vertices.Add(new NVertex(FromPolarCoordinates(R, (t + step) * 2 * (float)Math.PI, H), n));

                triangles.Add(new Triangle(verticesCount, verticesCount + 1, verticesCount + 2));
                verticesCount += 3;

              
            }
            Mesh cylinderMesh = new Mesh()
            {
                Name = name,
                Vertices = vertices.ToArray(),
                Triangles = triangles.ToArray()
            };

            return new LocalObject(cylinderMesh);
        }

        private static Vector4 FromPolarCoordinates(float R, float t, float z)
        {
            return new Vector4(
                R * (float)Math.Cos(t),
                R * (float)Math.Sin(t), z, 1);
        }

        private static float DegreesToRadians(float degrees) => (float)(degrees * Math.PI / 180);
    }
}
