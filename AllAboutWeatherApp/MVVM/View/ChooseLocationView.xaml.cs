using System.Windows;
using System.Windows.Controls;
using AllAboutWeatherApp.MVVM.ViewModel;

namespace AllAboutWeatherApp.MVVM.View;

public partial class ChooseLocationView : UserControl
{
    public ChooseLocationView()
    {
        InitializeComponent();
        HomeButton.DataContext = Application.Current.MainWindow?.DataContext;
    }
}