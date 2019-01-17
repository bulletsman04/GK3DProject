﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class Shaders
    {
        public static Color FragmentShader(Camera camera,Vector4 point, Vector4 normal, Vector4 IO, List<Vector4> lights)
        {
            Vector4 result = new Vector4();
            Vector4 nLight;
            Vector4 ligthColor = new Vector4(1,1,1,0);
            foreach (var light in lights)
            {
                //nLight = Vector4.Normalize(light - point);
                nLight = Vector4.Normalize(light);
                normal = Vector4.Normalize(normal);
                float cosVR = 0;


                //Vector4 V = Vector4.Normalize(new Vector4(camera.CPos.X, camera.CPos.Y, camera.CPos.Z, 0) - point);
                Vector4 V = Vector4.Normalize(new Vector4(0, -1, 0, 0));

                Vector4 RV = Vector4.Normalize(2 * normal - nLight);

                cosVR = (float)Math.Pow(Math.Max(V.X * RV.X + V.Y * RV.Y + V.Z * RV.Z,0), 19);

                float cosLN = normal.X * nLight.X + normal.Y * nLight.Y + normal.Z * nLight.Z;
                float R = ligthColor.X * (1 * IO.X * cosLN + 1* cosVR);
                float G = ligthColor.Y * (1 * IO.Y * cosLN + 1* cosVR);
                float B = ligthColor.Z * (1 * IO.Z * cosLN + 1 * cosVR);

              result += new Vector4(R,G,B,0);
                
            }

            if (result.X < 0) result.X = 0;
            if (result.X > 1) result.X= 1;
            if (result.Y < 0) result.Y = 0;
            if (result.Y > 1) result.Y = 1;
            if (result.Z < 0) result.Z = 0;
            if (result.Z > 1) result.Z = 1;

            return Color.FromArgb((byte)Math.Round(255 * result.X), (byte)Math.Round(255 * result.Y), (byte)Math.Round(255 * result.Z));
        }
    }
}
