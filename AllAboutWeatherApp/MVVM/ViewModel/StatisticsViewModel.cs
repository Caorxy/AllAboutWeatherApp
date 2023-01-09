using AllAboutWeatherApp.Core;
using AllAboutWeatherApp.Mediator;
using AllAboutWeatherApp.MVVM.Model;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class StatisticsViewModel : ObservableObject
{
    private StatisticsData? _statistics;

    public StatisticsData? Statistics
    {
        get => _statistics;
        private set
        {
            _statistics = value;
            OnPropertyChanged();
        }
    }

    public StatisticsViewModel()
    {
        
        // Listen for messages from the mediator
        Mediator.Mediator.GetInstance().Event += (_, e) =>
        {
            // Check the message type to determine if the message is intended for this view model
            if (e is not MediatorMessage {MessageType: "StatisticsData"} message) return;
            // Update the LocationData properties with the location data from the message
            var statisticsDataMessage = (StatisticsDataMessage) message;
            if (statisticsDataMessage.StatisticsData != null)
                Statistics = statisticsDataMessage.StatisticsData;
        };
    }
}