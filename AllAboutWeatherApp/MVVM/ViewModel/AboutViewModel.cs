﻿using System.Diagnostics;
using AllAboutWeatherApp.Core;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class AboutViewModel : ObservableObject
{
    public RelayCommand HyperlinkCommand { get; set; }

    public AboutViewModel()
    {
        HyperlinkCommand = new RelayCommand(o =>
        {
            string? url = o as string;
            if (url != null) Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        });
    }
}