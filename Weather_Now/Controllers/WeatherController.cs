using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Weather_Now.Models;

namespace Weather_Now.Controllers
{
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient;

        public WeatherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Main view
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                ViewBag.Error = "Please enter a city name.";
                return View("Index");
            }

            try
            {
                // Step 1: Get city coordinates using Open-Meteo geocoding API
                string geoUrl = $"https://geocoding-api.open-meteo.com/v1/search?name={city}";
                var geoResponse = await _httpClient.GetStringAsync(geoUrl);
                var geoData = JObject.Parse(geoResponse);
                var firstResult = geoData["results"]?.First;

                if (firstResult == null)
                {
                    ViewBag.Error = "City not found.";
                    return View("Index");
                }

                double latitude = (double)firstResult["latitude"];
                double longitude = (double)firstResult["longitude"];
                string cityName = (string)firstResult["name"];

                // Step 2: Get weather data
                string weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";
                var weatherResponse = await _httpClient.GetStringAsync(weatherUrl);
                var weatherData = JObject.Parse(weatherResponse);
                var current = weatherData["current_weather"];

                var model = new WeatherNowModel
                {
                    City = cityName,
                    Temperature = (double)current["temperature"],
                    WindSpeed = (double)current["windspeed"],
                    WeatherCode = current["weathercode"]?.ToString(),
                    WeatherDescription = "Current Weather"
                };

                return View("Result", model);
            }
            catch
            {
                ViewBag.Error = "Something went wrong. Try again later.";
                return View("Index");
            }
        }
    }
}

