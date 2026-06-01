using System;
using System.Windows;
using BlurayCreator.Models;
using BlurayCreator.Services;

namespace BlurayCreator
{
    public partial class WeatherDashboard : Window
    {
        private WeatherService _weatherService;

        public WeatherDashboard()
        {
            InitializeComponent();
            _weatherService = new WeatherService();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string cityName = CityInput.Text.Trim();

            if (string.IsNullOrEmpty(cityName))
            {
                ShowError("Please enter a city name!");
                return;
            }

            await SearchWeather(cityName);
        }

        private async System.Threading.Tasks.Task SearchWeather(string cityName)
        {
            try
            {
                ShowLoading(true);
                HideError();
                HideWeatherInfo();

                WeatherResponse weather = await _weatherService.GetWeatherByCityAsync(cityName);

                if (weather != null)
                {
                    DisplayWeatherInfo(weather);
                    ShowLoading(false);
                }
                else
                {
                    ShowError("City not found. Please try again.");
                    ShowLoading(false);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error: {ex.Message}");
                ShowLoading(false);
            }
        }

        private void DisplayWeatherInfo(WeatherResponse weather)
        {
            try
            {
                // City and basic info
                CityName.Text = $"{weather.CityName}, {weather.SystemInfo.Country}";
                UpdateTime.Text = DateTime.Now.ToString("HH:mm:ss");
                Coordinates.Text = $"Lat: {weather.Coordinates.Latitude:F2}°, Lon: {weather.Coordinates.Longitude:F2}°";

                // Temperature
                Temperature.Text = $"{weather.Main.Temperature:F1}";
                TempMin.Text = $"{weather.Main.TemperatureMin:F1}°C";
                TempMax.Text = $"{weather.Main.TemperatureMax:F1}°C";
                FeelsLike.Text = $"{weather.Main.FeelsLike:F1}°C";

                // Weather description and icon
                if (weather.Weather.Length > 0)
                {
                    WeatherDescription.Text = weather.Weather[0].Description.ToUpper();
                    WeatherIcon.Text = GetWeatherIcon(weather.Weather[0].Main);
                }

                // Details
                Humidity.Text = $"{weather.Main.Humidity}";
                WindSpeed.Text = $"{weather.Wind.Speed:F1}";
                Pressure.Text = $"{weather.Main.Pressure}";
                Visibility.Text = $"{(weather.Visibility / 1000.0):F1}";
                Cloudiness.Text = $"{weather.Clouds.Cloudiness}";

                // Show/Hide UI Elements
                WeatherCard.Visibility = Visibility.Visible;
                DetailsGrid.Visibility = Visibility.Visible;
                ExtraDetailsGrid.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                ShowError($"Error displaying weather info: {ex.Message}");
            }
        }

        private string GetWeatherIcon(string weatherMain)
        {
            return weatherMain.ToLower() switch
            {
                "clear" => "☀️",
                "clouds" => "☁️",
                "rain" => "🌧️",
                "drizzle" => "🌦️",
                "thunderstorm" => "⛈️",
                "snow" => "❄️",
                "mist" => "🌫️",
                "smoke" => "💨",
                "haze" => "🌫️",
                "dust" => "🌪️",
                "fog" => "🌫️",
                "sand" => "🌪️",
                "ash" => "🌋",
                "squall" => "💨",
                "tornado" => "🌪️",
                _ => "🌍"
            };
        }

        private void ShowLoading(bool show)
        {
            LoadingBorder.Visibility = show ? Visibility.Visible : Visibility.Hidden;
        }

        private void ShowError(string message)
        {
            ErrorMessage.Text = message;
            ErrorBorder.Visibility = Visibility.Visible;
        }

        private void HideError()
        {
            ErrorBorder.Visibility = Visibility.Hidden;
        }

        private void HideWeatherInfo()
        {
            WeatherCard.Visibility = Visibility.Hidden;
            DetailsGrid.Visibility = Visibility.Hidden;
            ExtraDetailsGrid.Visibility = Visibility.Hidden;
        }
    }
}
