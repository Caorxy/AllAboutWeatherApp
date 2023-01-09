using AllAboutWeatherApp.MVVM.Model;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.Strategy;

public interface IStrategy
{
    public void GetDataFromRepository(IRepository repository, GeoCoordinates searched);
}