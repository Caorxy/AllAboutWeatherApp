using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public interface ILocationDataRetriever
{
    Task<IEnumerable<LocationData>> GetLocations(SearchedLocationData locationSearchData);
}