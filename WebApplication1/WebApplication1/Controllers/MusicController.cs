using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MusicController : Controller
    {
        [HttpGet]
        public IEnumerable<MusicResponse> GetInfo(string title)
        {
            JObject obj = JObject.Parse(Get.GetStr($"https://ws.audioscrobbler.com/2.0/?method=track.search&track={title}&api_key=4c2db756c53ae9dbd6d9496c8e7f754c&format=json&limit=10"));

            IList<JToken> results = obj["results"]["trackmatches"]["track"].Children().ToList();

            foreach (JToken result in results)
            {
                MusicResponse mr = result.ToObject<MusicResponse>();

                yield return new MusicResponse
                {
                    Name = mr.Name,
                    Url = mr.Url
                };
            }
        }


    }

    public class MusicResponse
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}

