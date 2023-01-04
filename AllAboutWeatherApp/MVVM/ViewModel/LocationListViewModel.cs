using System.Collections.Generic;
using System.Linq;
using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class LocationListViewModel : ObservableObject
{
    private LocationData? _locationData1;
    private LocationData? _locationData2;
    private LocationData? _locationData3;
    private LocationData? _locationData4;
    public RelayCommand AccessWeatherForecastData { get; set; }

    public LocationData? LocationData1
    {
        get => _locationData1;
        set { 
            _locationData1 = value;
            OnPropertyChanged();
        }
    }

    public LocationData? LocationData2 
    {
        get => _locationData2;
        set { 
            _locationData2 = value;
            OnPropertyChanged();
        }
    }
    public LocationData? LocationData3 
    {
        get => _locationData3;
        set { 
            _locationData3 = value;
            OnPropertyChanged();
        }
    }
    public LocationData? LocationData4 
    {
        get => _locationData4;
        set { 
            _locationData4 = value;
            OnPropertyChanged();
        }
    }

    public LocationListViewModel(IRepository repository)
    {
        // Listen for messages from the mediator
        Mediator.Mediator.GetInstance().Event += (_, e) =>
        {
            // Check the message type to determine if the message is intended for this view model
            if (e is MediatorMessage message && message.MessageType == "LocationData")
            {
                // Update the LocationData properties with the location data from the message
                var locationDataMessage = (LocationDataMessage) message;
                SetLocationData(locationDataMessage.LocationData);
            }
        };
        
        
        AccessWeatherForecastData = new RelayCommand(async o =>
        {
            var searched = new GeoCoordinates();
            var choosenOption = o as string;
            LocationData? locationData;

            switch (choosenOption)
            {
                case "1": locationData = _locationData1; break;
                case "2": locationData = _locationData2; break;
                case "3": locationData = _locationData3; break;
                case "4": locationData = _locationData4; break;
                default: locationData = _locationData1;
                    break;
            }
            searched.Lat = locationData?.Lat;
            searched.Lon = locationData?.Lon;
            var forecastData = await repository.GetWeatherForecast(searched);

            Mediator.Mediator.GetInstance().OnEvent(this, new ForecastDataMessage
            {
                MessageType = "ForecastData",
                ForecastData = forecastData
            });
        });
    }

    public void SetLocationData(IEnumerable<LocationData>? locationData)
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
