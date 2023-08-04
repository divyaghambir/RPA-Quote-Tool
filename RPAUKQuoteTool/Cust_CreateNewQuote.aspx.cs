using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL_Layer;
using System.Data;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using RPADubaiQuoteTool;

namespace RPAUKCustomerQuote
{
    public partial class Cust_CreateNewQuote : System.Web.UI.Page
    {

        Boolean blnSundryItem = false;
        Cust_QuoteBAL objQuoteBAL = new Cust_QuoteBAL();
        Cust_CreateQuoteBAL obj = new Cust_CreateQuoteBAL();
        string UserRole = "";
        string UserName = "";

        protected void Page_Load(object sender, EventArgs e)
        {

                if (Session["login"] != null)
            {
                bool blnlogin = false;
                blnlogin = (bool)Session["login"];
                if (blnlogin)
                {

                    UserRole = (string)Session["UserRole"];
                    UserName = (string)Session["UserName"];

                    lblError.Text = "";
                    if (!IsPostBack)
                    {
                        FetchDetails();
                       
                        UserRole = (string)Session["UserRole"];
                        UserName = (string)Session["UserName"];
                      
                            drpPreparedBy.SelectedItem.Text = UserName;
                            drpPreparedBy.Enabled = false;

                            
                            DataTable dtSEEMailPhone = obj.GetUserInfo(UserName);
                            txtSEEMail.Text = dtSEEMailPhone.Rows[0][2].ToString();
                            txtSEPhone.Text = dtSEEMailPhone.Rows[0][3].ToString();






                        DataTable dt = new DataTable();


                        dt.Columns.AddRange(new DataColumn[15] { new DataColumn("Product Family"), new DataColumn("ItemNo"), new DataColumn("PartNo"), new DataColumn("Description"), new DataColumn("QTY"), new DataColumn("AvailableQty"), new DataColumn("StockAvailability"), new DataColumn("Weight"), new DataColumn("MOQ"), new DataColumn("LeadTime"), new DataColumn("SafetyStock"), new DataColumn("ListPrice"), new DataColumn("Discount"), new DataColumn("CostPrice"), new DataColumn("Total") });
                        DataRow dr = null;
                        dr = dt.NewRow();
                        // dr["Platform"] = string.Empty;
                        dr["PartNo"] = string.Empty;
                        dr["ItemNo"] = string.Empty;
                        dr["Product Family"] = string.Empty;
                        dr["Description"] = string.Empty;
                        dr["MOQ"] = string.Empty;
                        dr["AvailableQty"] = string.Empty;
                        dr["StockAvailability"] = string.Empty;
                        dr["Weight"] = string.Empty;
                        dr["LeadTime"] = string.Empty;
                        dr["SafetyStock"] = string.Empty;
                        dr["ListPrice"] = string.Empty;
                        dr["Discount"] = string.Empty;
                        dr["CostPrice"] = string.Empty;
                        dr["Total"] = string.Empty;
                        dr["QTY"] = "1";
                        
                        dt.Rows.Add(dr);
                        GridView1.DataSource = dt; // bind new datatable to grid
                        GridView1.DataBind();
                        ViewState["dt"] = dt;
                      
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


        private void GetQuoteNumber()
        {
            string QuoteNumber = "";
            string UserName = (string)Session["UserName"];
            string ID = obj.GetQuoteNumber(UserName);
            //ViewState["SerialNo"] = QuoteNumber;
           
            UserName = UserName.ToString().ToUpper().Substring(0, 3);
            
            if (ID == "")
            {
                ID = DateTime.Today.ToString("ddmmss");
            }
            QuoteNumber = UserName + ID;
            txtQuoteNum.Text = QuoteNumber;
            obj.UpdateQuoteNo(QuoteNumber, ID);
        }

        private void FetchDetails()
        {
           ;
            DateTime dtCreation;
            Boolean bl = DateTime.Now.IsDaylightSavingTime();
            if (bl == true)
            {
                dtCreation = DateTime.Now.AddHours(5);
            }
            else
            {
                dtCreation = DateTime.Now.AddHours(6);
            }
            txtCreationdate.Text = dtCreation.ToString("dd/MM/yyyy");

            DateTime startDate = dtCreation;
            DateTime expiryDate = startDate.AddMonths(1);
            txtExpirationDate.Text = expiryDate.ToString("dd/MM/yyyy");

                  

        }

      

        protected void btnAddNewItem_Click(object sender, EventArgs e)
        {

            //DataTable dt = new DataTable();
            //if (ViewState["CurrentTable"] != null)
            //{
            //    dt = (DataTable)ViewState["CurrentTable"];
            //}
            //DataRow dr = null;
            Page.Validate("AddNewItem");
            if (Page.IsValid)
            {
                DataTable dt = new DataTable();
                if (GridView1.Rows.Count > 0)
                {

                    for (int i = 0; i < GridView1.Columns.Count; i++)
                    {
                        string headername = GridView1.Columns[i].HeaderText;

                        dt.Columns.Add(headername);
                    }
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        DataRow dr = dt.NewRow();



                        for (int j = 0; j < GridView1.Columns.Count; j++)
                        {
                            //string a = Server.HtmlDecode(row.Cells[j].Text.Trim());

                            dr[j] = Server.HtmlDecode((row.Cells[j].Text.Trim()));

                        }
                        TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                        dr["PartNo"] = PartNum.Text;
                        TextBox Desc = (TextBox)row.FindControl("txtDesc");
                        dr["Description"] = Desc.Text;
                        TextBox QTY = (TextBox)row.FindControl("txtQTY");

                        dr["QTY"] = QTY.Text;

                       
                        
                        dt.Rows.Add(dr);
                    }
                }
                //GridView1.DataBind();
                DataRow dr1 = dt.NewRow();
                dr1 = dt.NewRow(); // add last empty row
                dr1["QTY"] = "1";
                dt.Rows.Add(dr1);
                ViewState["dt"] = dt;
                // ViewState["CurrentTable"] = dt;
                GridView1.DataSource = dt; // bind new datatable to grid
                GridView1.DataBind();

                if (GridView1.Rows.Count > 1)
                {
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                        PartNum.Focus();
                    }

                }
                //for (int i = 0; i < GridView1.Rows.Count; i++)
                //{
                //    foreach (GridViewRow row in GridView1.Rows)
                //    {

                //        TextBox PartNum = (TextBox)row.FindControl("txtPartNumber");
                //        PartNum.Focus();                    
                //    }

                //}
            }
        }



        protected void txtPartNo_TextChanged(object sender, EventArgs e)
        {

            ////Read existing table data
            DataTable dt = new DataTable();
            if (GridView1.Rows.Count > 0)
            {

                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    string headername = GridView1.Columns[i].HeaderText;

                    dt.Columns.Add(headername);
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    DataRow dr = dt.NewRow();

                    for (int j = 0; j < GridView1.Columns.Count; j++)
                    {
                        dr[j] = Server.HtmlDecode(row.Cells[j].Text);

                    }
                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                    dr["PartNo"] = PartNum.Text;
                    TextBox Desc = (TextBox)row.FindControl("txtDesc");
                    dr["Description"] = Desc.Text;
                    TextBox QTY = (System.Web.UI.WebControls.TextBox)row.FindControl("txtQTY");
                    dr["QTY"] = QTY.Text;
                   
                  
                    dt.Rows.Add(dr);
                }
            }

            //Get Item data based on part no
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txt = (TextBox)currentRow.FindControl("txtPartNo");
            string PartNo = "";

            if (txt.Text != string.Empty)
            {
                PartNo = txt.Text.ToString().Trim();
            }
            DataTable dtLineItem = new DataTable();
            dtLineItem = obj.GetItemDetails(PartNo, "NULL");
            int RowId = currentRow.DataItemIndex;
            if (dtLineItem.Rows.Count > 0)
            {



                dt.Rows[RowId]["Product Family"] = obj.ItemDiscGroup(dtLineItem.Rows[0]["ItemNo"].ToString());
                dt.Rows[RowId]["ItemNo"] = dtLineItem.Rows[0]["ItemNo"].ToString();
                dt.Rows[RowId]["PartNo"] = dtLineItem.Rows[0]["LegacyPartNo"].ToString();
                dt.Rows[RowId]["Description"] = dtLineItem.Rows[0]["Description1"].ToString();
                dt.Rows[RowId]["MOQ"] = dtLineItem.Rows[0]["MinOrderQty"].ToString();
                dt.Rows[RowId]["LeadTime"] = dtLineItem.Rows[0]["LeadTime"].ToString();
                dt.Rows[RowId]["AvailableQty"] = dtLineItem.Rows[0]["AvailableQty"].ToString();
                dt.Rows[RowId]["Weight"] = dtLineItem.Rows[0]["Weight"].ToString();

                float CostPrice = 0;
                // CostPrice = float.Parse(dtLineItem.Rows[0]["CostPrice"].ToString());
                dt.Rows[RowId]["CostPrice"] = "";

                dt.Rows[RowId]["ListPrice"] = string.Empty;
                dt.Rows[RowId]["Discount"] = string.Empty;

                dt.Rows[RowId]["Total"] = string.Empty;

                float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);

                dt.Rows[RowId]["SafetyStock"] = dtLineItem.Rows[0]["SafetyStock"].ToString();


                //07/06/2022/////////////////////////StockAvailablity
                DataTable dtAvailableStock = new DataTable();

                dtAvailableStock = obj.GetStock(PartNo);

                if (dtAvailableStock.Rows.Count > 0)
                {


                    dt.Rows[RowId]["StockAvailability"] = dtAvailableStock.Rows[0]["Quantity Open"].ToString() + " on " + DateTime.Parse(dtAvailableStock.Rows[0]["Due Date"].ToString()).ToString("dd/MM/yy");
                }
                else
                {
                    dt.Rows[RowId]["StockAvailability"] = "";
                }
                //////////////////////////////////////


                string netprice = string.Empty;
                ///float CostPrice = 0;
                DataTable dtCust = new DataTable();
                dtCust = obj.GetCompany(UserName);
                string cstNo = dtCust.Rows[0][0].ToString();
                netprice = obj.GetNetPrice(cstNo, PartNo, string.Empty, drpCurrency.SelectedItem.Text);

                if (netprice != string.Empty)
                {
                    dt.Rows[RowId]["ListPrice"] = string.Empty;
                    dt.Rows[RowId]["Discount"] = string.Empty;


                    CostPrice = float.Parse(netprice);

                    dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");

                    string Quantity = dt.Rows[RowId]["QTY"].ToString();
                    int Qty = 0;



                    if (Quantity != string.Empty)
                    {
                        Qty = Convert.ToInt32(Quantity);
                    }

                    float Total = 0;
                    Total = CostPrice * Qty;

                    dt.Rows[RowId]["Total"] = Total;

                }
                else
                {
                    DataTable dtPrice = new DataTable();
                    dtPrice = obj.GetPrice(PartNo, "NULL", "GBP");


                    if (dtPrice.Rows.Count != 0)
                    {
                        if (drpCurrency.SelectedItem.Text == "EUR")
                        {

                            dt.Rows[RowId]["ListPrice"] = (float.Parse(dtPrice.Rows[0]["ListPrice"].ToString()) * rate).ToString();
                        }
                        else
                        {
                            dt.Rows[RowId]["ListPrice"] = dtPrice.Rows[0]["ListPrice"].ToString();
                        }


                        float Disc = 0;

                        DataTable dtDisc = new DataTable();
                        dtDisc = obj.GetDiscount(dtLineItem.Rows[0]["ItemNo"].ToString(), cstNo);
                        if (dtDisc.Rows.Count != 0)
                        {
                            dt.Rows[RowId]["Discount"] = dtDisc.Rows[0]["DiscountPerc"].ToString();
                            Disc = float.Parse(dtDisc.Rows[0]["DiscountPerc"].ToString());


                        }
                        else
                        {
                            dt.Rows[RowId]["Discount"] = "";
                            Disc = 0;
                        }

                        float Listprice = float.Parse(dtPrice.Rows[0]["ListPrice"].ToString());
                        if (drpCurrency.SelectedItem.Text == "EUR")
                        {
                            Listprice = Listprice * rate;
                        }



                        CostPrice = Listprice - (Listprice * Disc / 100);

                        dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");



                        string Quantity = dt.Rows[RowId]["QTY"].ToString();
                        int Qty = 0;

                        if (Quantity != string.Empty)
                        {
                            Qty = Convert.ToInt32(Quantity);
                        }

                        float Total = 0;
                        Total = CostPrice * Qty;

                        dt.Rows[RowId]["Total"] = Total;
                    }
                    else
                    {
                        dt.Rows[RowId]["ListPrice"] = string.Empty;
                        dt.Rows[RowId]["Discount"] = string.Empty;
                        dt.Rows[RowId]["CostPrice"] = "POA";

                    }
                }

            }
            else
            {
                dt.Rows[RowId]["CostPrice"] = "";

                dt.Rows[RowId]["ListPrice"] = string.Empty;
                dt.Rows[RowId]["Discount"] = string.Empty;

                dt.Rows[RowId]["Total"] = string.Empty;

                dt.Rows[RowId]["Product Family"] = string.Empty;
                dt.Rows[RowId]["ItemNo"] = string.Empty;
              
                dt.Rows[RowId]["Description"] = string.Empty;
                dt.Rows[RowId]["MOQ"] = string.Empty;
                dt.Rows[RowId]["LeadTime"] = string.Empty;
                dt.Rows[RowId]["AvailableQty"] = string.Empty;
                dt.Rows[RowId]["Weight"] = string.Empty;
            }


            ViewState["dt"] = dt;
           
            GridView1.DataSource = dt; // bind new datatable to grid
            
            GridView1.DataBind();
          
            float GrandTotal = 0;
           

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string TotalPrice = dt.Rows[i]["Total"].ToString().Trim();
                if (TotalPrice == string.Empty)
                {
                    TotalPrice = "0";
                }
                if (TotalPrice != string.Empty)
                {
                    GrandTotal = GrandTotal + float.Parse(TotalPrice);

                   

                }
               
            }

          
            

