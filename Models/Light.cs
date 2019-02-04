using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LightBase
    {
       public Vector4 LigthColor { get; set; } = new Vector4(1, 1, 1, 0);
       public Vector4 LightPosition { get; set; }

        public LightBase(Vector4 lightPosition, Vector4 ligthColor)
        {
            LightPosition = lightPosition;
            LigthColor = ligthColor;
        }

    }
}
