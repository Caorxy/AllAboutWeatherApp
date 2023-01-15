using System;
using System.Collections.Generic;
using AllAboutWeatherApp.MVVM.Model.Composite;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model;

public interface ICalculateStatistics
{
    public StatisticsData GetStatisticsData(DateTime start, DateTime end, IWeatherData? historicalData);

    public IEnumerable<HistoricalWeatherData>? GetDataFromPeriod(DateTime start, DateTime end,
        IWeatherData? historicalData);
}