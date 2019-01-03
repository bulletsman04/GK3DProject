using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModels
{
    public class MainViewModel
    {
        public MenuViewModel MenuViewModel { get; set; }
        public CanvasViewModel CanvasViewModel { get; set; }
        public SettingsViewModel SettingsViewModel { get; set; }
        public BitmapManager BitmapManager { get; set; }

        public MainViewModel()
        {
            BitmapManager = new BitmapManager();

            MenuViewModel = new MenuViewModel();
            CanvasViewModel = new CanvasViewModel(BitmapManager);
            SettingsViewModel = new SettingsViewModel();
        }
    }
}
