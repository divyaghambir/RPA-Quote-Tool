using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace RPADubaiQuoteTool
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.watts.com");
            //smtpClient.Credentials = new System.Net.NetworkCredential("RPA@wattswater.com",); 
            smtpClient.UseDefaultCredentials = true;
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage("ukquotations@wattswater.com", "divya.chopra@wattswater.com");
            mail.Subject = "Test Email";
            mail.IsBodyHtml = true;
            mail.Body = "Dear Sir/Madam <br/> Please find attached your quotation and a copy of our terms and conditions  <br/><br/><br/> <b>IMPORTANT NOTICE:<br/>EMAIL <u><font color='blue'>RPA@WATTSWATER.COM</font></u> IS AN OUTGOING MAILBOX ONLY. <br/> THIS ADDRESS DOES NOT ACCEPT INCOMING MAIL, SO PLEASE DO NOT REPLY DIRECTLY OR SEND ORDERS TO THIS ADDRESS.<br/>PLEASE EMAIL PURCHASE ORDERS TO: <u><font color='blue'> wattsuk@wattswater.com</font></u></b>";

            //Setting From , To and CC 
            //mail.From = new MailAddress("divya.chopra@wattswater.com");
            //mail.To.Add(new MailAddress("RPA@wattswater.com"));
            smtpClient.Send(mail);
        }
    }
}