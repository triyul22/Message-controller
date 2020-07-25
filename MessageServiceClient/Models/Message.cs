using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MessageServiceClient.Models
{
    public class Message
    {
        [Display(Name ="Id")]
        public int Id { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "SentTime")]
        public DateTime SentTime { get; set; }
    }
}
