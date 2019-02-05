using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MvvmFoundation.Wpf;

namespace ViewModels
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
        }

        RelayCommand _closeCommand;
        RelayCommand _minimizeCommand;
        RelayCommand _documentationCommand;
        RelayCommand _demoCommand;


        public bool shouldInvoke { get; set; } = true;


        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(this.Close,
                        null);
                }
                return _closeCommand;
            }
        }

        public void Close()
        {
            Application.Current.MainWindow.Close();
        }
        public ICommand MinimizeCommand
        {
            get
            {
                if (_minimizeCommand == null)
                {
                    _minimizeCommand = new RelayCommand(() => Application.Current.MainWindow.WindowState = WindowState.Minimized,
                        null);
                }
                return _minimizeCommand;
            }
        }

        public ICommand DocumentationCommand
        {
            get
            {
                if (_documentationCommand == null)
                {
                    _documentationCommand = new RelayCommand(() =>
                        {
                            try
                            {
                                if (shouldInvoke)
                                    Process.Start(@"..\..\..\dokumentacja_v2.docx");
                            }
                            catch (Exception e)
                            {

                            }
                        },
                        null);
                }
                return _documentationCommand;
            }
        }

        public ICommand DemoCommand
        {
            get
            {
                if (_demoCommand == null)
                {
                    _demoCommand = new RelayCommand(() =>
                        {
                            try
                            {
                                if (shouldInvoke)
                                    Process.Start(@"..\..\..\demo.mp4");
                            }
                            catch (Exception e)
                            {

                            }
                        },
                        null);
                }
                return _demoCommand;
            }
        }
    }
}
