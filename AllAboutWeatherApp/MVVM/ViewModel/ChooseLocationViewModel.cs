using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class ChooseLocationViewModel
{
    public RelayCommand AccessLocationData { get; set; }
    
    public ChooseLocationViewModel(ILocationRepository locationRepository, LocationListViewModel locationListViewModel)
    {
        AccessLocationData = new RelayCommand(async o =>
        {
            SearchedLocationData searched = new SearchedLocationData();
            searched.Location = o as string;
            locationListViewModel.SetLocationData(await locationRepository.GetLocations(searched));
        });
    }
}
