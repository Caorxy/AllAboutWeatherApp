using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public class WeatherDataRetriever : IWeatherDataRetriever
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public WeatherDataRetriever()
    {
        _client = new HttpClient();
        _apiKey = "005acbc1300f9c9027a264d7028893f6";
    }

    public async Task<OpenWeatherForecast> GetWeatherForecast(GeoCoordinates coordinates)
    {
        OpenWeatherForecast forecast = null!;
        string requestUri = GetGeoRequestUri(coordinates);

        HttpResponseMessage response = await _client.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
            forecast = await response.Content.ReadAsAsync<OpenWeatherForecast>();
        
        return CorrectReceivedData(forecast);
    }


    private string GetGeoRequestUri(GeoCoordinates coordinates) =>
        $"https://api.openweathermap.org/data/2.5/forecast?lat={coordinates.Lat}&lon={coordinates.Lon}&appid={_apiKey}&units=metric";

    private OpenWeatherForecast CorrectReceivedData(OpenWeatherForecast forecastData)
    {
        if (forecastData.List != null)
            foreach (var forecast in forecastData.List)
            {
                if (forecast is {Rain: null})
                    forecast.Rain = new RainData {ThreeH = 0};

                forecast!.PathToIcon = "../../Images/weatherForecastIcons/" + forecast.Weather![0].Icon + ".png";
                forecast.Main!.Temp = Math.Round(forecast.Main.Temp);
            }

        return forecastData;
    }
}