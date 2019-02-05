using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using SystemColors = System.Windows.SystemColors;
using System.Numerics;

namespace Models
{
    public static class WorldObjectsCreator
    {
        private static WorldObject ShootBall;
        private static void CreateSceneBase(List<WorldObject> worldObjects)
        {

            LocalObject cuboid1 = LocalObjectsCreator.CreateCuboid("cuboid1", 4, 3, 0.3f);


            Matrix4x4 cuboid1Model = new Matrix4x4(
                1, 0, 0, 0,
                0, 1, 0, 0f,
                0, 0, 1, 0,
                0, 0, 0, 1
            );

            WorldObject cuboid1W = new WorldObject(cuboid1, cuboid1Model);

            foreach (var meshTriangle in cuboid1W.LocalObject.Mesh.Triangles)
            {

                meshTriangle.Color = TypesConverters.VectorColorFromRGB(23, 104, 23);

            }

            worldObjects.Add(cuboid1W);
        }
        private static void CreateBases(List<WorldObject> worldObjects)
        {
            // cube1
            float basesWidth = 0.4f;
            float zOffset = -0.7f;
            LocalObject cube1 = LocalObjectsCreator.CreateCube("cube1", basesWidth);


            Matrix4x4 cube1Model = new Matrix4x4(
                1, 0, 0, 1.5f,
                0, 1, 0, -2f,
                0, 0, 1, zOffset,
                0, 0, 0, 1
            );

            WorldObject cube1W = new WorldObject(cube1, cube1Model);
            
            foreach (var meshTriangle in cube1W.LocalObject.Mesh.Triangles)
            {

                meshTriangle.Color = TypesConverters.VectorColorFromRGB(127, 127, 127);

            }

            worldObjects.Add(cube1W);


            // cube2
            LocalObject cube2 = LocalObjectsCreator.CreateCube("cube2", basesWidth);


            Matrix4x4 cube2Model = new Matrix4x4(
                1, 0, 0, 0f,
                0, 1, 0, -2f,
                0, 0, 1, zOffset,
                0, 0, 0, 1
            );

            WorldObject cube2W = new WorldObject(cube2, cube2Model);

            foreach (var meshTriangle in cube2W.LocalObject.Mesh.Triangles)
            {

                meshTriangle.Color = TypesConverters.VectorColorFromRGB(127, 127, 127);

            }

            worldObjects.Add(cube2W);


            // cube3
            LocalObject cube3 = LocalObjectsCreator.CreateCube("cube3", basesWidth);


            Matrix4x4 cube3Model = new Matrix4x4(
                1, 0, 0, -1.5f,
                0, 1, 0, -2f,
                0, 0, 1, zOffset,
                0, 0, 0, 1
            );

            WorldObject cube3W = new WorldObject(cube3, cube3Model);

            foreach (var meshTriangle in cube3W.LocalObject.Mesh.Triangles)
            {

                meshTriangle.Color = TypesConverters.VectorColorFromRGB(127, 127, 127);

            }

            worldObjects.Add(cube3W);
        }
        private static void CreateShootObjects(List<WorldObject> worldObjects)
        {

            //sphere1
            float radius = 0.3f;
            LocalObject sphere1 = LocalObjectsCreator.CreateSphere("sphere1", radius);
            float zOffset = -(0.3f + 0.8f + 0.3f);

            Matrix4x4 sphere1Model = new Matrix4x4(
                1, 0, 0, 0f,
                0, 1, 0, -2f,
                0, 0, 1, zOffset,
                0, 0, 0, 1
            );
            
            WorldObject sphere1W = new WorldObject(sphere1, sphere1Model);
            sphere1W.Translation = new Vector3(0,-2f,zOffset);

            foreach (var meshTriangle in sphere1W.LocalObject.Mesh.Triangles)
            {
                meshTriangle.Color = TypesConverters.VectorColorFromRGB(255, 255, 0);
            }

            ShootBall = sphere1W;
            worldObjects.Add(sphere1W);

            //cylinder
            float cylinderRadius = 0.3f;
            float height = 0.6f;

            LocalObject cylinder = LocalObjectsCreator.CreateCylinder("cylinder", cylinderRadius,height);
            float zCylinderOffset = -(0.3f + 0.8f + 0.6f);

            Matrix4x4 cylinderModel = new Matrix4x4(
                1, 0, 0, -1.5f,
                0, 1, 0, -2f,
                0, 0, 1, zCylinderOffset,
                0, 0, 0, 1
            );

            WorldObject cylinderW = new WorldObject(cylinder, cylinderModel);

            foreach (var meshTriangle in cylinderW.LocalObject.Mesh.Triangles)
            {
                meshTriangle.Color = TypesConverters.VectorColorFromRGB(0, 255, 0);
            }

            worldObjects.Add(cylinderW);

            //// cone1
            LocalObject cone1 = LocalObjectsCreator.CreateCone("cone1", 0.3f, 0.4f);
            float zConeOffset = -(0.3f + 0.8f + 0.4f);
            Matrix4x4 cone1Model = new Matrix4x4(
                1, 0, 0, 1.5f,
                0, 1, 0, -2f,
                0, 0, 1, zConeOffset,
                0, 0, 0, 1
            );

            WorldObject cone1W = new WorldObject(cone1, cone1Model);

            foreach (var meshTriangle in cone1W.LocalObject.Mesh.Triangles)
            {
                var triangle = meshTriangle;
                triangle.Color = new Vector4(1,0,0,0);
            }
            worldObjects.Add(cone1W);
        }
        private static void CreateTurret(List<WorldObject> worldObjects)
        {
            // base
            float baseWidth = 0.6f;
            float zOffset = -(0.3f + 0.3f);
            LocalObject baseTurret = LocalObjectsCreator.CreateCuboid("baseTurret", baseWidth, baseWidth, 0.3f);


            Matrix4x4 baseTurretModel = new Matrix4x4(
                1, 0, 0, 0f,
                0, 1, 0, 3f,
                0, 0, 1, zOffset,
                0, 0, 0, 1
            );

            WorldObject baseTurretW = new WorldObject(baseTurret, baseTurretModel);


            foreach (var meshTriangle in baseTurretW.LocalObject.Mesh.Triangles)
            {

                meshTriangle.Color = TypesConverters.VectorColorFromRGB(100,100,100);

            }

            worldObjects.Add(baseTurretW);


            // turret
            float turretWidth = 0.3f;
            float zTurretOffset = -(0.3f + 0.3f + 0.3f + 0.5f);
            LocalObject turret = LocalObjectsCreator.CreateCuboid("turret", turretWidth, turretWidth, 0.5f);


            Matrix4x4 turretModel = new Matrix4x4(
                1, 0, 0, 0f,
                0, 1, 0, 3f,
                0, 0, 1, zTurretOffset,
                0, 0, 0, 1
            );

            WorldObject turretW = new WorldObject(turret, turretModel);
            turretW.Translation = new Vector3(0,3f,zTurretOffset);
            turretW.Rotation = new Vector3(0, 0, (float)Math.PI / 15);
            turretW.Type = ObjectType.Moving;

            foreach (var meshTriangle in turretW.LocalObject.Mesh.Triangles)
            {
                meshTriangle.Color = TypesConverters.VectorColorFromRGB(255,255,255);
            }


            // barrel
            float barrelRadius = 0.12f;
            float barrelHeight = 0.7f;

            float zBarrelOffset = -(0.3f + 0.3f + 0.3f + 0.5f + barrelRadius);
            LocalObject barrel = LocalObjectsCreator.CreateCylinder("barrel", barrelRadius, barrelHeight);


            Matrix4x4 barrelModel = new Matrix4x4(
                1, 0, 0, 0f,
                0, 0, -1, 2.7f,
                0, 1, 0, zBarrelOffset,
                0, 0, 0, 1
            );

            WorldObject barrelW = new WorldObject(barrel, barrelModel);
            barrelW.Translation = new Vector3(0,2.7f,zBarrelOffset);
            barrelW.Rotation = new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 20);
            barrelW.Type = ObjectType.Moving;


