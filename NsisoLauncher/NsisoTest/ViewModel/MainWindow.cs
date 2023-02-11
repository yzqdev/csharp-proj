using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsisoTest.ViewModel;
[ObservableObject]
public partial class MainWindow {
    
    MainWindow() {
        SpeedValues = new ObservableCollection<double>();
        for (int i = 0; i < 50; i++) {
            SpeedValues.Add(0);
        }
        ChartSeries = new ObservableCollection<ISeries> {
            new LineSeries<double> {

            Values = SpeedValues,
            Stroke = null,
            Fill = new SolidColorPaint { Color = SKColors.DarkOliveGreen }, LineSmoothness = 0, Tag = "下载速度"
        }
        };
    }
   public ObservableCollection<ISeries> ChartSeries { get; set; }


    public ObservableCollection<double> SpeedValues {
        get;

        set;
    }


    public Axis[] XAxes { get; set; } =
    {
        new Axis
        {
            //Name = "X axis",
            TextSize = 20,

            // LabelsPaint = null will not draw the axis labels.
            LabelsPaint = new SolidColorPaint{ Color = SKColors.CornflowerBlue },

            // SeparatorsPaint = null will not draw the separator lines
            SeparatorsPaint = new SolidColorPaint { Color = SKColors.LightBlue, StrokeThickness = 3 },

            Position = AxisPosition.End
        }
    };

    public Axis[] YAxes { get; set; } =
    {
        new Axis
        {
            //Name = "Y axis",
            TextSize = 20,
            LabelsPaint = new SolidColorPaint { Color = SKColors.Red },
            SeparatorsPaint = new SolidColorPaint { Color = SKColors.LightPink, StrokeThickness = 3 },
            Position = AxisPosition.End
        }
    };
}

