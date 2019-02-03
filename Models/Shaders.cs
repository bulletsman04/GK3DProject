﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class Shaders
    {
        public static Settings Settings { get; set; }
        public static Color FragmentShader(Camera camera,Vector4 point, Vector4 normal, Vector4 IO)
        {

            Vector4 result = Vector4.Zero;
            if (Settings.IsPhong == true)
            {
            result = CalculatePhong(camera, point, normal, IO);
            }
            else if(Settings.IsGouraud == true)
            {
                result = IO;
            }

            return Color.FromArgb((byte)Math.Round(255 * result.X), (byte)Math.Round(255 * result.Y), (byte)Math.Round(255 * result.Z));
        }

        public static Vector4 CalculatePhong(Camera camera, Vector4 point, Vector4 normal, Vector4 IO)
        {
            Vector4 result = new Vector4();
            Vector4 nLight;
            Vector4 ligthColor = new Vector4(1, 1, 1, 0);
            foreach (var light in Settings.Lights)
            {
                nLight = Vector4.Normalize(new Vector4(light.X - point.X, light.Y - point.Y, (light.Z - point.Z), 0));
                float cosVR = 0;

                Vector4 V = Vector4.Normalize(new Vector4(camera.CPos.X - point.X, camera.CPos.Y - point.Y, (camera.CPos.Z - point.Z), 0));

                Vector4 RV = Vector4.Normalize(2 * (normal.X * nLight.X + normal.Y * nLight.Y + normal.Z * nLight.Z) * normal - nLight);

                cosVR = (float)Math.Pow(Math.Max(V.X * RV.X + V.Y * RV.Y + V.Z * RV.Z, 0),Settings.MPhong);
             

                float cosLN = Math.Max(normal.X * nLight.X + normal.Y * nLight.Y + normal.Z * nLight.Z, 0);
                float R = ligthColor.X * (Settings.LambertRate * IO.X * cosLN + Settings.PhongRate * cosVR);
                float G = ligthColor.Y * (Settings.LambertRate * IO.Y * cosLN + Settings.PhongRate * cosVR);
                float B = ligthColor.Z * (Settings.LambertRate * IO.Z * cosLN + Settings.PhongRate * cosVR);

                result += new Vector4(R, G, B, 0);

            }

            if (result.X < 0) result.X = 0;
            if (result.X > 1) result.X = 1;
            if (result.Y < 0) result.Y = 0;
            if (result.Y > 1) result.Y = 1;
            if (result.Z < 0) result.Z = 0;
            if (result.Z > 1) result.Z = 1;

            return result;
        }
    }
}
