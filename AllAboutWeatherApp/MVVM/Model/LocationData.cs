namespace AllAboutWeatherApp.MVVM.Model;

public class LocationData
{
    public string? Name { get; set; }
/*
    public object? LocalNames { get; set; }
*/
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
}
