using System.Windows;
using System.Windows.Input;
using TopMostApp.Utils;

namespace TopMostApp.ViewModel
{
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon. In this sample, the
    /// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel
    {

        public ICommand MainCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () =>
                    {

                    }
                };
            }
        }
        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        public ICommand SettingCommand
        {
            get
            {
                return new DelegateCommand
                {
                     
                    CommandAction = () =>
                    {
                      SettingView settingView = new SettingView();
                        settingView.Show();
                    }
                };
            }
        }
        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                     
                    CommandAction = () =>
                    {
                        SettingView settingView = new SettingView();
                        settingView.Show();
                    }
                };
            }
        }

        /// <summary>
        /// Hides the main window. This command is only enabled if a window is open.
        /// </summary>
        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => {
                        AboutView aboutView = new AboutView();
                        aboutView.Show();
                    } 
                };
            }
        }


        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() };
            }
        }
    }
}
