using System;

namespace AllAboutWeatherApp.Mediator;

public class Mediator
{
    private static Mediator instance;

    private Mediator() {}

    public static Mediator GetInstance()
    {
        if (instance == null)
        {
            instance = new Mediator();
        }
        return instance;
    }

    public event EventHandler<object> Event;

    public void OnEvent(object sender, object e)
    {
        Event.Invoke(sender, e);
    }
}
