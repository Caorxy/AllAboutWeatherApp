using System.Collections.Generic;
using System.Threading.Tasks;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model;

public interface IRepository
{
    public Task<IEnumerable<LocationData>> GetLocations(SearchedLocationData locationSearchData);
    public Task<OpenWeatherForecast> GetWeatherForecast(GeoCoordinates coordinates);
    public Task<AirQualityData> GetAirQualityData(GeoCoordinates coordinates);
}