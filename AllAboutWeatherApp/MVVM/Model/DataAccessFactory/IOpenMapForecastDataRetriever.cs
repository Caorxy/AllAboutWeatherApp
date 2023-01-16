using System.Threading.Tasks;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.DataAccessFactory;

public interface IOpenMapForecastDataRetriever
{
    public Task<HistoricalData> GetForecastData(GeoCoordinates coordinates);
}