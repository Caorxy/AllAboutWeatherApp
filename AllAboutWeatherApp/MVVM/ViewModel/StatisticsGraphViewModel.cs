using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.MVVM.Model.HistoricalDataCollection;
using LiveChartsCore;
using LiveChartsCore.Easing;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class StatisticsGraphViewModel : ObservableObject
{
    private string[] _titles = { "Temperature", "Pressure", "Wind", "Humidity", "Rain" };
    private List<ISeries> _series;
    private int _pos;
    private string _title;
    private string _isNoteVisible;
            
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
    public string IsNoteVisible
    {
        get => _isNoteVisible;
        set
        {
            _isNoteVisible = value;
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
        _isNoteVisible = "Collapsed";
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

        switch (count)
        {
            case > 50000:
                IsNoteVisible = "Visible";
                CalculateAvg(values1, values2, 24*7);
                break;
            case > 5000:
                IsNoteVisible = "Visible";
                CalculateAvg(values1, values2, 24);
                break;
            default:
            {
                IsNoteVisible = "Collapsed";
                foreach (var data in CurrentGraphData!)
                {
                    values1.Add(_dataSelectors[_pos](data));
                    var date = data.Time.ToString(CultureInfo.InvariantCulture);
                    date = date[..^3];
                    values2.Add(date);
                }

                break;
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
                TextSize = 8,
                LabelsPaint = new SolidColorPaint(SKColors.White)
            }
        };


        columnSeries.PointMeasured += OnPointMeasured;
        Series = new List<ISeries> { columnSeries };
    }
    
    private void CalculateAvg(List<float> values1, List<string> values2, int divider) {
        float dailyAvg = 0;
        var date = "";
        var n = divider;
        foreach (var data in CurrentGraphData!) {
            dailyAvg += _dataSelectors[_pos](data);
            n--;
            if (n == divider/2){
                date = data.Time.ToString(CultureInfo.InvariantCulture);
                date = date[..^9];
            }
            if (n == 0)
            {
                values1.Add(dailyAvg / divider);
                values2.Add(date);
                dailyAvg = 0;
                date = "";
                n = divider;
            }
        }
    }
}

