using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLayer.Services
{
    public class MapService : IMapService
    {

        private readonly string? _apiKey = "";

        public MapService(string? apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> GetRouteGeoJson(string start, string end)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", _apiKey);

            var startGeoCode = await GetGeoCodeFromPlace(start);
            var endGeoCode = await GetGeoCodeFromPlace(end);

            var body = new
            {
                coordinates = new[] { startGeoCode, endGeoCode }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(body);
            var response = await client.PostAsync(
                "https://api.openrouteservice.org/v2/directions/driving-car/geojson",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return "error";
            }

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<double[]> GetGeoCodeFromPlace(string place)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "TourPlanner/1.0");

            var encodedPlace = HttpUtility.UrlEncode(place);

            var url = $"https://nominatim.openstreetmap.org/search?q={encodedPlace}&format=json&limit=1";
            var response = await client.GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();
            var results = JsonDocument.Parse(json).RootElement;

            if (results.GetArrayLength() == 0)
                throw new Exception($"No geocoding result for '{place}'.");

            var firstResult = results[0];
            var lat = double.Parse(firstResult.GetProperty("lat").GetString() ?? "-1", System.Globalization.CultureInfo.InvariantCulture);
            var lon = double.Parse(firstResult.GetProperty("lon").GetString() ?? "-1", System.Globalization.CultureInfo.InvariantCulture);

            return [lon, lat]; // GeoJSON uses [lon, lat]

        }

    }
}
