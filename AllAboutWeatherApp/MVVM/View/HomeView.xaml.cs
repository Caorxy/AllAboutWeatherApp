using System.Windows;
namespace AllAboutWeatherApp.MVVM.View;

public partial class HomeView 
{
    public HomeView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}