using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.DataAccessFactory;

public class HistoricalDataRetriever : IHistoricalDataRetriever
{
    private readonly HttpClient _client;

    public HistoricalDataRetriever()
    {
        _client = new HttpClient();
    }

    public async Task<HistoricalData> GetHistoricalData(GeoCoordinates coordinates)
    {
        HistoricalData forecast = null!;
        string requestUri = GetGeoRequestUri(coordinates);

        HttpResponseMessage response = await _client.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
            forecast = await response.Content.ReadAsAsync<HistoricalData>();

        return forecast;
    }


    private string GetGeoRequestUri(GeoCoordinates coordinates)
    {
        var lat = (double)coordinates.Lat!;
        var lon = (double)coordinates.Lon!;
        var infos = new string[3];
        infos[0] = DateTime.Now.AddDays(-20).Year.ToString();
        infos[1] = DateTime.Now.AddDays(-20).Month.ToString();
        infos[2] = DateTime.Now.AddDays(-20).Day.ToString();

        if (infos[1].Length < 2) infos[1] = "0" + infos[1];
        if (infos[2].Length < 2) infos[2] = "0" + infos[2];
        
        var dateNow = infos[0] + "-" + infos[1] + "-" + infos[2];
        
        return $"https://archive-api.open-meteo.com/v1/archive?latitude={lat.ToString(CultureInfo.InvariantCulture)}&longitude={lon.ToString(CultureInfo.InvariantCulture)}&start_date=2000-01-01&end_date={dateNow}&hourly=temperature_2m,relativehumidity_2m,pressure_msl,rain,windspeed_10m";
    }
    
}