            foreach (var meshTriangle in barrelW.LocalObject.Mesh.Triangles)
            {

                meshTriangle.Color = new Vector4(1, 1, 1, 0);

            }

            

            //bullet
            float radius = 0.1f;
            LocalObject bullet = LocalObjectsCreator.CreateSphere("bullet", radius);
            float zBulletOffset = -(0.3f + 0.3f + 0.3f + 0.5f + radius);

            Matrix4x4 bulletModel = new Matrix4x4(
                1, 0, 0, 0f,
                0, 1, 0, 1.9f,
                0, 0, 1, zBulletOffset,
                0, 0, 0, 1
            );


            WorldObject bulletW = new WorldObject(bullet, bulletModel);
            bulletW.Type = ObjectType.Moving;
            bulletW.Translation = new Vector3(0, 2f, zBulletOffset);

            int counter = 0;
            float yTranslation = -0.1f;
            float xTranslation = -0.4f * yTranslation;
            bulletW.UpdateTranslation  = () =>
            {
           

                bulletW.Translation = new Vector3(bulletW.Translation.X + xTranslation, bulletW.Translation.Y + yTranslation,
                   bulletW.Translation.Z);
                bulletW.Rotation = new Vector3(0, bulletW.Rotation.Y + 0.4f, (float)Math.PI/2);
                if (!(bulletW.Translation.Y <= -2f)) return;

                switch (counter)
                {
                    case 0:
                        xTranslation = 0;
                        bulletW.CameraXOffset = 0;
                        turretW.Rotation = new Vector3(0, 0, 0);
                        barrelW.Rotation = new Vector3((float)Math.PI / 2, 0, 0);
                        bulletW.Translation = new Vector3(0, 2f, zBulletOffset);
                        break;
                    case 1:
                        ShootBall.Type = ObjectType.Moving;
                        float bounce = 0;
                        float step = -0.1f;
                        Vector3 initTranslation = ShootBall.Translation;
                        ShootBall.UpdateTranslation = () =>
                        {
                            bounce += step;
                            ShootBall.Translation += new Vector3(0, 0, step);
                            if (initTranslation.Z==ShootBall.Translation.Z)
                            {
                                ShootBall.Type = ObjectType.Static;
                                ShootBall.Translation = initTranslation;
                                return;
                            }

                            if (bounce <= -0.5f)
                            {
                                step *= -1;
                            }
                            
                          
                        };
                        xTranslation = 0.35f * yTranslation;
                        bulletW.CameraXOffset = -5f * yTranslation;
                        turretW.Rotation = new Vector3(0, 0, -(float)Math.PI /15);
                         barrelW.Rotation = new Vector3((float)Math.PI / 2, 0, -(float)Math.PI /20);
                        bulletW.Translation = new Vector3(-0.1f, 2f, zBulletOffset);
                        break;
                    case 2:
                        xTranslation = -0.35f * yTranslation;
                        turretW.Rotation = new Vector3(0, 0, (float)Math.PI / 15);
                        barrelW.Rotation = new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 20);
                        bulletW.CameraXOffset = 5f * yTranslation;
                        bulletW.Translation = new Vector3(0.1f, 2f, zBulletOffset);
                        break;

                }

               
                counter = (++counter) % 3;
            };
            bulletW.MovingCamera = new Camera(Vector3.One, Vector3.One, new Vector3(0, 0, 1));
            bulletW.ObservingCamera = new Camera( Vector3.One, new Vector3(0.1f, 0.1f, -3f), new Vector3(0, 0, 1));
            bulletW.Update();
            int colorCounter = 0;
            foreach (var meshTriangle in bulletW.LocalObject.Mesh.Triangles)
            {
                meshTriangle.Color = colorCounter <= bulletW.LocalObject.Mesh.Triangles.Length / 2 ? TypesConverters.VectorColorFromRGB(196, 143, 5) : TypesConverters.VectorColorFromRGB(165, 102, 14);

                colorCounter++;
            }

