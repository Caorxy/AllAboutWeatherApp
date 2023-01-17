using System;
using System.Collections.Generic;
using System.Linq;
using AllAboutWeatherApp.MVVM.Model.DataStorage;
using AllAboutWeatherApp.MVVM.Model.HistoricalDataCollection;

namespace AllAboutWeatherApp.MVVM.Model;

public class CalculateStatistics : ICalculateStatistics
{
    public StatisticsData GetStatisticsData(DateTime start, DateTime end, IHistoricalWeatherData? historicalData)
    {
        var statisticsData = new StatisticsData();
        var source = historicalData?
            .Where(data => data.Time >= start && data.Time <= end).ToArray();

        var minTempData = source?.MinBy(data => data.Temperature);
        var maxTempData = source?.MaxBy(data => data.Temperature);
        var minPressData = source?.MinBy(data => data.Pressure);
        var maxPressData = source?.MaxBy(data => data.Pressure);
        var maxWindData = source?.MaxBy(data => data.WindSpeed);
        var minHumData = source?.MinBy(data => data.Humidity);
        var maxHumData = source?.MaxBy(data => data.Humidity);
        var maxRainData = source?.MaxBy(data => data.Rain);


        statisticsData.DateTime = new DateTime?[8];
        statisticsData.MinTemp = minTempData?.Temperature;
        statisticsData.DateTime[0] = minTempData?.Time;
        statisticsData.MaxTemp = maxTempData?.Temperature;
        statisticsData.DateTime[1] = maxTempData?.Time;
        statisticsData.MinPress = minPressData?.Pressure;
        statisticsData.DateTime[2] = minPressData?.Time;
        statisticsData.MaxPress = maxPressData?.Pressure;
        statisticsData.DateTime[3] = maxPressData?.Time;
        statisticsData.MaxWind = maxWindData?.WindSpeed;
        statisticsData.DateTime[4] = maxWindData?.Time;
        statisticsData.MinHum = minHumData?.Humidity;
        statisticsData.DateTime[5] = minHumData?.Time;
        statisticsData.MaxHum = maxHumData?.Humidity;
        statisticsData.DateTime[6] = maxHumData?.Time;
        statisticsData.MaxRain = maxRainData?.Rain;
        statisticsData.DateTime[7] = maxRainData?.Time;
        
        return statisticsData;
    }

    public IEnumerable<HistoricalWeatherData>? GetDataFromPeriod(DateTime start, DateTime end,
        IHistoricalWeatherData? historicalData)
    {
        return historicalData?
            .Where(data => data.Time >= start && data.Time <= end);
    }
}