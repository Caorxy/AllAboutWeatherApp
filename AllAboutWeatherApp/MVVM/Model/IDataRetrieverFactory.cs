namespace AllAboutWeatherApp.MVVM.Model;

public interface IDataRetrieverFactory
{
    ILocationDataRetriever CreateLocationDataRetriever();
    IWeatherDataRetriever CreateWeatherDataRetriever();
    IAirQualityDataRetriever CreateAirQualityDataRetriever();
    IReverseLocationDataRetriever CreateReverseLocationDataRetriever();

}
