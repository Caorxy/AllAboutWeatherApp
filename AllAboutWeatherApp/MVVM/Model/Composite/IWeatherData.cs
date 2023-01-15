using System.Collections.Generic;

namespace AllAboutWeatherApp.MVVM.Model.Composite;

public interface IWeatherData : IEnumerable<HistoricalWeatherData>
{
}
