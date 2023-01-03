using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class ChooseLocationViewModel
{
    public RelayCommand AccessLocationData { get; set; }
    
    public ChooseLocationViewModel(ILocationRepository locationRepository)
    {
        AccessLocationData = new RelayCommand(async o =>
        {
            var searched = new SearchedLocationData();
            searched.Location = o as string;
            var locationData = await locationRepository.GetLocations(searched);

            Mediator.Mediator.GetInstance().OnEvent(this, new LocationDataMessage
            {
                MessageType = "LocationData",
                LocationData = locationData
            });
        });
    }
}

