namespace AllAboutWeatherApp.MVVM.Model;

public class DataRetrieverFactory : IDataRetrieverFactory
{
    public ILocationDataRetriever CreateLocationDataRetriever()
    {
        return new LocationDataRetriever();
    }
    
    public IWeatherDataRetriever CreateWeatherDataRetriever()
    {
        return new WeatherDataRetriever();
    }
    
    public IAirQualityDataRetriever CreateAirQualityDataRetriever()
    {
        return new AirQualityDataRetriever();
    }
    
}