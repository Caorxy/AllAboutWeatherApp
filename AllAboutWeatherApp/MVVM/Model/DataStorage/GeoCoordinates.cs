using System.ComponentModel.DataAnnotations;

namespace AllAboutWeatherApp.MVVM.Model.DataStorage;

public class GeoCoordinates
{
    [Required] public double? Lat;
    [Required] public double? Lon;
}