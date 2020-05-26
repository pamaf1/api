using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsSearchController : ControllerBase
    {
        [HttpGet]
        public string GetInfo(string title)
        {
            JObject obj = JObject.Parse(Get.GetStr($"http://newsapi.org/v2/everything?q={title}&?country=ua&pageSize=10&popularity&apiKey=f55b620c90db427fb0aea1c47be944e6"));

            IList<JToken> results = obj["articles"].Children().ToList();
            string response = null;

            foreach (JToken result in results)
            {
                NewsResponse ns = result.ToObject<NewsResponse>();
                response += $"{ns.title}\n{ns.url}\n";
            }
            return response;
        }

    }
}
