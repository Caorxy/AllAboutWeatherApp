using System.Collections.Generic;
using AllAboutWeatherApp.MVVM.Model;

namespace AllAboutWeatherApp.Mediator;

public abstract class MediatorMessage
{
    public string? MessageType { get; set; }
}

public class LocationDataMessage : MediatorMessage
{
    public IEnumerable<LocationData>? LocationData { get; set; }
}
