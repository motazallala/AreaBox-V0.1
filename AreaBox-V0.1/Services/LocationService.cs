
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;

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

			var cityId = data.results[0].components["ISO_3166-2"][0].ToString();

			return cityId;
		}
	}
}
