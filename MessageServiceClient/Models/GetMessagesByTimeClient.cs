using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace MessageServiceClient.Models
{
    public class GetMessagesByTimeClient
    {
        private string BASE_URL = "http://localhost:49350/Service1.svc/";
        public List<Message> GetMessages()
        {
            try
            {
                var webClient = new WebClient();
                var json = webClient.DownloadString(BASE_URL + "getmessages");
                return JsonConvert.DeserializeObject<List<Message>>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}
