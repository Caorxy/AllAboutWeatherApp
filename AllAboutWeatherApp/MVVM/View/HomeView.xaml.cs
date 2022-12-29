using System.Windows;
using System.Windows.Controls;
namespace AllAboutWeatherApp.MVVM.View;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}