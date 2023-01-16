using System;
using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;
using AllAboutWeatherApp.MVVM.Model.Composite;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class WeatherForecastViewModel : ObservableObject
{
    private OpenWeatherForecast? _weatherForecastData;
    private ForecastData? _firstTabForecastData;
    private ForecastData? _secondTabForecastData;
    private ForecastData? _thirdTabForecastData;
    private int _currentPos;
    private string[] _opacitySide;
    private object? _currentView;
    private string? _buttonsVisibility;
    private WeatherForecastDefaultViewModel WeatherForecastDefaultVm { get; set; }
    public StatisticsGraphViewModel StatisticsGraphVm { get; set; }

    public object? CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand BackCommand { get; set; }
    public RelayCommand NextCommand { get; set; }
    public RelayCommand GraphViewCommand { get; set; }
    public RelayCommand DefaultViewCommand { get; set; }

    public ForecastData? FirstTabForecastData
    {
        get => _firstTabForecastData;
        private set
        {
            _firstTabForecastData = value;
            OnPropertyChanged();
        }
    }
    public ForecastData? SecondTabForecastData
    {
        get => _secondTabForecastData;
        private set
        {
            _secondTabForecastData = value;
            OnPropertyChanged();
        }
    }
    public ForecastData? ThirdTabForecastData
    {
        get => _thirdTabForecastData;
        private set
        {
            _thirdTabForecastData = value;
            OnPropertyChanged();
        }
    }

    public string? ButtonsVisibility
    {
        get => _buttonsVisibility;
        set
        {
            _buttonsVisibility = value;
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


    public WeatherForecastViewModel()
    {
        // Nasluchuje eventow od agregatora
        Mediator.Mediator.GetInstance().Event += (_, e) =>
        {
            // Sprawdza czy wiadomosc jest przeznaczona dla niego
            if (e is not MediatorMessage { MessageType: "ForecastData" } message) return;
            // aktualizuje swoje pola danymi ktore otrzymal
            var forecastDataMessage = (ForecastDataMessage) message;
            SetForecastData(forecastDataMessage.ForecastData);
        };        
        
        // Nasluchuje eventow od agregatora
        Mediator.Mediator.GetInstance().Event += (_, e) =>
        {
            // Sprawdza czy wiadomosc jest przeznaczona dla niego
            if (e is not MediatorMessage { MessageType: "OpenMapForecastData" } message) return;
            // aktualizuje swoje pola danymi ktore otrzymal
            var forecastDataMessage = (HistoricalDataMessage) message;
            
            IWeatherData historicalWeatherData = new HistoricalDataComposite(forecastDataMessage.HistoricalData?.HourlyWeatherInfo!);
            ICalculateStatistics calculateStatistics = new CalculateStatistics();
            if (StatisticsGraphVm != null)
                StatisticsGraphVm.CurrentGraphData = calculateStatistics.GetDataFromPeriod(DateTime.Now, DateTime.Now.AddDays(21), historicalWeatherData);
        };

        _buttonsVisibility = "Visible";
        _opacitySide = new[] {"1","0.7"};
        WeatherForecastDefaultVm = new WeatherForecastDefaultViewModel();
        StatisticsGraphVm = new StatisticsGraphViewModel();
        _currentView = WeatherForecastDefaultVm;
        
        BackCommand = new RelayCommand(_ => {
            if (_currentPos > 0)
            {
                _currentPos -= 3;
            }
            SetTabsData();
        });
        
        NextCommand = new RelayCommand(_ =>
        {
            if (_currentPos < 35)
            {
                _currentPos += 3;
            }

            SetTabsData();
        });

        GraphViewCommand = new RelayCommand(_ =>
        {
            ButtonsVisibility = "Collapsed";
            OpacitySide = new[] {"0.7","1"};
            CurrentView = StatisticsGraphVm;
        });
        
        DefaultViewCommand = new RelayCommand(_ =>
        {
            ButtonsVisibility = "Visible";
            OpacitySide = new[] {"1","0.7"};
            CurrentView = WeatherForecastDefaultVm;
        });
    }

    private void SetForecastData(OpenWeatherForecast? forecastData)
    {
        _currentPos = 0;
        _weatherForecastData = forecastData;
        SetTabsData();
    }

    private void SetTabsData()
    {
        FirstTabForecastData = _weatherForecastData?.List![_currentPos];
        SecondTabForecastData = _weatherForecastData?.List![_currentPos+1];
        ThirdTabForecastData = _weatherForecastData?.List![_currentPos+2];
    }
}