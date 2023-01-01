using System.Collections.Generic;
using System.Linq;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.MVVM.ViewModel;

public class LocationListViewModel
{
    public static LocationData? LocationData1 { get; set;}
    public static LocationData? LocationData2 { get; set;}
    public static LocationData? LocationData3 { get; set;}
    public static LocationData? LocationData4 { get; set;}


    public static void SetLocationData(IEnumerable<LocationData>? locationData)
    {
        var enumerable = locationData as LocationData[] ?? locationData?.ToArray();
        if(enumerable != null && enumerable.Any())
            LocationData1 = enumerable.ElementAt(0);
        if(enumerable != null && enumerable.Length >= 2)
            LocationData2 = enumerable.ElementAt(1);
        if(enumerable != null && enumerable.Length >= 3)
            LocationData3 = enumerable.ElementAt(2);
        if(enumerable != null && enumerable.Length >= 4)
            LocationData4 = enumerable.ElementAt(3);
    }
}