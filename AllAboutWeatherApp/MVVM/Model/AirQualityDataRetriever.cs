using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public class AirQualityDataRetriever : IAirQualityDataRetriever
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public AirQualityDataRetriever()
    {
        _client = new HttpClient();
        _apiKey = "005acbc1300f9c9027a264d7028893f6";
    }

    public async Task<IEnumerable<AirQualityData>> GetAirQualityData(GeoCoordinates coordinates)
    {
        IEnumerable<AirQualityData> airQuality = null!;
        string requestUri = GetGeoRequestUri(coordinates);

        HttpResponseMessage response = await _client.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
            airQuality = await response.Content.ReadAsAsync<IEnumerable<AirQualityData>>();

        return airQuality;
    }

    private string GetGeoRequestUri(GeoCoordinates coordinates) =>
        $"https://api.openweathermap.org/data/2.5/air_pollution?lat={coordinates.Lat}&lon={coordinates.Lon}&appid={_apiKey}";
}