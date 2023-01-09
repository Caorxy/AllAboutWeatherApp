using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public interface IReverseLocationDataRetriever
{
    Task<IEnumerable<LocationData>> GetLocations(GeoCoordinates coordinates);
}