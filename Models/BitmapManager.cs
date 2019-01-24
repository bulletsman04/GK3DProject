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
        // ToDo: Error eg while 500 and 475
        public int Width =500;
        public int Height =>500;


        public BitmapManager()
        {
            MainBitmap = new DirectBitmap(Width,Height);
        }

        public void RaiseBitmapChanged() => RaisePropertyChanged("MainBitmap");
    }
}
