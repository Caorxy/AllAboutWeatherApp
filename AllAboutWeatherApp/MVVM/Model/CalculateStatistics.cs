using System;
using System.Linq;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model;

public class CalculateStatistics : ICalculateStatistics
{
    public StatisticsData GetStatisticsData(DateTime start, DateTime end, HistoricalData? data)
    {
        var statisticsData = new StatisticsData();

        var startId = (int)data?.HourlyWeatherInfo?.Time?
            .Select((val, index) => Tuple.Create(val, index))
            .First(x => x.Item1.Year == start.Year && x.Item1.Month == start.Month && x.Item1.Day == start.Day)
            .Item2!;
        var endId = (int)data.HourlyWeatherInfo?.Time?
            .Select((val, index) => Tuple.Create(val, index))
            .First(x => x.Item1.Year == end.Year && x.Item1.Month == end.Month && x.Item1.Day == end.Day)
            .Item2!;

        const double tolerance = 0.01;
        double minTemp = (double)Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Temperature?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .Min(x => x.Item1)!;
        var minTempId = Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Temperature?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .First(x => Math.Abs((double)x.Item1! - minTemp) <= tolerance)
            .Item2;
        
        double maxTemp =  (double)Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Temperature?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .Max(x => x.Item1)!;
        var maxTempId = Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Temperature?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .First(x => Math.Abs((double)x.Item1! - maxTemp) <= tolerance)
            .Item2;
        
        double minPress = (double)Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Pressure?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .Min(x => x.Item1)!;
        var minPressId = Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Pressure?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .First(x => Math.Abs((double)x.Item1! - minPress) <= tolerance)
            .Item2;
        
        double maxPress = (double)Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Pressure?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .Max(x => x.Item1)!;
        var maxPressId = Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Pressure?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .First(x => Math.Abs((double)x.Item1! - maxPress) <= tolerance)
            .Item2;    
        
        double maxWind = (double)Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.WindSpeed?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .Max(x => x.Item1)!;
        var maxWindId = (int)data.HourlyWeatherInfo?.WindSpeed?
            .Select((val, index) => Tuple.Create(val, index))
            .First(x => Math.Abs((double)x.Item1! - maxWind) <= tolerance)
            .Item2!;
        
        double minHum = (double)Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Humidity?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .Min(x => x.Item1)!;
        var minHumId = Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Humidity?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .First(x => Math.Abs((double)x.Item1! - minHum) <= tolerance)
            .Item2;
        
        double maxHum = (double)Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Humidity?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .Max(x => x.Item1)!;
        var maxHumId = (int)data.HourlyWeatherInfo?.Humidity?
            .Select((val, index) => Tuple.Create(val, index))
            .First(x => Math.Abs((double)x.Item1! - maxHum) <= tolerance)
            .Item2!;
        
        double maxRain = (double)Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Rain?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .Max(x => x.Item1)!;
        var maxRainId = Enumerable.Range(startId, endId - startId + 1)
            .Select(i => data.HourlyWeatherInfo?.Rain?[i])
            .ToArray()
            .Select((val, index) => Tuple.Create(val, index))
            .First(x => Math.Abs((double)x.Item1! - maxRain) <= tolerance)
            .Item2;
        
        statisticsData.DateTime = new DateTime?[8];
        statisticsData.MinTemp = minTemp;
        statisticsData.DateTime[0] = data.HourlyWeatherInfo?.Time?[minTempId];
        statisticsData.MaxTemp = maxTemp;
        statisticsData.DateTime[1] = data.HourlyWeatherInfo?.Time?[maxTempId];
        statisticsData.MinPress = minPress;
        statisticsData.DateTime[2] = data.HourlyWeatherInfo?.Time?[minPressId];
        statisticsData.MaxPress = maxPress;
        statisticsData.DateTime[3] = data.HourlyWeatherInfo?.Time?[maxPressId];
        statisticsData.MaxWind = maxWind;
        statisticsData.DateTime[4] = data.HourlyWeatherInfo?.Time?[maxWindId];
        statisticsData.MinHum = minHum;
        statisticsData.DateTime[5] = data.HourlyWeatherInfo?.Time?[minHumId];
        statisticsData.MaxHum = maxHum;
        statisticsData.DateTime[6] = data.HourlyWeatherInfo?.Time?[maxHumId];
        statisticsData.MaxRain = maxRain;
        statisticsData.DateTime[7] = data.HourlyWeatherInfo?.Time?[maxRainId];
        
        return statisticsData;
    }
}