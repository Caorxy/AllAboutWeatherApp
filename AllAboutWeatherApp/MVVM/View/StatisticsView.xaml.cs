using System.Windows;
using System.Windows.Controls;

namespace AllAboutWeatherApp.MVVM.View;

public partial class StatisticsView : UserControl
{
    public StatisticsView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}