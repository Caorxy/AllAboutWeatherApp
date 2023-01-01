using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AllAboutWeatherApp.MVVM.Model;

public class LocationRepository : ILocationRepository
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public LocationRepository()
    {
        _client = new HttpClient();
        _apiKey = "005acbc1300f9c9027a264d7028893f6";
    }

    public async Task<IEnumerable<LocationData>> GetLocations(SearchedLocationData locationSearchData)
    {
        IEnumerable<LocationData> locations = null!;
        string requestUri = GetGeoRequestUri(locationSearchData);

        HttpResponseMessage response = await _client.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
            locations = await response.Content.ReadAsAsync<IEnumerable<LocationData>>();

        return locations;
    }

    private string GetGeoRequestUri(SearchedLocationData locationSearchData) =>
        $"https://api.openweathermap.org/geo/1.0/direct?q={locationSearchData.Location}&limit=5&appid={_apiKey}";
}