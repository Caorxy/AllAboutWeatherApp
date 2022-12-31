using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class ChooseLocationViewModel
{
    public RelayCommand AccessLocationData { get; set; }
    
    public ChooseLocationViewModel()
    {
        GeoCodingSingleton.GeoCodingClient geoClient = new GeoCodingSingleton.GeoCodingClient();
        AccessLocationData = new RelayCommand(async o =>
        {
            SearchedLocationData searched = new SearchedLocationData();
            searched.Location = o as string;
            await geoClient.GetLocations(searched);
        });
    }
}