            TxtGrandTotal.Text = GrandTotal.ToString("0.00");
            TextBox setfocus = (TextBox)currentRow.FindControl("txtPartNo");
            setfocus.Focus();
        }

        protected void txtDesc_TextChanged(object sender, EventArgs e)
        {

            ////Read existing table data
            DataTable dt = new DataTable();
            if (GridView1.Rows.Count > 0)
            {

                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    string headername = GridView1.Columns[i].HeaderText;

                    dt.Columns.Add(headername);
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    DataRow dr = dt.NewRow();

                    for (int j = 0; j < GridView1.Columns.Count; j++)
                    {
                        dr[j] = Server.HtmlDecode(row.Cells[j].Text);

                    }
                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                    dr["PartNo"] = PartNum.Text;
                    TextBox Desc = (TextBox)row.FindControl("txtDesc");
                    dr["Description"] = Desc.Text;
                    TextBox QTY = (System.Web.UI.WebControls.TextBox)row.FindControl("txtQTY");
                    dr["QTY"] = QTY.Text;
                   
                    dt.Rows.Add(dr);
                }
            }

            //Get Item data based on part no
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txt = (TextBox)currentRow.FindControl("txtDesc");
            string strDesc = "";

            if (txt.Text != string.Empty)
            {
                strDesc = txt.Text.ToString().Trim();
            }
            DataTable dtLineItem = new DataTable();
            dtLineItem = obj.GetItemDetails("NULL", strDesc);
            int RowId = currentRow.DataItemIndex;
            if (dtLineItem.Rows.Count > 0)
            {

                if (dtLineItem.Rows[0]["Status"].ToString() == "240")
                {
                    lblMessage.Text = "STATUS 240 ITEM – CHECK QTY AVAILABLE";
                }
                else
                {
                    lblMessage.Text = "";
                }
                dt.Rows[RowId]["Product Family"] = obj.ItemDiscGroup(dtLineItem.Rows[0]["ItemNo"].ToString());
                dt.Rows[RowId]["ItemNo"] = dtLineItem.Rows[0]["ItemNo"].ToString();
                dt.Rows[RowId]["PartNo"] = dtLineItem.Rows[0]["LegacyPartNo"].ToString();
                dt.Rows[RowId]["Description"] = dtLineItem.Rows[0]["Description1"].ToString();
                dt.Rows[RowId]["MOQ"] = dtLineItem.Rows[0]["MinOrderQty"].ToString();
                dt.Rows[RowId]["LeadTime"] = dtLineItem.Rows[0]["LeadTime"].ToString();
                dt.Rows[RowId]["AvailableQty"] = dtLineItem.Rows[0]["AvailableQty"].ToString();
                dt.Rows[RowId]["Weight"] = dtLineItem.Rows[0]["Weight"].ToString();
                float CostPrice = 0;
                CostPrice = float.Parse(dtLineItem.Rows[0]["CostPrice"].ToString());

                float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
              
                dt.Rows[RowId]["SafetyStock"] = dtLineItem.Rows[0]["SafetyStock"].ToString();

                dt.Rows[RowId]["CostPrice"] = "";

                dt.Rows[RowId]["ListPrice"] = string.Empty;
                dt.Rows[RowId]["Discount"] = string.Empty;

                dt.Rows[RowId]["Total"] = string.Empty;

                //07/06/2022/////////////////////////StockAvailablity
                DataTable dtAvailableStock = new DataTable();

                dtAvailableStock = obj.GetStock(dtLineItem.Rows[0]["LegacyPartNo"].ToString());

                if (dtAvailableStock.Rows.Count > 0)
                {


                    dt.Rows[RowId]["StockAvailability"] = dtAvailableStock.Rows[0]["Quantity Open"].ToString() + " on " + DateTime.Parse(dtAvailableStock.Rows[0]["Due Date"].ToString()).ToString("dd/MM/yy");
                }
                else
                {
                    dt.Rows[RowId]["StockAvailability"] = "";
                }
                //////////////////////////////////////

                string netprice = string.Empty;
                //float CostPrice = 0;
                DataTable dtCust = new DataTable();
                dtCust = obj.GetCompany(UserName);
                string cstNo = dtCust.Rows[0][0].ToString();
                netprice = obj.GetNetPrice(cstNo, string.Empty, strDesc, drpCurrency.SelectedItem.Text);

                if (netprice != string.Empty)
                {
                    dt.Rows[RowId]["ListPrice"] = string.Empty;
                    dt.Rows[RowId]["Discount"] = string.Empty;


                    CostPrice = float.Parse(netprice);

                    dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");

                }
                else
                {
                    DataTable dtPrice = new DataTable();
                    dtPrice = obj.GetPrice("NULL", strDesc, "GBP");


                    if (dtPrice.Rows.Count != 0)
                    {

                        dt.Rows[RowId]["ListPrice"] = dtPrice.Rows[0]["ListPrice"].ToString();



                      
                        DataTable dtDisc = new DataTable();
                        float Disc = 0;
                        dtDisc = obj.GetDiscount(dtLineItem.Rows[0]["ItemNo"].ToString(), cstNo);
                        if (dtDisc.Rows.Count != 0)
                        {
                            dt.Rows[RowId]["Discount"] = dtDisc.Rows[0]["DiscountPerc"].ToString();
                            Disc = float.Parse(dtDisc.Rows[0]["DiscountPerc"].ToString());
                        }
                        else
                        {
                            dt.Rows[RowId]["Discount"] = "";
                            Disc = 0;
                        }


                        float Listprice = float.Parse(dtPrice.Rows[0]["ListPrice"].ToString());
                        if (drpCurrency.SelectedItem.Text == "EUR")
                        {
                            Listprice = Listprice * rate;
                        }


                        CostPrice = Listprice - (Listprice * Disc / 100);

                        dt.Rows[RowId]["Unit Price"] = CostPrice.ToString("0.00");
                    }

                }

               
                string Quantity = dt.Rows[RowId]["QTY"].ToString();
                int Qty = 0;
               

               
                if (Quantity != string.Empty)
                {
                    Qty = Convert.ToInt32(Quantity);
                }
                
            }
            else
            {
               
                    dt.Rows[RowId]["CostPrice"] = "";

                    dt.Rows[RowId]["ListPrice"] = string.Empty;
                    dt.Rows[RowId]["Discount"] = string.Empty;

                    dt.Rows[RowId]["Total"] = string.Empty;

                    dt.Rows[RowId]["Product Family"] = string.Empty;
                    dt.Rows[RowId]["ItemNo"] = string.Empty;

                    dt.Rows[RowId]["Description"] = string.Empty;
                    dt.Rows[RowId]["MOQ"] = string.Empty;
                    dt.Rows[RowId]["LeadTime"] = string.Empty;
                    dt.Rows[RowId]["AvailableQty"] = string.Empty;
                    dt.Rows[RowId]["Weight"] = string.Empty;
                


            }



