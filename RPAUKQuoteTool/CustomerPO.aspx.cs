using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL_Layer;
using System.Data;
using Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;
using System.IO;

namespace RPAUKQuoteTool
{
    public partial class CustomerPO : System.Web.UI.Page
    {
        CreateReportBAL obj = new CreateReportBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                

                string quote = Request.QueryString["QuoteNo"];
                // string quote1 = Request.Params["QuoteNo"];
                 txtQuoteNum.Text = quote;
            
            }
          
        }

                       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (drpStatus.SelectedItem.Text == "Accept")
            {
                if (FileUpload1.HasFile == false || txtCustomerPO.Text == "")
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the customer PO# and upload the PO document')</script>");
                    return;
                }
                else
                {
                    string folderName = @"C:\UK_RPAQuoteTool_Deploy\App_Data\CustomerPO";

                    string pathString = System.IO.Path.Combine(folderName, txtQuoteNum.Text + "_" + txtCustomerPO.Text);
                    System.IO.Directory.CreateDirectory(pathString);

                    FileUpload1.SaveAs(System.IO.Path.Combine(pathString, FileUpload1.FileName));
                }
            }



            Response.Redirect("ThankYou.aspx");
        }
    }
}