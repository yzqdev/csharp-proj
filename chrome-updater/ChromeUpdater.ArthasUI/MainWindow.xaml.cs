using Arthas.Controls;
using ChromeUpdater.Core.Services;
using ChromeUpdater.Core ;
using System;
using System.Diagnostics;
using System.Windows;

namespace ChromeUpdater.ArthasUI {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var menu = sender as MetroMenuItem;
            if (menu == null) return;
            var header = (string)menu.Header;
            if (header.StartsWith("耍下"))
            {
                Process.Start(new ProcessStartInfo("https://github.com/shuax")
                {
                    UseShellExecute = true
                });
                 
            }
            else if (header.StartsWith("ONEO"))
            {
                Process.Start(new ProcessStartInfo("https://github.com/1217950746")
                {
                    UseShellExecute = true
                });
            }
               
            else if (header.StartsWith("手动"))
                 Process.Start(new ProcessStartInfo("https://www.iplaysoft.com/tools/chrome/") { UseShellExecute=true});
            else if (header.StartsWith("Chromium"))
                Process.Start("http://commondatastorage.googleapis.com/chromium-browser-continuous/index.html");
            else
                Process.Start(new ProcessStartInfo("https://github.com/TkYu") { UseShellExecute=true});
        }

        private void TxtPath_OnButtonClick(object sender, EventArgs e)
        {
            ((ChromeUpdaterCore)DataContext).CmdFolderBrowse.Execute(null);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //init messageservice here
            ServiceManager.Instance.AddService<IMessageService>(new MessageService(this));
        }
    }
}
