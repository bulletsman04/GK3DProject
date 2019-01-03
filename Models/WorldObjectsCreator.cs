using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

namespace Models
{
    public static class WorldObjectsCreator
    {

        public static List<WorldObject> Create()
        {
            List<WorldObject> worldObjects = new List<WorldObject>();

            // Cone 1
            Mesh cone1Mesh = new Mesh()
            {
                Name="Cone1",
                Vertices = new []
                {
                    MathNetHelper.V.DenseOfArray(new float[] {0, 0, 1, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {1, 0, 1, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {1, 1, 1, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] {0, 1, 1, 1}),
                    MathNetHelper.V.DenseOfArray(new float[] { 0.5f, 0.5f, -0.5f, 1})
                },
                Triangles = new []
                {
                    new Triangle(0,1,2),
                    new Triangle(0,3,2),
                    new Triangle(0,1,4),
                    new Triangle(0,3,4),
                    new Triangle(4,2,3),
                    new Triangle(4,2,1)
                }
            };

            LocalObject cone1 = new LocalObject(cone1Mesh);

            Matrix<float> cone1Model = MathNetHelper.M.DenseOfArray(new float[4, 4]
            {
                {1,0,0,0},
                {0,1, 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            });

            WorldObject cone1W = new WorldObject(cone1,cone1Model);

            worldObjects.Add(cone1W);


            return worldObjects;
        }
    }
}
