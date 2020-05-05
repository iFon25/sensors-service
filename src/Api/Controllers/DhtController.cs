using Api.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SensorsManager;

namespace Api.Controllers
{
    [ApiController]
    [Route("dht")]
    public class DhtController: ControllerBase
    {
        private readonly DhtOptions _dhtOptions;
        
        public DhtController(IOptions<DhtOptions> dhtOptions)
        {
            _dhtOptions = dhtOptions.Value;
        }
        
        [HttpGet("temperature")]
        public IActionResult GetTemperature()
        {
            if (!_dhtOptions.Enabled)
            {
                return StatusCode(501, "Датчик отключен в настройках.");
            }

            var temperature = Dht.GetTemperature(_dhtOptions.PinNumber, _dhtOptions.PinNumberingScheme);
            return Ok($"Температура: {temperature}°C.");
        }
        
        [HttpGet("humidity")]
        public IActionResult GetHumidity()
        {
            if (!_dhtOptions.Enabled)
            {
                return StatusCode(501, "Датчик отключен в настройках.");
            }

            var humidity = Dht.GetHumidity(_dhtOptions.PinNumber, _dhtOptions.PinNumberingScheme);
            return Ok($"Влажность воздуха: {humidity}%.");
        }
        
        [HttpGet("data")]
        public IActionResult GetData()
        {
            if (!_dhtOptions.Enabled)
            {
                return StatusCode(501, "Датчик отключен в настройках.");
            }

            var data = Dht.GetData(_dhtOptions.PinNumber, _dhtOptions.PinNumberingScheme);
            return Ok($"Температура: {data.temperature}°C.\n" +
                      $"Влажность воздуха: {data.humidity}%.");
        }
    }
}