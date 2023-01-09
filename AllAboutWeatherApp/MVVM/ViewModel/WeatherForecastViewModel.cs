using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class WeatherForecastViewModel : ObservableObject
{
    private OpenWeatherForecast? _weatherForecastData;
    private ForecastData? _firstTabForecastData;
    private ForecastData? _secondTabForecastData;
    private ForecastData? _thirdTabForecastData;
    private int _currentPos;
    public RelayCommand BackCommand { get; set; }
    public RelayCommand NextCommand { get; set; }

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

    public WeatherForecastViewModel()
    {
        // Listen for messages from the mediator
        Mediator.Mediator.GetInstance().Event += (_, e) =>
        {
            // Check the message type to determine if the message is intended for this view model
            if (e is MediatorMessage {MessageType: "ForecastData"} message)
            {
                // Update the LocationData properties with the location data from the message
                var forecastDataMessage = (ForecastDataMessage) message;
                SetForecastData(forecastDataMessage.ForecastData);
            }
        };
        
        BackCommand = new RelayCommand(_ => {
            if (_currentPos > 0)
            {
                _currentPos -= 3;
            }
            SetTabsData();
        });
        
        NextCommand = new RelayCommand(_ => { 
            if (_currentPos < 35)
            {
                _currentPos += 3;
            }
            SetTabsData();
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