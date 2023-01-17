using System;

namespace AllAboutWeatherApp.MVVM.Model.HistoricalDataCollection;

public class HistoricalWeatherData
{
    public DateTime Time { get; }
    public float Temperature { get; }
    public float Humidity { get; }
    public float Pressure { get; }
    public float Rain { get; }
    public float WindSpeed { get; }

    public HistoricalWeatherData(DateTime time, float temperature, float humidity, float pressure, float rain, float windSpeed)
    {
        Time = time;
        Temperature = temperature;
        Humidity = humidity;
        Pressure = pressure;
        Rain = rain;
        WindSpeed = windSpeed;
    }
}
