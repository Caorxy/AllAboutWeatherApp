using System;
using System.Collections.Generic;

namespace AllAboutWeatherApp.MVVM.Model.DataStorage;

public class AirQualityData
{
    public GeoCoordinates? coord;
    public List<AirPollutionData>? List { get; set; }
}

public class AirPollutionData
{
    public long Dt { get; set; }
    public DateTime DtFormat { get; set; }
    public MainAirQualityData? Main { get; set; }
    public ComponentsData? Components { get; set; }
}

public class MainAirQualityData
{
    public int Aqi { get; set; }
    public string? AqiText { get; set; }
    public string? Color { get; set; }
    public string? PathToIcon { get; set; }
}

public class ComponentsData
{
    public double Co { get; set; }
    public double No { get; set; }
    public double No2 { get; set; }
    public double O3 { get; set; }
    public double So2 { get; set; }
    public double Pm2_5 { get; set; }
    public double Pm10 { get; set; }
    public double Nh3 { get; set; }
}