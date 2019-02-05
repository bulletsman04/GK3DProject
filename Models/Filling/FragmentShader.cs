using System;
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
            var result = Settings.IsPhong == true ? CalculatePhong(camera, point, normal, IO) : IO;

            return Color.FromArgb((byte)Math.Round(255 * result.X), (byte)Math.Round(255 * result.Y), (byte)Math.Round(255 * result.Z));
        }

        public static Vector4 CalculatePhong(Camera camera, Vector4 point, Vector4 normal, Vector4 IO)
        {
            Vector4 result = new Vector4();
            Vector4 lightColor = Vector4.One;
            foreach (var light in Settings.Lights)
            {
                var nLight = Vector4.Normalize(light.LightPosition - point);

                lightColor = GetLightColor(light, nLight);

                float cosVR = GetPhongAngel(camera, point, normal, nLight);

                float cosLN = Math.Max(normal.X * nLight.X + normal.Y * nLight.Y + normal.Z * nLight.Z, 0);
                float R = lightColor.X * (Settings.LambertRate * IO.X * cosLN + Settings.PhongRate * cosVR);
                float G = lightColor.Y * (Settings.LambertRate * IO.Y * cosLN + Settings.PhongRate * cosVR);
                float B = lightColor.Z * (Settings.LambertRate * IO.Z * cosLN + Settings.PhongRate * cosVR);

                result += new Vector4(R, G, B, 0);

            }

            result += new Vector4(Settings.Ambient, Settings.Ambient, Settings.Ambient,0);

            if (result.X < 0) result.X = 0;
            if (result.X > 1) result.X = 1;
            if (result.Y < 0) result.Y = 0;
            if (result.Y > 1) result.Y = 1;
            if (result.Z < 0) result.Z = 0;
            if (result.Z > 1) result.Z = 1;

            return result;
        }

        private static float GetPhongAngel(Camera camera, Vector4 point, Vector4 normal, Vector4 nLight)
        {
            Vector4 V = Vector4.Normalize(new Vector4(camera.CPos.X - point.X, camera.CPos.Y - point.Y, (camera.CPos.Z - point.Z), 0));

            Vector4 RV = Vector4.Normalize(2 * (Vector4.Dot(normal,nLight)) * normal - nLight);

            var cosVR = (float)Math.Pow(Math.Max(Vector4.Dot(V, RV), 0), Settings.MPhong);
            return cosVR;
        }

        private static Vector4 GetLightColor(LightBase light, Vector4 nLight)
        {
            Vector4 lightColor;
            if (light is SpotLight spotLight)
            {
                int factor = 15;
                float cosDL = (float)Math.Pow(Math.Max(Vector4.Dot(-spotLight.DVector,nLight), 0), factor);
                lightColor = spotLight.LigthColor * cosDL;
            }
            else
            {
                lightColor = light.LigthColor * Settings.DayFactor;
            }

            return lightColor;
        }
    }
}
