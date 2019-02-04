using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmFoundation.Wpf;

namespace Models
{
    public class BitmapManager : ObservableObject
    {
        private DirectBitmap _mainBitmap;

        public DirectBitmap MainBitmap
        {
            get { return _mainBitmap; }
            set
            {
                _mainBitmap = value;
                RaisePropertyChanged("MainBitmap");
            }
        }
        public int Width =400;
        public int Height =>400;


        public BitmapManager()
        {
            MainBitmap = new DirectBitmap(Width,Height);
        }

        public void RaiseBitmapChanged() => RaisePropertyChanged("MainBitmap");
    }
}
