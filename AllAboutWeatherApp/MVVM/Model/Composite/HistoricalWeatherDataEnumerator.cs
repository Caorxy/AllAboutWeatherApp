using System.Collections;
using System.Collections.Generic;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.Composite;

public class HistoricalDataEnumerator : IEnumerator<HistoricalWeatherData>
{
    private readonly HourlyWeatherData _hourlyWeatherData;
    private int _currentIndex;

    public HistoricalDataEnumerator(HourlyWeatherData hourlyWeatherData)
    {
        _hourlyWeatherData = hourlyWeatherData;
        _currentIndex = -1;
    }

    public HistoricalWeatherData Current => new HistoricalWeatherData(_hourlyWeatherData.Time![_currentIndex],
        _hourlyWeatherData.Temperature![_currentIndex],
        _hourlyWeatherData.Humidity![_currentIndex],
        _hourlyWeatherData.Pressure![_currentIndex],
        _hourlyWeatherData.Rain![_currentIndex],
        _hourlyWeatherData.WindSpeed![_currentIndex]);

    object IEnumerator.Current => Current;

    public void Dispose() { }

    public bool MoveNext() => ++_currentIndex < _hourlyWeatherData.Time.Length;

    public void Reset() => _currentIndex = -1;
}

