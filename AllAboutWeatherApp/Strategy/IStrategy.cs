using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.Strategy;

public interface IStrategy
{
    public void GetDataFromRepository(IRepository repository, GeoCoordinates searched);
}