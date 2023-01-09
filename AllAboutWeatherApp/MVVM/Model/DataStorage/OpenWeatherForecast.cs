using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AllAboutWeatherApp.MVVM.Model.DataStorage;

public class OpenWeatherForecast
{
    public string? Cod { get; set; }
    public decimal Message { get; set; }
    public int Cnt { get; set; }
    public List<ForecastData?>? List { get; set; }
    public City? City { get; set; }
}

public class ForecastData
{
    public DateTime Date { get; set; }
    public MainData? Main { get; set; }
    public List<WeatherData>? Weather { get; set; }
    public CloudData? Clouds { get; set; }
    public WindData? Wind { get; set; }
    public double Visibility { get; set; }
    public double Pop { get; set; }
    public RainData? Rain { get; set; }
    public SnowData? Snow { get; set; }
    public SysData? Sys { get; set; }
    public DateTime Dt_txt { get; set; }
    public string? PathToIcon {get; set; }
}


public class MainData
{
    public double Temp { get; set; }
    public double Feels_Like { get; set; }
    public double Temp_Min { get; set; }
    public double Temp_Max { get; set; }
    public int Pressure { get; set; }
    public int Sea_Level { get; set; }
    public int Grnd_Level { get; set; }
    public int Humidity { get; set; }
    public double Temp_Kf { get; set; }
}

public class WeatherData
{
    public int Id { get; set; }
    public string? Main { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
}

public class CloudData
{
    public int All { get; set; }
}

public class WindData
{
    public double Speed { get; set; }
    public int Deg { get; set; }
    public double Gust { get; set; }
}

public class RainData
{
    [JsonProperty(PropertyName = "3h")]
    public double ThreeH { get; set; }
}

public class SnowData
{
    public double ThreeH { get; set; }
}

public class SysData
{
    public string? Pod { get; set; }
}

public class City
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public GeoCoordinates? Coord { get; set; }
    public string? Country { get; set; }
    public int Population { get; set; }
    public int Timezone { get; set; }
    public int Sunrise { get; set; }
    public int Sunset { get; set; }
}


