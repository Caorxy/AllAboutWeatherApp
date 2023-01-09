using System.Collections.Generic;
using System.Threading.Tasks;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.DataAccessFactory;

public interface ILocationDataRetriever
{
    Task<IEnumerable<LocationData>> GetLocations(SearchedLocationData locationSearchData);
}