﻿using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModelsSamples.Lines.Padding
{
    public class ViewModel
    {
        public IEnumerable<ISeries> Series { get; set; }
            = new ObservableCollection<ISeries>
            {
                // this series fits the draw margin area
                // the key is to set the DataPadding to 0,0
                // also remove GeometryStroke, GeometryFill and GeometrySize
                // to prevent the series from reserving a space for the series geometry.
                new LineSeries<double>
                {
                    Values = new ObservableCollection<double> { 2, 1, 3, 5, 3, 4, 6 },
                    GeometryStroke = null,
                    GeometryFill = null,
                    GeometrySize = 0,
                    DataPadding = new LvcPoint(0,0)
                }
            };

        public DrawMarginFrame DrawMarginFrame => new DrawMarginFrame
        {
            Fill = new SolidColorPaint(new SKColor(220, 220, 220)),
            Stroke = new SolidColorPaint(new SKColor(180, 180, 180), 2)
        };
    }
}
