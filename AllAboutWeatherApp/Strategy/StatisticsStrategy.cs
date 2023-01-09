using System;
using System.Linq;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.Strategy;

public class StatisticsStrategy : IStrategy
{
    public async void GetDataFromRepository(IRepository repository, GeoCoordinates searched)
    {
        var forecastData = await repository.GetWeatherForecast(searched);
        var statisticsData = CalculateStatistics(forecastData);

        Mediator.Mediator.GetInstance().OnEvent(this, new StatisticsDataMessage
        {
            MessageType = "StatisticsData",
            StatisticsData = statisticsData
        });
    }

    private static StatisticsData CalculateStatistics(OpenWeatherForecast forecastData)
    {
        var statisticsData = new StatisticsData();
        if (forecastData.List == null) return statisticsData;
        var minTemp = forecastData.List.OrderBy(o => o?.Main?.Temp_Min).First();
        var maxTemp = forecastData.List.OrderBy(o => o?.Main?.Temp_Max).Last();
        var minPress = forecastData.List.OrderBy(o => o?.Main?.Pressure).First();
        var maxPress = forecastData.List.OrderBy(o => o?.Main?.Pressure).Last();
        var maxWind = forecastData.List.OrderBy(o => o?.Wind?.Speed).Last();
        var minHum = forecastData.List.OrderBy(o => o?.Main?.Humidity).First();
        var maxHum = forecastData.List.OrderBy(o => o?.Main?.Humidity).Last();
        var maxRain = forecastData.List.OrderBy(o => o?.Rain?.ThreeH).Last();

        statisticsData.DateTime = new DateTime?[8];
        statisticsData.MinTemp = minTemp?.Main?.Temp_Min;
        statisticsData.DateTime[0] = minTemp?.Dt_txt;
        statisticsData.MaxTemp = maxTemp?.Main?.Temp_Max;
        statisticsData.DateTime[1] = maxTemp?.Dt_txt;
        statisticsData.MinPress = minPress?.Main?.Pressure;
        statisticsData.DateTime[2] = minPress?.Dt_txt;
        statisticsData.MaxPress = maxPress?.Main?.Pressure;
        statisticsData.DateTime[3] = maxPress?.Dt_txt;
        statisticsData.MaxWind = maxWind?.Wind?.Speed;
        statisticsData.DateTime[4] = maxWind?.Dt_txt;
        statisticsData.MinHum = minHum?.Main?.Humidity;
        statisticsData.DateTime[5] = minHum?.Dt_txt;
        statisticsData.MaxHum = maxHum?.Main?.Humidity;
        statisticsData.DateTime[6] = maxHum?.Dt_txt;
        statisticsData.MaxRain = maxRain?.Rain?.ThreeH;
        statisticsData.DateTime[7] = maxRain?.Dt_txt;

        return statisticsData;
    }
}