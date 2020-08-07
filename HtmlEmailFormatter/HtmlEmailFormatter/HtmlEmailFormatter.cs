using HtmlEmailFormatter.HtmlEmailFormatter.Html.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace HtmlEmailFormatter.HtmlEmailFormatter
{
    public class HtmlEmailFormatter
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; } = 587;
        public bool SSL { get; set; } = true;
        public bool UseDefaultCredentials { get; set; } = false;
        public bool IsHTML { get; set; } = true;
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ToAddress { get; set; }

        public string template { get; set; } = "<div>{}</div>";

        public List<Element> elements { get; set; } = new List<Element>();
        public List<string> sections { get; set; } = new List<string>();

        private Type type { get { return GetType(); } }
        private SmtpClient smtpClient;

        public HtmlEmailFormatter()
        {

        }

        /// <summary>
        /// An alternative to setting the properties individually
        /// Pass a dictionary where the keys equal the property names
        /// </summary>
        /// <param name="dic"></param>
        public HtmlEmailFormatter setProperties(Dictionary<string, object> dic)
        {
            foreach (KeyValuePair<string, object> entry in dic)
            {
                PropertyInfo pi = type.GetProperty(entry.Key);
                if (!(pi == null))
                    pi.SetValue(this, entry.Value);
            }
            return this;
        }

        //create and return instance of element class
        public Element createComponent() =>
            new Element();

        //add elemnt to elemts list
        public HtmlEmailFormatter addSection(Element element)
        {
            elements.Add(element);
            sections.Add(element.build());
            return this;
        }

        public HtmlEmailFormatter buildTemplate()
        {
            template = template.Replace("{}", string.Concat(string.Join(string.Empty, sections)));
            Body = template;
            return this;
        }

        //Main send function 
        public void sendMail()
        {
            //build, config, & send
            smtpBuild();
            smtpConfig();
            smtpSend();
        }

        //build the HTML email
        private void smtpBuild()
        {

        }

        private void smtpConfig()
        {
            smtpClient = new SmtpClient
            {
                Host = Host,
                Port = Port, //default = 587
                EnableSsl = SSL, //default = true
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = UseDefaultCredentials, //default = false
                Credentials = new NetworkCredential(
                        new MailAddress(Email).Address,
                        Password
                    )
            };
        }

        private void smtpSend()
        {
            var message = new MailMessage(Email, ToAddress);
            message.IsBodyHtml = IsHTML;
            message.Subject = Subject;
            message.Body = Body;
            using (message)
            {
                try
                {
                    smtpClient.Send(message);
                }
                catch (Exception e)
                {
                    throw new Exception(e.ToString());
                }
            }
        }
    }
}
