using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlurayCreator.Models;
using Newtonsoft.Json;

namespace BlurayCreator.Services
{
    public class WeatherService
    {
        private const string ApiBaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        private const string ApiKey = "b6fd43b5d8882a4b057a0e0e00d5f933";
        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherResponse> GetWeatherByCityAsync(string cityName, string units = "metric")
        {
            try
            {
                string url = $"{ApiBaseUrl}?q={cityName}&units={units}&appid={ApiKey}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    WeatherResponse weatherData = JsonConvert.DeserializeObject<WeatherResponse>(content);
                    return weatherData;
                }
                else
                {
                    throw new Exception($"API Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching weather data: {ex.Message}", ex);
            }
        }

        public async Task<WeatherResponse> GetWeatherByCoordinatesAsync(double latitude, double longitude, string units = "metric")
        {
            try
            {
                string url = $"{ApiBaseUrl}?lat={latitude}&lon={longitude}&units={units}&appid={ApiKey}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    WeatherResponse weatherData = JsonConvert.DeserializeObject<WeatherResponse>(content);
                    return weatherData;
                }
                else
                {
                    throw new Exception($"API Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching weather data: {ex.Message}", ex);
            }
        }
    }
}
