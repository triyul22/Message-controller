using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MessageServiceClient.Models;
using MessageServiceClient.ViewModels;

namespace MessageServiceClient.Controllers
{
    public class MessageController : Controller
    {
        [Route("/api/get")]
        public IActionResult Index()
        {
            var allMessagesClient = new GetMessagesByTimeClient();
            ViewBag.messageList = allMessagesClient.GetMessages();
            return View("Index");
        }

        [Route("/api/new")]
        public IActionResult GetNewMessages()
        {
            GetAllMessagesClient allMessagesClient = new GetAllMessagesClient();
            ViewBag.newMessages = allMessagesClient.GetAllMessagesAsync().Result;
            return View("Receiver");
        }

        [Route("/api/send")]
        [HttpGet]
        public IActionResult Send()
        {
            return View("SendMessage");
        }

        [Route("/api/send")]
        [HttpPost]
        public IActionResult SendMessage(MessageViewModel mvm)
        {
            SendMessageClient smc = new SendMessageClient();
            if (smc.sendMessage(mvm.message))
            {
                return RedirectToAction("Send");
            }
            return RedirectToAction("Send");
        }
    }    
}
