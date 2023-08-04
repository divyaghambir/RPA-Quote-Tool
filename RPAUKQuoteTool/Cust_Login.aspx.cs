using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using BAL_Layer;

namespace RPAUKCustomerQuote
{
    public partial class Cust_Login : System.Web.UI.Page
    {
        Cust_LoginBAL obj=new Cust_LoginBAL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string Username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            int count = obj.ValidateCredential(Username, password);
            if (count > 0)
            {
                Session["UserName"] = Username;
                string UserRole = ConfigurationManager.AppSettings[Username];
                Session["UserRole"] = UserRole;
                Session["login"] = true;
                Response.Redirect("Cust_Dashboard.aspx");

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