using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL_Layer;
using ClosedXML.Excel;
using System.Configuration;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;




namespace RPAUKQuoteTool
{
    public partial class CreateNewQuote : System.Web.UI.Page
    {

        Boolean blnSundryItem = false;
        QuoteBAL objQuoteBAL = new QuoteBAL();
        CreateQuoteBAL obj = new CreateQuoteBAL();
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
                    if (UserRole == "Sales Engineer") //03-11-20 removed SM from here per Business Request
                    {
                        GridView1.Columns[15].Visible = true;  //change mad eby Divya to hide GM % for SE & SE
                        lblTotalGM.Visible = true;
                        txtTotalGM.Visible = true;
                    }


                    if (!IsPostBack)
                    {
                        FetchDetails();
                        //GetQuoteNumber();
                        txtCarriage.Visible = false;

                        UserRole = (string)Session["UserRole"];
                        UserName = (string)Session["UserName"];
                        if (UserRole == "Sales Engineer")
                        {
                            drpPreparedBy.SelectedItem.Text = UserName;
                            drpPreparedBy.Enabled = false;
                            DataTable dtSEEMailPhone = obj.GetSEEmailPhone(UserName);
                            txtSEEMail.Text = dtSEEMailPhone.Rows[0][0].ToString();
                            txtSEPhone.Text = dtSEEMailPhone.Rows[0][1].ToString();
                            RFdrpPreparedBy.InitialValue = "1";
                            RFSavedrpPreparedBy.InitialValue = "1";
                            GridView1.Columns[15].Visible = true;  //change mad eby Divya to hide GM % for SE & SE
                            lblTotalGM.Visible = true;
                            txtTotalGM.Visible = true;
                        }
                        else if (UserRole == "Sales Manager")  //03-11-20 made GM% visible for SM per Business Request
                        {
                            drpPreparedBy.SelectedItem.Text = UserName;
                            drpPreparedBy.Enabled = false;
                            DataTable dtSEEMailPhone = obj.GetSEEmailPhone(UserName);
                            txtSEEMail.Text = dtSEEMailPhone.Rows[0][0].ToString();
                            txtSEPhone.Text = dtSEEMailPhone.Rows[0][1].ToString();
                            RFdrpPreparedBy.InitialValue = "1";
                            RFSavedrpPreparedBy.InitialValue = "1";
                            GridView1.Columns[15].Visible = true;
                            lblTotalGM.Visible = true;
                            txtTotalGM.Visible = true;
                        }
                        else if (UserRole == "Admin")
                        {
                            drpPreparedBy.SelectedItem.Text = UserName;
                            drpPreparedBy.Enabled = false;
                            DataTable dtSEEMailPhone = obj.GetSEEmailPhone(UserName);
                            txtSEEMail.Text = dtSEEMailPhone.Rows[0][0].ToString();
                            txtSEPhone.Text = dtSEEMailPhone.Rows[0][1].ToString();
                            RFdrpPreparedBy.InitialValue = "1";
                            RFSavedrpPreparedBy.InitialValue = "1";
                            GridView1.Columns[15].Visible = true;
                            lblTotalGM.Visible = true;
                            txtTotalGM.Visible = true;
                        }


                        DataTable dt = new DataTable();


                        dt.Columns.AddRange(new DataColumn[19] { new DataColumn("Product Family"), new DataColumn("ItemNo"), new DataColumn("PartNo"), new DataColumn("Description"), new DataColumn("QTY"), new DataColumn("AvailableQty"), new DataColumn("StockAvailability"), new DataColumn("Weight"), new DataColumn("MOQ"), new DataColumn("LeadTime"), new DataColumn("SafetyStock"), new DataColumn("ListPrice"), new DataColumn("Discount"), new DataColumn("Unit Price"), new DataColumn("AdditionalDiscount"), new DataColumn("Unit Price after Extra Discount"), new DataColumn("Total Price after Extra Discount"), new DataColumn("GM"), new DataColumn("CostPrice") });
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
                        dr["Unit Price"] = string.Empty;
                        dr["QTY"] = "1";
                        dr["AdditionalDiscount"] = "0";
                        dr["CostPrice"] = 0;

                        dt.Rows.Add(dr);
                        GridView1.DataSource = dt; // bind new datatable to grid
                        GridView1.DataBind();
                        ViewState["dt"] = dt;
                        //GridView1.Columns[11].HeaderText = "Discount%";
                        //GridView1.Columns[14].HeaderText = "GM%";

                    }

                    if (chkPerforma.Checked == true)
                    {
                        if (drpCustomer.SelectedItem.Text.Contains("SUNDRY"))
                        {
                            lblSundryBranch.Visible = true;
                            txtSundryBranch.Visible = true;
                            lblSundryBranch.Text = "Delivery Address";
                        }
                        else if (drpCustBranch.SelectedItem.Text.Contains("SUNDRY"))
                        {
                            lblSundryBranch.Visible = true;
                            txtSundryBranch.Visible = true;
                        }
                        else
                        {
                            lblSundryBranch.Visible = false;
                            txtSundryBranch.Visible = false;
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


        private void GetQuoteNumber()
        {
            string QuoteNumber = "";
            string UserName = (string)Session["UserName"];
            string ID = obj.GetQuoteNumber(UserName);
            //ViewState["SerialNo"] = QuoteNumber;
            if (UserName.Contains("Mark"))
            {
                UserName = "MAP";
            }
            else if (UserName.Contains("Richard Kleiser"))
            {
                UserName = "RIK";
            }
            else
            {
                UserName = UserName.ToString().ToUpper().Substring(0, 3);
            }
            if (ID == "")
            {
                ID = DateTime.Today.ToString("ddmmss");
            }
            QuoteNumber = UserName + ID;
            txtQuoteNum.Text = QuoteNumber;
            obj.UpdateQuoteNo(QuoteNumber,ID);
        }

        private void FetchDetails()
        {
            DataTable dt = new DataTable();
            dt = obj.LoadCustomer("NULL");
            drpCustomer.DataSource = dt;
            //drpCustomer.DataTextField = "Customer";
            drpCustomer.DataValueField = "CustomerName";
            drpCustomer.DataBind();
            drpCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            drpCustomer.Items.Insert(1, new System.Web.UI.WebControls.ListItem("SUNDRY ACCOUNT", "1"));


            DataTable dtCarriage = new DataTable();
            dtCarriage = obj.GetCarriageCharge();
            drpCarriageCharges.DataSource = dtCarriage;
            drpCarriageCharges.DataValueField = "Charge";
            drpCarriageCharges.DataBind();
            drpCarriageCharges.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            //txtCreationdate.Text = DateTime.Now.ToString();
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

            DataTable dtPrepby = new DataTable();
            dtPrepby = obj.getPreparedByList();
            drpPreparedBy.DataSource = dtPrepby;
            drpCustomer.DataValueField = "Name";
            drpPreparedBy.DataBind();

           /* DataTable dtPGM = new DataTable();
            dtPGM = obj.GetPGMDAta();
            DrpGroupName.DataSource = dtPGM;
            DrpGroupName.DataValueField = "Description";
            DrpGroupName.DataBind();
            DrpGroupName.Items.Insert(0, new ListItem("Select", "0"));*/

        }

        protected void drpCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drpCustomer.SelectedItem.Text == "SUNDRY ACCOUNT")
            {
                lblCustName.Visible = true;
                txtCustName.Visible = true;
                lblCustBranch.Visible = false;
                drpCustBranch.Visible = false;
                lblCustNo.Visible = false;
                DrpCustNo.Visible = false;
                txtCurrency.Enabled = true;
                chkPerforma.Checked = true;
                lblSundryBranch.Visible = true;
                txtSundryBranch.Visible = true;
                lblSundryBranch.Text = "Delivery Address";

            }
            else
            {
                lblCustName.Visible = false;
                txtCustName.Visible = false;
                lblCustBranch.Visible = true;
                drpCustBranch.Visible = true;
                lblCustNo.Visible = true;
                DrpCustNo.Visible = true;
                txtCurrency.Enabled = false;
                lblSundryBranch.Visible = false;
                txtSundryBranch.Visible = false;
                if (chkPerforma.Checked == true)
                {
                    lblSundryBranch.Visible = true;
                    txtSundryBranch.Visible = true;
                    lblSundryBranch.Text = "Delivery Address";
                }
                else
                {
                    lblSundryBranch.Visible = false;
                    txtSundryBranch.Visible = false;
                    lblSundryBranch.Text = "Sundry Branch";
                }

                DataTable dt = new DataTable();
                string CustomerName = drpCustomer.SelectedItem.Text.Substring(0, drpCustomer.SelectedItem.Text.LastIndexOf("|"));
                string City = drpCustomer.SelectedItem.Text.Substring(drpCustomer.SelectedItem.Text.LastIndexOf("|") + 1);
                dt = obj.GetCustomerNumber(CustomerName,City);
                DrpCustNo.DataSource = dt;
                DrpCustNo.DataValueField = "CustomerNumber";
                DrpCustNo.DataBind();
                DrpCustNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));

