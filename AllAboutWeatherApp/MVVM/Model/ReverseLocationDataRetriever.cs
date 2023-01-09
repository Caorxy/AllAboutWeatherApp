using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public class ReverseLocationDataRetriever : IReverseLocationDataRetriever
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public ReverseLocationDataRetriever()
    {
        _client = new HttpClient();
        _apiKey = "005acbc1300f9c9027a264d7028893f6";
    }

    public async Task<IEnumerable<LocationData>> GetLocations(GeoCoordinates coordinates)
    {
        IEnumerable<LocationData> locations = null!;
        string requestUri = GetGeoRequestUri(coordinates);

        HttpResponseMessage response = await _client.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
            locations = await response.Content.ReadAsAsync<IEnumerable<LocationData>>();

        return locations;
    }

    private string GetGeoRequestUri(GeoCoordinates coordinates) =>
        $"https://api.openweathermap.org/geo/1.0/reverse?lat={coordinates.Lat}&lon={coordinates.Lon}&appid={_apiKey}";

}