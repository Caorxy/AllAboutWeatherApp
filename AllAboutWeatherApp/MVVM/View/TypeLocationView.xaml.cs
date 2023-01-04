using System.Windows;
using System.Windows.Controls;
using AllAboutWeatherApp.MVVM.ViewModel;

namespace AllAboutWeatherApp.MVVM.View;

public partial class TypeLocationView : UserControl
{
    public TypeLocationView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}