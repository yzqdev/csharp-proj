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

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;

namespace KeqingNiuza.ViewModel
{
    internal class WishPredictionModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private LineSeries<double> lineSeries;
        private List<ISeries> series;
        public List<ISeries> Series { get => series; set { series = value; OnPropertyChanged(); } }

        public WishPredictionModel()
        {


            lineSeries = new LineSeries<double>
            {
                Values = Value,

                Stroke = new SolidColorPaint(SKColors.Red)
                {
                    StrokeThickness = 1
                },
                GeometrySize = 0,
                TooltipLabelFormatter =
        (chartPoint) => $"概率: {chartPoint.PrimaryValue.ToString("P4")},次数:{chartPoint.SecondaryValue}",
                Name = "abb",

            };
            Series = new List<ISeries> {
                lineSeries
            };

            // sharing the same instance for both charts will keep the zooming and panning synced
            SharedXAxis = new Axis[] { new Axis {
            Name = "抽卡次数",
            TextSize = 9,
              NamePaint = new SolidColorPaint
                    {
                        Color = SKColors.Red,
                        //FontFamily = LiveChartsSkiaSharp.MatchChar('أ'), // Arab 
                        //FontFamily = LiveChartsSkiaSharp.MatchChar('あ'), // Japanease 
                        FontFamily = LiveChartsSkiaSharp.MatchChar('你') // Chinese 汉语 sample
                        //FontFamily = "Times New Roman"
                       
                    },
            } };
            SharedYAxis = new Axis[] { new Axis {
                Labeler=YSectionFormatter,
            Name    ="获取概率",

                TextSize = 9,
                MaxLimit=1,
                MinLimit=0,
                NamePaint = new SolidColorPaint
                    {
                        Color = SKColors.Red,
                        //FontFamily = LiveChartsSkiaSharp.MatchChar('أ'), // Arab 
                        //FontFamily = LiveChartsSkiaSharp.MatchChar('あ'), // Japanease 
                        FontFamily = LiveChartsSkiaSharp.MatchChar('你') // Chinese 汉语 sample
                        //FontFamily = "Times New Roman"
                    },} };
        }

        public Axis[] SharedXAxis { get; set; }
        public Axis[] SharedYAxis { get; set; }




        private IEnumerable<double> _Value;
        public IEnumerable<double> Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged();
            }
        }

        private int _XSection;
        public int XSection
        {
            get { return _XSection; }
            set
            {
                _XSection = value;
                OnPropertyChanged();
            }
        }

        private double _YSection;
        public double YSection
        {
            get { return _YSection; }
            set
            {
                _YSection = value;
                OnPropertyChanged();
            }
        }

        private Visibility _AxisSectionVisibility = Visibility.Hidden;
        public Visibility AxisSectionVisibility
        {
            get { return _AxisSectionVisibility; }
            set
            {
                _AxisSectionVisibility = value;
                OnPropertyChanged();
            }
        }


        private bool _ShowSectionDataLabel;
        public bool ShowSectionDataLabel
        {
            get { return _ShowSectionDataLabel; }
            set
            {
                _ShowSectionDataLabel = value;
                OnPropertyChanged();
            }
        }



        public Func<double, string> YSectionFormatter => value => value.ToString("P4");

        public async void ShowChart(int index, int count)
        {
            await Task.Run(() =>
            {
                switch (index)
                {
                    case 0:
                        lineSeries.Values = Prediction.GetCharacterDensityAndDistributionWithUp(count).distribution.Prepend(0);
                        break;
                    case 1:
                        lineSeries.Values = Prediction.GetCharacterDensityAndDistribution(count).distribution.Prepend(0);
                        break;
                    case 2:
                        lineSeries.Values = Prediction.GetSpecifiedWeaponDensityAndDistribution(count).distribution.Prepend(0);
                        break;
                    case 3:
                        lineSeries.Values = Prediction.GetWeaponDensityAndDistribution(count).distribution.Prepend(0);
                        break;
                }
                Series = new List<ISeries> {
                lineSeries
            };
            });
        }


        private void CartesianChart_MouseMove(object sender, MouseEventArgs e)
        {
            if (Value != null)
            {
                var chart = sender as CartesianChart;
                var point = e.GetPosition(chart);
                //var p = chart.ConvertToChartValues(point);
                //XSection = (int)Math.Round(p.X);
                //try
                //{
                //    YSection = Value.ElementAt(XSection);

                //}
                //catch { }
            }
        }

        private void CartesianChart_MouseEnter(object sender, MouseEventArgs e)
        {
            AxisSectionVisibility = Visibility.Visible;
            ShowSectionDataLabel = true;
        }

        private void CartesianChart_MouseLeave(object sender, MouseEventArgs e)
        {
            AxisSectionVisibility = Visibility.Hidden;
            ShowSectionDataLabel = false;
        }


    }
}
