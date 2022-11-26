using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KeqingNiuza.Core.Wish;
using KeqingNiuza.ViewModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;

namespace KeqingNiuza.View
{
    /// <summary>
    /// WishPredictionView.xaml 的交互逻辑
    /// </summary>
    public partial class WishPredictionView : UserControl 
    {
        public WishPredictionView()
        {
            InitializeComponent();
            
        }
        WishPredictionModel ViewModel { get; set; }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var count = (int)NumericUpDown.Value;
            var index = ComboBox.SelectedIndex;
            var a = Prediction.GetCharacterDensityAndDistributionWithUp(count).distribution.Prepend(0);
            ViewModel.ShowChart(index,count);
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel == null)
            {
                await Task.Run(() => ViewModel = new WishPredictionModel());
                DataContext = ViewModel;
            }
        }
    }
}
