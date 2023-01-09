using System;

namespace AllAboutWeatherApp.MVVM.Model;

public class StatisticsData
{
    public double? MinTemp { get; set; }
    public double? MaxTemp { get; set; }
    public double? MinPress { get; set; }
    public double? MaxPress { get; set; }
    public double? MaxWind { get; set; }
    public double? MinHum { get; set; }
    public double? MaxHum { get; set; }
    public double? MaxRain { get; set; }
    public DateTime?[]? DateTime { get; set; }
}