using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace RPADubaiQuoteTool
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage("rpa@wattswater.com", "divya.chopra@wattswater.com");
            SmtpClient smtpClient = new SmtpClient("smtp.watts.com");
            smtpClient.UseDefaultCredentials = true;
            mail.Subject = "test email";
            mail.IsBodyHtml = true;
            MailAddress copy1 = new MailAddress("alan.fahy@wattswater.com"); //choprad send email to admin 2/4/20
            mail.Bcc.Add(copy1);
            mail.CC.Add("monika.bullock@wattswater.com");
            mail.CC.Add("satish.reddy@wattswater.com");
            smtpClient.Send(mail);
        }
    }
}