using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand AboutViewCommand { get; set; }
    public RelayCommand ChooseLocationCommand { get; set; }
    public RelayCommand LocationListViewCommand { get; set; }
    public RelayCommand WeatherForecastViewCommand { get; set; }

    public HomeViewModel HomeVm { get; set; }
    public AboutViewModel AboutVm { get; set; }
    public LocationListViewModel LocationListVm { get; set; }
    public TypeLocationViewModel TypeLocationVm { get; set; }
    public WeatherForecastViewModel WeatherForecastVm { get; set; }
    private object? _currentView;

    public object? CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }
    
    
    
    public MainViewModel()
    {
        IRepository repository = new Repository(new DataRetrieverFactory());
        HomeVm = new HomeViewModel();
        AboutVm = new AboutViewModel();
        LocationListVm = new LocationListViewModel(repository);
        TypeLocationVm = new TypeLocationViewModel(repository);
        WeatherForecastVm = new  WeatherForecastViewModel();
        CurrentView = HomeVm;

        HomeViewCommand = new RelayCommand(_ =>
        {
            CurrentView = HomeVm;
        });

        AboutViewCommand = new RelayCommand(_ =>
        {
            CurrentView = AboutVm;
        });
        
        ChooseLocationCommand = new RelayCommand(_ =>
        {
            CurrentView = TypeLocationVm;
        });        
                
        WeatherForecastViewCommand = new RelayCommand(_ =>
        {
            CurrentView = WeatherForecastVm;
        });        
        
        LocationListViewCommand = new RelayCommand( _ =>
        {
            CurrentView = LocationListVm;
        });

    }
}