            ViewState["dt"] = dt;
            // ViewState["CurrentTable"] = dt;
            GridView1.DataSource = dt; // bind new datatable to grid
            //GridView1.Columns[11].HeaderText = "Additional Discount%";
            //GridView1.Columns[14].HeaderText = "GM%";
            GridView1.DataBind();
            ////obj.testmethod();
            float GrandTotal = 0;
            float TotalCost = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string TotalPrice = dt.Rows[i]["CostPrice"].ToString().Trim();
                if (TotalPrice == string.Empty)
                {
                    TotalPrice = "0";
                }
                if (TotalPrice != string.Empty)
                {
                    GrandTotal = GrandTotal + float.Parse(TotalPrice);
                }
               

            }

          
            TxtGrandTotal.Text = GrandTotal.ToString("0.00");
            TextBox setfocus = (TextBox)currentRow.FindControl("txtPartNo");
            setfocus.Focus();
        }

        protected void SelectCheckBox_OnCheckedChanged(object sender, EventArgs e)
        {

            ////Read existing table data
            DataTable dt = new DataTable();

            if (GridView1.Rows.Count > 0)
            {

                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    string headername = GridView1.Columns[i].HeaderText;
                    /* if (headername == "Discount%")
                     {
                         headername = "Additional Discount";
                     }

                     else if (headername == "GM%")
                     {
                         headername = "GM";
                     }*/
                    dt.Columns.Add(headername);
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < GridView1.Columns.Count; j++)
                    {
                        dr[j] = Server.HtmlDecode(row.Cells[j].Text);

                    }
                    TextBox PartNum = (TextBox)row.FindControl("txtPartNumber");
                    dr["PartNo"] = PartNum.Text;
                    TextBox QTY = (TextBox)row.FindControl("txtQTY");
                    dr["QTY"] = QTY.Text;
                    TextBox Discount = (TextBox)row.FindControl("txtDiscount");
                    dr["AdditionalDiscount"] = Discount.Text;
                    dt.Rows.Add(dr);
                }
            }
            CheckBox chk = sender as CheckBox;
            GridViewRow currentRow = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int RowId = currentRow.DataItemIndex;
            if (chk.Checked)
            {
                DataRow dr1 = dt.Rows[RowId];
                dr1.Delete();
            }
            if (dt.Rows.Count < 1)
            {
                DataRow dr1 = dt.NewRow();
                dr1 = dt.NewRow(); // add last empty row
                dt.Rows.Add(dr1);
            }
            GridView1.DataSource = dt; // bind new datatable to grid
                                       // GridView1.Columns[8].HeaderText = "Discount%";
                                       //GridView1.Columns[11].HeaderText = "GM%";
            GridView1.DataBind();
            ViewState["dt"] = dt;
            float GrandTotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string TotalPrice = dt.Rows[i]["CostPrice"].ToString().Trim();
                if (TotalPrice != string.Empty)
                {
                    GrandTotal = GrandTotal + float.Parse(TotalPrice);
                }

            }
            CheckBox setfocus = (CheckBox)currentRow.FindControl("SelectCheckBox");
            setfocus.Focus();
        }
        protected void txtQTY_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            //int RowId = currentRow.DataItemIndex;


            TextBox txtQty = (TextBox)currentRow.FindControl("txtQty");


            if (txtQty.Text == "0")
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid Qty')</script>");
                lblError.Text = "Invalid Qty";
                return;
            }
            if (currentRow.Cells[15].Text.Trim() == "&nbsp;" || currentRow.Cells[15].Text.Trim() == "")
            {
                lblError.Text = "Cost Price not found for this item";
                return;
            }
            else
            {
                GetDiscountDetails(sender);
            }
        }

        public void GetDiscountDetails(object sender)
        {
            ////Read existing table data
            DataTable dt = new DataTable();
            string CostPrice = "";
            if (GridView1.Rows.Count > 0)
            {

                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    string headername = GridView1.Columns[i].HeaderText;
                    dt.Columns.Add(headername);
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    DataRow dr = dt.NewRow();



                    for (int j = 0; j < GridView1.Columns.Count; j++)
                    {
                        dr[j] = Server.HtmlDecode(row.Cells[j].Text);

                    }
                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                    dr["PartNo"] = PartNum.Text;
                    if (PartNum.Text == string.Empty)
                    {
                        continue;
                    }
                    float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                    

                   
                  
                    TextBox Desc = (TextBox)row.FindControl("txtDesc");
                    dr["Description"] = Desc.Text;
                    TextBox QTY = (TextBox)row.FindControl("txtQTY");
                    dr["QTY"] = QTY.Text;
                   
                  
                    dt.Rows.Add(dr);
                }
            }
            //Get Item data based on part no
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            int RowId = currentRow.DataItemIndex;
            int Quantity = 0;
          
            string txtQty = dt.Rows[RowId]["QTY"].ToString();
          
            float Discount = 0;
            CostPrice = dt.Rows[RowId]["CostPrice"].ToString().Trim();


            if (txtQty != string.Empty)
            {
                Quantity = Convert.ToInt32(txtQty);
            }



            dt.Rows[RowId]["Total"] = (float.Parse(CostPrice) * Quantity).ToString();
            ViewState["dt"] = dt;
            GridView1.DataSource = dt; // bind new datatable to grid

            GridView1.DataBind();
          

            float GrandTotal = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["CostPrice"].ToString().Trim() != string.Empty && dt.Rows[i]["CostPrice"].ToString().Trim() != "POA")
                {
                    GrandTotal = GrandTotal + float.Parse(dt.Rows[i]["CostPrice"].ToString().Trim()) * Convert.ToInt32(dt.Rows[i]["Qty"].ToString());
                }
            }

            TxtGrandTotal.Text = GrandTotal.ToString("0.00");
           

            TextBox setfocus = (TextBox)currentRow.FindControl("txtPartNo");
            setfocus.Focus();
        }



        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            GetDiscountDetails(sender);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("Save");
            if (Page.IsValid)
            {


                bool blnQtyZero = false;
                bool blnCOstPrice = false;
                string Cost_Price = "";
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    TextBox txtQty = (TextBox)gr.FindControl("txtQty");
                    if (txtQty.Text == "0")
                    {
                        blnQtyZero = true;
                    }
                    if (gr.Cells[15].Text.Trim() == "&nbsp;" || gr.Cells[15].Text.Trim() == "&#160;" || gr.Cells[15].Text.Trim() == "")
                    {
                        blnCOstPrice = true;
                    }

                   
                }


                if (blnQtyZero)
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid Qty')</script>");
                    lblError.Text = "Invalid Qty";
                    return;
                }
                if (blnCOstPrice)
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid Qty')</script>");
                    lblError.Text = "Cost Price not found for this item";
                    return;
                }

                else
                {
                    string Quote = txtQuoteNum.Text.Trim();
                    if (Quote == string.Empty)
                    {
                        GetQuoteNumber();
                    }
                    string Status = "Draft";
                    SaveQuoteDetails(Status);
                    string QuoteNumber = txtQuoteNum.Text;
                    string Message = "Quote saved successfully with quote number " + QuoteNumber;
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('" + Message + "')</script>");
                    //Server.Transfer("Dashboard.aspx");

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('" + Message + "');window.location ='Cust_Dashboard.aspx';",
                   true);
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Quote saved successfully')</script>");
                }


            }
        }
        private iTextSharp.text.Font font = FontFactory.GetFont("Times Roman", 11, iTextSharp.text.Font.TIMES_ROMAN);
        private iTextSharp.text.Font fontAdd = FontFactory.GetFont("Times Roman", 8, iTextSharp.text.Font.NORMAL);
        private iTextSharp.text.Font fontRed = FontFactory.GetFont("Times Roman", 11, Color.RED);

        private void SaveQuoteDetails(string Status)
        {
            //Get Control details
            string QuoteNumber = txtQuoteNum.Text;
            string CustomerName = txtCustName.Text;
            string CustomerNumber = string.Empty;
            CustomerNumber =txtCustNo.Text;
          

            string CustomerEmail = txtCustEmail.Text;
            string CustomerPhone = txtCustPhone.Text;
            string ProjectName = txtProjectName.Text;
            string RefNo = txtRefNo.Text;

            string Currency = drpCurrency.SelectedItem.Text;
            string PreparedBy = drpPreparedBy.SelectedItem.Text;
            string PreparedByEmail = txtSEEMail.Text;
            string PreparedByPhone = txtSEPhone.Text;
            string SalesPerson = txtSalesPerson.Text;
            string SalesPersonEmail = txtSPEmail.Text;
            string SalesPersonPhone = txtSPPhone.Text;
            
            string Comments = txtComments.Text;


            if (FileUpload1.HasFile)
            {
                string folderName = @"C:\UKCustomerQuote\App_Data\AdditonalDocs";

                string pathString = System.IO.Path.Combine(folderName, QuoteNumber);
                System.IO.Directory.CreateDirectory(pathString);

                FileUpload1.SaveAs(System.IO.Path.Combine(pathString, FileUpload1.FileName));

            }



            string UserRole = (string)Session["UserRole"];
            if (UserRole == "Admin")
            {
                PreparedBy = "On Behalf Of " + PreparedBy;
            }
            DateTime CreationDate = DateTime.ParseExact(txtCreationdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime ExpirationDate = DateTime.ParseExact(txtExpirationDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


            string CarriageCharge = "";

          
            CarriageCharge = txtCarriage.Text;
            
            string ProductFamily = "";
            string ItemNo = "";
            string PartNo = "";
            string Desc = "";
            int QTY = 0;
            string MOQ = "";
            string LeadTime = "";
            string AvailableQty = "";
            string StockAvailable = "";
            string Weight = "";
            string SafetyStock = "";
            string ListPrice = "";
            string Discount = "";
         
            string CostPrice = "";
            string totalPrice = "";
            float AverageUnitPrice = 0;


            string Version = txtQuoteNum.Text + "_0";
            // string SerialNo = ViewState["SerialNo"].ToString();
          

            obj.DeleteExistingQuote(QuoteNumber);

            string total = TxtGrandTotal.Text;
            AverageUnitPrice = float.Parse(total);

            foreach (GridViewRow gr in GridView1.Rows)
            {
                if (gr.Cells[2].Text.Contains("&") == false && gr.Cells[2].Text.Contains("amp") == false && gr.Cells[2].Text.Contains("#160;") == false)
                {
                    ProductFamily = gr.Cells[2].Text;
                }
                else
                    ProductFamily = String.Empty;
                //ProductFamily = gr.Cells[1].Text;
                if (gr.Cells[3].Text.Contains("&") == false && gr.Cells[3].Text.Contains("amp") == false && gr.Cells[3].Text.Contains("#160;") == false)
                {
                    ItemNo = gr.Cells[3].Text;
                }
                else
                    ItemNo = String.Empty;
                TextBox PartNoTextbox = (TextBox)gr.FindControl("txtPartNo");
                PartNo = PartNoTextbox.Text;
                if (PartNo.ToString().ToUpper().Contains("SUNDRY"))
                {
                    blnSundryItem = true;
                }


                if (PartNo == string.Empty)
                {
                    continue;
                }

                TextBox DescTextbox = (TextBox)gr.FindControl("txtDesc");
                Desc = DescTextbox.Text;


                // Desc = gr.Cells[4].Text;
                TextBox QtyTextbox = (TextBox)gr.FindControl("txtQTY");
                string Qty = QtyTextbox.Text;
                if (Qty != string.Empty)
                {
                    QTY = Convert.ToInt32(Qty);
                }
                if (gr.Cells[7].Text.Contains("&") == false && gr.Cells[7].Text.Contains("amp") == false && gr.Cells[7].Text.Contains("#160;") == false)
                {
                    AvailableQty = gr.Cells[7].Text;
                }
                else
                    AvailableQty = String.Empty;

                ///07/06/22 Stock Availablity
                if (gr.Cells[8].Text.Contains("&") == false && gr.Cells[8].Text.Contains("amp") == false && gr.Cells[8].Text.Contains("#160;") == false)
                {
                    StockAvailable = gr.Cells[8].Text;
                }
                else
                    StockAvailable = String.Empty;

                //////////////////////////////////////////


                if (gr.Cells[9].Text.Contains("&") == false && gr.Cells[9].Text.Contains("amp") == false && gr.Cells[9].Text.Contains("#160;") == false)
                {
                    MOQ = gr.Cells[9].Text;
                }
                else
                    MOQ = String.Empty;
               
                               
                LeadTime = gr.Cells[10].Text;
                if (gr.Cells[11].Text.Contains("&") == false && gr.Cells[11].Text.Contains("amp") == false && gr.Cells[11].Text.Contains("#160;") == false)
                {
                    SafetyStock = gr.Cells[11].Text;
                }
                else
                    SafetyStock = String.Empty;

                // SafetyStock = gr.Cells[8].Text;

                if (gr.Cells[12].Text.Contains("&") == false && gr.Cells[12].Text.Contains("amp") == false && gr.Cells[12].Text.Contains("#160;") == false)
                {
                    Weight = gr.Cells[12].Text;
                }
                else
                    Weight = String.Empty;

                if (gr.Cells[13].Text.Contains("&") == false && gr.Cells[13].Text.Contains("amp") == false && gr.Cells[13].Text.Contains("#160;") == false)
                {
                    ListPrice = gr.Cells[13].Text;
                }
                else
                    ListPrice = String.Empty;
                //ListPrice = gr.Cells[9].Text;

                if (gr.Cells[14].Text.Contains("&") == false && gr.Cells[14].Text.Contains("amp") == false && gr.Cells[14].Text.Contains("#160;") == false)
                {
                    Discount = gr.Cells[14].Text;
                }
                else
                    Discount = String.Empty;


                if (gr.Cells[15].Text.Contains("&") == false && gr.Cells[15].Text.Contains("amp") == false && gr.Cells[15].Text.Contains("#160;") == false)
                {
                    CostPrice = gr.Cells[15].Text;
                }
                else
                    CostPrice = String.Empty;

                if (gr.Cells[16].Text.Contains("&") == false && gr.Cells[16].Text.Contains("amp") == false && gr.Cells[16].Text.Contains("#160;") == false)
                {
                    totalPrice = gr.Cells[16].Text;
                }
                else
                    totalPrice = String.Empty;



                obj.SaveQuote(QuoteNumber, CustomerName, CustomerNumber, CustomerEmail, CustomerPhone, ProjectName, RefNo, Currency, PreparedBy, PreparedByEmail, PreparedByPhone, SalesPerson, SalesPersonEmail, SalesPersonPhone, ProductFamily, ItemNo, PartNo, Desc, QTY, MOQ, LeadTime, AvailableQty, Weight, SafetyStock, ListPrice, Discount, CostPrice, totalPrice,Status, CreationDate, ExpirationDate, CarriageCharge, Version, Comments, total,StockAvailable);
               
            }
          


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate("Submit");
            if (Page.IsValid)
            {
                

                bool blnQtyZero = false;
                string Unit_Price = "";
                bool blnCOstPrice = false;
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    TextBox txtQty = (TextBox)gr.FindControl("txtQty");
                    if (txtQty.Text == "0")
                    {
                        blnQtyZero = true;
                    }

                    if (gr.Cells[15].Text.Trim() == "&nbsp;" || gr.Cells[15].Text.Trim() == "&#160;" || gr.Cells[15].Text.Trim() == "")
                    {
                        blnCOstPrice = true;
                    }


                }


                if (blnQtyZero)
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid Qty')</script>");
                    lblError.Text = "Invalid Qty";
                    return;
                }
                if (blnCOstPrice)
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid Qty')</script>");
                    lblError.Text = "Cost Price not found for this item";
                    return;
                }
                if (txtSalesPerson.Text == string.Empty || txtSPEmail.Text == string.Empty)
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Enter Sales Person details')</script>");
                    lblError.Text = "Enter Sales Person details";
                    return;
                }
               
                else
                {
                    string Quote = txtQuoteNum.Text.Trim();
                    if (Quote == string.Empty)
                    {
                        GetQuoteNumber();
                    }
                    string Status = "Submitted";
                    SaveQuoteDetails(Status);
                    string QuoteNumber = txtQuoteNum.Text;

                    SmtpClient smtpClient = new SmtpClient("smtp.watts.com");
                    smtpClient.UseDefaultCredentials = true;
                    string UserName = (string)Session["UserName"];
                    string UserRole = (string)Session["UserRole"];
                    string Manager = string.Empty;

                    Manager = txtSalesPerson.Text;


                    DataTable dt = new DataTable();
                    if (GridView1.Rows.Count > 0)
                    {

                        for (int i = 0; i < GridView1.Columns.Count; i++)
                        {
                            string headername = GridView1.Columns[i].HeaderText;

                            dt.Columns.Add(headername);
                        }
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            DataRow dr = dt.NewRow();

                            for (int j = 0; j < GridView1.Columns.Count; j++)
                            {
                                //string a = Server.HtmlDecode(row.Cells[j].Text.Trim());

                                dr[j] = Server.HtmlDecode((row.Cells[j].Text.Trim()));

                            }
                            TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                            dr["PartNo"] = PartNum.Text;
                            TextBox Desc = (TextBox)row.FindControl("txtDesc");
                            dr["Description"] = Desc.Text;
                            TextBox QTY = (TextBox)row.FindControl("txtQTY");
                            dr["QTY"] = QTY.Text;

                            dt.Rows.Add(dr);
                        }
                    }

                    dt.Columns.Remove(dt.Columns[0]);
                    dt.Columns.Remove(dt.Columns[0]);
                    dt.Columns.Remove(dt.Columns[0]);
                    dt.Columns.Remove(dt.Columns[4]);
                    dt.Columns.Remove(dt.Columns[4]);
                    dt.Columns.Remove(dt.Columns[4]);
                    dt.Columns.Remove(dt.Columns[4]);
                    dt.Columns.Remove(dt.Columns[4]);
                    dt.Columns.Remove(dt.Columns[4]);

                    //Create a table
                    iTextSharp.text.Table table = new iTextSharp.text.Table(dt.Columns.Count);

                    int[] widths = new int[dt.Columns.Count];
                    for (int x = 0; x < dt.Columns.Count; x++)
                    {

                        widths[x] = 200;
                        string cellText = dt.Columns[x].ToString().Trim();

                        if (cellText == "ItemNo")
                        {
                            widths[x] = 210;
                        }
                        else if (cellText == "PartNo")
                        {
                            widths[x] = 210;
                        }
                        else if (cellText == "Description")
                        {
                            widths[x] = 400;
                        }
                        else if (cellText == "QTY")
                        {
                            widths[x] = 120;
                        }

                        else if (cellText == "LeadTime")
                        {
                            cellText = "Lead Time Working Days";

                            widths[x] = 220;
                        }


                        iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);

                        cell.BackgroundColor = new Color(System.Drawing.ColorTranslator.FromHtml("#5F9EA0"));

                        table.Cellpadding = 2;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;

                        table.AddCell(cell);
                    }
                    //table.SetWidths(widths);

                    //Transfer rows from GridView to table
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            string cellText = "";


                            cellText = Server.HtmlDecode(dt.Rows[i][j].ToString());

                            if (j == 3) //if available qty<=o make it blank
                            {
                                if (cellText.Trim() != string.Empty)
                                {
                                    if (Convert.ToInt32(cellText) <= 0)
                                    {
                                        cellText = "";
                                    }
                                }
                            }



                            if (j == 4 || j == 6 || j == 7)
                            {
                                if (drpCurrency.SelectedItem.Text == "EUR")
                                {
                                    cellText = "€" + cellText;
                                }
                                else
                                {
                                    cellText = "£" + cellText;
                                }
                            }

                            iTextSharp.text.Cell cell = new iTextSharp.text.Cell(cellText);

                            iTextSharp.text.Font foncell = FontFactory.GetFont("Arial", 10);



                            //table.Cellpadding = 2;

                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            //table.AddCell(cell,foncell);
                            table.AddCell(new Phrase(cellText.ToString(), foncell));

                        }


                    }

                    table.SetWidths(widths);
                    table.Width = 100;

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //grdQuote.RenderControl(hw);
                    MemoryStream memoryStream = new MemoryStream();
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 10f, 50f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    writer.PageEvent = new ITextEvents();
                    pdfDoc.Open();


                    string imageURL = "C://logo.PNG";
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(140f, 100f);
                    //Give space before image
                    jpg.SpacingBefore = 10f;
                    //Give some space after the image
                    jpg.SpacingAfter = 1f;
                    jpg.Alignment = Element.ALIGN_RIGHT;
                    pdfDoc.Add(jpg);






                    pdfDoc.Add(new Phrase("\r\n                                                                                                                                     Processed by WATTS RPA\r\n", font));




                    pdfDoc.Add(new Phrase("\r\n                                                            CUSTOMER QUOTATION\r\n", font));


                    PdfPTable tableAdd = new PdfPTable(2);
                    tableAdd.WidthPercentage = 100;
                    /* PdfPCell cellIAdd = new PdfPCell(new Phrase("Invoice Address:", font));
                     cellIAdd.HorizontalAlignment = Element.ALIGN_LEFT;
                     cellIAdd.Border = 0;
                     tableAdd.AddCell(cellIAdd);*/

                    PdfPCell cellDAdd = new PdfPCell(new Phrase("", font));
                    cellDAdd.HorizontalAlignment = Element.ALIGN_LEFT;
                    cellDAdd.Border = 0;
                    tableAdd.AddCell(cellDAdd);


                    DataTable dtCustAdd = new DataTable();
                    // dtCustAdd = objCreateQuoteBAL.GetCustomerAddress(txtCustName.Text);

                    PdfPCell cellI1 = new PdfPCell(new Phrase(txtCustName.Text.Trim(), font));
                    cellI1.HorizontalAlignment = Element.ALIGN_LEFT;
                    cellI1.Border = 0;
                    tableAdd.AddCell(cellI1);

                    PdfPCell cellI2 = new PdfPCell(new Phrase(txtCustName.Text.Trim(), font));
                    cellI2.HorizontalAlignment = Element.ALIGN_LEFT;
                    cellI2.Border = 0;
                    tableAdd.AddCell(cellI2);







                    pdfDoc.Add(tableAdd);

                    pdfDoc.Add(new Phrase("\r\n \r\n\r\n", font));

                    PdfPTable table1 = new PdfPTable(2);

                    table1.WidthPercentage = 100;
                    PdfPCell cell1 = new PdfPCell(new Phrase("Quotation Ref: " + this.txtQuoteNum.Text.Trim(), font));
                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell1.Border = 0;
                    table1.AddCell(cell1);




                    PdfPCell cell2 = new PdfPCell(new Phrase("Offer Creation Date  : " + txtCreationdate.Text, font));
                    cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell2.Border = 0;

                    table1.AddCell(cell2);


                    PdfPCell cell14 = new PdfPCell(new Phrase(""));
                    cell14.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell14.Border = 0;
                    table1.AddCell(cell14);

                    PdfPCell cell3 = new PdfPCell(new Phrase("Offer Expiry Date  : " + txtExpirationDate.Text, font));
                    cell3.Colspan = 2;
                    cell3.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell3.Border = 0;
                    table1.AddCell(cell3);

                    string CustomerNo = "";

                    CustomerNo = txtCustNo.Text.Trim();

                    PdfPCell cell4 = new PdfPCell(new Phrase("Customer No : " + CustomerNo, font));
                    cell4.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell4.Border = 0;
                    table1.AddCell(cell4);

                    PdfPCell cell5 = new PdfPCell(new Phrase("Customer Email : " + this.txtCustEmail.Text.Trim(), font));
                    cell5.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell5.Border = 0;
                    table1.AddCell(cell5);

                    PdfPCell cell6 = new PdfPCell(new Phrase("Customer Phone  : " + this.txtCustPhone.Text.Trim(), font));
                    cell6.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell6.Border = 0;
                    table1.AddCell(cell6);

                    PdfPCell cell7 = new PdfPCell(new Phrase("SalesPerson : " + this.txtSalesPerson.Text.Trim(), font));
                    cell7.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell7.Border = 0;
                    table1.AddCell(cell7);

                    PdfPCell cell8 = new PdfPCell(new Phrase("SalesPerson Email : " + this.txtSPEmail.Text.Trim(), font));
                    cell8.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell8.Border = 0;
                    table1.AddCell(cell8);

                    PdfPCell cell9 = new PdfPCell(new Phrase("SalesPerson Phone : " + this.txtSPPhone.Text.Trim(), font));
                    cell9.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell9.Border = 0;
                    table1.AddCell(cell9);

                    PdfPCell cell10 = new PdfPCell(new Phrase("Project Name  : " + this.txtProjectName.Text.Trim(), font));
                    cell10.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell10.Border = 0;
                    table1.AddCell(cell10);

                    PdfPCell cell11 = new PdfPCell(new Phrase("Customer Reference : " + this.txtRefNo.Text.Trim(), font));
                    cell11.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell11.Border = 0;
                    table1.AddCell(cell11);


                    PdfPCell cell12 = new PdfPCell(new Phrase("Prepared by  : " + this.drpPreparedBy.SelectedItem.Text, font));
                    cell12.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell12.Border = 0;
                    table1.AddCell(cell12);

                    PdfPCell cell13 = new PdfPCell(new Phrase("Currency: " + this.drpCurrency.SelectedItem.Text, font));
                    cell13.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell13.Border = 0;
                    table1.AddCell(cell13);


                    float CarriageVal = 0;
                    string currencysymbol = "£";

                    CarriageVal = float.Parse(txtCarriage.Text);


                    float Total = float.Parse(TxtGrandTotal.Text) + CarriageVal;

                    pdfDoc.Add(table1);

                    htmlparser.Parse(sr);
                    pdfDoc.Add(table);
                    //if (UserRole == "Sales Engineer")
                    pdfDoc.Add(new Phrase("\r\n\r\n Comments : " + txtComments.Text.Trim(), font));

                    pdfDoc.Add(new Phrase("\r\n\r\n                                                                                                                       Carriage Charge : " + currencysymbol + CarriageVal, font));


                    pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total : " + currencysymbol + Total.ToString("0.##"), font));//03-16-20 remove GM% from any output file


                    pdfDoc.Add(new Phrase("\r\n\r\n", font));





                    htmlparser.Parse(sr);
                    writer.CloseStream = false;
                    pdfDoc.Close();

                    Response.Write(pdfDoc);

                   Response.End();



                    memoryStream.Position = 0;


                    smtpClient = new SmtpClient("smtp.watts.com");
                    smtpClient.UseDefaultCredentials = true;
                    string MailTo = txtSEEMail.Text.ToString();
                    MailMessage mail = new MailMessage("RPA@wattswater.com", MailTo);
                    mail.Subject = QuoteNumber + " is submitted";
                    // string QuoteURL = "https://rpaquotationtooldubai.wattswater.com/Cust_Quote.aspx?QuoteNo=" + QuoteNumber + "&Status=" + CurrentStatus + "&UserName=" + UserName + "&UserRole=" + Role;

                    mail.IsBodyHtml = true;
                    mail.Body = "Dear " + drpPreparedBy.SelectedItem.Text + Environment.NewLine + "Please find the submitted quote attached.";
                    mail.Attachments.Add(new Attachment(memoryStream, QuoteNumber + ".pdf"));

                    smtpClient.Send(mail);


                   



                    /*string Message = "Quote submitted successfully with quote number " + QuoteNumber;


                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('" + Message + "');window.location ='Dashboard.aspx';",
                   true);*/


                    Response.Redirect("Cust_Dashboard.aspx");







                }


            }
        }


       

        protected void drpPreparedBy_SelectedIndexChanged(object sender, EventArgs e)
        {

          


        }

       

       

        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            GetDiscountDetails(sender);
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["dt"] as DataTable;
            dt.Rows[index].Delete();
            if (dt.Rows.Count == 0)
            {
                //dt.Columns.AddRange(new DataColumn[18] { new DataColumn("Product Family"), new DataColumn("ItemNo"), new DataColumn("PartNo"), new DataColumn("Description"), new DataColumn("QTY"), new DataColumn("AvailableQty"), new DataColumn("Weight"), new DataColumn("MOQ"), new DataColumn("LeadTime"), new DataColumn("SafetyStock"), new DataColumn("ListPrice"), new DataColumn("Discount"), new DataColumn("Unit Price"), new DataColumn("AdditionalDiscount"), new DataColumn("Unit Price after Extra Discount"), new DataColumn("Total Price after Extra Discount"), new DataColumn("GM"), new DataColumn("CostTotal") });
                DataRow dr = null;
                dr = dt.NewRow();
                // dr["Platform"] = string.Empty;
                dr["PartNo"] = string.Empty;
                dr["ItemNo"] = string.Empty;
                dr["Product Family"] = string.Empty;
                dr["Description"] = string.Empty;
                dr["MOQ"] = string.Empty;
                dr["AvailableQty"] = string.Empty;
                dr["Weight"] = string.Empty;
                dr["LeadTime"] = string.Empty;
                dr["SafetyStock"] = string.Empty;
                dr["ListPrice"] = string.Empty;
                dr["Discount"] = string.Empty;
                dr["CostPrice"] = string.Empty;
                dr["QTY"] = "1";
               
                //dr["CostTotal"] = 0;

                dt.Rows.Add(dr);
                ViewState["dt"] = dt;
                GridView1.DataSource = dt; // bind new datatable to grid
                GridView1.DataBind();

            }
            else
            {
                ViewState["dt"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                float GrandTotal = 0;
               
                
                TxtGrandTotal.Text = GrandTotal.ToString("0,0.00");




            }
        }

      
        private void SavePDFFile(string cReportName, MemoryStream pdfStream)
        {
            //Check file exists, delete  
            if (File.Exists(cReportName))
            {
                File.Delete(cReportName);
            }
            using (FileStream file = new FileStream(cReportName, FileMode.Create, FileAccess.Write))
            {
                byte[] bytes = new byte[pdfStream.Length];
                pdfStream.Read(bytes, 0, (int)pdfStream.Length);
                file.Write(bytes, 0, bytes.Length);
                //pdfStream.Close();
            }
        }
        //private static Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        //private static Microsoft.Office.Interop.Excel.Sheets mWorkSheets;
        //private static Microsoft.Office.Interop.Excel.Worksheet mWSheet1;
        // private static Microsoft.Office.Interop.Excel.Application oXL;

     

        protected void txtCarriage_TextChanged(object sender, EventArgs e)
        {



            if (TxtGrandTotal.Text != "0" && TxtGrandTotal.Text != string.Empty)
            {
                TxtGrandTotal.Text = float.Parse(TxtGrandTotal.Text).ToString("0.00")+float.Parse(txtCarriage.Text);
                float TotalOverallCost = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {


                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                    TextBox Qty = (TextBox)row.FindControl("txtQty");

                    if (PartNum.Text == string.Empty)
                    {
                        continue;
                    }
                    



                }



            }
        }
    }
}