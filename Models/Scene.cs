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
        public List<Vector4> LightCources;

        public Scene()
        {
            WorldObjects = WorldObjectsCreator.Create();
            Camera = new Camera(new Vector3( 0f, 0f, 0f ),
                //ToDo: problem with 0,0,-10
                new Vector3(3f,0f, -2f),
                new Vector3(0f, 0f, 1f));
            LightCources = new List<Vector4>();
            LightCources.Add(new Vector4(0,0,4f,0));
        }
    }
}
