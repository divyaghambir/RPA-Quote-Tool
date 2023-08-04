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

namespace RPADubaiQuoteTool
{
    public partial class GenerateReport : System.Web.UI.Page
    {
        CreateReportBAL obj = new CreateReportBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
          
        }

      
        protected void cldCReationDateStart_SelectionChanged(object sender, EventArgs e)
        {
            TextBox1.Text = cldCReationDateStart.SelectedDate.ToShortDateString();  //CReation Date Start
            cldCReationDateStart.Visible = false;
        }

        protected void cldCReationDateEnd_SelectionChanged(object sender, EventArgs e)
        {
            TextBox2.Text = cldCReationDateEnd.SelectedDate.ToShortDateString(); //Creation date end
            cldCReationDateEnd.Visible = false;
        }

        protected void imgCreationDtStart_Click(object sender, ImageClickEventArgs e)
        {
            cldCReationDateStart.Visible = true;
        }

        protected void imgCreationDtEnd_Click(object sender, ImageClickEventArgs e)
        {
            cldCReationDateEnd.Visible = true;
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {

            if ((TextBox1.Text != string.Empty && TextBox2.Text == string.Empty) || (TextBox2.Text != string.Empty && TextBox2.Text == string.Empty))
            {
                lblError.Text = "Enter both start and end dates";
                return;

            }
            else if (TextBox1.Text!=string.Empty && TextBox2.Text!=string.Empty)
            { 
                if(DateTime.Parse(TextBox2.Text) < DateTime.Parse(TextBox1.Text))
                { 
                 lblError.Text = "Start date should be earlier than the end date";
                 return;
                }
            }

            System.Data.DataTable dtReport = new System.Data.DataTable();
            string SelectedStatus;
            if (drpStatus.SelectedIndex != 0)
            {
                SelectedStatus = drpStatus.SelectedValue;
            }
            else
                SelectedStatus = string.Empty;
            
                dtReport = obj.CreateReport(TextBox1.Text, TextBox2.Text, SelectedStatus);


            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dtReport,"Quotations Report");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Report.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }


        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

            /*if (Convert.ToDateTime(TextBox1.Text) > DateTime.Now)
            {
                lblError.Text = "Date cannot be greater than today";
            }
            else if (Convert.ToDateTime(TextBox1.Text) > Convert.ToDateTime(TextBox2.Text))
            {
                lblError.Text = "Start date cannot be greater than the end date";
            }*/

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            /*if (Convert.ToDateTime(TextBox2.Text) > DateTime.Now)
            {
                lblError.Text = "Date cannot be greater than today";
            }
            else if (TextBox1.Text == string.Empty)
            {
                lblError.Text = "Start date cannot be blank";
            }
            else if (Convert.ToDateTime(TextBox1.Text) > Convert.ToDateTime(TextBox2.Text))
            {
                lblError.Text = "Start date cannot be greater than the end date";
            }
            */

        }
    }
}