using System.Windows;

namespace AllAboutWeatherApp.MVVM.View;

public partial class AboutView
{
    public AboutView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}