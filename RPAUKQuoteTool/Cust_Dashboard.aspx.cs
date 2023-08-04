using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL_Layer;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

namespace RPAUKCustomerQuote
{
    public partial class Cust_Dashboard : System.Web.UI.Page
    {
        Cust_DashboardBAL obj = new Cust_DashboardBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            bool blnlogin = false;
            if (Session["login"] != null)
            {
                blnlogin = (bool)Session["login"];
                if (blnlogin)
                {
                    //string UserName = Request["userId"];
                    if (!IsPostBack)
                    {
                        string UserRole = (string)Session["UserRole"];
                        string UserName = (string)Session["UserName"];
                        string FilterName = "";

                        btnCreateQuote.Visible = true;
                       
                       
                        DataTable dt = new DataTable();
                        dt = obj.LoadDashboard(UserName, false);
                       
                        grdDashboard.DataSource = dt;
                        grdDashboard.DataBind();

                       
                    }
                }
                else
                {
                    Response.Redirect("Cust_Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Cust_Login.aspx");
            }

        }


        protected void grdDashboard_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Open")

            {   //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                //Reference the GridView Row.
                GridViewRow row = grdDashboard.Rows[rowIndex];//Fetch value of Quote Number
                string QuoteNo = row.Cells[1].Text;
                string Status = row.Cells[8].Text;
                string UserRole = (string)Session["UserRole"];
                string UserName = (string)Session["UserName"];
                Response.Redirect("Cust_Quote.aspx?QuoteNo=" + QuoteNo + "&Status=" + Status + "&UserName=" + UserName + "&UserRole=" + UserRole);
            }
        }


      
        protected void btnCreateQuote_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cust_CreateNewQuote.aspx");
        }

       

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Dashboard" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            grdDashboard.GridLines = GridLines.Both;
            grdDashboard.HeaderStyle.Font.Bold = true;
            grdDashboard.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

      

      
        protected void btnQuoteDetails_Click(object sender, EventArgs e)
        {
            DataTable dtQuoteDetails = new DataTable();
            dtQuoteDetails = obj.LoadAllQuoteDetails();
            string attachment = "attachment; filename=city.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dtQuoteDetails.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dtQuoteDetails.Rows)
            {
                tab = "";
                for (i = 0; i < dtQuoteDetails.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();

        }




    }
}