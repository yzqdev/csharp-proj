using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveChartsCore.Measure;

namespace NsisoTest {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
    
        public MainWindow() {
      
            InitializeComponent();
               
        }
        
        public async static Task<string> HttpGetStringAsync(string uri) {

            try {
                var httpClientHandler = new HttpClientHandler {
                    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
                };
                using HttpClient client = new(httpClientHandler);
                return await client.GetStringAsync(uri);
            } catch (TaskCanceledException) {
                return null;
            }
        }

    

        private async void reqBtn_Click(object sender, RoutedEventArgs e) {
            var url = "https://bmclapi2.bangbang93.com/mc/game/version_manifest.json";
            var result = await HttpGetStringAsync(url);
            Console.WriteLine(result);
            resTextBlock.Text= result;
        }
    }
}
