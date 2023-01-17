using System.Collections;
using System.Collections.Generic;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.HistoricalDataCollection;

public class HistoricalDataCollection : IHistoricalWeatherData
{
    private readonly HourlyWeatherData _hourlyWeatherData;

    public HistoricalDataCollection(HourlyWeatherData hourlyWeatherData)
    {
        _hourlyWeatherData = hourlyWeatherData;
    }

    public IEnumerator<HistoricalWeatherData> GetEnumerator()
    {
        return new HistoricalDataEnumerator(_hourlyWeatherData);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}




