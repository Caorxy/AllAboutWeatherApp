using System;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model;

public interface ICalculateStatistics
{
    public StatisticsData GetStatisticsData(DateTime start, DateTime end, HistoricalData? data);
}