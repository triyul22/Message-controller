using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace MessageService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "getmessages", ResponseFormat = WebMessageFormat.Json)]
        List<Message> GetMessages();

        List<Message> GetMessagesByDate();

        [OperationContract]
        [WebInvoke(Method = "POST",  UriTemplate = "sendmessage", ResponseFormat =WebMessageFormat.Json, RequestFormat =WebMessageFormat.Json)]
        Task<bool> SendMessageAsync(Message message);
    }
}
