using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL_Layer;
using System.Configuration;

namespace RPADubaiQuoteTool
{
    public partial class Login : System.Web.UI.Page
    {
        LoginBAL obj = new LoginBAL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string Username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            int count = obj.ValidateCredential(Username,password);
            if(count>0)
            {
                Session["UserName"] = Username;
                string UserRole = ConfigurationManager.AppSettings[Username];
                Session["UserRole"] = UserRole;
                Session["login"] = true;
                Response.Redirect("Dashboard.aspx");
                
            }
            else
            {
                lblmessage.Text = "Invalid Credential";
                Session["login"] = false;
            }
        }

        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {
          
        }
    }
}