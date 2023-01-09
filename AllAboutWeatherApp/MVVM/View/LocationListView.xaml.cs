using System.Windows;
namespace AllAboutWeatherApp.MVVM.View;

public partial class LocationListView 
{
    public LocationListView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}