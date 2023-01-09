using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public interface IWeatherDataRetriever
{
    Task<OpenWeatherForecast> GetWeatherForecast(GeoCoordinates coordinates);
}