                DataTable dtCust = new DataTable();
                dtCust = obj.GetTermsCode(CustomerName);
                if (dtCust.Rows.Count > 0)
                {
                    if (dtCust.Rows[0][0].ToString() == "000A")
                    {
                        lblPerforma.Text = "This is a Performa customer";
                        chkPerforma.Checked = true;
                    }
                }
                else
                {
                    lblPerforma.Text = "";
                }

            }
        
        }

        protected void DrpCustNo_SelectedIndexChanged(object sender, EventArgs e)
        {

           // string PaymentTerms = "";
            string CustomerName = drpCustomer.SelectedItem.Text;
            string CustNo = DrpCustNo.SelectedItem.Text;

            
            if (CustNo != "Select") 
            { 
                DataTable dtSalesPerson = obj.GetSalesPersonData(CustNo);
                txtCurrency.Text = dtSalesPerson.Rows[0][0].ToString();
                txtSalesPerson.Text= dtSalesPerson.Rows[0][1].ToString();
                txtSPEmail.Text = dtSalesPerson.Rows[0][2].ToString();
                txtSPPhone.Text = dtSalesPerson.Rows[0][3].ToString();

                DataTable dtCustBranch = obj.GetCustomerBranch(CustNo);
                drpCustBranch.DataSource = dtCustBranch;
                drpCustBranch.DataValueField = "Branch";
                drpCustBranch.DataBind();
                drpCustBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                drpCustBranch.Items.Insert(1, new System.Web.UI.WebControls.ListItem("SUNDRY BRANCH", "1"));
            }
            else
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid Customer Number selection')</script>");
                lblError.Text = "Invalid Customer Number selection";
                return;
            }

            if (GridView1.Rows.Count > 0)
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
                            dr[j] = Server.HtmlDecode(row.Cells[j].Text);

                        }
                        TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                        dr["PartNo"] = PartNum.Text;
                        TextBox Desc = (TextBox)row.FindControl("txtDesc");
                        dr["Description"] = Desc.Text;
                        TextBox QTY = (System.Web.UI.WebControls.TextBox)row.FindControl("txtQTY");
                        dr["QTY"] = QTY.Text;
                        TextBox leadtime = (System.Web.UI.WebControls.TextBox)row.FindControl("txtLeadTime");
                        dr["LeadTime"] = leadtime.Text;
                        TextBox UnitPrice = (System.Web.UI.WebControls.TextBox)row.FindControl("txtUnitPrice");
                        dr["Unit Price"] = UnitPrice.Text;
                        TextBox Discount = (System.Web.UI.WebControls.TextBox)row.FindControl("txtDiscount");
                        dr["AdditionalDiscount"] = Discount.Text;
                        dt.Rows.Add(dr);
                    }
                }
                float GrandTotal = 0;
                float TotalforGM = 0;
                float COstTotal = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                    TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                    TextBox txtDiscount = (TextBox)row.FindControl("txtDiscount");


                    string netprice = string.Empty;
                    float UnitPrice = 0;
                    string custNo = "";
                    if (PartNum.Text != "")
                    {



                        if (drpCustomer.SelectedItem.Text == "SUNDRY ACCOUNT")
                        {
                            custNo = "Select";

                        }
                        else
                        {
                            custNo = DrpCustNo.SelectedItem.Text.ToString();
                        }
                        netprice = obj.GetNetPrice(custNo, PartNum.Text, string.Empty, txtCurrency.Text);
                        if (netprice != string.Empty)
                        {

                            row.Cells[13].Text = string.Empty;
                            row.Cells[14].Text = string.Empty;


                            UnitPrice = float.Parse(netprice);

                            // row.Cells[15].Text = UnitPrice.ToString("0.00");
                            txtUnitPrice.Text = UnitPrice.ToString("0.00");
                            float AddDiscount = 0;

                            TextBox Qty = (TextBox)row.FindControl("txtQty");
                            //row.Cells[16].Text = AddDiscount.ToString();
                            txtDiscount.Text = AddDiscount.ToString();
                            string Quantity = Qty.Text;
                            int iQty = 0;
                            float UnitPriceafterExtraDiscount = 0;
                            float CostPrice = 0;
                            if (UnitPrice.ToString() != string.Empty)
                            {
                                UnitPriceafterExtraDiscount = UnitPrice;
                                row.Cells[17].Text = UnitPriceafterExtraDiscount.ToString("0.00");
                            }
                            if (Quantity != string.Empty)
                            {
                                iQty = Convert.ToInt32(Quantity);
                            }
                            //Costprice = float.Parse(dtLineItem.Rows[0]["CostPrice"].ToString());

                            CostPrice = float.Parse(row.Cells[20].Text.ToString());
                            float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                            if (txtCurrency.Text == "EUR")
                            {
                                CostPrice = CostPrice * rate;

                            }


                            float TotalPriceafterExtraDiscount = UnitPriceafterExtraDiscount * iQty;
                            row.Cells[18].Text = TotalPriceafterExtraDiscount.ToString("0.00");
                            row.Cells[20].Text = CostPrice.ToString("0.00");
                            row.Cells[19].Text = (((TotalPriceafterExtraDiscount - CostPrice) / TotalPriceafterExtraDiscount) * 100).ToString("0.00");

                        }
                        else
                        {
                            DataTable dtPrice = new DataTable();
                            dtPrice = obj.GetPrice(PartNum.Text, "NULL", "GBP");
                            float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);

                            if (dtPrice.Rows.Count != 0)
                            {
                                if (txtCurrency.Text == "EUR")
                                {

                                    row.Cells[13].Text = (float.Parse(dtPrice.Rows[0]["ListPrice"].ToString()) * rate).ToString();
                                }
                                else
                                {
                                    row.Cells[13].Text = dtPrice.Rows[0]["ListPrice"].ToString();
                                }


                                float Disc = 0;
                                DataTable dtDisc = new DataTable();
                                dtDisc = obj.GetDiscount(row.Cells[3].Text, custNo);
                                if (dtDisc.Rows.Count != 0)
                                {
                                    row.Cells[14].Text = dtDisc.Rows[0]["DiscountPerc"].ToString();
                                    Disc = float.Parse(dtDisc.Rows[0]["DiscountPerc"].ToString());


                                }
                                else
                                {
                                    row.Cells[14].Text = "";
                                    Disc = 0;
                                }

                                float Listprice = float.Parse(dtPrice.Rows[0]["ListPrice"].ToString());
                                if (txtCurrency.Text == "EUR")
                                {
                                    Listprice = Listprice * rate;
                                }



                                UnitPrice = Listprice - (Listprice * Disc / 100);

                                //row.Cells[15].Text = UnitPrice.ToString("0.00");
                                txtUnitPrice.Text = UnitPrice.ToString("0.00");



                                float AddDiscount = 0;


                                //row.Cells[16].Text = AddDiscount.ToString();
                                txtDiscount.Text = AddDiscount.ToString();
                                TextBox Qty = (TextBox)row.FindControl("txtQty");
                                string Quantity = Qty.Text;
                                int iQty = 0;
                                float UnitPriceafterExtraDiscount = 0;

                                if (UnitPrice.ToString() != string.Empty)
                                {
                                    UnitPriceafterExtraDiscount = UnitPrice;
                                    row.Cells[17].Text = UnitPriceafterExtraDiscount.ToString("0.00");
                                }
                                if (Quantity != string.Empty)
                                {
                                    iQty = Convert.ToInt32(Quantity);
                                }
                                float CostPrice = 0;
                                CostPrice = float.Parse(row.Cells[20].Text);
                                float TotalPriceafterExtraDiscount = UnitPriceafterExtraDiscount * iQty;
                                row.Cells[18].Text = TotalPriceafterExtraDiscount.ToString("0.00");
                                if (txtCurrency.Text == "EUR")
                                {
                                    CostPrice = CostPrice * rate;
                                }
                                row.Cells[20].Text = CostPrice.ToString("0.00");
                                row.Cells[19].Text = (((TotalPriceafterExtraDiscount - CostPrice) / TotalPriceafterExtraDiscount) * 100).ToString("0.00");



                                string TotalPrice = row.Cells[18].Text.ToString();
                                if (TotalPrice == string.Empty)
                                {
                                    TotalPrice = "0";
                                }
                                if (TotalPrice != string.Empty)
                                {
                                    GrandTotal = GrandTotal + float.Parse(TotalPrice);

                                    if (row.Cells[4].Text.ToUpper().Contains("SUNDRY") == false)
                                    {
                                        TotalforGM = TotalforGM + float.Parse(TotalPrice);
                                    }

                                }
                                string Cost = row.Cells[20].Text.Trim();
                                if (Cost != "")
                                {
                                    COstTotal = COstTotal + float.Parse(Cost);
                                }

                                float TotalGM = 0;
                                TotalGM = ((TotalforGM - COstTotal) / TotalforGM) * 100;
                                txtTotalGM.Text = TotalGM.ToString("0.00");
                                TxtGrandTotal.Text = GrandTotal.ToString();

                            }
                        }
                    }

                }

            }



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
            if(Page.IsValid)
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
                       
                        TextBox LeadTime = (TextBox)row.FindControl("txtLeadTime");
                        dr["LeadTime"] = LeadTime.Text;
                     TextBox UnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                     dr["Unit Price"] = UnitPrice.Text;
                        TextBox Discount = (TextBox)row.FindControl("txtDiscount");
                    dr["AdditionalDiscount"] = Discount.Text;
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
                    TextBox leadtime = (System.Web.UI.WebControls.TextBox)row.FindControl("txtLeadTime");
                    dr["LeadTime"] = leadtime.Text;
                    TextBox UnitPrice = (System.Web.UI.WebControls.TextBox)row.FindControl("txtUnitPrice");
                    dr["Unit Price"] = UnitPrice.Text;
                    TextBox Discount = (System.Web.UI.WebControls.TextBox)row.FindControl("txtDiscount");
                    dr["AdditionalDiscount"] = Discount.Text;
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
                float rate =float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                if (txtCurrency.Text == "EUR")
                {
                    CostPrice = CostPrice * rate;
                    dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                }
                else
                {
                    dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                }
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
                float UnitPrice = 0;
                netprice = obj.GetNetPrice(DrpCustNo.SelectedItem.Text.ToString(), PartNo, string.Empty,txtCurrency.Text);

                if (netprice != string.Empty)
                {
                    dt.Rows[RowId]["ListPrice"] = string.Empty;
                    dt.Rows[RowId]["Discount"] = string.Empty;

                    //float Listprice = float.Parse(dtPrice.Rows[0]["ListPrice"].ToString());
                    //float Disc = float.Parse(dtPrice.Rows[0]["DiscountPerc"].ToString());
                    //UnitPrice = Listprice - (Listprice * Disc / 100);
                    UnitPrice = float.Parse(netprice);

                    dt.Rows[RowId]["Unit Price"] = UnitPrice.ToString("0.00");
                    float AddDiscount = 0;
                    // AddDiscount = float.Parse(txtDisc.Text.ToString().Trim());

                    dt.Rows[RowId]["AdditionalDiscount"] = AddDiscount.ToString();
                    string Quantity = dt.Rows[RowId]["QTY"].ToString();
                    int Qty = 0;
                    float UnitPriceafterExtraDiscount = 0;
                    float Costprice = 0;
                    if (UnitPrice.ToString() != string.Empty)
                    {
                        UnitPriceafterExtraDiscount = UnitPrice;
                        dt.Rows[RowId]["Unit Price after Extra Discount"] = UnitPriceafterExtraDiscount.ToString("0.00");
                    }
                    if (Quantity != string.Empty)
                    {
                        Qty = Convert.ToInt32(Quantity);
                    }
                    //Costprice = float.Parse(dtLineItem.Rows[0]["CostPrice"].ToString());

                    CostPrice = float.Parse(dtLineItem.Rows[0]["CostPrice"].ToString());
                
                    if (txtCurrency.Text == "EUR")
                    {
                        CostPrice = CostPrice * rate;
                       
                    }
                  

                    float TotalPriceafterExtraDiscount = UnitPriceafterExtraDiscount * Qty;
                    dt.Rows[RowId]["Total Price after Extra Discount"] = TotalPriceafterExtraDiscount.ToString("0.00");
                    dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                    dt.Rows[RowId]["GM"] = (((TotalPriceafterExtraDiscount - CostPrice) / TotalPriceafterExtraDiscount) * 100).ToString("0.00");

                }
                else
                {
                    DataTable dtPrice = new DataTable();
                    dtPrice = obj.GetPrice(PartNo, "NULL", "GBP");


                    if (dtPrice.Rows.Count != 0)
                    {
                        if (txtCurrency.Text == "EUR")
                        {
                            
                            dt.Rows[RowId]["ListPrice"] = (float.Parse(dtPrice.Rows[0]["ListPrice"].ToString())*rate).ToString();
                        }
                        else
                        {
                            dt.Rows[RowId]["ListPrice"] = dtPrice.Rows[0]["ListPrice"].ToString();
                        }

                        
                        float Disc = 0;
                        DataTable dtDisc = new DataTable();
                        dtDisc = obj.GetDiscount(dtLineItem.Rows[0]["ItemNo"].ToString(), DrpCustNo.SelectedItem.Text);
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
                        if (txtCurrency.Text == "EUR")
                        {
                            Listprice = Listprice * rate;
                        }

                           

                        UnitPrice = Listprice - (Listprice * Disc / 100);

                        dt.Rows[RowId]["Unit Price"] = UnitPrice.ToString("0.00");

                    
                // TextBox txtDisc = (TextBox)currentRow.FindControl("txtDiscount");
                float AddDiscount = 0;
                        // AddDiscount = float.Parse(txtDisc.Text.ToString().Trim());

                        dt.Rows[RowId]["AdditionalDiscount"] = AddDiscount.ToString();
                        string Quantity = dt.Rows[RowId]["QTY"].ToString();
                        int Qty = 0;
                        float UnitPriceafterExtraDiscount = 0;
                      
                        if (UnitPrice.ToString() != string.Empty)
                        {
                            UnitPriceafterExtraDiscount = UnitPrice;
                            dt.Rows[RowId]["Unit Price after Extra Discount"] = UnitPriceafterExtraDiscount.ToString("0.00");
                        }
                        if (Quantity != string.Empty)
                        {
                            Qty = Convert.ToInt32(Quantity);
                        }
                        CostPrice = float.Parse(dtLineItem.Rows[0]["CostPrice"].ToString());
                        float TotalPriceafterExtraDiscount = UnitPriceafterExtraDiscount * Qty;
                        dt.Rows[RowId]["Total Price after Extra Discount"] = TotalPriceafterExtraDiscount.ToString("0.00");
                        if (txtCurrency.Text == "EUR")
                        {
                            CostPrice = CostPrice * rate;
                        }
                        dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                        dt.Rows[RowId]["GM"] = (((TotalPriceafterExtraDiscount - CostPrice) / TotalPriceafterExtraDiscount) * 100).ToString("0.00");

                    }
                }

            }
            else
            {
                DataTable dtICOItem = new DataTable();
                dtICOItem = obj.GetICOItem(PartNo, string.Empty);
                float CostPrice = 0;
                if (dtICOItem.Rows.Count > 0)
                {
                    dt.Rows[RowId]["Product Family"] = "WOTH";
                    dt.Rows[RowId]["ItemNo"] = dtICOItem.Rows[0]["ItemNo"].ToString();
                    dt.Rows[RowId]["PartNo"] = PartNo;
                    dt.Rows[RowId]["Description"] = dtICOItem.Rows[0]["Description"].ToString();
                    dt.Rows[RowId]["MOQ"] = "";
                    dt.Rows[RowId]["LeadTime"] = "";
                    dt.Rows[RowId]["AvailableQty"] = "";
                    dt.Rows[RowId]["Weight"] = "";


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
                    float rate =float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                    CostPrice = float.Parse(dtICOItem.Rows[0]["CostPrice"].ToString());
                    if(txtCurrency.Text=="EUR")
                    {
                        CostPrice = CostPrice * rate;
                    }
                    dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                    dt.Rows[RowId]["SafetyStock"] = "";




                    float ICOPrice = 0;
                    string ICODisc = string.Empty;
                    if (dtICOItem.Rows[0]["Price"].ToString() != " ")
                    {
                        ICOPrice = float.Parse(dtICOItem.Rows[0]["Price"].ToString());
                        if (txtCurrency.Text == "EUR")
                        {
                            ICOPrice = float.Parse(dtICOItem.Rows[0]["Price"].ToString()) * rate;
                        }
                    }
                    else
                    {
                        ICOPrice = 0;
                    }

                    ICODisc = obj.GetICODiscount(DrpCustNo.SelectedItem.Text.ToString());
                    float Disc = 0;
                    float UnitPrice = 0;
                    float Listprice = float.Parse(ICOPrice.ToString("0.00"));
                    if (ICODisc == "")
                    {
                        Disc = 0;
                    }
                    else
                    {
                        Disc = float.Parse(ICODisc);
                    }
                    dt.Rows[RowId]["ListPrice"] = ICOPrice;
                    dt.Rows[RowId]["Discount"] = ICODisc;
                    UnitPrice = Listprice - (Listprice * Disc / 100);


                    dt.Rows[RowId]["Unit Price"] = UnitPrice.ToString("0.00");


                    float AddDiscount = 0;
                    // AddDiscount = float.Parse(txtDisc.Text.ToString().Trim());

                    dt.Rows[RowId]["AdditionalDiscount"] = AddDiscount.ToString();
                    string Quantity = dt.Rows[RowId]["QTY"].ToString();
                    int Qty = 0;
                    float UnitPriceafterExtraDiscount = 0;
                    
                    
                    CostPrice = float.Parse(dtICOItem.Rows[0]["CostPrice"].ToString());
                    if (UnitPrice.ToString() != string.Empty)
                    {
                        UnitPriceafterExtraDiscount = UnitPrice;
                        dt.Rows[RowId]["Unit Price after Extra Discount"] = UnitPriceafterExtraDiscount.ToString("0.00");
                    }
                    if (Quantity != string.Empty)
                    {
                        Qty = Convert.ToInt32(Quantity);
                    }
                    float TotalPriceafterExtraDiscount = UnitPriceafterExtraDiscount * Qty;
                    dt.Rows[RowId]["Total Price after Extra Discount"] = TotalPriceafterExtraDiscount.ToString("0.00");
                    dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                    dt.Rows[RowId]["GM"] = (((TotalPriceafterExtraDiscount - CostPrice) / TotalPriceafterExtraDiscount) * 100).ToString("0.00");
                }

                else
                {
                    /*dt.Rows[RowId]["Product Family"] = "";
                    dt.Rows[RowId]["ItemNo"] = "";
                    dt.Rows[RowId]["PartNo"] = "";
                    dt.Rows[RowId]["Description"] = "";
                    dt.Rows[RowId]["QTY"] = "";
                    dt.Rows[RowId]["MOQ"] = "";
                    dt.Rows[RowId]["LeadTime"] = "";
                    dt.Rows[RowId]["AvailableQty"] = "";
                    dt.Rows[RowId]["Weight"] = "";
                    dt.Rows[RowId]["CostPrice"] = "";
                    dt.Rows[RowId]["SafetyStock"] = "";
                    dt.Rows[RowId]["ListPrice"] = "";
                    dt.Rows[RowId]["Discount"] = "";
                    dt.Rows[RowId]["Unit Price"] = "";
                    dt.Rows[RowId]["AdditionalDiscount"] = "";
                    dt.Rows[RowId]["Unit Price After Extra Discount"] = "";
                    dt.Rows[RowId]["Total Price after Extra Discount"] = "";
                    dt.Rows[RowId]["GM"] = "";
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid Part Number')</script>"); commmented on 09/22/20*/
                }
            }

            ViewState["dt"] = dt;
            // ViewState["CurrentTable"] = dt;
            GridView1.DataSource = dt; // bind new datatable to grid
            //GridView1.Columns[11].HeaderText = "Additional Discount%";
            //GridView1.Columns[14].HeaderText = "GM%";
            GridView1.DataBind();
            ////obj.testmethod();
            float GrandTotal = 0;
            float TotalforGM = 0;
            float COstTotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string TotalPrice = dt.Rows[i]["Total Price after Extra Discount"].ToString().Trim();
                if (TotalPrice == string.Empty)
                {
                    TotalPrice = "0";
                }
                if (TotalPrice != string.Empty)
                {
                    GrandTotal = GrandTotal + float.Parse(TotalPrice);

                    if (dt.Rows[i]["PartNo"].ToString().ToUpper().Contains("SUNDRY") == false)
                    {
                        TotalforGM = TotalforGM + float.Parse(TotalPrice);
                    }

                }
                string Cost= dt.Rows[i]["CostPrice"].ToString().Trim();
                if (Cost != "")
                {
                    COstTotal = COstTotal + float.Parse(Cost);
                }
            }

            float TotalGM = 0;
            TotalGM = ((TotalforGM - COstTotal) / TotalforGM) * 100;
            txtTotalGM.Text = TotalGM.ToString("0.00");
           
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
                    TextBox leadtime = (System.Web.UI.WebControls.TextBox)row.FindControl("txtLeadTime");
                    dr["LeadTime"] = leadtime.Text;
                    TextBox UnitPrice = (System.Web.UI.WebControls.TextBox)row.FindControl("txtUnitPrice");
                    dr["Unit Price"] = UnitPrice.Text;
                    TextBox AddDiscount = (System.Web.UI.WebControls.TextBox)row.FindControl("txtDiscount");
                    dr["AdditionalDiscount"] = AddDiscount.Text;
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
                if (txtCurrency.Text == "EUR")
                {
                    CostPrice = CostPrice * rate;
                    dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                }

                dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                dt.Rows[RowId]["SafetyStock"] = dtLineItem.Rows[0]["SafetyStock"].ToString();


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
                float UnitPrice = 0;
                netprice = obj.GetNetPrice(DrpCustNo.SelectedItem.Text.ToString(), string.Empty, strDesc,txtCurrency.Text);

                if (netprice != string.Empty)
                {
                    dt.Rows[RowId]["ListPrice"] = string.Empty;
                    dt.Rows[RowId]["Discount"] = string.Empty;

                 
                    UnitPrice = float.Parse(netprice);

                    dt.Rows[RowId]["Unit Price"] = UnitPrice.ToString("0.00");

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
                        dtDisc = obj.GetDiscount(dtLineItem.Rows[0]["ItemNo"].ToString(), DrpCustNo.SelectedItem.Text);
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
                        if (txtCurrency.Text == "EUR")
                        {
                            Listprice = Listprice * rate;
                        }


                        UnitPrice = Listprice - (Listprice * Disc / 100);

                        dt.Rows[RowId]["Unit Price"] = UnitPrice.ToString("0.00");
                    }

                }

                // TextBox txtDisc = (TextBox)currentRow.FindControl("txtDiscount");
                float AddDiscount = 0;
                // AddDiscount = float.Parse(txtDisc.Text.ToString().Trim());

                dt.Rows[RowId]["AdditionalDiscount"] = AddDiscount.ToString();
                string Quantity = dt.Rows[RowId]["QTY"].ToString();
                int Qty = 0;
                float UnitPriceafterExtraDiscount = 0;
                
               // CostPrice = float.Parse(dtLineItem.Rows[0]["CostPrice"].ToString());
                if (UnitPrice.ToString() != string.Empty)
                {
                    UnitPriceafterExtraDiscount = UnitPrice;
                    dt.Rows[RowId]["Unit Price after Extra Discount"] = UnitPriceafterExtraDiscount.ToString("0.00");
                }
                if (Quantity != string.Empty)
                {
                    Qty = Convert.ToInt32(Quantity);
                }
                float TotalPriceafterExtraDiscount = UnitPriceafterExtraDiscount * Qty;
                dt.Rows[RowId]["Total Price after Extra Discount"] = TotalPriceafterExtraDiscount.ToString("0.00");

                dt.Rows[RowId]["GM"] = (((TotalPriceafterExtraDiscount - CostPrice) / TotalPriceafterExtraDiscount) * 100).ToString("0.00");
            }
            else
            {
                DataTable dtICOItem = new DataTable();
                dtICOItem = obj.GetICOItem(string.Empty, strDesc);
                if (dtICOItem.Rows.Count > 0)
                {
                    dt.Rows[RowId]["Product Family"] = "WOTH";
                    dt.Rows[RowId]["ItemNo"] = dtICOItem.Rows[0]["ItemNo"].ToString();
                    dt.Rows[RowId]["PartNo"] = dtICOItem.Rows[0]["LegacyPartNo"].ToString();
                    dt.Rows[RowId]["Description"] = strDesc;
                    dt.Rows[RowId]["MOQ"] = "";
                    dt.Rows[RowId]["LeadTime"] = "";
                    dt.Rows[RowId]["AvailableQty"] = "";
                    dt.Rows[RowId]["Weight"] = "";
                    float CostPrice = 0;
                    CostPrice = float.Parse(dtICOItem.Rows[0]["CostPrice"].ToString());
                    float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                    if (txtCurrency.Text == "EUR")
                    {
                        CostPrice = CostPrice * rate;
                    }

                        dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                    dt.Rows[RowId]["SafetyStock"] = "";

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

                        float UnitPrice = 0;


                    float ICOPrice = 0;
                    string ICODisc = string.Empty;
                    if (dtICOItem.Rows[0]["Price"].ToString() != " ")
                    {

                        ICOPrice = float.Parse(dtICOItem.Rows[0]["Price"].ToString());
                        if (txtCurrency.Text == "EUR")
                        {
                            ICOPrice = ICOPrice * rate;
                        }
                    }
                    else
                    {
                        ICOPrice = 0;
                    }

                    ICODisc = obj.GetICODiscount(DrpCustNo.SelectedItem.Text.ToString());
                    float Listprice = float.Parse(ICOPrice.ToString("0.00"));
                    float Disc = 0;
                    if (ICODisc == "")
                    {
                        Disc = 0;
                    }
                    else
                    {
                        Disc = float.Parse(ICODisc);
                    }


                    dt.Rows[RowId]["ListPrice"] = ICOPrice;
                    dt.Rows[RowId]["Discount"] = ICODisc;
                    UnitPrice = Listprice - (Listprice * Disc / 100);


                    dt.Rows[RowId]["Unit Price"] = UnitPrice.ToString("0.00");


                    float AddDiscount = 0;
                    // AddDiscount = float.Parse(txtDisc.Text.ToString().Trim());

                    dt.Rows[RowId]["AdditionalDiscount"] = AddDiscount.ToString();
                    string Quantity = dt.Rows[RowId]["QTY"].ToString();
                    int Qty = 0;
                    float UnitPriceafterExtraDiscount = 0;
                    //float CostPrice = 0;
                   // CostPrice = float.Parse(dtICOItem.Rows[0]["CostPrice"].ToString());
                    if (UnitPrice.ToString() != string.Empty)
                    {
                        UnitPriceafterExtraDiscount = UnitPrice;
                        dt.Rows[RowId]["Unit Price after Extra Discount"] = UnitPriceafterExtraDiscount.ToString("0.00");
                    }
                    if (Quantity != string.Empty)
                    {
                        Qty = Convert.ToInt32(Quantity);
                    }
                    float TotalPriceafterExtraDiscount = UnitPriceafterExtraDiscount * Qty;
                    dt.Rows[RowId]["Total Price after Extra Discount"] = TotalPriceafterExtraDiscount.ToString("0.00");
                    dt.Rows[RowId]["GM"] = (((TotalPriceafterExtraDiscount - CostPrice) / TotalPriceafterExtraDiscount) * 100).ToString("0.00");
                }
               

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
                string TotalPrice = dt.Rows[i]["Total Price after Extra Discount"].ToString().Trim();
                if (TotalPrice == string.Empty)
                {
                    TotalPrice = "0";
                }
                if (TotalPrice != string.Empty)
                {
                    GrandTotal = GrandTotal + float.Parse(TotalPrice);
                }
                string CostPrice = dt.Rows[i]["CostPrice"].ToString().Trim();
                TotalCost = TotalCost + float.Parse(CostPrice);

            }

            float TotalGM = 0;
            TotalGM = ((GrandTotal - TotalCost) / GrandTotal) * 100;
            txtTotalGM.Text = TotalGM.ToString("0.00");


            //float CarriageVal = 0;
        
           // TxtGrandTotal.Text = (GrandTotal + CarriageVal).ToString("0.00");
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
                string TotalPrice = dt.Rows[i]["Total Price after Extra Discount"].ToString().Trim();
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


            TextBox txtQty =(TextBox) currentRow.FindControl("txtQty");

                       
            if (txtQty.Text== "0")
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid Qty')</script>");
                lblError.Text= "Invalid Qty";
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
                    CostPrice = obj.getCostPrice(PartNum.Text);

                    if (CostPrice != "")
                    {
                        if (txtCurrency.Text == "EUR")
                        {
                            CostPrice = (float.Parse(CostPrice) * rate).ToString();
                        }
                    }
                    if (CostPrice == "")
                    {
                        DataTable dtICO = new DataTable();
                        dtICO = obj.GetICOItem(PartNum.Text, string.Empty);
                        if(dtICO.Rows.Count!=0)
                        { 

                            CostPrice = dtICO.Rows[0]["CostPrice"].ToString();
                            if (txtCurrency.Text == "EUR")
                            {
                                CostPrice = (float.Parse(CostPrice) * rate).ToString();
                            }
                        }
                    }
                    dr["CostPrice"] = float.Parse(CostPrice).ToString("0.00");
                    TextBox Desc = (TextBox)row.FindControl("txtDesc");
                    dr["Description"] = Desc.Text;
                    TextBox QTY = (TextBox)row.FindControl("txtQTY");
                    dr["QTY"] = QTY.Text;
                    TextBox leadtime = (System.Web.UI.WebControls.TextBox)row.FindControl("txtLeadTime");
                    dr["LeadTime"] = leadtime.Text;
                    TextBox txtUnitPrice = (System.Web.UI.WebControls.TextBox)row.FindControl("txtUnitPrice");
                    dr["Unit Price"] = txtUnitPrice.Text;
                    TextBox Dist = (TextBox)row.FindControl("txtDiscount");
                    dr["AdditionalDiscount"] = Dist.Text;
                    dt.Rows.Add(dr);
                }
            }
            //Get Item data based on part no
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            int RowId = currentRow.DataItemIndex;
            int Quantity = 0;
            float UnitPriceafterExtraDiscount = 0;
            float TotalPriceafterExtraDiscount = 0;
            string txtQty = dt.Rows[RowId]["QTY"].ToString();
            string disc = dt.Rows[RowId]["AdditionalDiscount"].ToString().Trim();
            float Discount = 0;
            string UnitPrice = dt.Rows[RowId]["Unit Price"].ToString().Trim();



            if (disc != string.Empty)
            {
                Discount = float.Parse(disc);
            }
            if (UnitPrice != string.Empty)
            {
                float ConvUnitPrice = (float.Parse(UnitPrice));
                UnitPriceafterExtraDiscount = ConvUnitPrice - ((ConvUnitPrice * Discount) / 100);
                dt.Rows[RowId]["Unit Price after Extra Discount"] = UnitPriceafterExtraDiscount.ToString("0.00");
            }
            if (txtQty != string.Empty)
            {
                Quantity = Convert.ToInt32(txtQty);
            }

            string txtUnitPriceAfterDiscount = dt.Rows[RowId]["Unit Price after Extra Discount"].ToString().Trim();
            if (txtUnitPriceAfterDiscount != string.Empty)
            {
                UnitPriceafterExtraDiscount = float.Parse(txtUnitPriceAfterDiscount);
            }
            TotalPriceafterExtraDiscount = (UnitPriceafterExtraDiscount * Quantity);
            dt.Rows[RowId]["Total Price after Extra Discount"] = TotalPriceafterExtraDiscount.ToString("0.00");
            //Calculate GM%
            //string UnitPrice = dt.Rows[0]["Unit Price"].ToString();
            float CostPriceVal = 0;
            if (CostPrice != string.Empty)
            {
                CostPriceVal = float.Parse(CostPrice);
            }
            dt.Rows[RowId]["CostPrice"] = CostPriceVal.ToString("0.00");
            float TotalCost = CostPriceVal * Quantity;
           
            float GM = 0;
            GM = (((TotalPriceafterExtraDiscount - TotalCost) * 100) / TotalPriceafterExtraDiscount);

          
            dt.Rows[RowId]["GM"] = GM.ToString("0,0.00");
           
            GridView1.Columns[15].Visible = true;
            ViewState["dt"] = dt;
            GridView1.DataSource = dt; // bind new datatable to grid
            
            GridView1.DataBind();
            if (UserRole == "Sales Engineer") //03-11-20 removed SM from here per Business Request
            {
                GridView1.Columns[15].Visible = true;
            }

            float GrandTotal = 0;
            float totalforGM = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string TotalPrice = dt.Rows[i]["Total Price after Extra Discount"].ToString().Trim();

                if (TotalPrice != string.Empty)
                {
                    GrandTotal = GrandTotal + float.Parse(TotalPrice);

                    if (dt.Rows[i]["PartNo"].ToString().ToUpper().Contains("SUNDRY") == false)
                    {
                        totalforGM = totalforGM + float.Parse(TotalPrice);
                    }
                }


            }
            float TotalOverallCost = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
             
                TotalOverallCost = TotalOverallCost + float.Parse(dt.Rows[i]["CostPrice"].ToString().Trim()) * Convert.ToInt32(dt.Rows[i]["Qty"].ToString());

            }
           
            float TotalGM = 0;

            
            TotalGM = (((totalforGM - TotalOverallCost) * 100) / totalforGM);
          
                
            TxtGrandTotal.Text = GrandTotal.ToString("0.00");
            txtTotalGM.Text = TotalGM.ToString("0.00");

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
                
                if (chkPerforma.Checked==true &&  drpCustomer.SelectedItem.Text == "SUNDRY ACCOUNT" && (txtCustName.Text.Trim() == "" || txtSundryBranch.Text.Trim() == ""))
                {
                    
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the customer address')</script>");
                    lblError.Text= "Please enter the customer address";
                    return;
                }
                if (drpCarriageCharges.SelectedItem.Text.Contains("Add New") && txtCarriage.Text.Trim() == "")
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the carriage charges')</script>");
                    lblError.Text = "Please enter the carriage charges";
                    return;
                }




                bool blnQtyZero = false;
                string Unit_Price="";
                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        TextBox txtQty = (TextBox)gr.FindControl("txtQty");
                        if (txtQty.Text == "0")
                        {
                            blnQtyZero = true;
                        }

                    TextBox UPTextbox = (TextBox)gr.FindControl("txtUnitPrice");
                    Unit_Price = UPTextbox.Text;
                }


                if (blnQtyZero)
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid Qty')</script>");
                    lblError.Text = "Invalid Qty";
                    return;
                }
                else if (Unit_Price=="" || Unit_Price=="0.00" || Unit_Price == "0")
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the Unit Price')</script>");
                    lblError.Text = "Please enter the Unit Price";
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
                   "alert('" + Message + "');window.location ='Dashboard.aspx';",
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
            string CustomerName = drpCustomer.SelectedItem.Text;
            string CustomerNumber = string.Empty;
            if (DrpCustNo.Visible == true)
            {
                 CustomerNumber = DrpCustNo.SelectedItem.Text;
            }
            else
            {
                 CustomerNumber = "";
            }
            
            string CustomerEmail = txtCustEmail.Text;
            string CustomerPhone = txtCustPhone.Text;
            string ProjectName = txtProjectName.Text;
            string OppurtunityId = txtOppurtunityId.Text;
            string Status_Comment = string.Empty;
            string isPerforma = string.Empty;
            if (chkPerforma.Checked == true)
            {
                isPerforma = "Proforma";
            }
            else
            {
                isPerforma = "Quote";
            }
            //string PaymentTerms = string.Empty;
            //string PartialDelivery = drpPartialDelivery.SelectedItem.Text;
            string Currency =txtCurrency.Text;            
            string PreparedBy = drpPreparedBy.SelectedItem.Text;
            string PreparedByEmail = txtSEEMail.Text;
            string PreparedByPhone = txtSEPhone.Text;
            string SalesPerson = txtSalesPerson.Text;
            string SalesPersonEmail = txtSPEmail.Text;
            string SalesPersonPhone = txtSPPhone.Text;
            string CustomerNameAdd = txtCustName.Text;
            string Comments = txtComments.Text;


            if (FileUpload1.HasFile)
            {
                string folderName = @"C:\UK_RPAQuoteTool_Deploy\App_Data\AdditonalDocs";

                string pathString = System.IO.Path.Combine(folderName, QuoteNumber);
                System.IO.Directory.CreateDirectory(pathString);

                FileUpload1.SaveAs(System.IO.Path.Combine(pathString, FileUpload1.FileName));

            }



            string CustomerBranch = string.Empty;
            string SundryBranch = string.Empty;
            if (drpCustBranch.SelectedItem.Text != string.Empty && drpCustBranch.SelectedItem.Text != "SUNDRY BRANCH")
            {
                 CustomerBranch = drpCustBranch.SelectedItem.Text;
            }
            else
            {
                CustomerBranch = "SUNDRY BRANCH";
                SundryBranch  = txtSundryBranch.Text;
            }

            if (drpCustomer.SelectedItem.Text.Contains("SUNDRY"))
            {
                if (chkPerforma.Checked == true)
                {
                    if (txtSundryBranch.Visible == true)
                    {
                        SundryBranch = txtSundryBranch.Text;
                    }
                }
            }
            string UserRole = (string)Session["UserRole"];
            if (UserRole=="Admin")
            {
                PreparedBy = "On Behalf Of " + PreparedBy;
            }
            DateTime CreationDate = DateTime.ParseExact(txtCreationdate.Text,"dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture);
            DateTime ExpirationDate = DateTime.ParseExact(txtExpirationDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


            string CarriageCharge = "";

            if (drpCarriageCharges.SelectedItem.Text.Contains("Add New"))
            {
                CarriageCharge = txtCarriage.Text;
            }
            else
            {
                CarriageCharge = drpCarriageCharges.SelectedItem.Text;
            }
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
            string UnitPrice = "";
            string CostPrice = "";
            string AddDiscount = "";
            string UnitPriceAfterDiscount = "";
            string TotalPriceAfterDiscount = "";
            float GM = 0;
            //string Country = txtCustomerCountry.Text;
            string Version = txtQuoteNum.Text + "_0";
            // string SerialNo = ViewState["SerialNo"].ToString();
            //Delete existing records
            float AverageUnitPrice = 0;
            float AverageAddDiscount = 0;
            float AverageGM = 0;
            //string StandardCost = "";
            float AvgTotalPrice = 0;
            int SalesManagerFlag = 0;
            int GMFlag = 0;

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
                //MOQ = gr.Cells[6].Text;

                TextBox LeadTimetextbox = (TextBox)gr.FindControl("txtLeadTime");
                 LeadTime = LeadTimetextbox.Text;
                //LeadTime = gr.Cells[7].Text;
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
                    ListPrice = String.Empty;

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

                
                TextBox txtUnitPrice = (TextBox)gr.FindControl("txtUnitPrice");
                UnitPrice = txtUnitPrice.Text;
                
                             
                TextBox DiscountTextbox = (TextBox)gr.FindControl("txtDiscount");
                AddDiscount = DiscountTextbox.Text;
                UnitPriceAfterDiscount = gr.Cells[17].Text;
                TotalPriceAfterDiscount = gr.Cells[18].Text;
                float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                if (PartNo.ToString().ToUpper().Contains("SUNDRY"))
                {
                    CostPrice = "0";
                }
                else
                {
                    CostPrice = obj.getCostPrice(PartNo);
                    if (CostPrice != "")
                    {
                        if (txtCurrency.Text == "EUR")
                        {
                            CostPrice = (float.Parse(CostPrice) * rate).ToString();
                        }
                    }
                    if (CostPrice == "")
                    {
                        DataTable dtICO = new DataTable();
                        dtICO = obj.GetICOItem(PartNo, string.Empty);
                        CostPrice = dtICO.Rows[0]["CostPrice"].ToString();
                        if (txtCurrency.Text == "EUR")
                        {
                            CostPrice = (float.Parse(CostPrice) * rate).ToString();
                        }
                    }
                }
            

              

                float ConvertedUnitPrice = 0;
                float ConvertedAddDiscount = 0;
                float ConvertedGM = 0;
                float ConvertedTotalPrice = 0;
                
                if (TotalPriceAfterDiscount.Trim() != string.Empty && TotalPriceAfterDiscount.Trim() != "0")
                {
                    ConvertedUnitPrice = float.Parse(TotalPriceAfterDiscount);
                }
                if (AddDiscount.Trim() != string.Empty && AddDiscount.Trim() != "0")
                {
                    ConvertedAddDiscount = float.Parse(AddDiscount);
                }

                if (CostPrice.Trim() != string.Empty && CostPrice.Trim() != "0") //added by divya
                {
                    ConvertedTotalPrice = float.Parse(CostPrice) * QTY;
                }
                else
                    ConvertedTotalPrice = 0;

                    GM = ((ConvertedUnitPrice - ConvertedTotalPrice) * 100) / ConvertedUnitPrice;
               

               
                
                AverageAddDiscount = AverageAddDiscount + ConvertedAddDiscount;
                if (PartNo.ToString().ToUpper().Contains("SUNDRY")==false)
                {
                      AvgTotalPrice = AvgTotalPrice + ConvertedTotalPrice;
                }


                
                //string total = TxtGrandTotal.Text;
                

                if (AverageUnitPrice != 0)
                {

                    if (PartNo.ToString().ToUpper().Contains("SUNDRY"))
                    {
                        
                            AverageUnitPrice = AverageUnitPrice - ConvertedUnitPrice;
                        
                    }
                   
                }

                string ExportFlag = "";
                if (chkExportCustomer.Checked)
                {
                    ExportFlag = "Yes";
                }
                else
                {
                    ExportFlag = "No";
                }
               
                obj.SaveQuote(QuoteNumber, CustomerName, CustomerNumber,CustomerBranch,CustomerEmail ,CustomerPhone,ProjectName, OppurtunityId, string.Empty, string.Empty, Currency, PreparedBy,PreparedByEmail,PreparedByPhone,SalesPerson,SalesPersonEmail,SalesPersonPhone,ProductFamily,ItemNo,PartNo, Desc, QTY, MOQ, LeadTime,AvailableQty,Weight, SafetyStock,ListPrice,Discount,UnitPrice,AddDiscount, UnitPriceAfterDiscount, TotalPriceAfterDiscount, GM.ToString("0,0.00"), Status, CreationDate, ExpirationDate, CarriageCharge, Version,Comments, total,string.Empty,CostPrice,SundryBranch,isPerforma,Status_Comment,ExportFlag,txtVAT.Text,StockAvailable);
                if (txtCustName.Text != string.Empty && txtCustName.Visible == true)
                {
                    string strCustName = string.Empty;
                    string strCustAdd = string.Empty;
                    if (txtCustName.Text.Contains("\r\n"))
                    {
                        strCustName = txtCustName.Text.Substring(0, txtCustName.Text.IndexOf("\r\n"));
                        strCustAdd = txtCustName.Text.Substring(txtCustName.Text.IndexOf("\r\n") + 1);
                    }
                    else
                    {
                        strCustName = txtCustName.Text.Substring(0);
                    }

                    obj.SaveSundryCust(QuoteNumber, strCustName, strCustAdd);
                }
            }
            string Approval1 = "";
            //string Approval2 = "";
            string Approval3 = "";

           
                AverageGM = ((AverageUnitPrice - AvgTotalPrice) * 100) / AverageUnitPrice;
            

            AverageAddDiscount = AverageAddDiscount / GridView1.Rows.Count;



            if((AverageUnitPrice >= 2500 && AverageUnitPrice <= 5000 && UserName!= "Richard Kleiser") || (AverageUnitPrice > 5000 && UserName == "Richard Kleiser")) //03-11-20 removed BDM from approval matrix
                {

                SalesManagerFlag = 1;

            }
            else if ((AverageUnitPrice > 5000  || AverageGM<25 || blnSundryItem==true) && (UserName != "Richard Kleiser"))
            {

                GMFlag = 1;
            }

           


            if (GMFlag == 1)
            {
                if (UserRole == "Sales Engineer" || UserRole == "Admin")
                {
                    Approval1 = "Pending";
                    //Approval2 = "Pending";
                    Approval3 = "Pending";
                }
                else if (UserRole == "Sales Manager")
                {
                    Approval1 = "Approved";
                    //Approval2 = "Pending";
                    Approval3 = "Pending";
                }
            }
            else if (SalesManagerFlag == 1)
            {
                if ((UserRole == "Sales Engineer" && UserName != "Richard Kleiser") || UserRole == "Admin")
                {
                    Approval1 = "Pending";
                    //Approval2 = "NA";
                    Approval3 = "NA";
                }
                else if (UserRole == "Sales Engineer" && UserName == "Richard Kleiser")
                {
                    Approval1 = "Approved";
                    Approval3 = "Pending";
                }

                else if (UserRole == "Sales Manager")
                {
                    Approval1 = "Approved";
                    Approval3 = "NA";
                }

            }
            else
            {
                Approval1 = "NA";
                Approval3 = "NA";

                                                                                         
            }
            //End of Matrix calculation
            if (UserRole == "Sales Manager" && SalesManagerFlag == 1 && Status == "Pending Approval") //03-17-20 to change status to approved only on Submit
            {
                Status = "Approved";
                obj.UpdateQuoteDetails(QuoteNumber, Status);
            }
            else if (SalesManagerFlag == 0 && GMFlag == 0 && Status == "Pending Approval")
            {
                Status = "Approved";
                obj.UpdateQuoteDetails(QuoteNumber, Status);
            }

            obj.UpdateMatrixDetails(Approval1, Approval3,AverageGM.ToString(), QuoteNumber);

           

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate("Submit");
            if (Page.IsValid)
            {
                if (chkPerforma.Checked == true && drpCustomer.SelectedItem.Text == "SUNDRY ACCOUNT" && (txtCustName.Text.Trim() == "" || txtSundryBranch.Text.Trim() == ""))
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the customer address')</script>");
                    lblError.Text = "Please enter the customer address";
                    return;
                }
                if (drpCarriageCharges.SelectedItem.Text.Contains("Add New") && txtCarriage.Text.Trim() == "")
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the carriage charges')</script>");
                    lblError.Text = "Please enter the carriage charges";
                    return;
                }
                if (drpCustBranch.SelectedItem.Text.Contains("SUNDRY") && txtSundryBranch.Text.Trim() == "")
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the carriage charges')</script>");
                    lblError.Text = "Please enter the Sundry branch";
                    return;
                }
                bool blnQtyZero = false;
                string Unit_Price = "";
                foreach (GridViewRow gr in GridView1.Rows)
                    {
                        TextBox txtQty = (TextBox)gr.FindControl("txtQty");
                        if (txtQty.Text == "0")
                        {
                            blnQtyZero = true;
                        }
                    TextBox UPTextbox = (TextBox)gr.FindControl("txtUnitPrice");
                    Unit_Price = UPTextbox.Text;
                    

                }


                    if (blnQtyZero)
                    {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid Qty')</script>");
                    lblError.Text = "Invalid Qty";
                        return;
                    }
                    else if (txtSalesPerson.Text == string.Empty || txtSEEMail.Text == string.Empty)
                    {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Enter Sales Person details')</script>");
                    lblError.Text = "Enter Sales Person details";
                        return;
                    }
                    else if (Unit_Price == "" || Unit_Price == "0.00" || Unit_Price == "0")
                    {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the Unit Price')</script>");
                    lblError.Text = "Please enter the Unit Price";
                        return;
                    }
                    else
                    {
                        string Quote = txtQuoteNum.Text.Trim();
                        if (Quote == string.Empty)
                        {
                            GetQuoteNumber();
                        }
                        string Status = "Pending Approval";
                        SaveQuoteDetails(Status);
                        string QuoteNumber = txtQuoteNum.Text;

                        SmtpClient smtpClient = new SmtpClient("smtp.watts.com");
                        smtpClient.UseDefaultCredentials = true;
                        string UserName = (string)Session["UserName"];
                        string UserRole = (string)Session["UserRole"];
                        string Manager = string.Empty;

                        Manager = txtSalesPerson.Text;
                        DataTable dtApprovals = new DataTable();
                        dtApprovals = objQuoteBAL.GetApprovaldata(QuoteNumber);


                        string Approval1 = "";
                        //string Approval2 = "";
                        string Approval3 = "";

                        if (dtApprovals.Rows.Count > 0)
                        {
                            Approval1 = dtApprovals.Rows[0]["approval1"].ToString();
                            //Approval2 = dtApprovals.Rows[0]["approval2"].ToString();
                            Approval3 = dtApprovals.Rows[0]["approval3"].ToString();
                        }

                    if (Approval1 == "NA" && Approval3 == "NA")
                    {
                        if (UserRole == "Sales Engineer" || UserRole == "Admin")
                        {

                            //string MailTo = ConfigurationManager.AppSettings["Email" + Manager];
                            string MailTo3 = txtSEEMail.Text;
                            MailMessage mail3 = new MailMessage("ukquotations@wattswater.com", MailTo3);
                            mail3.Subject = QuoteNumber + " is auto approved"; //divya added space
                            MailAddress copy3 = new MailAddress(ConfigurationManager.AppSettings["EmailAlan Fahy"]); //choprad send email to admin 2/4/20
                            MailAddress copy2 = new MailAddress(txtSPEmail.Text);
                            mail3.CC.Add(copy2);
                            mail3.Bcc.Add(copy3);
                            mail3.Body = "Your quote is auto approved";
                            smtpClient.Send(mail3);

                        }
                    }
                    else
                    {
                        if (UserRole == "Sales Engineer" || UserRole == "Admin")
                        {

                            //string MailTo = ConfigurationManager.AppSettings["Email" + Manager];
                            string MailTo3 = txtSPEmail.Text;
                            MailMessage mail3 = new MailMessage("ukquotations@wattswater.com", MailTo3);
                            mail3.Subject = QuoteNumber + " Pending Approval"; //divya added space
                            MailAddress copy3 = new MailAddress(ConfigurationManager.AppSettings["EmailAlan Fahy"]); //choprad send email to admin 2/4/20
                            mail3.Bcc.Add(copy3);

                            // string QuoteURL =+ QuoteNumber + "&Status=" + CurrentStatus + "&UserName=" + UserName + "&UserRole=" + Role;
                            string CurrentStatus = "Pending Approval";
                            string Role = "Sales Manager";
                            string site = "ukrpaquotetool.wattswater.com/Quote.aspx?QuoteNo=" + QuoteNumber + "&Status=" + CurrentStatus + "&UserName=" + UserName + "&UserRole=" + Role;
                            //string QuoteURL = 
                            //mail.Body = @"New Quote is pending for Approval. Click the link below to approve the quote  < a href = ""https://"+site+"> Quote </a>";
                            CurrentStatus = CurrentStatus.Replace(" ", "%20");
                            UserName = UserName.Replace(" ", "%20");
                            Role = Role.Replace(" ", "%20");
                            mail3.Body = "https://ukrpaquotetool.wattswater.com/Quote.aspx?QuoteNo=" + QuoteNumber + "&Status=" + CurrentStatus + "&UserName=" + UserName + "&UserRole=" + Role;


                            smtpClient.Send(mail3);
                        }
                    }


                            string Message = "Quote submitted successfully with quote number " + QuoteNumber;


                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                           "alert",
                           "alert('" + Message + "');window.location ='Dashboard.aspx';",
                           true);



                            if ((Approval1 == "NA" && Approval3 == "NA") || (UserRole == "Sales Manager" && Approval1 == "Approved" && Approval3 == "NA"))
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
                                        TextBox LeadTime = (TextBox)row.FindControl("txtLeadTime");
                                        dr["LeadTime"] = LeadTime.Text;
                                        TextBox Discount = (TextBox)row.FindControl("txtDiscount");
                                        dr["AdditionalDiscount"] = Discount.Text;
                                        dt.Rows.Add(dr);
                                    }
                                }
                      

                        dt.Columns.Remove(dt.Columns[0]);
                        dt.Columns.Remove(dt.Columns[0]);
                        dt.Columns.Remove(dt.Columns[0]);


                        dt.Columns.Remove(dt.Columns[5]);
                        dt.Columns.Remove(dt.Columns[5]);
                        dt.Columns.Remove(dt.Columns[6]);
                        dt.Columns.Remove(dt.Columns[6]);
                        dt.Columns.Remove(dt.Columns[6]);
                        dt.Columns.Remove(dt.Columns[6]);
                        dt.Columns.Remove(dt.Columns[6]);
                        dt.Columns.Remove(dt.Columns[6]);

                        dt.Columns.Remove(dt.Columns[8]);
                        dt.Columns.Remove(dt.Columns[8]);
                       



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
                                    else if (cellText == "Unit Price After Extra Discount")
                                    {
                                        widths[x] = 210;
                                    }

                                    else if (cellText == "Total Price After Extra Discount")
                                    {
                                        widths[x] = 210;
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
                                    if (j == 4) //if available qty<=o make it blank
                                    {
                                        if (cellText.Trim() != string.Empty)
                                        {
                                            if (Convert.ToInt32(cellText) <= 0)
                                            {
                                                cellText = "";
                                            }
                                        }
                                    }

                                    if (j == 6 || j == 7)
                                        {
                                            if (txtCurrency.Text == "EUR")
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

                                StringWriter sw = new StringWriter();
                                HtmlTextWriter hw = new HtmlTextWriter(sw);


                                MemoryStream memoryStream = new MemoryStream();

                                StringReader sr = new StringReader(sw.ToString());
                                Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 10f, 50f);
                                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                                writer.PageEvent = new RPADubaiQuoteTool.ITextEvents();

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
                                pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               Watts Industries UK LTD", fontAdd));
                                pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               COLMWORTH BUSINESS PARK", fontAdd));
                                pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               EATON SOCON", fontAdd));
                                pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               ST NEOTS PE19 8YX, UK. ", fontAdd));
                                pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               Tel : +44 (0) 1480 407074 \r\n\r\n", fontAdd));
                                if (chkPerforma.Checked == true)
                                {
                                    pdfDoc.Add(new Phrase("\r\n                                                            Proforma Invoice\r\n", font));
                                    if (chkExportCustomer.Checked == true)
                                    {
                                        if (txtVAT.Text != "")
                                        {
                                            pdfDoc.Add(new Phrase("\r\n                                                  OUR VAT No. "+txtVAT.Text+"\r\n", font));
                                        }
                                    }
                                    else
                                    {
                                        pdfDoc.Add(new Phrase("\r\n                                                  OUR VAT No. GB 590 7580 12\r\n", font));
                                    }
                                }
                                else
                                {
                                    pdfDoc.Add(new Phrase("\r\n                                                            QUOTATION\r\n", font));
                                }

                                DataTable dtAddress = new DataTable();
                                if (chkPerforma.Checked == false)
                                {
                                    if (DrpCustNo.Visible == true)
                                    {

                                        if (drpCustBranch.SelectedItem.Text == "Select" || drpCustBranch.SelectedItem.Text == "")
                                        {
                                            dtAddress = obj.GetCustNameAddress(string.Empty, DrpCustNo.SelectedItem.Text);

                                        }
                                        else if (drpCustBranch.SelectedItem.Text != "SUNDRY BRANCH")
                                        {
                                            dtAddress = obj.GetCustNameAddress(drpCustBranch.SelectedItem.Text, DrpCustNo.SelectedItem.Text);
                                        }

                                        if (dtAddress.Rows.Count != 0)
                                        {
                                            pdfDoc.Add(new Phrase("\r\n" + dtAddress.Rows[0][0].ToString(), font));
                                            pdfDoc.Add(new Phrase("\r\n" + dtAddress.Rows[0][1].ToString(), font));
                                            pdfDoc.Add(new Phrase("\r\n" + dtAddress.Rows[0][2].ToString(), font));
                                            pdfDoc.Add(new Phrase("\r\n" + dtAddress.Rows[0][3].ToString(), font));
                                            pdfDoc.Add(new Phrase("\r\n" + dtAddress.Rows[0][4].ToString(), font));
                                          
                                }
                                        else
                                        {
                                            pdfDoc.Add(new Phrase("\r\n" + txtSundryBranch.Text.ToString(), font));
                                        }
                                    }
                                    else
                                    {
                                        string[] strSundryCust = txtCustName.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                        int i = strSundryCust.Length;
                                        for (int j = 0; j <= i - 1; j++)
                                        {
                                            pdfDoc.Add(new Phrase("\r\n" + strSundryCust[j].ToString(), font));
                                        }

                                    }
                                }
                                else
                                {
                                    PdfPTable tableAdd = new PdfPTable(2);
                                    tableAdd.WidthPercentage = 100;
                                    PdfPCell cellIAdd = new PdfPCell(new Phrase("Invoice Address:", font));
                                    cellIAdd.HorizontalAlignment = Element.ALIGN_LEFT;
                                    cellIAdd.Border = 0;
                                    tableAdd.AddCell(cellIAdd);

                                    PdfPCell cellDAdd = new PdfPCell(new Phrase("Delivery Address", font));
                                    cellDAdd.HorizontalAlignment = Element.ALIGN_LEFT;
                                    cellDAdd.Border = 0;
                                    tableAdd.AddCell(cellDAdd);

                                    if (drpCustomer.SelectedItem.Text.Contains("SUNDRY") == false)
                                    {
                                        DataTable dtCustAdd = new DataTable();
                                        dtCustAdd = obj.GetCustomerAddress(drpCustomer.SelectedItem.Text);


                                        //pdfDoc.Add(new Phrase("\r\n" + "Invoice Address:".PadRight(115- "Invoice Address:".Length) +"Delivery Address:", font));
                                        if (drpCustBranch.SelectedItem.Text.Contains("SUNDRY"))
                                        {
                                            string[] strSundryBranch = txtSundryBranch.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                            int i = strSundryBranch.Length;

                                            PdfPCell cellI1 = new PdfPCell(new Phrase(drpCustomer.SelectedItem.Text.Trim(), font));
                                            cellI1.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI1.Border = 0;
                                            tableAdd.AddCell(cellI1);

                                            PdfPCell cellI2 = new PdfPCell(new Phrase(strSundryBranch[0].ToString(), font));
                                            cellI2.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI2.Border = 0;
                                            tableAdd.AddCell(cellI2);

                                            PdfPCell cellI3 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][0].ToString().Trim(), font));
                                            cellI3.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI3.Border = 0;
                                            tableAdd.AddCell(cellI3);
                                            if (strSundryBranch.Length >= 2)
                                            {
                                                PdfPCell cellI4 = new PdfPCell(new Phrase(strSundryBranch[1].ToString(), font));
                                                cellI4.HorizontalAlignment = Element.ALIGN_LEFT;
                                                cellI4.Border = 0;
                                                tableAdd.AddCell(cellI4);
                                            }


                                            PdfPCell cellI5 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][1].ToString().Trim(), font));
                                            cellI5.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI5.Border = 0;
                                            tableAdd.AddCell(cellI5);

                                            if (strSundryBranch.Length >= 3)
                                            {
                                                PdfPCell cellI6 = new PdfPCell(new Phrase(strSundryBranch[2].ToString(), font));
                                                cellI6.HorizontalAlignment = Element.ALIGN_LEFT;
                                                cellI6.Border = 0;
                                                tableAdd.AddCell(cellI6);
                                            }

                                            PdfPCell cellI7 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][2].ToString().Trim(), font));
                                            cellI7.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI7.Border = 0;
                                            tableAdd.AddCell(cellI7);

                                            if (strSundryBranch.Length >= 4)
                                            {
                                                PdfPCell cellI8 = new PdfPCell(new Phrase(strSundryBranch[3].ToString(), font));
                                                cellI8.HorizontalAlignment = Element.ALIGN_LEFT;
                                                cellI8.Border = 0;
                                                tableAdd.AddCell(cellI8);
                                            }

                                            PdfPCell cellI9 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][3].ToString().Trim(), font));
                                            cellI9.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI9.Border = 0;
                                            tableAdd.AddCell(cellI9);

                                            if (strSundryBranch.Length >= 5)
                                            {
                                                PdfPCell cellI10 = new PdfPCell(new Phrase(strSundryBranch[4].ToString(), font));
                                                cellI10.HorizontalAlignment = Element.ALIGN_LEFT;
                                                cellI10.Border = 0;
                                                tableAdd.AddCell(cellI10);
                                            }

