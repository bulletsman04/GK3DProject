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
            Camera = new Camera(MathNetHelper.V.DenseOfArray(new [] { 0f, 0f, 0f }),
                MathNetHelper.V.DenseOfArray(new[] { 10f, -2f, 2f }),
                MathNetHelper.V.DenseOfArray(new[] { 0, 0, 1f }));
        }
    }
}
