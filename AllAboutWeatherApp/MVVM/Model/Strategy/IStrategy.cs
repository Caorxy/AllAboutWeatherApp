using AllAboutWeatherApp.MVVM.Model;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.Strategy;

public interface IStrategy
{
    public void GetDataFromRepository(IRepository repository, GeoCoordinates searched);
}