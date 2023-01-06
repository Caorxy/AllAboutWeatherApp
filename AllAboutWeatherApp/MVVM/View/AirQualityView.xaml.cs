using System.Windows;
using System.Windows.Controls;

namespace AllAboutWeatherApp.MVVM.View;

public partial class AirQualityView : UserControl
{
    public AirQualityView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}