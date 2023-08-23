using Microsoft.AspNetCore.Mvc;

namespace RoutingExample.Controllers;
[ApiController]
public class WeatherForecastController : ControllerBase
{  
    [HttpGet("""{**url:notcontains(test)}""")]
    public string NoTest(string url)
    {
        return $"No Test: {url}";
    }

    [HttpGet("""{**url}""")]
    public string Test(string url)
    {
        return $"Test: {url}";
    }
}
