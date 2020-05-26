using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Get
    {
        public static string GetStr(string A)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(A);

            request.Method = "GET";

            WebResponse response = request.GetResponse();

            Stream s = response.GetResponseStream();

            StreamReader reader = new StreamReader(s);

            string answer = reader.ReadToEnd();

            response.Close();

            return answer;
        }

    }
}

