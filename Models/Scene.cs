using System;
using System.Collections.Generic;
using System.Linq;
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
            Camera = new Camera(MathNetHelper.V.DenseOfArray(new [] { 0.5f, 0.5f, 0.5f }),
                MathNetHelper.V.DenseOfArray(new[] { 3f, 0.5f, 0.5f }),
                MathNetHelper.V.DenseOfArray(new[] { 0, 0, 1f }));
        }
    }
}
