using Microsoft.AspNetCore.Mvc;

namespace Static.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(IWebHostEnvironment webHostEnvironment, ILogger<WeatherForecastController> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

      

     

    }
}