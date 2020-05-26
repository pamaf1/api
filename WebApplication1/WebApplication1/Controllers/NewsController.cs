using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : Controller
    {
        [HttpGet("/news")]
        public IEnumerable<NewsResponse> GetInfo()
        {

            JObject obj = JObject.Parse(Get.GetStr("http://newsapi.org/v2/top-headlines?country=ua&pageSize=10&apiKey=f55b620c90db427fb0aea1c47be944e6"));

            IList<JToken> results = obj["articles"].Children().ToList();

            foreach (JToken result in results)
            {
                NewsResponse ns = result.ToObject<NewsResponse>();

                yield return new NewsResponse
                {
                    title = ns.title,
                    url = ns.url
                };
            }

        }

    }
}
