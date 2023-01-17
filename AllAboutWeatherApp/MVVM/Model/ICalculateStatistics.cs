using System;
using System.Collections.Generic;
using AllAboutWeatherApp.MVVM.Model.DataStorage;
using AllAboutWeatherApp.MVVM.Model.HistoricalDataCollection;

namespace AllAboutWeatherApp.MVVM.Model;

public interface ICalculateStatistics
{
    public StatisticsData GetStatisticsData(DateTime start, DateTime end, IHistoricalWeatherData? historicalData);

    public IEnumerable<HistoricalWeatherData>? GetDataFromPeriod(DateTime start, DateTime end,
        IHistoricalWeatherData? historicalData);
}