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

            // cone1
            LocalObject cone1 = LocalObjectsCreator.CreateCone("cone1");

            Matrix<float> cone1Model = MathNetHelper.M.DenseOfArray(new float[4, 4]
            {
                {1,0,0,0},
                {0,1, 0, 0},
                {0, 0, 1,2f},
                {0, 0, 0, 1}
            });

            WorldObject cone1W = new WorldObject(cone1,cone1Model);

            worldObjects.Add(cone1W);

            // cube1

            LocalObject cube1 = LocalObjectsCreator.CreateCube("cube1");

            Matrix<float> cube1Model = MathNetHelper.M.DenseOfArray(new float[4, 4]
            {
                {1,0,0,0},
                {0,1, 0, 0},
                {0, 0, 1,0},
                {0, 0, 0, 1}
            });

            WorldObject cube1W = new WorldObject(cube1, cube1Model);

            worldObjects.Add(cube1W);

            // sphere1

            LocalObject sphere1 = LocalObjectsCreator.CreateSphere("sphere1");

            Matrix<float> sphere1Model = MathNetHelper.M.DenseOfArray(new float[4, 4]
            {
                {1,0,0,0},
                {0,1, 0, -3f},
                {0, 0, 1,0},
                {0, 0, 0, 1}
            });

            WorldObject sphere1W = new WorldObject(sphere1, sphere1Model);

            worldObjects.Add(sphere1W);


            return worldObjects;
        }




        
    }
}
