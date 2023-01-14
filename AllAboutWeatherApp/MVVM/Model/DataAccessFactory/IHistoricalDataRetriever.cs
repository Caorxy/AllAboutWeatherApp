using System.Threading.Tasks;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.DataAccessFactory;

public interface IHistoricalDataRetriever
{
    public Task<HistoricalData> GetHistoricalData(GeoCoordinates coordinates);
}