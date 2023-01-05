using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public interface IAirQualityDataRetriever
{
    Task<IEnumerable<AirQualityData>> GetAirQualityData(GeoCoordinates coordinates);
}