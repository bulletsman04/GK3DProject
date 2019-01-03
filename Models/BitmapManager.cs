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
                RaisePropertyChanged("PixelMap");
            }
        }

        public int Width => 1000;
        public int Height => 1000;
    }
}
