using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class TypeLocationViewModel
{
    public RelayCommand AccessLocationData { get; set; }
    
    public TypeLocationViewModel(IRepository repository)
    {
        AccessLocationData = new RelayCommand(async o =>
        {
            var searched = new SearchedLocationData();
            searched.Location = o as string;
            var locationData = await repository.GetLocations(searched);

            Mediator.Mediator.GetInstance().OnEvent(this, new LocationDataMessage
            {
                MessageType = "LocationData",
                LocationData = locationData
            });
        });
    }
}

