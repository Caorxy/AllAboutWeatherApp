using System.Windows;
using System.Windows.Controls;

namespace AllAboutWeatherApp.MVVM.View;

public partial class LocationListView : UserControl
{
    public LocationListView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}