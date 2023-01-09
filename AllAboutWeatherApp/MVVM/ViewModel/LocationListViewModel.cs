using System.Collections.Generic;
using System.Linq;
using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;
using AllAboutWeatherApp.MVVM.Model.DataAccessFactory;
using AllAboutWeatherApp.MVVM.Model.DataStorage;
using AllAboutWeatherApp.Strategy;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class LocationListViewModel : ObservableObject
{
    private LocationData? _locationData1;
    private LocationData? _locationData2;
    private LocationData? _locationData3;
    private LocationData? _locationData4;
    private string? _purpose;
    public RelayCommand AccessWeatherForecastData { get; set; }

    public string? Purpose
    {
        get => _purpose;
        set
        {
            _purpose = value;
            OnPropertyChanged();
        }
        
    }
    
    public LocationData? LocationData1
    {
        get => _locationData1;
        private set { 
            _locationData1 = value;
            OnPropertyChanged();
        }
    }

    public LocationData? LocationData2 
    {
        get => _locationData2;
        private set { 
            _locationData2 = value;
            OnPropertyChanged();
        }
    }
    public LocationData? LocationData3 
    {
        get => _locationData3;
        private set { 
            _locationData3 = value;
            OnPropertyChanged();
        }
    }
    public LocationData? LocationData4 
    {
        get => _locationData4;
        private set { 
            _locationData4 = value;
            OnPropertyChanged();
        }
    }

    public LocationListViewModel()
    {
        // Listen for messages from the mediator
        Mediator.Mediator.GetInstance().Event += (_, e) =>
        {
            // Check the message type to determine if the message is intended for this view model
            if (e is MediatorMessage {MessageType: "LocationData"} message)
            {
                // Update the LocationData properties with the location data from the message
                var locationDataMessage = (LocationDataMessage) message;
                SetLocationData(locationDataMessage.LocationData);
            }
        };
        
        
        AccessWeatherForecastData = new RelayCommand( o =>
        {
            var searched = new GeoCoordinates();
            var chosenOption = o as string;

            var locationData = chosenOption switch
            {
                "1" => _locationData1,
                "2" => _locationData2,
                "3" => _locationData3,
                "4" => _locationData4,
                _ => _locationData1
            };
            searched.Lat = locationData?.Lat;
            searched.Lon = locationData?.Lon;

            var context = new Context(new Repository(new DataRetrieverFactory()));
            switch (Purpose)
            {
                case "WeatherForecast" : { context.SetStrategy(new WeatherForecastStrategy()); } break;
                case "AirQuality": { context.SetStrategy(new AirQualityStrategy()); } break;
                case "Statistics": { context.SetStrategy(new StatisticsStrategy()); } break;
            }
            
            context.ExecuteStrategy(searched);
        });
    }

    private void SetLocationData(IEnumerable<LocationData>? locationData)
    {
        var enumerable = locationData as LocationData[] ?? locationData?.ToArray();
        if (enumerable != null && enumerable.Any())
            LocationData1 = enumerable.ElementAt(0);
        if (enumerable is {Length: >= 2})
            LocationData2 = enumerable.ElementAt(1);
        if (enumerable is {Length: >= 3})
            LocationData3 = enumerable.ElementAt(2);
        if (enumerable is {Length: >= 4})
            LocationData4 = enumerable.ElementAt(3);
    }
}
