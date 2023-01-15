using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.MVVM.Model.Composite;
using LiveChartsCore;
using LiveChartsCore.Easing;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class StatisticsGraphViewModel : ObservableObject
{
    private string[] _titles = { "Temperature", "Pressure", "Wind", "Humidity", "Rain" };
    private List<ISeries> _series;
    private int _pos;
    private string _title;
            
    private readonly Dictionary<int, Func<HistoricalWeatherData, float>> _dataSelectors = new()
    {
        {0, data => data.Temperature},
        {1, data => data.Pressure},
        {2, data => data.WindSpeed},
        {3, data => data.Humidity},
        {4, data => data.Rain},
    };

    public RelayCommand Next { get; set; }
    public RelayCommand Back { get; set; }

    private IEnumerable<HistoricalWeatherData>? _currentGraphData;

    private Axis[] _xaxes;

    public Axis[] XAxes
    {
        get => _xaxes;
        set
        {
            _xaxes = value;
            OnPropertyChanged();
        }
    }
    public IEnumerable<HistoricalWeatherData>? CurrentGraphData
    {
        get => _currentGraphData;
        set
        {
            _currentGraphData = value;
            OnGraphPropertyChanged();
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public List<ISeries> Series
    {
        get => _series;
        set
        {
            _series = value;
            OnPropertyChanged();
        }
    }

    public StatisticsGraphViewModel()
    {
        var values1 = new List<float>();

        var columnSeries1 = new ColumnSeries<float>
        {
            Values = values1,
            Stroke = null,
            Padding = 2
        };

        columnSeries1.PointMeasured += OnPointMeasured;

        _pos = 0;
        _title = _titles[_pos];
        _series = new List<ISeries> { columnSeries1 };
        _xaxes = Array.Empty<Axis>();

        Next = new RelayCommand(_ =>
        {
            _pos = (_pos + 1) % 5;
            Title = _titles[_pos];
            OnGraphPropertyChanged();
        });

        Back = new RelayCommand(_ =>
        {
            _pos = _pos == 0 ? 4 : _pos - 1;
            Title = _titles[_pos];
            OnGraphPropertyChanged();
        });
    }

    private void OnPointMeasured(ChartPoint<float, RoundedRectangleGeometry, LabelGeometry> point)
    {
        var visual = point.Visual;
        if (visual is null) return;

        var delayedFunction = new DelayedFunction(EasingFunctions.BuildCustomElasticOut(1.5f, 0.60f), point, 0.01f);

        _ = visual
            .TransitionateProperties(
                nameof(visual.Y),
                nameof(visual.Height))
            .WithAnimation(animation =>
                animation
                    .WithDuration(delayedFunction.Speed)
                    .WithEasingFunction(delayedFunction.Function));
    }

    private void OnGraphPropertyChanged()
    {
        var values1 = new List<float>();
        var values2 = new List<string>();
        var count = CurrentGraphData?.Count();

        if (count > 5000)
        {
            CalculateAvg(values1, values2);
        }
        else
        {
            foreach (var data in CurrentGraphData!)
            {
                values1.Add(_dataSelectors[_pos](data));
                var date = data.Time.ToString(CultureInfo.InvariantCulture);
                date = date[..^3];
                values2.Add(date);
            }
        }


        var columnSeries = new ColumnSeries<float>
        {
            Values = values1,
            Name = Title,
            Stroke = null,
            Padding = 2
        };
        
        XAxes = new []
        {
            new Axis
            {
                Name = "Date",
                Labels = values2,
                TextSize = 8
            }
        };


        columnSeries.PointMeasured += OnPointMeasured;
        Series = new List<ISeries> { columnSeries };
    }
    
    private void CalculateAvg(List<float> values1, List<string> values2) {
        float dailyAvg = 0;
        string date = "";
        var n = 24;
        foreach (var data in CurrentGraphData!) {
            dailyAvg += _dataSelectors[_pos](data);
            n--;
            switch (n)
            {
                case 12:
                    date = data.Time.ToString(CultureInfo.InvariantCulture);
                    date = date[..^9];
                    break;
                case 0:
                    values1.Add(dailyAvg/24);
                    values2.Add(date);
                    dailyAvg = 0;
                    date = "";
                    n = 24;
                    break;
            }
        }
    }

}