            worldObjects.Add(bulletW);
            worldObjects.Add(barrelW);
            worldObjects.Add(turretW);
        }
        private static void CreateLights(List<WorldObject> worldObjects)
        {

            //sphere1
            float radius = 0.1f;
            LocalObject sphere1 = LocalObjectsCreator.CreateSphere("light1", radius);

            Matrix4x4 sphere1Model = new Matrix4x4(
                1, 0, 0, -3.3f,
                0, 1, 0, -3f,
                0, 0, 1, -3f,
                0, 0, 0, 1
            );

            WorldObject sphere1W = new WorldObject(sphere1, sphere1Model);

            foreach (var meshTriangle in sphere1W.LocalObject.Mesh.Triangles)
            {
                meshTriangle.Color = new Vector4(1, 1, 1, 0);
            }
            worldObjects.Add(sphere1W);

            //sphere2
            LocalObject sphere2 = LocalObjectsCreator.CreateSphere("light2", radius);

            Matrix4x4 sphere2Model = new Matrix4x4(
                1, 0, 0, -3.3f,
                0, 1, 0, 3f,
                0, 0, 1, -3f,
                0, 0, 0, 1
            );

            WorldObject sphere2W = new WorldObject(sphere2, sphere2Model);

            foreach (var meshTriangle in sphere2W.LocalObject.Mesh.Triangles)
            {
                meshTriangle.Color = new Vector4(1, 1, 1, 0);
            }
            worldObjects.Add(sphere2W);



        }


        public static List<WorldObject> Create()
        {
            List<WorldObject> worldObjects = new List<WorldObject>();
            CreateSceneBase(worldObjects);
            CreateBases(worldObjects);
            CreateTurret(worldObjects);
            CreateShootObjects(worldObjects);
            CreateLights(worldObjects);
            return worldObjects;
        }




        
    }
}
