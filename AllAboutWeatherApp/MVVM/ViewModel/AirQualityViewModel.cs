using System.Collections.Generic;
using System.Linq;
using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class AirQualityViewModel : ObservableObject
{
    private AirQualityData? _airQuality;
    private AirPollutionData? _mainAirPollutionData;
    private Dictionary<string, double>? _pm25data;
    private Dictionary<string, double>? _pm10data;

    private AirQualityData? AirQuality
    {
        get => _airQuality;
        set
        {
            _airQuality = value; 
            OnPropertyChanged();
        }
    }    
    public AirPollutionData? MainAirPollutionData
    {
        get => _mainAirPollutionData;
        private set
        {
            _mainAirPollutionData = value; 
            OnPropertyChanged();
        }
    }

    public Dictionary<string, double>? Pm25Data
    {
        get => _pm25data;
        set
        {
            _pm25data = value;
            OnPropertyChanged();
        }
    }
    
    public Dictionary<string, double>? Pm10Data
    {
        get => _pm10data;
        set
        {
            _pm10data = value;
            OnPropertyChanged();
        }
    }

    public AirQualityViewModel()
    {
        // Listen for messages from the mediator
        Mediator.Mediator.GetInstance().Event += (_, e) =>
        {
            // Check the message type to determine if the message is intended for this view model
            if (e is not MediatorMessage {MessageType: "AirQualityData"} message) return;
            // Update the AirQualityData properties with the data from the message
            var airQualityDataMessage = (AirQualityDataMessage) message;
            AirQuality = airQualityDataMessage.AirQualityData;
            MainAirPollutionData = AirQuality?.List?[^1];
            var dic1 = new Dictionary<string, double>();
            var dic2 = new Dictionary<string, double>();
            if (AirQuality?.List == null) return;
            foreach (var data in AirQuality.List.Where(data => data.Components != null))
            {
                if (data.Components == null) continue;
                dic1?.Add(data.DtFormat.TimeOfDay.ToString()[..5], data.Components.Pm2_5); 
                dic2?.Add(data.DtFormat.TimeOfDay.ToString()[..5], data.Components.Pm10); 
            }
            Pm25Data = dic1;
            Pm10Data = dic2;
        };
    }


}