using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace MessageService
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime SentTime { get; set; }
    }
}