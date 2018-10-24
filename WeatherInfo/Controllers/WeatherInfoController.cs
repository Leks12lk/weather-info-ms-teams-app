using Microsoft.AspNetCore.Mvc;
using WeatherInfo.Services;

namespace WeatherInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherInfoController : ControllerBase
    {
        private readonly IWeatherInfoService _weatherInfoService;

        public WeatherInfoController(IWeatherInfoService weatherInfoService)
        {
            _weatherInfoService = weatherInfoService;
        }

        public IActionResult GetWeatherByCity(string city, string unit)
        {
            var result = _weatherInfoService.GetWeatherByCityName(city, unit);
            return Ok(result);
        }
    }
}