﻿using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace ViewModelsSamples.Events.Cartesian
{
    public class ViewModel
    {
        public ViewModel()
        {
            var data = new[]
            {
                new City { Name = "Tokyo", Population = 4 },
                new City { Name = "New York", Population = 6 },
                new City { Name = "Seoul", Population = 2 },
                new City { Name = "Moscow", Population = 8 },
                new City { Name = "Shanghai", Population = 3 },
                new City { Name = "Guadalajara", Population = 4 }
            };

            var columnSeries = new ColumnSeries<City>
            {
                Values = data,
                TooltipLabelFormatter = point => $"{point.Model.Name} {point.Model.Population} Million",
                Mapping = (city, point) =>
                {
                    point.PrimaryValue = city.Population; // use the population property in this series // mark
                    point.SecondaryValue = point.Context.Index;
                }
            };

            columnSeries.DataPointerDown += ColumnSeries_DataPointerDown;

            Series = new ISeries[]
            {
                columnSeries,
                new LineSeries<int> { Values = new[] { 6, 7, 2, 9, 6, 2 } },
            };
        }

        private void ColumnSeries_DataPointerDown(
            IChartView chart,
            IEnumerable<ChartPoint<City, RoundedRectangleGeometry, LabelGeometry>> points)
        {
            // the event passes a collection of the points that were triggered by the pointer down event.
            foreach (var point in points)
            {
                Trace.WriteLine($"[series.dataPointerDownEvent] clicked on {point.Model.Name}");
            }
        }

        public IEnumerable<ISeries> Series { get; set; }

        // XAML platforms also support ICommands
        public ICommand DataPointerDownCommand { get; set; } = new RelayCommand(); // mark
    }
}
