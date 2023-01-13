using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class StatisticsViewModel : ObservableObject
{
    public RelayCommand Button1Clicked { get; set; }
    public RelayCommand Button2Clicked { get; set; }
    public RelayCommand Button3Clicked { get; set; }
    public RelayCommand Button4Clicked { get; set; }
    public RelayCommand ListViewCommand { get; set; }
    public RelayCommand GraphViewCommand { get; set; }
    private StatisticsData? _statistics;
    private string[] _opacity;
    private string[] _opacitySide;
    private string? _isDatePickerVisible;
        
    public StatisticsData? Statistics
    {
        get => _statistics;
        private set
        {
            _statistics = value;
            OnPropertyChanged();
        }
    }

    public string? IsDatePickerVisible
    {
        get => _isDatePickerVisible;
        private set
        {
            _isDatePickerVisible = value;
            OnPropertyChanged();
        }
    }

    public string[] Opacity
    {
        get => _opacity;
        private set
        {
            _opacity = value;
            OnPropertyChanged();
        }
    }

    public string[] OpacitySide
    {
        get => _opacitySide;
        private set
        {
            _opacitySide = value;
            OnPropertyChanged();
        }
    }

    public StatisticsViewModel()
    {
        
        // Nasluchuje eventow od agregatora
        Mediator.Mediator.GetInstance().Event += (_, e) =>
        {
            // Sprawdza czy wiadomosc jest przeznaczona dla niego
            if (e is not MediatorMessage {MessageType: "StatisticsData"} message) return;
            // aktualizuje swoje pola danymi ktore otrzymal
            var statisticsDataMessage = (StatisticsDataMessage) message;
            if (statisticsDataMessage.StatisticsData != null)
                Statistics = statisticsDataMessage.StatisticsData;
        };

        _isDatePickerVisible = "Collapsed";
        _opacity = new[] { "1", "0.2", "0.2", "0.2" };
        _opacitySide = new[] { "1", "0.7" };
        
        Button1Clicked = new RelayCommand( _ =>
        {
            IsDatePickerVisible = "Collapsed";
            Opacity = new[] { "1", "0.2", "0.2", "0.2" };
        });        
        Button2Clicked = new RelayCommand( _ =>
        {
            IsDatePickerVisible = "Collapsed";
            Opacity = new[] { "0.2", "1", "0.2", "0.2" };
        });        
        Button3Clicked = new RelayCommand( _ =>
        {
            IsDatePickerVisible = "Collapsed";
            Opacity = new[] { "0.2", "0.2", "1", "0.2" };
        });        
        Button4Clicked = new RelayCommand( _ =>
        {
            IsDatePickerVisible = "Visible";
            Opacity = new[] { "0.2", "0.2", "0.2", "1" };
        });      
        ListViewCommand = new RelayCommand( _ =>
        {
            OpacitySide = new[] { "1", "0.7" };
        });    
        GraphViewCommand = new RelayCommand( _ =>
        {
            OpacitySide = new[] { "0.7", "1" };
        });
        
    }
}