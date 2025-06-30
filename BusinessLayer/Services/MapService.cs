using BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Identity.Client;
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

        private readonly ILoggerWrapper _logger;

        public MapService(string? apiKey, ILoggerWrapper logger)
        {
            _apiKey = apiKey;
            _logger = logger;
        }

        public async Task<string> GetRouteGeoJson(string start, string end)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", _apiKey);

            double[] startGeoCode = [0.0, 0.0];
            double[] endGeoCode = [0.0, 0.0];

            try
            {
                startGeoCode = await GetGeoCodeFromPlace(start);
                endGeoCode = await GetGeoCodeFromPlace(end);
            } catch (Exception ex)
            {
                _logger.Error($"Error getting geocode for start or end location [From: {start}; To: {end}]: {ex.Message}");
                return "error";
            }

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
                _logger.Error($"Failed to get route from OpenRouteService [From: {start}; To: {end}]: {error}");
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
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(url);
            } catch (Exception ex)
            {
                _logger.Error($"Exception during geocode API call for '{place}': {ex}");
                throw;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get geocode for place: {place}. Status code: {response.StatusCode}");
            }

            var contentType = response.Content.Headers.ContentType?.MediaType;

            if (contentType == null || !contentType.Contains("json"))
            {
                throw new Exception($"Unexpected content type: {contentType} for place: {place}");
            }

            var json = await response.Content.ReadAsStringAsync();
            JsonElement results;
            try
            {
                results = JsonDocument.Parse(json).RootElement;
            } catch (System.Text.Json.JsonException ex)
            {
                throw new Exception($"{ex.Message}");
            }


            if (results.GetArrayLength() == 0)
                throw new Exception("No results found for place: " + place);

            var firstResult = results[0];
            var lat = double.Parse(firstResult.GetProperty("lat").GetString() ?? "-1", System.Globalization.CultureInfo.InvariantCulture);
            var lon = double.Parse(firstResult.GetProperty("lon").GetString() ?? "-1", System.Globalization.CultureInfo.InvariantCulture);

            return [lon, lat]; // GeoJSON uses [lon, lat]

        }

    }
}
