using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Scene
    {
        public List<WorldObject> WorldObjects { get; set; }
        public Camera Camera { get; set; }

        public Scene()
        {
            WorldObjects = WorldObjectsCreator.Create();
            Camera = new Camera(new Vector3( 0f, 0f, 0f ),
                new Vector3(4f, 0f, 0f),
                new Vector3(0f, 0f, 1f));
        }
    }
}
