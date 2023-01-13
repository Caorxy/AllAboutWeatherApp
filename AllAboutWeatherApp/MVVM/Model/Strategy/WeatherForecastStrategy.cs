using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.Strategy;

public class WeatherForecastStrategy: IStrategy
{
    public async void GetDataFromRepository(IRepository repository, GeoCoordinates searched)
    {
        var forecastData = await repository.GetWeatherForecast(searched);

        Mediator.Mediator.GetInstance().OnEvent(this, new ForecastDataMessage
        {
            MessageType = "ForecastData",
            ForecastData = forecastData
        });
    }
}