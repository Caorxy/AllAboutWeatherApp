using AllAboutWeatherApp.MVVM.Model;
using AllAboutWeatherApp.Core;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class StatisticsViewModel : ObservableObject
{
    private OpenWeatherForecast? _weatherForecastData;
    private string? _aboutLocation;
    public RelayCommand GetWeatherData { get; set; }

    public OpenWeatherForecast? WeatherForecastData
    {
        get => _weatherForecastData;
        set
        {
            _weatherForecastData = value;
            OnPropertyChanged();
        }
    }  
    public string? AboutLocation
    {
        get => _aboutLocation;
        set
        {
            _aboutLocation = value;
            OnPropertyChanged();
        }
    }

    public StatisticsViewModel(IRepository repository)
    {
        GetWeatherData = new RelayCommand(async _ =>
        {
            var x = RandomGeoCoordinates.GetRandomGeoCoordinates();
            var locationData = await repository.GetLocations(x);
            var y = locationData as LocationData[];
            var z = new GeoCoordinates { Lat = y[0].Lat, Lon = y[0].Lon};
            WeatherForecastData = await  repository.GetWeatherForecast(z);
        });
    }
}