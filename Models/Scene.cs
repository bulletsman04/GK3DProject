using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MvvmFoundation.Wpf;

namespace Models
{
    public class Scene
    {
        public List<WorldObject> WorldObjects { get; set; }
        public WorldObject MovingObject { get; set; }
        public Camera Camera { get; set; }
        public Camera StaticCamera { get; set; }

        public List<Vector4> LightCources;
        public Settings Settings { get; set; }
        public PropertyObserver<Settings> SettingsObserver { get; set; }

        public Scene(Settings settings)
        {
            Settings = settings;
            WorldObjects = WorldObjectsCreator.Create();
            FindMovingObject();
            StaticCamera = new Camera(new Vector3( 0f, 0, 0 ),
                //ToDo: problem with 0,0,-10
                //new Vector3(0f,-1f, -1.5f), - cant see sphere on that
                new Vector3(5f,5f, -4f),
                new Vector3(0f, 0f, 1f));
            Camera = StaticCamera;
            LightCources = new List<Vector4>();
            LightCources.Add(new Vector4(0,0,4f,0));
            RegisterPropertiesChanged();
        }

        private void FindMovingObject()
        {
            foreach (WorldObject worldObject in WorldObjects)
            {
                if (worldObject.Camera != null)
                {
                    MovingObject = worldObject;
                    return;
                    
                }
            }
        }

        private void RegisterPropertiesChanged()
        {
            SettingsObserver = new PropertyObserver<Settings>(Settings)
                .RegisterHandler(n => n.IsMoving, ListChangedHandler);

        }

        private void ListChangedHandler(Settings s)
        {
            Camera = MovingObject.Camera;
            MovingObject.IsCameraSet = true;
        }
    }
}
