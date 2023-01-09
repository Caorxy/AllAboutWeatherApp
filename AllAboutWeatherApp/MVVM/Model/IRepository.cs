using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public interface IRepository
{
    public Task<IEnumerable<LocationData>> GetLocations(SearchedLocationData locationSearchData);
    public Task<IEnumerable<LocationData>> GetLocations(GeoCoordinates coordinates);
    public Task<OpenWeatherForecast> GetWeatherForecast(GeoCoordinates coordinates);
    public Task<AirQualityData> GetAirQualityData(GeoCoordinates coordinates);
}