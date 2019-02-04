using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PointLight: LightBase
    {
        public PointLight(Vector4 lightPosition, Vector4 ligthColor) : base(lightPosition, ligthColor)
        {
        }
    }
}
