using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.Strategy;

public class Context
{
    private IStrategy? _strategy;
    private readonly IRepository? _repository;

    public Context(IRepository repository)
    {
        _repository = repository;
    }
    
    public void SetStrategy(IStrategy strategy)
    {
        _strategy = strategy;
    }

    public void ExecuteStrategy(GeoCoordinates coordinates)
    {
        if (_repository != null) _strategy?.GetDataFromRepository(_repository, coordinates);
    }
}