using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Dynamic;
//using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Data;

namespace Scheduled_Tasks
{
    class Tasks
    {
        static void Main(string[] args)
        {
            Tasks task = new Tasks();


            string confi = task.ConfigLoadJson();
            List<Parameters> items = JsonConvert.DeserializeObject<List<Parameters>>(confi);

            task.sendEmail(items);
        }

        public void sendEmail(List<Parameters> items)
        {

            string data = DataLoadJson();

            DataTable dt = (DataTable)JsonConvert.DeserializeObject(data, (typeof(DataTable)));
            HtmlBody body = new HtmlBody();

            for (int i = 0; i < items.Count; i++)
            {
                var fromAddress = new MailAddress(items[i].frm);
                var fromPassword = items[i].password;
                var toAddress = new MailAddress(items[i].to);
                string subject = items[i].subject;

                string MailBody = body.MailBodyData(dt);

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = MailBody

                })

                    smtp.Send(message);

            }
        }

        public string ConfigLoadJson()
        {
            using (StreamReader r = new StreamReader("keys.json"))
            {
                string json = r.ReadToEnd();

                return json;
            }
        }

        public string DataLoadJson()
        {
            using (StreamReader r = new StreamReader("empdata.json"))
            {
                string json = r.ReadToEnd();

                return json;
            }
        }
    }
}
