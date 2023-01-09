using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.Strategy;

public class AirQualityStrategy: IStrategy
{
    public async void GetDataFromRepository(IRepository repository, GeoCoordinates searched)
    {
        var airQualityData = await repository.GetAirQualityData(searched);

        Mediator.Mediator.GetInstance().OnEvent(this, new AirQualityDataMessage
        {
            MessageType = "AirQualityData",
            AirQualityData = airQualityData
        });
    }

}