//////////////////////////////////////////////////////////////////////
                                    PdfPCell cellI90 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][4].ToString().Trim(), font));
                                    cellI90.HorizontalAlignment = Element.ALIGN_LEFT;
                                    cellI90.Border = 0;
                                    tableAdd.AddCell(cellI90);

                                    if (strSundryBranch.Length >= 6)
                                    {
                                        PdfPCell cellI100 = new PdfPCell(new Phrase(strSundryBranch[5].ToString(), font));
                                        cellI100.HorizontalAlignment = Element.ALIGN_LEFT;
                                        cellI100.Border = 0;
                                        tableAdd.AddCell(cellI100);
                                    }

                                    PdfPCell cellI91 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][5].ToString().Trim(), font));
                                    cellI91.HorizontalAlignment = Element.ALIGN_LEFT;
                                    cellI91.Border = 0;
                                    tableAdd.AddCell(cellI91);

                                    if (strSundryBranch.Length >= 7)
                                    {
                                        PdfPCell cellI101 = new PdfPCell(new Phrase(strSundryBranch[6].ToString(), font));
                                        cellI101.HorizontalAlignment = Element.ALIGN_LEFT;
                                        cellI101.Border = 0;
                                        tableAdd.AddCell(cellI101);
                                    }
////////////////////////////////////////////////////////////////////////




                                }
                                else if (drpCustBranch.SelectedItem.Text == "Select" || drpCustBranch.SelectedItem.Text == "")
                                        {
                                            PdfPCell cellI1 = new PdfPCell(new Phrase(drpCustomer.SelectedItem.Text.Trim(), font));
                                            cellI1.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI1.Border = 0;
                                            tableAdd.AddCell(cellI1);

                                            PdfPCell cellI2 = new PdfPCell(new Phrase(drpCustomer.SelectedItem.Text.Trim(), font));
                                            cellI2.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI2.Border = 0;
                                            tableAdd.AddCell(cellI2);

                                            PdfPCell cellI3 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][0].ToString().Trim(), font));
                                            cellI3.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI3.Border = 0;
                                            tableAdd.AddCell(cellI3);

                                            PdfPCell cellI4 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][0].ToString().Trim(), font));
                                            cellI4.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI4.Border = 0;
                                            tableAdd.AddCell(cellI4);

                                            PdfPCell cellI5 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][1].ToString().Trim(), font));
                                            cellI5.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI5.Border = 0;
                                            tableAdd.AddCell(cellI5);

                                            PdfPCell cellI6 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][1].ToString().Trim(), font));
                                            cellI6.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI6.Border = 0;
                                            tableAdd.AddCell(cellI6);

                                            PdfPCell cellI7 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][2].ToString().Trim(), font));
                                            cellI7.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI7.Border = 0;
                                            tableAdd.AddCell(cellI7);

                                            PdfPCell cellI8 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][2].ToString().Trim(), font));
                                            cellI8.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI8.Border = 0;
                                            tableAdd.AddCell(cellI8);

                                            PdfPCell cellI11 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][3].ToString().Trim(), font));
                                            cellI11.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI11.Border = 0;
                                            tableAdd.AddCell(cellI11);

                                            PdfPCell cellI12 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][3].ToString().Trim(), font));
                                            cellI12.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI12.Border = 0;
                                            tableAdd.AddCell(cellI12);

                                        }
                                        else
                                        {
                                            dtAddress = obj.GetCustNameAddress(drpCustBranch.SelectedItem.Text, DrpCustNo.SelectedItem.Text);

                                            PdfPCell cellI1 = new PdfPCell(new Phrase(drpCustomer.SelectedItem.Text.Trim(), font));
                                            cellI1.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI1.Border = 0;
                                            tableAdd.AddCell(cellI1);

                                            PdfPCell cellI2 = new PdfPCell(new Phrase(dtAddress.Rows[0][0].ToString(), font));
                                            cellI2.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI2.Border = 0;
                                            tableAdd.AddCell(cellI2);

                                            PdfPCell cellI3 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][0].ToString().Trim(), font));
                                            cellI3.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI3.Border = 0;
                                            tableAdd.AddCell(cellI3);

                                            PdfPCell cellI4 = new PdfPCell(new Phrase(dtAddress.Rows[0][1].ToString().Trim(), font));
                                            cellI4.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI4.Border = 0;
                                            tableAdd.AddCell(cellI4);

                                            PdfPCell cellI5 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][1].ToString().Trim(), font));
                                            cellI5.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI5.Border = 0;
                                            tableAdd.AddCell(cellI5);

                                            PdfPCell cellI6 = new PdfPCell(new Phrase(dtAddress.Rows[0][2].ToString(), font));
                                            cellI6.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI6.Border = 0;
                                            tableAdd.AddCell(cellI6);

                                            PdfPCell cellI7 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][2].ToString().Trim(), font));
                                            cellI7.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI7.Border = 0;
                                            tableAdd.AddCell(cellI7);

                                            PdfPCell cellI8 = new PdfPCell(new Phrase(dtAddress.Rows[0][3].ToString(), font));
                                            cellI8.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI8.Border = 0;
                                            tableAdd.AddCell(cellI8);

                                            PdfPCell cellI9 = new PdfPCell(new Phrase(dtCustAdd.Rows[0][3].ToString().Trim(), font));
                                            cellI9.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI9.Border = 0;
                                            tableAdd.AddCell(cellI9);

                                            PdfPCell cellI10 = new PdfPCell(new Phrase(dtAddress.Rows[0][4].ToString(), font));
                                            cellI10.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI10.Border = 0;
                                            tableAdd.AddCell(cellI10);

                                        }
                                    }
                                    else
                                    {
                                        string[] strSundryCust = txtCustName.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                        string[] strSundryBranch = txtSundryBranch.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                        int i = strSundryCust.Length;
                                        int k = strSundryBranch.Length;

                                        PdfPCell cellI1 = new PdfPCell(new Phrase(strSundryCust[0], font));
                                        cellI1.HorizontalAlignment = Element.ALIGN_LEFT;
                                        cellI1.Border = 0;
                                        tableAdd.AddCell(cellI1);

                                        PdfPCell cellI2 = new PdfPCell(new Phrase(strSundryBranch[0], font));
                                        cellI2.HorizontalAlignment = Element.ALIGN_LEFT;
                                        cellI2.Border = 0;
                                        tableAdd.AddCell(cellI2);

                                        if (strSundryCust.Length >= 2)
                                        {
                                            PdfPCell cellI3 = new PdfPCell(new Phrase(strSundryCust[1], font));
                                            cellI3.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI3.Border = 0;
                                            tableAdd.AddCell(cellI3);
                                        }

                                        if (strSundryBranch.Length >= 2)
                                        {
                                            PdfPCell cellI4 = new PdfPCell(new Phrase(strSundryBranch[1].ToString(), font));
                                            cellI4.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI4.Border = 0;
                                            tableAdd.AddCell(cellI4);
                                        }


                                        if (strSundryCust.Length >= 3)
                                        {
                                            PdfPCell cellI5 = new PdfPCell(new Phrase(strSundryCust[2], font));
                                            cellI5.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI5.Border = 0;
                                            tableAdd.AddCell(cellI5);
                                        }
                                        if (strSundryBranch.Length >= 3)
                                        {
                                            PdfPCell cellI6 = new PdfPCell(new Phrase(strSundryBranch[2], font));
                                            cellI6.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI6.Border = 0;
                                            tableAdd.AddCell(cellI6);
                                        }

                                        if (strSundryCust.Length >= 4)
                                        {
                                            PdfPCell cellI7 = new PdfPCell(new Phrase(strSundryCust[3], font));
                                            cellI7.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI7.Border = 0;
                                            tableAdd.AddCell(cellI7);
                                        }

                                        if (strSundryBranch.Length >= 4)
                                        {
                                            PdfPCell cellI8 = new PdfPCell(new Phrase(strSundryBranch[3], font));
                                            cellI8.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI8.Border = 0;
                                            tableAdd.AddCell(cellI8);
                                        }
                                        if (strSundryCust.Length >= 5)
                                        {
                                            PdfPCell cellI9 = new PdfPCell(new Phrase(strSundryCust[4], font));
                                            cellI9.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI9.Border = 0;
                                            tableAdd.AddCell(cellI9);
                                        }

                                        if (strSundryBranch.Length >= 5)
                                        {
                                            PdfPCell cellI10 = new PdfPCell(new Phrase(strSundryBranch[4], font));
                                            cellI10.HorizontalAlignment = Element.ALIGN_LEFT;
                                            cellI10.Border = 0;
                                            tableAdd.AddCell(cellI10);
                                        }
                                ///////////////////////////
                                if (strSundryCust.Length >= 6)
                                {
                                    PdfPCell cellI98 = new PdfPCell(new Phrase(strSundryCust[5], font));
                                    cellI98.HorizontalAlignment = Element.ALIGN_LEFT;
                                    cellI98.Border = 0;
                                    tableAdd.AddCell(cellI98);
                                }

                                if (strSundryBranch.Length >= 6)
                                {
                                    PdfPCell cellI1090 = new PdfPCell(new Phrase(strSundryBranch[5], font));
                                    cellI1090.HorizontalAlignment = Element.ALIGN_LEFT;
                                    cellI1090.Border = 0;
                                    tableAdd.AddCell(cellI1090);
                                }

                                if (strSundryCust.Length >= 7)
                                {
                                    PdfPCell cellI981 = new PdfPCell(new Phrase(strSundryCust[6], font));
                                    cellI981.HorizontalAlignment = Element.ALIGN_LEFT;
                                    cellI981.Border = 0;
                                    tableAdd.AddCell(cellI981);
                                }

                                if (strSundryBranch.Length >= 7)
                                {
                                    PdfPCell cellI1091 = new PdfPCell(new Phrase(strSundryBranch[6], font));
                                    cellI1091.HorizontalAlignment = Element.ALIGN_LEFT;
                                    cellI1091.Border = 0;
                                    tableAdd.AddCell(cellI1091);
                                }


                            }

                            pdfDoc.Add(tableAdd);
                                }



                                pdfDoc.Add(new Phrase("\r\n \r\n\r\n", font));
                                PdfPTable table1 = new PdfPTable(2);
                                table1.WidthPercentage = 100;
                                if (chkPerforma.Checked == false)
                                {

                                    PdfPCell cell1 = new PdfPCell(new Phrase("Quotation Ref: " + this.txtQuoteNum.Text.Trim(), font));
                                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                                    cell1.Border = 0;
                                    table1.AddCell(cell1);
                                }
                                else
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase("Proforma Invoice: " + this.txtQuoteNum.Text.Trim(), font));
                                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                                    cell1.Border = 0;
                                    table1.AddCell(cell1);

                                }

                                PdfPCell cell2 = new PdfPCell(new Phrase("Offer Creation Date  : " + txtCreationdate.Text, font));
                                cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell2.Border = 0;

                                table1.AddCell(cell2);


                                PdfPCell cell14 = new PdfPCell(new Phrase("Customer : " + drpCustomer.SelectedItem.Text, font));
                                cell14.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell14.Border = 0;
                                table1.AddCell(cell14);

                                PdfPCell cell3 = new PdfPCell(new Phrase("Offer Expiry Date  : " + txtExpirationDate.Text, font));
                                cell3.Colspan = 2;
                                cell3.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell3.Border = 0;
                                table1.AddCell(cell3);

                                string CustomerNo = "";
                                if (DrpCustNo.SelectedItem.Text.Trim() == "Select")
                                {
                                    CustomerNo = "";
                                }
                                else
                                    CustomerNo = DrpCustNo.SelectedItem.Text.Trim();

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

                                PdfPCell cell11 = new PdfPCell(new Phrase("Customer Reference : " + this.txtOppurtunityId.Text.Trim(), font));
                                cell11.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell11.Border = 0;
                                table1.AddCell(cell11);


                                PdfPCell cell12 = new PdfPCell(new Phrase("Prepared by  : " + this.drpPreparedBy.SelectedItem.Text, font));
                                cell12.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell12.Border = 0;
                                table1.AddCell(cell12);

                                PdfPCell cell13 = new PdfPCell(new Phrase("Currency: "+this.txtCurrency.Text, font));
                                cell13.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell13.Border = 0;
                                table1.AddCell(cell13);


                            float CarriageVal = 0;
                            string currencysymbol = "£";
                            if (drpCarriageCharges.SelectedItem.Text.Contains("Add New"))
                            {
                                CarriageVal = float.Parse(txtCarriage.Text);
                            }
                            else
                            {
                                string temp = drpCarriageCharges.SelectedItem.Text.Substring(drpCarriageCharges.SelectedItem.Text.IndexOf("£"));
                                string Carriage = temp.Substring(1, temp.IndexOf("/") - 1);
                                 CarriageVal = float.Parse(Carriage);

                                float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                                if (txtCurrency.Text == "EUR")
                                {
                                    CarriageVal = CarriageVal * rate;
                                   currencysymbol = "€";
                                }
                        }
                                float Total = float.Parse(TxtGrandTotal.Text) + CarriageVal;


                                pdfDoc.Add(table1);

                                htmlparser.Parse(sr);
                                pdfDoc.Add(table);
                                //if (UserRole == "Sales Engineer")
                                pdfDoc.Add(new Phrase("\r\n\r\n Comments : " + txtComments.Text.Trim(), font));

                                pdfDoc.Add(new Phrase("\r\n\r\n                                                                                                                       Carriage Charge : "+ currencysymbol  + CarriageVal, font));
                                if (chkPerforma.Checked == false)
                                {
                                    pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total Excl VAT : "+ currencysymbol+ Total.ToString("0.##"), font));//03-16-20 remove GM% from any output file
                                }
                                else
                                {
                                    if (chkExportCustomer.Checked == true)
                                    {
                                        pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total : "+currencysymbol + Total.ToString("0.##"), font));
                                    }
                                    else
                                    {
                                        float VAT = (20 * Total) / 100;
                                        pdfDoc.Add(new Phrase("\r\n                                                                                                                       VAT : "+currencysymbol + VAT.ToString("0.##"), font));
                                        Total = Total + VAT;
                                        pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total Incl VAT : "+currencysymbol + Total.ToString("0.##"), font));
                                    }
                                }

                                if (chkPerforma.Checked == true)
                                {
                                    pdfDoc.Add(new Phrase("\r\n\r\n Payment can be made by credit/debit card - please phone your card details through to Accounts\r\n", font));
                                    pdfDoc.Add(new Phrase("on 01480-407074 Email:ukaccounts@wattswater.com. You can also pay by bank transfer\r\n", font));
                                    pdfDoc.Add(new Phrase("However we must receive the amount in full and all bank charges are to be paid by your Company.\r\n", fontRed));
                                    pdfDoc.Add(new Phrase("Payments by any method should quote our proforma invoice number above\r\n", font));
                                    pdfDoc.Add(new Phrase("to ensure speedy processing of your order.\r\n", font));
                                    pdfDoc.Add(new Phrase("\r\n OUR BANK DETAILS ARE AS FOLLOWS:\r\n"));
                                    
                            if (txtCurrency.Text != "EUR")
                            {
                                pdfDoc.Add(new Phrase("** This account is for STERLING payments only **\r\n", fontRed));
                                pdfDoc.Add(new Phrase("GBP Payments\r\n", fontRed));
                                pdfDoc.Add(new Phrase("Bank: HSBC UK Bank PLC\r\n", font));
                                pdfDoc.Add(new Phrase("Account Name: Watts Industries UK Limited\r\n", font));
                                pdfDoc.Add(new Phrase("Account Number: 31360841\r\n", font));
                                pdfDoc.Add(new Phrase("Sort Code: 401160\r\n", font));
                                pdfDoc.Add(new Phrase("Account Type: Current\r\n", font));
                                pdfDoc.Add(new Phrase("IBAN: GB67HBUK40116031360841\r\n", font));
                                pdfDoc.Add(new Phrase("IBAN BIC (SWIFT): HBUKGB4B\r\n", font));
                            }
                            else
                            {
                                
                                pdfDoc.Add(new Phrase("\r\nEUR Payments\r\n", fontRed));
                                pdfDoc.Add(new Phrase("Bank: HSBC The Netherlands\r\n", font));
                                pdfDoc.Add(new Phrase("Account Name: Watts Industries UK Limited\r\n", font));
                                pdfDoc.Add(new Phrase("Account Number: 421-038464-015\r\n", font));
                                pdfDoc.Add(new Phrase("Account Type: Current\r\n", font));
                                pdfDoc.Add(new Phrase("IBAN: NL54HSBC2031738348\r\n", font));
                                pdfDoc.Add(new Phrase("IBAN BIC (SWIFT): HSBCNL2A\r\n", font));
                            }

                        }
                                pdfDoc.Add(new Phrase("\r\n\r\n", font));
                                if (chkPerforma.Checked == false)
                                {
                                    pdfDoc.Add(new Phrase("Please send all purchase orders to Wattsuk@wattswater.com\r\n", font));
                                }
                                pdfDoc.Add(new Phrase("Lead Times are based on date of quotation and are subject to change’ \r\n", font));
                                pdfDoc.Add(new Phrase("This is subjected to the standard Watts UK Terms and Conditions as attached \r\n", font));
                                if (chkPerforma.Checked == false)
                                {
                                    pdfDoc.Add(new Phrase("When Placing a Purchase Order could you please include the Quotation Number for our Reference \r\n", font));
                                }
                                // grdQuote.RenderControl(hw);


                                htmlparser.Parse(sr);
                                writer.CloseStream = false;
                                pdfDoc.Close();

                                //byte[] bytes = memoryStream.ToArray();
                                memoryStream.Position = 0;
                                if (chkPerforma.Checked == true)
                                {
                                    string Customer = "";
                                    if (drpCustomer.SelectedItem.Text.Contains("SUNDRY"))
                                    {
                                        string[] strSundryCust = txtCustName.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                        Customer = strSundryCust[0];
                                    }
                                    else
                                    {
                                        Customer = drpCustomer.SelectedItem.Text;
                                    }
                                   
                                    SavePDFFile(@"C:\UK_RPAQuoteTool_Deploy\App_Data\Proformas\" + QuoteNumber + ".pdf", memoryStream);
                                }
                                //create pdf ends
                                smtpClient = new SmtpClient("smtp.watts.com");
                                smtpClient.UseDefaultCredentials = true;
                                string MailTo = txtCustEmail.Text.ToString();

                                string MailTo2 = ConfigurationManager.AppSettings["Email" + drpPreparedBy.SelectedItem.Text];
                                MailMessage mail = new MailMessage("ukquotations@wattswater.com", MailTo);
                                if (chkPerforma.Checked == false)
                                {
                                    mail.Subject = "Watts Industries UK Quote-" + QuoteNumber;
                                }
                                else
                                {
                                    mail.Subject = "Watts Industries UK Proforma Invoice-" + QuoteNumber;
                                }
                                mail.IsBodyHtml = true;
                                if (chkPerforma.Checked == false)
                                {
                                    string str = "Dear Sir/Madam <br/> Please find attached your quotation and a copy of our terms and conditions  <br/><br/><br/>";
                                    str = str + "Please click the link below to take the action. <br/> https://testrpaquotationtool.wattswater.com/CustomerPO.aspx?QuoteNo=" + QuoteNumber;
                                    mail.Body = str;
                                }
                                else
                                {
                                    mail.Body = "Dear Sir/Madam <br/> Please find attached your Pro Forma Invoice along with our Terms and Conditions<br/>Can you please confirm that the  Invoice & Delivery address are correct <br/><br/><br/>";
                                }
                                Attachment data = new Attachment(Server.MapPath("~/App_Data/Watts Uk Terms  Conditions.pdf"));
                                if (chkPerforma.Checked == false)
                                {
                                    mail.Attachments.Add(new Attachment(memoryStream, QuoteNumber + ".pdf"));
                                }
                                else
                                {
                                    mail.Attachments.Add(new Attachment(@"C:\UK_RPAQuoteTool_Deploy\App_Data\Proformas\" + QuoteNumber + ".pdf"));
                                }
                                mail.Attachments.Add(data);
                                if (Directory.Exists(Path.Combine(@"C:\UK_RPAQuoteTool_Deploy\App_Data\AdditonalDocs", QuoteNumber)))
                                {
                                    string[] filePaths = Directory.GetFiles(Path.Combine(@"C:\UK_RPAQuoteTool_Deploy\App_Data\AdditonalDocs", QuoteNumber));
                                    // Get the files that their extension are either pdf of xls. 
                                    var files = filePaths.Where(x => Path.GetExtension(x).Contains(".pdf") ||
                                                                    Path.GetExtension(x).Contains(".xls"));

                                    // Loop through the files enumeration and attach each file in the mail.
                                    foreach (var file in files)
                                    {
                                        var attachment = new Attachment(file);
                                        mail.Attachments.Add(attachment);
                                    }

                                }

                                MailAddress copy = new MailAddress(ConfigurationManager.AppSettings["EmailAlan Fahy"]); //choprad send email to admin 2/4/20

                                mail.Bcc.Add(copy);
                                mail.CC.Add(new MailAddress(MailTo2));
                                mail.CC.Add("rpa@wattswater.com");
                                mail.CC.Add(txtSPEmail.Text);

                                smtpClient.Send(mail);

                                obj.UpdateEmail(QuoteNumber);


                                MailMessage mail1 = new MailMessage("ukquotations@wattswater.com", MailTo2);
                                mail1.Subject = QuoteNumber + " is approved";
                                mail1.Body = "Your Quote " + QuoteNumber + " is Approved and sent to the customer.";
                                MailAddress copy1 = new MailAddress(ConfigurationManager.AppSettings["EmailAlan Fahy"]); //choprad send email to admin 2/4/20
                                mail1.Bcc.Add(copy1);
                                mail1.CC.Add(txtSPEmail.Text);
                                smtpClient.Send(mail1);




                                Response.Redirect("Dashboard.aspx");
                            }





                        

                    
                }
            
                
            }
        }
        

        protected void Import_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            using (XLWorkbook workbook = new XLWorkbook(FileUpload1.PostedFile.InputStream))
            {
                IXLWorksheet sheet = workbook.Worksheet(1);
                
                bool firstRow = true;
                foreach(IXLRow row in sheet.Rows())
                {
                    if (firstRow)
                    {
                        
                        dt.Columns.Add("PartNo");
                        dt.Columns.Add("QTY");
                        firstRow = false;
                    }
                    else
                    {
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                        GridView1.DataSource = dt;
                       // GridView1.DataBind();
                    }
                }
            }
            //Validate the datatable
            DataTable Validateddt = new DataTable();
            DataRow dtRow;
            string DeletedPartno = "";
            Validateddt.Columns.AddRange(new DataColumn[15] {new DataColumn("Product Family"), new DataColumn("ItemNo"), new DataColumn("PartNo"), new DataColumn("Description"), new DataColumn("QTY"), new DataColumn("MOQ"), new DataColumn("LeadTime"), new DataColumn("SafetyStock"), new DataColumn("ListPrice"), new DataColumn("Discount"), new DataColumn("Unit Price"), new DataColumn("AdditionalDiscount"), new DataColumn("Unit Price after Extra Discount"), new DataColumn("Total Price after Extra Discount"), new DataColumn("GM") });
            for (int i=0; i<dt.Rows.Count;i++)
            {
                string partno = dt.Rows[i]["PartNo"].ToString();
                //string qty= dt.Rows[i]["QTY"].ToString();
                //int RowCount = 0;
                DataTable Newdt = new DataTable();
                
                
                // Newdt =obj.ValidatePartNo(partno);
                Newdt = obj.GetItemDetails(partno,"NULL");
                

                DataRow dr = dt.Rows[i];
                if (Newdt.Rows.Count < 1)
                {
                    DataTable dtLineItem = new DataTable();
                    dtLineItem = obj.GetNetItemDetails(partno, string.Empty);
                    if (dtLineItem.Rows.Count > 0)
                    {
                        dtRow = Validateddt.NewRow();
                        dtRow["Product Family"] = "";
                        dtRow["ItemNo"] = dtLineItem.Rows[0]["ItemNo"].ToString();
                        dtRow["PartNo"] = dtLineItem.Rows[0]["LegacyPartNo"].ToString();
                        dtRow["Description"] = dtLineItem.Rows[0]["Description1"].ToString();
                        dtRow["QTY"] = dt.Rows[i]["QTY"].ToString(); 
                        dtRow["MOQ"] = dtLineItem.Rows[0]["MinOrderQty"].ToString();
                        dtRow["LeadTime"] = dtLineItem.Rows[0]["LeadTime"].ToString();
                        dtRow["SafetyStock"] = dtLineItem.Rows[0]["SafetyStock"].ToString();

                        string netprice = string.Empty;
                        float UnitPrice = 0;
                        netprice = obj.GetNetPrice(DrpCustNo.SelectedItem.Text, partno, string.Empty,txtCurrency.Text);

                        if (netprice != string.Empty)
                        {
                            dtRow["ListPrice"] = string.Empty;
                            dtRow["Discount"] = string.Empty;

                            //float Listprice = float.Parse(dtPrice.Rows[0]["ListPrice"].ToString());
                            //float Disc = float.Parse(dtPrice.Rows[0]["DiscountPerc"].ToString());
                            //UnitPrice = Listprice - (Listprice * Disc / 100);
                            UnitPrice = float.Parse(netprice);

                            dtRow["Unit Price"] = UnitPrice.ToString("0.00");

                        }
                        else
                        {
                            DataTable dtPrice = new DataTable();
                            dtPrice = obj.GetPrice(partno, "NULL","GBP");


                            if (dtPrice.Rows.Count != 0)
                            {

                                dtRow["ListPrice"] = dtPrice.Rows[0]["ListPrice"].ToString();

                                DataTable dtDisc = new DataTable();
                                float Disc = 0;
                                dtDisc = obj.GetDiscount(dtLineItem.Rows[0]["ItemNo"].ToString(), DrpCustNo.SelectedItem.Text);
                                if (dtDisc.Rows.Count != 0)
                                {
                                    dtRow["Discount"] = dtDisc.Rows[0]["DiscountPerc"].ToString();
                                    Disc = float.Parse(dtDisc.Rows[0]["DiscountPerc"].ToString());
                                }
                                else
                                {
                                    dtRow["Discount"] = "";
                                    Disc = 0;
                                }
                               

                                float Listprice = float.Parse(dtPrice.Rows[0]["ListPrice"].ToString());
                                
                                UnitPrice = Listprice - (Listprice * Disc / 100);

                                dtRow["Unit Price"] = UnitPrice.ToString("0.00");
                            }

                        }

                        // TextBox txtDisc = (TextBox)currentRow.FindControl("txtDiscount");
                        float AddDiscount = 0;
                        // AddDiscount = float.Parse(txtDisc.Text.ToString().Trim());

                        dtRow["AdditionalDiscount"] = AddDiscount.ToString();
                        string Quantity = dt.Rows[0]["QTY"].ToString();
                        int Qty = 0;
                        float UnitPriceafterExtraDiscount = 0;
                        if (UnitPrice.ToString() != string.Empty)
                        {
                            UnitPriceafterExtraDiscount = UnitPrice;
                            dtRow["Unit Price after Extra Discount"] = UnitPriceafterExtraDiscount.ToString("0.00");
                        }
                        if (Quantity != string.Empty)
                        {
                            Qty = Convert.ToInt32(Quantity);
                        }
                        float TotalPriceafterExtraDiscount = UnitPriceafterExtraDiscount * Qty;
                        dtRow["Total Price after Extra Discount"] = TotalPriceafterExtraDiscount.ToString("0.00");

                        float UnitPriceVal = 0;
                        if (UnitPrice.ToString() != string.Empty)
                        {
                            UnitPriceVal = UnitPrice;
                        }
                        float TotalCost = UnitPriceVal * Qty;


                        float GM = 0;

                        GM = (((TotalPriceafterExtraDiscount - TotalCost) * 100) / TotalPriceafterExtraDiscount);

                        Validateddt.Rows.Add(dtRow);
                    }
                    else
                    {
                        DataTable dtICO = new DataTable();
                        dtICO = obj.GetICOItem(partno, string.Empty);
                        if (dtICO.Rows.Count > 0)
                        {
                            dtRow = Validateddt.NewRow();
                            dtRow["Product Family"] = "WOTH";
                            dtRow["ItemNo"] = string.Empty;
                            dtRow["PartNo"] = partno;
                            dtRow["Description"] = dtICO.Rows[0]["Description"].ToString();
                            dtRow["QTY"] = dt.Rows[i]["QTY"].ToString();
                            dtRow["MOQ"] = string.Empty;
                            dtRow["LeadTime"] = string.Empty;
                            dtRow["SafetyStock"] = string.Empty;

                            string ICOPrice = string.Empty;
                            string ICODisc = string.Empty;
                            float UnitPrice = 0;


                            ICOPrice = dtICO.Rows[0]["Price"].ToString();
                            ICODisc = obj.GetICODiscount(DrpCustNo.SelectedItem.Text.ToString());
                            float Listprice = float.Parse(ICOPrice);
                            float Disc = 0;
                            if (ICODisc == "")
                            {
                                Disc = 0;
                            }
                            else
                            {
                                Disc = float.Parse(ICODisc);
                            }
                            

                            dtRow["ListPrice"] = ICOPrice;
                            dtRow["Discount"] = ICODisc;
                            UnitPrice = Listprice - (Listprice * Disc / 100);
                            UnitPrice = float.Parse(ICOPrice);

                            dtRow["Unit Price"] = UnitPrice.ToString("0.00");

                            dtRow["AdditionalDiscount"] = "0";


                            int Quantity = 0;
                            float UnitPriceafterExtraDiscount = 0;
                            float TotalPriceafterExtraDiscount = 0;
                            string txtQty = dt.Rows[0]["QTY"].ToString().Trim();
                            string Adddisc = "0";
                            float AddDiscount = 0;
                            // string UnitPrice = ConvertedString.ToString("0.00");
                            if (Adddisc != string.Empty)
                            {
                                AddDiscount = float.Parse(Adddisc);
                            }

                            if (UnitPrice.ToString() != string.Empty)
                            {
                                UnitPriceafterExtraDiscount = UnitPrice;
                                dtRow["Unit Price after Extra Discount"] = UnitPriceafterExtraDiscount.ToString("0.00");
                            }
                            if (txtQty != string.Empty)
                            {
                                Quantity = Convert.ToInt32(txtQty);
                            }
                            TotalPriceafterExtraDiscount = UnitPriceafterExtraDiscount * Quantity;
                            dtRow["Total Price after Extra Discount"] = TotalPriceafterExtraDiscount.ToString("0.00");

                            //Calculate GM%
                            //string UnitPrice = dt.Rows[0]["Unit Price"].ToString();
                            float UnitPriceVal = 0;
                            if (UnitPrice.ToString() != string.Empty)
                            {
                                UnitPriceVal = UnitPrice;
                            }
                            float TotalCost = UnitPriceVal * Quantity;


                            float GM = 0;

                            GM = (((TotalPriceafterExtraDiscount - TotalCost) * 100) / TotalPriceafterExtraDiscount);

                            Validateddt.Rows.Add(dtRow);



                        }
                        else
                        {
                            DeletedPartno = DeletedPartno + "  " + partno;
                            dr.Delete();
                            i = i - 1;
                        }

                    }
                }
                else
                {
                    dtRow = Validateddt.NewRow();
                    dtRow["Product Family"] = Newdt.Rows[0]["ItemDiscountGrp"].ToString();
                    dtRow["ItemNo"] = Newdt.Rows[0]["ItemNo"].ToString();
                    dtRow["PartNo"] = Newdt.Rows[0]["LegacyPartNo"].ToString();
                    dtRow["Description"] = Newdt.Rows[0]["Description1"].ToString();
                    dtRow["QTY"] = dt.Rows[i]["QTY"].ToString();
                    dtRow["MOQ"] = Newdt.Rows[0]["MinOrderQty"].ToString();
                    dtRow["LeadTime"] = Newdt.Rows[0]["LeadTime"].ToString();
                    dtRow["SafetyStock"] = Newdt.Rows[0]["SafetyStock"].ToString();

                    string netprice = string.Empty;
                    float UnitPrice = 0;
                    netprice = obj.GetNetPrice(DrpCustNo.SelectedItem.Text.ToString(), partno, string.Empty,txtCurrency.Text);

                    if (netprice != string.Empty)
                    {
                        dtRow["ListPrice"] = string.Empty;
                        dtRow["Discount"] = string.Empty;

                        //float Listprice = float.Parse(dtPrice.Rows[0]["ListPrice"].ToString());
                        //float Disc = float.Parse(dtPrice.Rows[0]["DiscountPerc"].ToString());
                        //UnitPrice = Listprice - (Listprice * Disc / 100);
                        UnitPrice = float.Parse(netprice);

                        dtRow["Unit Price"] = UnitPrice.ToString("0.00");

                    }
                    else
                    {
                        DataTable dtPrice = new DataTable();
                        dtPrice = obj.GetPrice(partno, "NULL","GBP");


                        if (dtPrice.Rows.Count != 0)
                        {

                            dtRow["ListPrice"] = dtPrice.Rows[0]["ListPrice"].ToString();

                            DataTable dtDisc = new DataTable();
                            float Disc = 0;
                            dtDisc = obj.GetDiscount(Newdt.Rows[0]["ItemNo"].ToString(), DrpCustNo.SelectedItem.Text);
                            if (dtDisc.Rows.Count != 0)
                            {
                                dtRow["Discount"] = dtDisc.Rows[0]["DiscountPerc"].ToString();
                                Disc = float.Parse(dtDisc.Rows[0]["DiscountPerc"].ToString());
                            }
                            else
                            {
                                dtRow["Discount"] = "";
                                Disc = 0;
                            }
                          

                            float Listprice = float.Parse(dtPrice.Rows[0]["ListPrice"].ToString());
                           
                            UnitPrice = Listprice - (Listprice * Disc / 100);

                            dtRow["Unit Price"] = UnitPrice.ToString("0.00");
                        }

                    }
                


                    //dtRow["Unit Price"] = ConvertedString.ToString("0.00");
                   // dtRow["Unit Price"] = Newdt.Rows[0]["UnitPrice"].ToString();
                   dtRow["AdditionalDiscount"] = "0";


                    int Quantity = 0;
                    float UnitPriceafterExtraDiscount = 0;
                    float TotalPriceafterExtraDiscount = 0;
                    string txtQty = dt.Rows[0]["QTY"].ToString().Trim();
                    string Adddisc = "0";
                    float AddDiscount = 0;
                   // string UnitPrice = ConvertedString.ToString("0.00");
                    if (Adddisc != string.Empty)
                    {
                        AddDiscount = float.Parse(Adddisc);
                    }

                    if (UnitPrice.ToString() != string.Empty)
                    {
                        UnitPriceafterExtraDiscount = UnitPrice;
                        dtRow["Unit Price after Extra Discount"] = UnitPriceafterExtraDiscount.ToString("0.00");
                    }
                    if (txtQty != string.Empty)
                    {
                        Quantity = Convert.ToInt32(txtQty);
                    }
                    TotalPriceafterExtraDiscount = UnitPriceafterExtraDiscount * Quantity;
                    dtRow["Total Price after Extra Discount"] = TotalPriceafterExtraDiscount.ToString("0.00");

                    //Calculate GM%
                    //string UnitPrice = dt.Rows[0]["Unit Price"].ToString();
                    float UnitPriceVal = 0;
                    if (UnitPrice.ToString() != string.Empty)
                    {
                        UnitPriceVal = UnitPrice;
                    }
                    float TotalCost = UnitPriceVal * Quantity;
                   
                    
                    float GM = 0;

                    GM = (((TotalPriceafterExtraDiscount - TotalCost) * 100) / TotalPriceafterExtraDiscount);
                    
                    Validateddt.Rows.Add(dtRow);
                }
               
            }
            //dt.AcceptChanges();
            if (DeletedPartno != string.Empty)
            {
                string ErrorMessage = "Invalid Part Nos: " + DeletedPartno;
                lblMessage.Text = ErrorMessage;
            }
            
            GridView1.DataSource = Validateddt;
            GridView1.DataBind();
            
            float GrandTotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string TotalPrice = Validateddt.Rows[i]["Total Price after Extra Discount"].ToString();
                if (TotalPrice != string.Empty)
                {
                    GrandTotal = GrandTotal + float.Parse(TotalPrice);
                }

            }

            float CarriageVal = 0;
           
            TxtGrandTotal.Text =GrandTotal.ToString("0.00");

        }

        protected void drpPreparedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            DataTable dtSEEMailPhone = obj.GetSEEmailPhone(drpPreparedBy.SelectedItem.Text);
            txtSEEMail.Text = dtSEEMailPhone.Rows[0][0].ToString();
            txtSEPhone.Text = dtSEEMailPhone.Rows[0][1].ToString();
           

        }

        protected void drpCarriageCharges_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drpCarriageCharges.SelectedItem.Text.Contains("Add New") == false)
            {
                txtCarriage.Visible = false;
                string temp = drpCarriageCharges.SelectedItem.Text.Substring(drpCarriageCharges.SelectedItem.Text.IndexOf("£"));
                string Carriage = temp.Substring(1, temp.IndexOf("/") - 1);
                float CarriageVal = float.Parse(Carriage);
                float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                if (txtCurrency.Text == "EUR")
                {
                    CarriageVal = CarriageVal * rate;
                }
            }
            else
            {
                txtCarriage.Visible = true;
            }
          
            if (TxtGrandTotal.Text != "0" && TxtGrandTotal.Text != string.Empty)
            {
                TxtGrandTotal.Text = float.Parse(TxtGrandTotal.Text).ToString("0.00");
                float TotalOverallCost = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {


                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                    TextBox Qty = (TextBox)row.FindControl("txtQty");

                    if (PartNum.Text == string.Empty)
                    {
                        continue;
                    }
                    string CostPrice = obj.getCostPrice(PartNum.Text);
                    
                    if (CostPrice == "")
                    {
                        DataTable dtICO = new DataTable();
                        dtICO = obj.GetICOItem(PartNum.Text, string.Empty);
                        CostPrice = dtICO.Rows[0]["CostPrice"].ToString();
                    }
                    
                    TotalOverallCost = TotalOverallCost + (float.Parse(CostPrice) * Convert.ToInt32(Qty.Text));



                }

                float TotalGM = (((float.Parse(TxtGrandTotal.Text) - TotalOverallCost) * 100) / float.Parse(TxtGrandTotal.Text));
                txtTotalGM.Text = TotalGM.ToString("0.00"); 


            }


            

        }

        protected void drpCustBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtSP = new DataTable();
            if(drpCustBranch.SelectedItem.Value !="0")
            {
                if (drpCustBranch.SelectedItem.Text == "SUNDRY BRANCH")
                {
                    lblSundryBranch.Visible = true;
                    txtSundryBranch.Visible = true;
                    txtSalesPerson.Enabled = true;
                    txtSPEmail.Enabled = true;
                    txtSPPhone.Enabled = true;
                }
                else
                {
                    dtSP = obj.GetSalesPersonfromBranch(drpCustBranch.SelectedItem.Text, DrpCustNo.SelectedItem.Text);
                    txtSalesPerson.Text = dtSP.Rows[0][0].ToString();
                    txtSPEmail.Text = dtSP.Rows[0][1].ToString();
                    txtSPPhone.Text = dtSP.Rows[0][2].ToString();
                }

              

            }
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
                dr["Unit Price"] = string.Empty;
                dr["QTY"] = "1";
                dr["AdditionalDiscount"] = "0";
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
                float TotalOverallCost = 0;
                float TotalforGM = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string TotalPrice = dt.Rows[i]["Total Price after Extra Discount"].ToString();
                    if (TotalPrice != string.Empty)
                    {
                        GrandTotal = GrandTotal + float.Parse(TotalPrice);
                        if (dt.Rows[i]["PartNo"].ToString().ToUpper().Contains("SUNDRY") == false)
                        {
                            TotalforGM = TotalforGM + float.Parse(TotalPrice);
                        }
                        TotalOverallCost = TotalOverallCost + float.Parse(dt.Rows[i]["CostPrice"].ToString().Trim()) * Convert.ToInt32(dt.Rows[i]["Qty"].ToString());

                    }

                }
                TxtGrandTotal.Text = GrandTotal.ToString("0,0.00");
                

                float TotalGM = 0;

                // if (IncoTerms == "Ex-Factory")
                //{
                TotalGM = (((GrandTotal - TotalOverallCost) * 100) / GrandTotal);
                txtTotalGM.Text = TotalGM.ToString("0,0.00");


            }
        }

        protected void chkPerforma_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPerforma.Checked == true)
            {
                if (drpCustomer.SelectedItem.Text.Contains("SUNDRY"))
                {
                    lblSundryBranch.Visible = true;
                    txtSundryBranch.Visible = true;
                    lblSundryBranch.Text = "Delivery Address";
                }
                else
                {
                    lblSundryBranch.Visible = false;
                    txtSundryBranch.Visible = false;
                }
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
         private static Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private static Microsoft.Office.Interop.Excel.Sheets mWorkSheets;
        private static Microsoft.Office.Interop.Excel.Worksheet mWSheet1;
        private static Microsoft.Office.Interop.Excel.Application oXL;

        protected void chkExportCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExportCustomer.Checked)
            {
                lblVAT.Visible = true;
                txtVAT.Visible = true;
            }
            else
            {
                lblVAT.Visible = false;
                txtVAT.Visible = false;
            }

        }

        protected void txtCarriage_TextChanged(object sender, EventArgs e)
        {
            


            if (TxtGrandTotal.Text != "0" && TxtGrandTotal.Text != string.Empty)
            {
                TxtGrandTotal.Text = float.Parse(TxtGrandTotal.Text).ToString("0.00");
                float TotalOverallCost = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {


                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                    TextBox Qty = (TextBox)row.FindControl("txtQty");

                    if (PartNum.Text == string.Empty)
                    {
                        continue;
                    }
                    string CostPrice = obj.getCostPrice(PartNum.Text);

                    if (CostPrice == "")
                    {
                        DataTable dtICO = new DataTable();
                        dtICO = obj.GetICOItem(PartNum.Text, string.Empty);
                        CostPrice = dtICO.Rows[0]["CostPrice"].ToString();
                    }

                    TotalOverallCost = TotalOverallCost + (float.Parse(CostPrice) * Convert.ToInt32(Qty.Text));



                }

                float TotalGM = (((float.Parse(TxtGrandTotal.Text) - TotalOverallCost) * 100) / float.Parse(TxtGrandTotal.Text));
                txtTotalGM.Text = TotalGM.ToString("0.00");


            }
        }
    }
}