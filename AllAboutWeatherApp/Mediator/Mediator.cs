using System;

namespace AllAboutWeatherApp.Mediator;

public class Mediator
{
    private static Mediator? _instance;
    public event EventHandler<object>? Event;

    private Mediator() {}

    public static Mediator GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Mediator();
        }
        return _instance;
    }
    
    public void OnEvent(object sender, object e)
    {
        Event?.Invoke(sender, e);
    }
}
