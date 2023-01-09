using System.Windows;
namespace AllAboutWeatherApp.MVVM.View;

public partial class WeatherForecastView 
{
    public WeatherForecastView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}