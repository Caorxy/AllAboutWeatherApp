﻿using System.Windows;
using System.Windows.Controls;

namespace AllAboutWeatherApp.MVVM.View;

public partial class AboutView : UserControl
{
    public AboutView()
    {
        InitializeComponent();
        DataContext = Application.Current.MainWindow?.DataContext;
    }
}