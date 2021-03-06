﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

public enum ObjectType
{
    Static,
    Moving
}
namespace Models
{
    public class WorldObject
    {
        public LocalObject LocalObject { get; set; }
        public Matrix4x4 ModelMatrix { get; set; }
        public Vector3 Rotation { get; set; } 
        public Vector3 Translation { get; set; }
        public Camera MovingCamera { get; set; }
        public Camera ObservingCamera { get; set; }
        public SpotLight Light1 { get; set; }
        public SpotLight Light2 { get; set; }

        public ObjectType Type { get; set; }

        public bool IsMovingCameraSet { get; set; } = false;
        public bool IsObservingCameraSet { get; set; } = false;

        public float CameraXOffset { get; set; }


        public WorldObject(LocalObject localObject, Matrix4x4 modelMatrix)
        {
            LocalObject = localObject;
            ModelMatrix = modelMatrix;
            Rotation = Translation = Vector3.Zero;
            Type = ObjectType.Static;
            Light1 = new SpotLight(Vector4.One, TypesConverters.VectorColorFromRGB(255,255,0),Vector4.One );
            Light2 = new SpotLight(Vector4.One, TypesConverters.VectorColorFromRGB(255, 0, 0), Vector4.One);


        }

        public Action UpdateRotation { get; set; }
        public Action UpdateTranslation{ get; set; }


        public void Update()
        {

            if(Type == ObjectType.Static)
                return;

            ModelMatrix = Matrix4x4.Identity;
            

            if (Translation!=Vector3.Zero)
            {
                UpdateTranslation?.Invoke();
                ModelMatrix *= new Matrix4x4(
                    1, 0, 0, Translation.X,
                    0, 1, 0, Translation.Y,
                    0, 0, 1, Translation.Z,
                    0, 0, 0, 1
                );
            }
            if (Rotation.Y != 0)
            {
                ModelMatrix *= new Matrix4x4(
                    (float)Math.Cos(Rotation.Y), 0, (float)Math.Sin(Rotation.Y), 0,
                    0, 1, 0, 0,
                    -(float)Math.Sin(Rotation.Y), 0, (float)Math.Cos(Rotation.Y), 0,
                    0, 0, 0, 1
                );
            }
            if (Rotation.Z != 0)
            {
                ModelMatrix *= new Matrix4x4(
                    (float)Math.Cos(Rotation.Z), -(float)Math.Sin(Rotation.Z), 0, 0,
                    (float)Math.Sin(Rotation.Z), (float)Math.Cos(Rotation.Z), 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1
                );
            }
          
            if (Rotation.X != 0)
            {
                ModelMatrix *= new Matrix4x4(
                    1,0,0,0,
                    0,(float)Math.Cos(Rotation.X), -(float)Math.Sin(Rotation.X), 0,
                    0,(float)Math.Sin(Rotation.X), (float)Math.Cos(Rotation.X), 0,
                    0, 0, 0, 1
                );
            }

            if (IsMovingCameraSet)
            {
                MovingCamera.CPos = new Vector3(Translation.X + CameraXOffset, Translation.Y +1.1f, Translation.Z);
                MovingCamera.CTarget = Translation;
            }


            if (IsObservingCameraSet)
            {
                ObservingCamera.CTarget = Translation;
            }

            Light1.LightPosition = new Vector4(Translation.X,Translation.Y,Translation.Z,0);
            Light1.DVector = Vector4.Normalize(new Vector4(Translation.X, Translation.Y - 1f, Translation.Z, 0) - Light1.LightPosition);
            Light2.LightPosition = new Vector4(Translation.X, Translation.Y, Translation.Z, 0);
            Light2.DVector = Vector4.Normalize(new Vector4(Translation.X, Translation.Y + 1f, Translation.Z, 0) - Light1.LightPosition);
        }

        
    }
}
