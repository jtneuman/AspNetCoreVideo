using AspNetCoreVideo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreVideo.ViewComponents
{
    public class Message : ViewComponent
    {
        private ImessageService _message;

        public Message(ImessageService message)
        {
            _message = message;
        }

        public IViewComponentResult Invoke()
        {
            var model = _message.GetMessage();
            return View("Default", model);
        }

    }
}
