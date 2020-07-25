using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net.WebSockets;

namespace MessageServiceClient.Models
{
    public class SendMessageClient
    {
        private string BASE_URL = "http://localhost:49350/Service1.svc/";
        public bool sendMessage(Message message)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Message));

                MemoryStream mem = new MemoryStream();
                message.SentTime = DateTime.Now;
                ser.WriteObject(mem, message);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClient = new WebClient();
                webClient.Headers["Content-Type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(BASE_URL + "sendmessage", "POST", data);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
