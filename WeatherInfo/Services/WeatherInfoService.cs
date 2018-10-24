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
            // try parse enum in order to validate its passing value
            unit = Enum.TryParse(unit, out UnitType _) ? unit.ToLower() : Enum.GetName(typeof(UnitType), UnitType.Metric).ToLower();

            var baseApiUrl = _configuration["OPEN_WEATHER_API_URL"];
            var apiToken = _configuration["OPEN_WEATHER_API_TOKEN"];
            var url = $"{baseApiUrl}{apiToken}&q={city}&units={unit}";

            HttpClient client = new HttpClient();
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync(url).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;

            return responseContent;
        }
    }
}
