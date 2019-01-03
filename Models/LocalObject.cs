using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LocalObject
    {
        // ToDo: Probably list of meshes
        public Mesh Mesh { get; set; }

        public LocalObject(Mesh mesh)
        {
            Mesh = mesh;
        }
    }
}
