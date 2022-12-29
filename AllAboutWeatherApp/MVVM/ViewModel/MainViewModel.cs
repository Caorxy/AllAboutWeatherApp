using System.Diagnostics;
using System.Windows.Input;
using AllAboutWeatherApp.Core;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand AboutViewCommand { get; set; }
    public ICommand ChangeViewCommand { get; set; }

    public HomeViewModel HomeVm { get; set; }
    public AboutViewModel AboutVm { get; set; }
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
        CurrentView = HomeVm;

        HomeViewCommand = new RelayCommand(_ =>
        {
            CurrentView = HomeVm;
        });

        AboutViewCommand = new RelayCommand(_ =>
        {
            CurrentView = AboutVm;
        });
        
        ChangeViewCommand = new RelayCommand(view =>
        {
            CurrentView = view;
        });
    }
}