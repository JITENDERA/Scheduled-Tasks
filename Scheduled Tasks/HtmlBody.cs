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

            return MailBody;
        }

        public string MailBodyDataGroup(DataTable maildata)
        {
            var result = (from p in maildata.AsEnumerable()
                          group p by p["designation"]
                             into r
                          select new
                          {
                              Designation = r.Key,
                              Group_Income = r.Sum((s) => decimal.Parse(s["income"].ToString()))
                          }).ToList();

            string MailBody = "<table style=\"color:blue; border: 1px solid #dddddd\"><tr><th>Designation</th><th>Group Income</th></tr>";
            foreach (var item in result)
            {
                MailBody += "<tr><td>" + item.Designation + "</td><td>" + item.Group_Income + "</td></tr>";
            }

            MailBody += "</table>";
            return MailBody;
        }

        public string MailBodyDesignationGroup(DataTable maildata)
        {
            var result = from table in maildata.AsEnumerable()
                         group table by new { Designation = table["designation"] }
                         into data

                         select new
                         {
                             Value = data.Key,
                             ColumnValues = data
                         };

            string MailBody = "<table style=\"color:blue; border: 1px solid #dddddd\"><tr><th>Employee Name</th><th>Income</th></tr>";

            foreach (var key in result)

            {

                MailBody += "<p style=\"color:red\">" + key.Value.Designation + "</p>";
                
                foreach (var columnValue in key.ColumnValues)

                {
                    MailBody += "<tr><td>" + columnValue["Name"].ToString() + "</td><td>" + columnValue["income"].ToString() + "</td></tr>";

                }

            }
            MailBody += "</table>";
            return MailBody;
        }
    }
}
