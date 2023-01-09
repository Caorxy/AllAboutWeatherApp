using System.Windows;
namespace AllAboutWeatherApp.MVVM.View;

public partial class StatisticsView 
{
    public StatisticsView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}