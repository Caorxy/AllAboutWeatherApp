using System;
using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;
using AllAboutWeatherApp.MVVM.Model.Composite;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class StatisticsViewModel : ObservableObject
{
    public RelayCommand Button1Clicked { get; set; }
    public RelayCommand Button2Clicked { get; set; }
    public RelayCommand Button3Clicked { get; set; }
    public RelayCommand Button4Clicked { get; set; }
    public RelayCommand ListViewCommand { get; set; }
    public RelayCommand GraphViewCommand { get; set; }
    private HistoricalData? _historicalData;
    private StatisticsData? _statistics;
    private ICalculateStatistics _calculateStatistics;
    private string[] _opacity;
    private string[] _opacitySide;
    private string? _isDatePickerVisible;
    private string? _isWaitingForDataVisible;
    private StatisticsListViewModel StatisticsListVm { get; set; }
    public StatisticsGraphViewModel StatisticsGraphVm{ get; set; }
    private object? _currentView;
    
    public object? CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public StatisticsData? Statistics
    {
        get => _statistics;
        private set
        {
            _statistics = value;
            OnPropertyChanged();
        }
    }

    public string? IsDatePickerVisible
    {
        get => _isDatePickerVisible;
        private set
        {
            _isDatePickerVisible = value;
            OnPropertyChanged();
        }
    }
    public string? IsWaitingForDataVisible
    {
        get => _isWaitingForDataVisible;
        private set
        {
            _isWaitingForDataVisible = value;
            OnPropertyChanged();
        }
    }

    public string[] Opacity
    {
        get => _opacity;
        private set
        {
            _opacity = value;
            OnPropertyChanged();
        }
    }

    public string[] OpacitySide
    {
        get => _opacitySide;
        private set
        {
            _opacitySide = value;
            OnPropertyChanged();
        }
    }

    private DateTime _startDate;
    private DateTime _endDate;
    public DateTime StartDate 
    {
        get => _startDate;
        set
        {
            if (value >= new DateTime(1993, 01, 01))
            {

                _startDate = value;
                OnPropertyChanged();
                if(EndDate > value)
                    CustomDateStats();
            }
        }
    }
    public DateTime EndDate 
    {
        get => _endDate;
        set
        {
            if (value <= DateTime.Now)
            {
                _endDate = value;
                if (value > DateTime.Now.AddDays(-21))
                    _endDate = DateTime.Now.AddDays(-21);
                OnPropertyChanged();
                if(StartDate < value)
                    CustomDateStats();
            }
        }
    }

    public StatisticsViewModel(ICalculateStatistics calculateStatistics)
    {
        
        // Nasluchuje eventow od agregatora
        Mediator.Mediator.GetInstance().Event += (_, e) =>
        {
            // Sprawdza czy wiadomosc jest przeznaczona dla niego
            if (e is not MediatorMessage {MessageType: "StatisticsData"} message) return;
            // aktualizuje swoje pola danymi ktore otrzymal
            var statisticsDataMessage = (StatisticsDataMessage) message;
            if (statisticsDataMessage.StatisticsData != null)
                Statistics = statisticsDataMessage.StatisticsData;
        };       
        
        // Nasluchuje eventow od agregatora
        Mediator.Mediator.GetInstance().Event += (_, e) =>
        {
            // Sprawdza czy wiadomosc jest przeznaczona dla niego
            if (e is not MediatorMessage {MessageType: "HistoricalData"} message) return;
            // aktualizuje swoje pola danymi ktore otrzymal
            var historicalDataMessage = (HistoricalDataMessage) message;
            if (historicalDataMessage.HistoricalData != null)
                _historicalData = historicalDataMessage.HistoricalData;

            StartDate = new DateTime(2022, 12, 01);
            EndDate = DateTime.Now.AddDays(-21);
            CustomDateStats();
            IsWaitingForDataVisible = "Collapsed";
        };

        _isWaitingForDataVisible = "Visible";
        _isDatePickerVisible = "Collapsed";
        _opacity = new[] { "1", "0.2", "0.2", "0.2" };
        _opacitySide = new[] { "1", "0.7" };
        _startDate = DateTime.Today.AddDays(-21);
        _endDate = DateTime.Today.AddDays(-21);
        _calculateStatistics = calculateStatistics;
        StatisticsListVm = new StatisticsListViewModel();
        StatisticsGraphVm = new StatisticsGraphViewModel();
        _currentView = StatisticsListVm;

        Button1Clicked = new RelayCommand( _ =>
        {
            IsDatePickerVisible = "Collapsed";
            Opacity = new[] { "1", "0.2", "0.2", "0.2" };
            StartDate = DateTime.Now.AddDays(-51);
            EndDate = DateTime.Now.AddDays(-21);
            CustomDateStats();
        });        
        Button2Clicked = new RelayCommand( _ =>
        {
            IsDatePickerVisible = "Collapsed";
            Opacity = new[] { "0.2", "1", "0.2", "0.2" };
            StartDate = DateTime.Now.AddDays(-386);
            EndDate = DateTime.Now.AddDays(-21);
            CustomDateStats();
        });        
        Button3Clicked = new RelayCommand( _ =>
        {
            IsDatePickerVisible = "Collapsed";
            Opacity = new[] { "0.2", "0.2", "1", "0.2" };
            StartDate = new DateTime(1993, 01, 01);
            EndDate = DateTime.Now.AddDays(-21);
            CustomDateStats();
        });        
        Button4Clicked = new RelayCommand( _ =>
        {
            IsDatePickerVisible = "Visible";
            Opacity = new[] { "0.2", "0.2", "0.2", "1" };
        });
        ListViewCommand = new RelayCommand(_ =>
        {
            OpacitySide = new[] { "1", "0.7" };
            CurrentView = StatisticsListVm;
        });
        GraphViewCommand = new RelayCommand( _ =>
        {
            OpacitySide = new[] { "0.7", "1" };
            CurrentView = StatisticsGraphVm;
        });
    }

    private void CustomDateStats()
    {
        IWeatherData historicalWeatherData = new HistoricalDataComposite(_historicalData?.HourlyWeatherInfo!);
        Statistics = _calculateStatistics.GetStatisticsData(StartDate, EndDate, historicalWeatherData);
        StatisticsGraphVm.CurrentGraphData =  _calculateStatistics.GetDataFromPeriod(StartDate, EndDate, historicalWeatherData);
    }
}