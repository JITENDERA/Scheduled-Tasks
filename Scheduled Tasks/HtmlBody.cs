using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Scheduled_Tasks
{
   public class HtmlBody
    {
        public string MailBodyData(DataTable maildata)
        {
            string MailBody = "<table style=\"color:blue; border: 1px solid #dddddd\"><tr><th>Employee Name</th><th>Employee Designation</th><th>Employee Income</th></tr>";
           
            for (int loopCount = 0; loopCount < maildata.Rows.Count; loopCount++)
            {
                MailBody += "<tr><td>" + maildata.Rows[loopCount]["name"] + "</td><td>" + maildata.Rows[loopCount]["designation"] + "</td><td>" + maildata.Rows[loopCount]["income"] + "</td></tr>";
            }
            MailBody += "</table>";


            var result = (from p in maildata.AsEnumerable()
                          group p by p["designation"]
                              into r
                          select new
                          {
                              Designation = r.Key,
                              Group_Income = r.Sum((s) => decimal.Parse(s["income"].ToString()))
                          }).ToList();



            MailBody += "<br/>";

            string MailBody2 = "<table style=\"color:blue; border: 1px solid #dddddd\"><tr><th>Designation</th><th>Group Income</th></tr>";


            foreach (var item in result)
            {
                MailBody2 += "<tr><td>" + item.Designation + "</td><td>" + item.Group_Income + "</td></tr>";
            }
            
            MailBody2 += "</table>";

            MailBody += MailBody2;

            return MailBody;
        }
    }
}
