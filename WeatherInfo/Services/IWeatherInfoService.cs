
namespace WeatherInfo.Services
{
    public interface IWeatherInfoService
    {
        string GetWeatherByCityName(string city, string unit);
    }
}
