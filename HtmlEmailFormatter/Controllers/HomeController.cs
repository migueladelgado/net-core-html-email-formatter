using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HtmlEmailFormatter.Models;
using HtmlEmailFormatter.HtmlEmailFormatter.Requests;

namespace HtmlEmailFormatter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SendEmail([FromBody]EmailRequest request)
        {
            var formatter = new HtmlEmailFormatter.HtmlEmailFormatter();
            foreach(var section in request.body.sections)
            {
                var sec = formatter.createComponent();

                if (section.container)
                    sec.addElement().addStyleSheet("container").build();

                foreach (var element in section.elements)
                {
                    var el = sec.addElement();
                    if (element.style)
                        el.addStyleSheet(element.type);
                    el.addWords(element.text);
                    el.build();
                }

                formatter.addSection(sec);
            }
            try
            {
                formatter.buildTemplate().setProperties(getSmtpProperties()).sendMail();
                return Ok();
            } catch
            {
                return Error();
            }
        }

        private Dictionary<string, object> getSmtpProperties()
        {
            return new Dictionary<string, object>()
            {
                {"Email", "YOUR_EMAIL" },
                {"Password",  "YOUR_SUPER_SAFE_PASSWORD" },
                {"Subject", "SUBJECT" },
                {"ToAddress", "EMAIL_TO_SEND_TO" },
                {"Port", 587 },//port specified by your email provider
                {"Host", "smtp.gmail.com"} //your email provider
            };
        }
    }
}
