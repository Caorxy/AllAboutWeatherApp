using System.Collections;
using System.Collections.Generic;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.Composite;

public class HistoricalDataComposite : IWeatherData
{
    private readonly HourlyWeatherData _hourlyWeatherData;

    public HistoricalDataComposite(HourlyWeatherData hourlyWeatherData)
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




