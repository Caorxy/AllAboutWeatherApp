using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.DataAccessFactory;

public class LocationDataRetriever : ILocationDataRetriever
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public LocationDataRetriever()
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
