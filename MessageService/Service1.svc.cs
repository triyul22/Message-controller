using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MessageService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service1 : IService1, IDisposable
    {
        private SqlConnection _connection;
        private List<Message> messageList;
        Service1()
        {
            _connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Asus\\source\\repos\\Web service project\\MessageService\\App_Data\\MyDataBase.mdf;Integrated Security=True");
            messageList = new List<Message>();
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        
        //remove this method
        public List<Message> GetMessages()
        {
            try
            {
                string query = $"select * from Messages";
                var command = new SqlCommand(query, _connection);
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    messageList.Add(new Message
                    {
                        Id = dataReader.GetInt32(0),
                        Text = dataReader.GetString(1),
                        SentTime = dataReader.GetDateTime(2)
                    });
                }
                return messageList;
            }
            catch
            {
                return new List<Message>();
            }
        }

        public List<Message> GetMessagesByDate()
        {
            try
            {
                string query = $"select * from Messages where SentTime >=  and SentTime<=";
                var command = new SqlCommand(query, _connection);
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    messageList.Add(new Message
                    {
                        Id = dataReader.GetInt32(0),
                        Text = dataReader.GetString(1),
                        SentTime = dataReader.GetDateTime(2)
                    });
                }
                return messageList;
            }
            catch
            {
                return new List<Message>();
            }
        }

        public async System.Threading.Tasks.Task<bool> SendMessageAsync(Message message)
        {
            string query = $"insert into Messages (Id, Text, SentTime) values ({message.Id}," +
                $" '{message.Text}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}')";
            SqlCommand command = new SqlCommand(query, _connection);
            command.ExecuteNonQuery();
            try
            {
                using (var socket = new ClientWebSocket())
                {
                    var uri = new Uri("wss://localhost:44382/api/new");
                    var cts = new System.Threading.CancellationTokenSource();
                    await socket.ConnectAsync(uri, cts.Token);
                    if (socket.State == WebSocketState.Open)
                    {
                        ArraySegment<byte> bytesSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes($"{message.Id} {message.Text} {message.SentTime}"));
                        await socket.SendAsync(bytesSend, WebSocketMessageType.Text, true, cts.Token);
                    }
                }
            }
            catch(WebSocketException e)
            {
                Console.WriteLine(e.Message);
            }
            return true;
        }        
    }
}
