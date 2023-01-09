using System;
using System.Net.Http;
using System.Threading.Tasks;
using AllAboutWeatherApp.MVVM.Model.DataStorage;

namespace AllAboutWeatherApp.MVVM.Model.DataAccessFactory;

public class AirQualityDataRetriever : IAirQualityDataRetriever
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    public AirQualityDataRetriever()
    {
        _client = new HttpClient();
        _apiKey = "005acbc1300f9c9027a264d7028893f6";
    }

    public async Task<AirQualityData> GetAirQualityData(GeoCoordinates coordinates)
    {
        AirQualityData? airQuality = null!;
        var now = DateTimeOffset.Now.ToUnixTimeSeconds();
        var requestUri = GetGeoRequestUri(coordinates, now);

        HttpResponseMessage response = await _client.GetAsync(requestUri);

        if (response.IsSuccessStatusCode)
            airQuality = await response.Content.ReadAsAsync<AirQualityData>();

        return CorrectReceivedData(airQuality);
    }

    private string GetGeoRequestUri(GeoCoordinates coordinates, long now) =>
        $"https://api.openweathermap.org/data/2.5/air_pollution/history?lat={coordinates.Lat}&lon={coordinates.Lon}&start={now-43200}&end={now}&appid={_apiKey}";

    private AirQualityData CorrectReceivedData(AirQualityData airQuality)
    {
        if (airQuality.List == null) return airQuality;

        if (airQuality.List[^1].Main == null) return airQuality;
        var mainAirQualityData = airQuality.List[^1].Main;
        foreach (var airQualityData in airQuality.List)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds( airQualityData.Dt ).ToLocalTime();
            airQualityData.DtFormat = dateTime;
        }
        if (mainAirQualityData == null) return airQuality;
        switch (mainAirQualityData.Aqi)
        {
            case 1:
            {
                mainAirQualityData.Color = "DarkGreen";
                mainAirQualityData.AqiText = "Good";
                mainAirQualityData.PathToIcon = "../../Images/airQualityIndex/veryHappy.png";
            }
                break;
            case 2:
            {
                mainAirQualityData.Color = "YellowGreen";
                mainAirQualityData.AqiText = "Fair";
                mainAirQualityData.PathToIcon = "../../Images/airQualityIndex/happy.png";
            }
                break;
            case 3:
            {
                mainAirQualityData.Color = "Yellow";
                mainAirQualityData.AqiText = "Moderate";
                mainAirQualityData.PathToIcon = "../../Images/airQualityIndex/neutral.png";
            }
                break;
            case 4:
            {
                mainAirQualityData.Color = "Red";
                mainAirQualityData.AqiText = "Poor";
                mainAirQualityData.PathToIcon = "../../Images/airQualityIndex/sad.png";
            }
                break;
            case 5:
            {
                mainAirQualityData.Color = "DarkRed";
                mainAirQualityData.AqiText = "Very Poor";
                mainAirQualityData.PathToIcon = "../../Images/airQualityIndex/dead.png";
            }
                break;
        }

        return airQuality;
    }
}