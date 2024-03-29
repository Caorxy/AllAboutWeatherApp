﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AllAboutWeatherApp.MVVM.Model.DataAccessFactory;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model;

public class Repository : IRepository
{
    private readonly IDataRetrieverFactory _dataRetrieverFactory;

    public Repository(IDataRetrieverFactory dataRetrieverFactory)
    {
        _dataRetrieverFactory = dataRetrieverFactory;
    }

    public async Task<IEnumerable<LocationData>> GetLocations(SearchedLocationData locationSearchData)
    {
        ILocationDataRetriever locationDataRetriever = _dataRetrieverFactory.CreateLocationDataRetriever();
        return await locationDataRetriever.GetLocations(locationSearchData);
    } 
    
    public async Task<OpenWeatherForecast> GetWeatherForecast(GeoCoordinates coordinates)
    {
        IWeatherDataRetriever weatherDataRetriever = _dataRetrieverFactory.CreateWeatherDataRetriever();
        return await weatherDataRetriever.GetWeatherForecast(coordinates);
    }    
    
    public async Task<AirQualityData> GetAirQualityData(GeoCoordinates coordinates)
    {
        IAirQualityDataRetriever airQualityDataRetriever = _dataRetrieverFactory.CreateAirQualityDataRetriever();
        return await airQualityDataRetriever.GetAirQualityData(coordinates);
    }
    
    public async Task<HistoricalData> GetHistoricalData(GeoCoordinates coordinates)
    {
        IHistoricalDataRetriever historicalDataRetriever = _dataRetrieverFactory.CreateHistoricalDataRetriever();
        return await historicalDataRetriever.GetHistoricalData(coordinates);
    }    
    
    public async Task<HistoricalData> GetOpenMapForecast(GeoCoordinates coordinates)
    {
        IOpenMapForecastDataRetriever openMapDataRetriever = _dataRetrieverFactory.CreateOpenMapForecastDataRetriever();
        return await openMapDataRetriever.GetForecastData(coordinates);
    }
}

