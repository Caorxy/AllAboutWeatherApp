using System;
using Newtonsoft.Json;

namespace AllAboutWeatherApp.MVVM.Model.DataStorage;

public class HistoricalData
{
    [JsonProperty(PropertyName = "hourly")]
    public HourlyWeatherData? HourlyWeatherInfo;
}
public class HourlyWeatherData
{
    [JsonProperty(PropertyName = "time")]
    public DateTime[]? Time { get; set; }
    [JsonProperty(PropertyName = "temperature_2m")]
    public double?[]? Temperature { get; set; }
    [JsonProperty(PropertyName = "relativehumidity_2m")]
    public double?[]? Humidity { get; set; }
    [JsonProperty(PropertyName = "pressure_msl")]
    public double?[]? Pressure { get; set; }
    [JsonProperty(PropertyName = "rain")]
    public double?[]? Rain { get; set; }
    [JsonProperty(PropertyName = "windspeed_10m")]
    public double?[]? WindSpeed { get; set; }
}
