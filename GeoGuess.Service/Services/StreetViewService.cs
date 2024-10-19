using GeoGuess.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Globalization;

public class StreetViewService : IStreetViewService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _apiBaseUrl;
    private readonly string _streetView;
    private readonly string _geocode;
    private readonly string _parameterSize;

    public StreetViewService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["GoogleMaps:ApiKey"];
        _apiBaseUrl = configuration["GoogleMaps:ApiBaseUrl"];
        _streetView = configuration["GoogleMaps:StreetView"];
        _geocode = configuration["GoogleMaps:Geocode"];
        _parameterSize = configuration["GoogleMaps:ParameterSize"];
    }

    public async Task<string> GetStreetView(double latitude, double longitude)
    {
        string url = $"{_apiBaseUrl}/{_streetView}?{_parameterSize}&location={latitude},{longitude}&key={_apiKey}";

        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode) return url;
        
        return null;
    }

    public async Task<string> GetCountryCodeByCoordinates(double latitude, double longitude)
    {
        using (HttpClient client = new())
        {

            string formattedLatitude = latitude.ToString(CultureInfo.InvariantCulture);
            string formattedLongitude = longitude.ToString(CultureInfo.InvariantCulture);

            string requestUrl = $"{_apiBaseUrl}/{_geocode}/json?&latlng={formattedLatitude},{formattedLongitude}&key={_apiKey}";

            HttpResponseMessage response = await client.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(jsonResponse);

                var addressComponents = json["results"]?[0]?["address_components"];
                if (addressComponents != null)
                {
                    foreach (var component in addressComponents)
                    {
                        var types = component["types"]?.ToObject<string[]>();
                        if (types != null && Array.Exists(types, t => t == "country"))
                        {
                            return component["short_name"]?.ToString();
                        }
                    }
                }
            }
        }

        return string.Empty;
    }



}
