using System.Windows;
using System.Windows.Controls;
using AllAboutWeatherApp.MVVM.ViewModel;

namespace AllAboutWeatherApp.MVVM.View;

public partial class AboutView : UserControl
{
    public AboutView()
    {
        InitializeComponent();
        HomeButton.DataContext = Application.Current.MainWindow?.DataContext;
        BorderAbout.DataContext = (DataContext as MainViewModel)?.AboutVm;
        BottomText.DataContext = (DataContext as MainViewModel)?.AboutVm;
    }
}