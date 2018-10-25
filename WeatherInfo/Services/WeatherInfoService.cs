using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using WeatherInfo.Models;

namespace WeatherInfo.Services
{
    public class WeatherInfoService : IWeatherInfoService
    {
        private readonly IConfiguration _configuration;
        public WeatherInfoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetWeatherByCityName(string city, string unit)
        {
            if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(unit))
                throw new ArgumentException("Arguments are not valid");
            
            // try parse enum in order to validate its passing value
            unit = Enum.TryParse(unit, out UnitType _) ? unit.ToLower() : Enum.GetName(typeof(UnitType), UnitType.Metric).ToLower();
          
            var url = GenerateWeatherApiUrl(city, unit);

            return CallWeatherApi(url);
        }

        private string GenerateWeatherApiUrl(string city, string unit)
        {
            var baseApiUrl = _configuration["OPEN_WEATHER_API_URL"];
            var apiToken = _configuration["OPEN_WEATHER_API_TOKEN"];
            return $"{baseApiUrl}{apiToken}&q={city}&units={unit}";
        }

        private static string CallWeatherApi(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException("API url is not valid");

            var client = new HttpClient();
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
            // Make GET call to weather API
            var response = client.GetAsync(url).Result;
            // Get response content
            var responseContent = response.Content.ReadAsStringAsync().Result;
            return responseContent;
        }
    }
}
