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
    public class WorldObject
    {
        public LocalObject LocalObject { get; set; }
        public Matrix4x4 ModelMatrix { get; set; }
        public Vector3 Rotation { get; set; }

        public WorldObject(LocalObject localObject, Matrix4x4 modelMatrix)
        {
            LocalObject = localObject;
            ModelMatrix = modelMatrix;
            Rotation = new Vector3();
        }

        public Action UpdateRotation;

        public void Update()
        {
            if(UpdateRotation!=null)
                 UpdateRotation();
            else
            {
               return;
            }
             ModelMatrix = new Matrix4x4(
            (float) Math.Cos(Rotation.Z),0, (float)Math.Sin(Rotation.Z), 0,
             0,1, 0,0,
            (float)-Math.Sin(Rotation.Z), 0,  (float)Math.Cos(Rotation.Z), 0,
             0, 0, 0, 1
                 );
        }
    }
}
