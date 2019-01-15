using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using SystemColors = System.Windows.SystemColors;

namespace Models
{
    public static class WorldObjectsCreator
    {

        public static List<WorldObject> Create()
        {
            List<WorldObject> worldObjects = new List<WorldObject>();
            Random r = new Random();
            var colors = new Color[9]
            {
                Color.Purple, Color.Green, Color.Orange, Color.Brown, Color.Blue,Color.Gold,Color.AliceBlue,Color.CornflowerBlue, Color.DarkBlue
            };
            
            // cone1
            LocalObject cone1 = LocalObjectsCreator.CreateCone("cone1");

            Matrix<float> cone1Model = MathNetHelper.M.DenseOfArray(new float[4, 4]
            {
                {1,0,0,0},
                {0,1, 0, 0},
                {0, 0, 1,0},
                {0, 0, 0, 1}
            });

            WorldObject cone1W = new WorldObject(cone1,cone1Model);

            foreach (var meshTriangle in cone1W.LocalObject.Mesh.Triangles)
            {
                
                meshTriangle.Color = Color.Green;
                
            }
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

            //worldObjects.Add(cube1W);


            Matrix<float> cube2Model = MathNetHelper.M.DenseOfArray(new float[4, 4]
            {
                {1,0,0,0},
                {0,1, 0, 3f},
                {0, 0, 1,0},
                {0, 0, 0, 1}
            });

            WorldObject cube2W = new WorldObject(cube1, cube2Model);

           // worldObjects.Add(cube2W);

            // sphere1

            LocalObject sphere1 = LocalObjectsCreator.CreateSphere("sphere1");

            Matrix<float> sphere1Model = MathNetHelper.M.DenseOfArray(new float[4, 4]
            {
                {1,0,0,0f},
                {0,1, 0,3f},
                {0, 0, 1,0},
                {0, 0, 0, 1}
            });

            WorldObject sphere1W = new WorldObject(sphere1, sphere1Model);
            
            foreach (var meshTriangle in sphere1W.LocalObject.Mesh.Triangles)
            {

                 meshTriangle.Color = colors[r.Next(9)];
               // meshTriangle.Color = Color.CornflowerBlue;
            }

            worldObjects.Add(sphere1W);

            //Matrix<float> sphere2Model = MathNetHelper.M.DenseOfArray(new float[4, 4]
            //{
            //    {1,0,0,0},
            //    {0,1, 0, 0f},
            //    {0, 0, 1,0},
            //    {0, 0, 0, 1}
            //});

            //WorldObject sphere2W = new WorldObject(sphere1, sphere2Model);

            //worldObjects.Add(sphere2W);


            return worldObjects;
        }




        
    }
}
