using System.Collections.Generic;
using System.Linq;
using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class AirQualityViewModel : ObservableObject
{
    private AirQualityData? _airQuality;
    private AirPollutionData? _mainAirPollutionData;
    private Dictionary<double, double>? _pm25data;
    private Dictionary<double, double>? _pm10data;

    public AirQualityData? AirQuality
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
        set
        {
            _mainAirPollutionData = value; 
            OnPropertyChanged();
        }
    }

    public Dictionary<double, double>? Pm25Data
    {
        get => _pm25data;
        set
        {
            _pm25data = value;
            OnPropertyChanged();
        }
    }
    
    public Dictionary<double, double>? Pm10Data
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
            if (e is MediatorMessage message && message.MessageType == "AirQualityData")
            {
                // Update the AirQualityData properties with the data from the message
                var airQualityDataMessage = (AirQualityDataMessage) message;
                AirQuality = airQualityDataMessage.AirQualityData;
                MainAirPollutionData = AirQuality?.List?[0];
                Pm25Data = new Dictionary<double, double>();
                Pm10Data = new Dictionary<double, double>();
                if (AirQuality?.List == null) return;
                foreach (var data in AirQuality.List.Where(data => data.Components != null))
                {
                    if (data.Components == null) continue;
                    Pm25Data.Add(data.Components.Pm2_5, data.Dt);
                    Pm10Data.Add(data.Components.Pm10, data.Dt);
                }
            }
        };
    }


}