using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class WeatherController : Controller
    {
        private static string response = null;

        [HttpGet("/weather")]
        public IEnumerable<AllWeather> GetInfo()
        {
            using (var webClient = new WebClient())
            {
                response = webClient.DownloadString("https://api.openweathermap.org/data/2.5/weather?q=Kyiv&units=metric&appid=5ce72445995b7a80993c0a5d8e8e2611&lang=ua");
            }
            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

            JObject result1 = JObject.Parse(response);
            IList<JToken> results1 = result1["weather"].Children().ToList();

            foreach (JToken result2 in results1)
            {
                Weather w = result2.ToObject<Weather>();
                yield return new AllWeather
                {
                    Temp = weatherResponse.Main.Temp,
                    Feels_Like = weatherResponse.Main.Feels_Like,
                    description = w.description
                };

            }

        }

    }

    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }

    }
    public class TemperatureInfo
    {
        public float Temp { get; set; }
        public float Feels_Like { get; set; }
    }
    public class Weather
    {
        public string description { get; set; }
    }
    public class AllWeather
    {
        public float Temp { get; set; }
        public float Feels_Like { get; set; }
        public string description { get; set; }
    }

}