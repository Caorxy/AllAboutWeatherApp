using System;

namespace AllAboutWeatherApp.MVVM.Model.DataStorage;

public class StatisticsData
{
    public float? MinTemp { get; set; }
    public float? MaxTemp { get; set; }
    public float? MinPress { get; set; }
    public float? MaxPress { get; set; }
    public float? MaxWind { get; set; }
    public float? MinHum { get; set; }
    public float? MaxHum { get; set; }
    public float? MaxRain { get; set; }
    public DateTime?[]? DateTime { get; set; }
}