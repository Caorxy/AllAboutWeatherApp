namespace AllAboutWeatherApp.MVVM.Model.DataAccessFactory;

public interface IDataRetrieverFactory
{
    ILocationDataRetriever CreateLocationDataRetriever();
    IWeatherDataRetriever CreateWeatherDataRetriever();
    IAirQualityDataRetriever CreateAirQualityDataRetriever();
    IHistoricalDataRetriever CreateHistoricalDataRetriever();
    IOpenMapForecastDataRetriever CreateOpenMapForecastDataRetriever();
}
