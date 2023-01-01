using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public interface ILocationRepository
{
    Task<IEnumerable<LocationData>> GetLocations(SearchedLocationData locationSearchData);
}