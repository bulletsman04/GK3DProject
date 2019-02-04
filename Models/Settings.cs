using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MvvmFoundation.Wpf;

namespace Models
{
    public class Settings: ObservableObject
    {
        private bool _isPhong = true;
        private bool _isGouraud = false;

        private bool _isStatic = true;
        private bool _isObserving = false;
        private bool _isMoving = false;

        private float _lambertRate = 1f;
        private float _phongRate = 0.2f;
        private int _mPhong = 50;
        private float _dayFactor = 1;

        public List<LightBase> Lights { get; set; }

        public Settings()
        {
            Lights = new List<LightBase>()
            {
                new PointLight(new Vector4(-3f,3f,-3f,0),new Vector4(1,1,1,0)),
                new PointLight(new Vector4(-3f,-3f,-3f,0),new Vector4(1,1,1,0))
            };
        }

        public bool IsPhong
        {
            get => _isPhong;
            set
            {
                _isPhong = value;
                RaisePropertyChanged("IsPhong");
            }
        }

        public int MPhong
        {
            get { return _mPhong; }
            set
            {
                _mPhong = value;
                RaisePropertyChanged("MPhong");
            }
        }

        public float LambertRate
        {
            get { return _lambertRate; }
            set
            {
                _lambertRate = value;
                RaisePropertyChanged("LambertRate");
            }
        }

        public float PhongRate
        {
            get { return _phongRate; }
            set
            {
                _phongRate = value;
                RaisePropertyChanged("PhongRate");
            }
        }

        public bool IsGouraud
        {
            get => _isGouraud;
            set
            {
                _isGouraud = value;
                RaisePropertyChanged("IsGouraud");
            }
        }

        public bool IsStatic
        {
            get => _isStatic;
            set
            {
                _isStatic = value;
                RaisePropertyChanged("IsStatic");
            }
        }

        public bool IsObserving
        {
            get => _isObserving;
            set
            {
                _isObserving = value;
                RaisePropertyChanged("IsObserving");
            }
        }

        public bool IsMoving
        {
            get => _isMoving;
            set
            {
                _isMoving = value;
                RaisePropertyChanged("IsMoving");
            }
        }

        public float DayFactor
        {
            get { return _dayFactor; }
            set
            {
                _dayFactor = value;
                RaisePropertyChanged("DayFactor");
            }
        }
    }
}
