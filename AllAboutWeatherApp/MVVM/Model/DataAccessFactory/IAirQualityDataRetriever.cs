using System.Threading.Tasks;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.DataAccessFactory;

public interface IAirQualityDataRetriever
{
    Task<AirQualityData> GetAirQualityData(GeoCoordinates coordinates);
}