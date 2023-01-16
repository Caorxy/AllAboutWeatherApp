using System.Windows;
using System.Windows.Controls;

namespace AllAboutWeatherApp.MVVM.View;

public partial class WeatherForecastDefaultView : UserControl
{
    public WeatherForecastDefaultView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}