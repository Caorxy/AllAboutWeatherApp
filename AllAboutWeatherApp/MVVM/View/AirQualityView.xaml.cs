using System.Windows;

namespace AllAboutWeatherApp.MVVM.View;

public partial class AirQualityView
{
    public AirQualityView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}