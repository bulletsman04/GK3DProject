﻿using System;
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
        
        public Settings Settings { get; set; }
        public PropertyObserver<Settings> SettingsObserver { get; set; }

        public Scene(Settings settings)
        {
            Settings = settings;
            WorldObjects = WorldObjectsCreator.Create();
            FindMovingObject();
            StaticCamera = new Camera(
                new Vector3( 0f,0, 0 ),
                new Vector3(0,-7f, -6f),
                new Vector3(0f, 0f, 1f));
            Camera = StaticCamera;
            RegisterPropertiesChanged();
        }

        private void FindMovingObject()
        {
            foreach (WorldObject worldObject in WorldObjects)
            {
                if (worldObject.MovingCamera != null)
                {
                    MovingObject = worldObject;
                    Settings.Lights.Add(MovingObject.Light1);
                    Settings.Lights.Add(MovingObject.Light2);
                    return;
                }
            }
        }

        private void RegisterPropertiesChanged()
        {
            SettingsObserver = new PropertyObserver<Settings>(Settings)
                .RegisterHandler(n => n.IsMoving, MovingCameraHandler)
                .RegisterHandler(n => n.IsStatic, StaticCameraHandler)
                .RegisterHandler(n => n.IsObserving, ObservingCameraHandler);

        }

        private void StaticCameraHandler(Settings s)
        {
            if(!Settings.IsStatic)
                return;

            Camera = StaticCamera;
            MovingObject.IsMovingCameraSet = false;
            MovingObject.IsObservingCameraSet = false;

        }

        private void ObservingCameraHandler(Settings s)
        {
            if (!Settings.IsObserving)
                return;
            Camera = MovingObject.ObservingCamera;
            MovingObject.IsMovingCameraSet = false;
            MovingObject.IsObservingCameraSet = true;
        }

        private void MovingCameraHandler(Settings s)
        {
            if (!Settings.IsMoving)
                return;
            Camera = MovingObject.MovingCamera;
            MovingObject.IsMovingCameraSet = true;
            MovingObject.IsObservingCameraSet = false;
        }
    }
}
