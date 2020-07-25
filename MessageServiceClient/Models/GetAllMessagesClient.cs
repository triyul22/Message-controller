using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MessageServiceClient.Models
{
    public class GetAllMessagesClient
    {
        private string BASE_URL = "ws://localhost:49350/Service1.svc/";
        public async Task<string> GetAllMessagesAsync()
        {
            try
            {
                using (var socket = new ClientWebSocket())
                {
                    var uri = new Uri($"{BASE_URL}sendmessage");
                    var cts = new System.Threading.CancellationTokenSource();
                    await socket.ConnectAsync(uri, cts.Token);
                    var response = "";
                    while (socket.State == WebSocketState.Open)
                    {
                        ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[128]);
                        WebSocketReceiveResult result = await socket.ReceiveAsync(bytesReceived, cts.Token);
                        response = System.Text.Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                    }
                    return response;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
