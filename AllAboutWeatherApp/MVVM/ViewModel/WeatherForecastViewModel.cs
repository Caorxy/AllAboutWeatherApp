using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class WeatherForecastViewModel : ObservableObject
{
    private OpenWeatherForecast? _weatherForecastData;

    public OpenWeatherForecast? WeatherForecastData
    {
        get => _weatherForecastData;
        set
        {
            _weatherForecastData = value;
            OnPropertyChanged();
        }
    }

    public WeatherForecastViewModel()
    {
        // Listen for messages from the mediator
        Mediator.Mediator.GetInstance().Event += (_, e) =>
        {
            // Check the message type to determine if the message is intended for this view model
            if (e is MediatorMessage message && message.MessageType == "ForecastData")
            {
                // Update the LocationData properties with the location data from the message
                var forecastDataMessage = (ForecastDataMessage) message;
                SetForecastData(forecastDataMessage.ForecastData);
            }
        };
    }

    private void SetForecastData(OpenWeatherForecast? forecastData)
    {
        WeatherForecastData = forecastData;
    }
}