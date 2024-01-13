
using AreaBox_V0._1.Models.Location;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AreaBox_V0._1.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _apiMap;

        public LocationService(HttpClient httpClient, IOptions<Dictionary<string, string>> apiMapOptions)
        {
            _httpClient = httpClient;
            _apiMap = apiMapOptions.Value;
        }

        public async Task<string> GetGeolocation(double latitude, double longitude)
        {
            if (!_apiMap.TryGetValue("ApiMapKey", out var apiKey) ||
                !_apiMap.TryGetValue("ApiMapUrl", out var apiUrl))
            {
                return null;
            }

            var apiUrlWithCoordinates = $"{apiUrl}{latitude}+{longitude}&key={apiKey}";

            var response = await _httpClient.GetStringAsync(apiUrlWithCoordinates);
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            var key = data.results[0].components["ISO_3166-2"][0].ToString();
            var country = data.results[0].components["country"].ToString();
            var city = data.results[0].components["state"].ToString();

            var geolocationInfo = new GeolocationInfo
            {
                Key = key,
                Country = country,
                City = city
            };

            return JsonConvert.SerializeObject(geolocationInfo);
        }

        public async Task<GeolocationInfo> GetGeolocationObject(double latitude, double longitude)
        {
            if (!_apiMap.TryGetValue("ApiMapKey", out var apiKey) ||
                !_apiMap.TryGetValue("ApiMapUrl", out var apiUrl))
            {
                return null;
            }

            var apiUrlWithCoordinates = $"{apiUrl}{latitude}+{longitude}&key={apiKey}";

            var response = await _httpClient.GetStringAsync(apiUrlWithCoordinates);
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            var key = data.results[0].components["ISO_3166-2"][0].ToString();
            var country = data.results[0].components["country"].ToString();
            var city = data.results[0].components["state"].ToString();

            var geolocationInfo = new GeolocationInfo
            {
                Key = key,
                Country = country,
                City = city
            };

            return geolocationInfo;
        }
    }
}
