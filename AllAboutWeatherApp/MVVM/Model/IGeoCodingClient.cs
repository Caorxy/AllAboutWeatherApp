using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public interface IGeoCodingClient
{
    Task<IEnumerable<LocationData>?> GetLocations(SearchedLocationData locationSearchDto);
}