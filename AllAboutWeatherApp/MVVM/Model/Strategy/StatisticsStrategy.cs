using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.Strategy;

public class StatisticsStrategy : IStrategy
{
    public async void GetDataFromRepository(IRepository repository, GeoCoordinates searched)
    {
        var historicalData = await repository.GetHistoricalData(searched);
        Mediator.Mediator.GetInstance().OnEvent(this, new HistoricalDataMessage
        {
            MessageType = "HistoricalData",
            HistoricalData = historicalData
        });
    }
}