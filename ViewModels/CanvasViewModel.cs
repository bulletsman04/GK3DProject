using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MvvmFoundation.Wpf;
using Models;

namespace ViewModels
{
    public class CanvasViewModel : ObservableObject
    {
        private ImageSource _imageSource;

        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged("ImageSource");
            }
        }

        private BitmapManager _bitmapManager;

        public PropertyObserver<BitmapManager> BitmapObserver { get; set; }

        public CanvasViewModel(BitmapManager bitmapManager)
        {
            _bitmapManager = bitmapManager;
            //BitmapChangedHandler(_bitmapManager);
            RegisterPropertiesChanged();
        }

        public void RegisterPropertiesChanged()
        {
            BitmapObserver = new PropertyObserver<BitmapManager>(_bitmapManager)
                .RegisterHandler(n => n.MainBitmap, BitmapChangedHandler);
        }

        private void BitmapChangedHandler(BitmapManager bitmapManager)
        {
            ImageSource = TypesConverters.BitmapToImageSource(bitmapManager.MainBitmap.Bitmap);
        }
    }
}
