using System;

namespace AllAboutWeatherApp.MVVM.Model;

public static class RandomGeoCoordinates
{
    public static  GeoCoordinates GetRandomGeoCoordinates()
    {
        var random = new Random();
        var coordinates = new GeoCoordinates
        {
            Lat = random.NextDouble() * 180 - 90,
            Lon =random.NextDouble() * 360 - 180
        };
        return coordinates;
    }
}