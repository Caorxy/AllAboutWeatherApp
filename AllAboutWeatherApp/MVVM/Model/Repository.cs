using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public class Repository : IRepository
{
    private readonly IDataRetrieverFactory _dataRetrieverFactory;

    public Repository(IDataRetrieverFactory dataRetrieverFactory)
    {
        _dataRetrieverFactory = dataRetrieverFactory;
    }

    public async Task<IEnumerable<LocationData>> GetLocations(SearchedLocationData locationSearchData)
    {
        ILocationDataRetriever locationDataRetriever = _dataRetrieverFactory.CreateLocationDataRetriever();
        return await locationDataRetriever.GetLocations(locationSearchData);
    } 
    
    public async Task<OpenWeatherForecast> GetWeatherForecast(GeoCoordinates coordinates)
    {
        IWeatherDataRetriever locationDataRetriever = _dataRetrieverFactory.CreateWeatherDataRetriever();
        return await locationDataRetriever.GetWeatherForecast(coordinates);
    }    
    
    public async Task<AirQualityData> GetAirQualityData(GeoCoordinates coordinates)
    {
        IAirQualityDataRetriever airQualityDataRetriever = _dataRetrieverFactory.CreateAirQualityDataRetriever();
        return await airQualityDataRetriever.GetAirQualityData(coordinates);
    }  
    
    public async Task<IEnumerable<LocationData>> GetLocations(GeoCoordinates coordinates)
    {
        IReverseLocationDataRetriever locationDataRetriever =
            _dataRetrieverFactory.CreateReverseLocationDataRetriever();
        return await locationDataRetriever.GetLocations(coordinates);
    }
    
}

