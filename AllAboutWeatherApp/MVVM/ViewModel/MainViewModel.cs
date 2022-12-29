using System.Windows.Input;
using AllAboutWeatherApp.Core;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand AboutViewCommand { get; set; }
    public RelayCommand ChooseLocationCommand { get; set; }
    public ICommand ChangeViewCommand { get; set; }

    public HomeViewModel HomeVm { get; set; }
    public AboutViewModel AboutVm { get; set; }
    public ChooseLocationViewModel ChooseLocationVm { get; set; }
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
        HomeVm = new HomeViewModel();
        AboutVm = new AboutViewModel();
        ChooseLocationVm = new ChooseLocationViewModel();
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
            CurrentView = ChooseLocationVm;
        });
        
        ChangeViewCommand = new RelayCommand(view =>
        {
            CurrentView = view;
        });
    }
}