using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
