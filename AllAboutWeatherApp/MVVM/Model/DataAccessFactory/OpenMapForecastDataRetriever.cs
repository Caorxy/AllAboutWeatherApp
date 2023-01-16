using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.DataAccessFactory;

public class OpenMapForecastDataRetriever : IOpenMapForecastDataRetriever
{
    private readonly HttpClient _client;

    public OpenMapForecastDataRetriever()
    {
        _client = new HttpClient();
    }
    
    public async Task<HistoricalData> GetForecastData(GeoCoordinates coordinates)
    {
        HistoricalData data = null!;
        string requestUri = GetGeoRequestUri(coordinates);

        HttpResponseMessage response = await _client.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
            data = await response.Content.ReadAsAsync<HistoricalData>();

        return data;
    }


    private string GetGeoRequestUri(GeoCoordinates coordinates)
    {

        var lat = (double)coordinates.Lat!;
        var lon = (double)coordinates.Lon!;

        
        return $"https://api.open-meteo.com/v1/forecast?latitude={lat.ToString(CultureInfo.InvariantCulture)}&longitude={lon.ToString(CultureInfo.InvariantCulture)}&hourly=temperature_2m,relativehumidity_2m,pressure_msl,rain,windspeed_10m&timezone=GMT";
    }

}