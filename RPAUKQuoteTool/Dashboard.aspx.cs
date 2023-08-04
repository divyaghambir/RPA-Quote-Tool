using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL_Layer;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

namespace RPADubaiQuoteTool
{
    public partial class Dashboard : System.Web.UI.Page
    {
        DashboardBAL obj = new DashboardBAL();

        protected void Page_Load(object sender, EventArgs e)
            {
            bool blnlogin=false;
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

                        if (UserRole == "Sales Engineer" || UserRole == "Sales Manager" || UserRole == "Admin")
                        {
                            btnCreateQuote.Visible = true;
                        }
                        if (UserRole == "Admin")
                        {
                            btnGenerateReport.Visible = true;
                        }
                        else
                        {
                            btnGenerateReport.Visible = false;
                        }

                        DataTable dt = new DataTable();
                        dt = obj.LoadDashboard(UserName, false);
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row[2].ToString() == "SUNDRY ACCOUNT")
                            {
                                DataTable dt1 = obj.GetCustomerName(row[0].ToString());
                                if (dt1.Rows.Count > 0)
                                {
                                    row[2] = dt1.Rows[0][0].ToString();
                                }
                            }

                        }
                        grdDashboard.DataSource = dt;
                        grdDashboard.DataBind();

                        foreach (GridViewRow row in grdDashboard.Rows)
                        {
                            if ((DateTime.ParseExact(row.Cells[7].Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.Now).Days <= 5 && (DateTime.ParseExact(row.Cells[7].Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.Now).Days > 3)
                            {
                                if (row.Cells[8].Text.ToUpper() == "APPROVED")
                                {
                                    row.BackColor = System.Drawing.Color.Orange;
                                }
                            }
                            else if ((DateTime.ParseExact(row.Cells[7].Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.Now).Days <= 3)
                            {
                                if (row.Cells[8].Text.ToUpper() == "APPROVED")
                                {
                                    row.BackColor = System.Drawing.Color.Red;
                                }
                            }
                        }


                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
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
                Response.Redirect("Quote.aspx?QuoteNo=" + QuoteNo + "&Status=" + Status + "&UserName=" + UserName + "&UserRole=" + UserRole);
            }
        }


        protected void btnSearchQuote_Click(object sender, EventArgs e)
        {
            // string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string ColumnName = drpSearch.SelectedItem.Text.Trim();
            string ColumnValue = txtSearch.Text.Trim();
            string Username = (string)Session["UserName"];
            string UserRole = (string)Session["UserRole"];
            DataTable dt = new DataTable();
            if (ColumnName == "Customer Name")
            {
                ColumnName = "CustomerName";
            }
            else if (ColumnName == "Customer Number")
            {
                ColumnName = "CustomerNo";
            }
            else if (ColumnName == "Type")
            {
                ColumnName = "isPerforma";
            }

            if (ColumnName == "Select")
            {
                if (UserRole == "Sales Engineer")
                {
                    string PreparedBy = "[Prepared By] ='" + Username + "' or [Prepared By] ='On Behalf Of " + Username + "'";
                    //dt = obj.LoadDashboard(Username, PreparedBy);
                    dt = obj.LoadDashboard(Username,true);
                }
                else if (UserRole == "Sales Manager")
                {
                    string PreparedBy = "";

                    PreparedBy = "[Prepared By] = 'Kirsty Anderson' or [Prepared By] = 'On Behalf Of Kirsty Anderson' or [Prepared By] = 'test_SE' or[Prepared By] = 'On Behalf Of test_SE' or [Prepared By] = 'Trevor Harling' or [Prepared By] = 'On Behalf Of Trevor Harling' or [Prepared By] = 'Tomas Diaz' or [Prepared By] = 'On Behalf Of Tomas Diaz' or [Prepared By] = 'Richard Price' or [Prepared By] = 'On Behalf Of Richard Price' or [Prepared By] = 'Juliana Paradello' or [Prepared By] = 'On Behalf Of Juliana Paradello' or [Prepared By] = 'Andrew Clarke' or [Prepared By] = 'On Behalf Of Andrew Clarke' or  [Prepared By] = 'Maria Kennedy' or [Prepared By] = 'On Behalf Of Maria Kennedy' or [Prepared By] = '" + Username + "'";

                    dt = obj.LoadDashboard(Username, PreparedBy);
                }

                else
                {
                    dt = obj.LoadDashboard(Username,true);
                }

            }
            else if (ColumnName == "SalesEngineer")
            {
                string PreparedBy = "[Prepared By] ='" + txtSearch.Text + "' or [Prepared By] ='On Behalf Of " + txtSearch.Text + "'";
                dt = obj.LoadDashboard(Username, PreparedBy);
            }
            else
            {
                //dt = obj.LoadFilteredDashboard(Username, ColumnName, ColumnValue);
                dt = obj.LoadFilteredDashboard(ColumnName, ColumnValue);

            }

            foreach (DataRow row in dt.Rows)
            {
                if (row[2].ToString() == "SUNDRY ACCOUNT")
                {
                    DataTable dt1 = obj.GetCustomerName(row[0].ToString());
                    if (dt1.Rows.Count > 0)
                    {
                        row[2] = dt1.Rows[0][0].ToString();
                    }
                }

            }

            grdDashboard.DataSource = dt;
            grdDashboard.DataBind();

            foreach (GridViewRow row in grdDashboard.Rows)
            {
                if ((DateTime.ParseExact(row.Cells[7].Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.Now).Days <= 5 && (DateTime.ParseExact(row.Cells[7].Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.Now).Days > 3)
                {
                    if (row.Cells[8].Text.ToUpper() == "APPROVED")
                    {
                        row.BackColor = System.Drawing.Color.Orange;
                    }
                }
                else if ((DateTime.ParseExact(row.Cells[7].Text.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.Now).Days <= 3)
                {
                    if (row.Cells[8].Text.ToUpper() == "APPROVED")
                    {
                        row.BackColor = System.Drawing.Color.Red;
                    }
                }
            }

        }

        protected void btnCreateQuote_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateNewQuote.aspx");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            
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

        protected void drpSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("GenerateReport.aspx");
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