using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SpotLight: LightBase
    {
        public Vector4 DVector { get; set; }
        public SpotLight(Vector4 lightPosition, Vector4 ligthColor,Vector4 spotPoint) : base(lightPosition, ligthColor)
        {
            DVector = Vector4.Normalize(spotPoint - lightPosition);
        }
    }
}
