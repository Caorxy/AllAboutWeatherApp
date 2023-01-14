using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.MVVM.Model;
using AllAboutWeatherApp.MVVM.Model.DataAccessFactory;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand AboutViewCommand { get; set; }
    public RelayCommand ChooseLocationCommand { get; set; }
    public RelayCommand LocationListViewCommand { get; set; }
    public RelayCommand DataViewCommand { get; set; }

    private HomeViewModel HomeVm { get; set; }
    public AboutViewModel AboutVm { get; set; }
    public LocationListViewModel LocationListVm { get; set; }
    public TypeLocationViewModel TypeLocationVm { get; set; }
    public WeatherForecastViewModel WeatherForecastVm { get; set; }
    public AirQualityViewModel AirQualityVm { get; set; }
    public StatisticsViewModel StatisticsVm { get; set; }
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
        ICalculateStatistics calculate = new CalculateStatistics();
        HomeVm = new HomeViewModel();
        AboutVm = new AboutViewModel();
        LocationListVm = new LocationListViewModel(repository);
        TypeLocationVm = new TypeLocationViewModel(repository);
        WeatherForecastVm = new  WeatherForecastViewModel();
        AirQualityVm = new  AirQualityViewModel();
        StatisticsVm = new StatisticsViewModel(calculate);
        CurrentView = HomeVm;

        HomeViewCommand = new RelayCommand(_ =>
        {
            CurrentView = HomeVm;
        });

        AboutViewCommand = new RelayCommand(_ =>
        {
            CurrentView = AboutVm;
        });
        
        ChooseLocationCommand = new RelayCommand(o =>
        {
            LocationListVm.Purpose = o as string;
            CurrentView = TypeLocationVm;
        });        
                
        DataViewCommand = new RelayCommand(_ =>
        {
            switch (LocationListVm.Purpose)
            {
                case "WeatherForecast" : CurrentView = WeatherForecastVm;
                    break;
                case "AirQuality" : CurrentView = AirQualityVm;
                    break; 
                case "Statistics" : CurrentView = StatisticsVm;
                    break;
            }
            
        });        
        
        LocationListViewCommand = new RelayCommand( _ =>
        {
            CurrentView = LocationListVm;
        });

    }
}