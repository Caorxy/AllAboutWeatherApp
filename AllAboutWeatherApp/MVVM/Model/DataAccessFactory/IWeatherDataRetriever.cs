using System.Threading.Tasks;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.DataAccessFactory;

public interface IWeatherDataRetriever
{
    Task<OpenWeatherForecast> GetWeatherForecast(GeoCoordinates coordinates);
}