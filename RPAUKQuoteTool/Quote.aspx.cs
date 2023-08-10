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
using System.Net.Mail;




namespace RPADubaiQuoteTool
{
    public partial class Quote : System.Web.UI.Page
    {

        QuoteBAL obj = new QuoteBAL();
        CreateQuoteBAL objCreateQuoteBAL = new CreateQuoteBAL();
        Boolean blnSundryItem = false;
        string UserRole = "";
        string UserName = "";
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserRole"] = Request["UserRole"];
            Session["UserName"] = Request["UserName"];
            UserRole = (string)Session["UserRole"];
            UserName = (string)Session["UserName"];



            if (!IsPostBack)
            {

                DataTable dtCarriage = new DataTable();
                dtCarriage = objCreateQuoteBAL.GetCarriageCharge();
                drpCarriageCharges.DataSource = dtCarriage;
                drpCarriageCharges.DataValueField = "Charge";
                drpCarriageCharges.DataBind();
                drpCarriageCharges.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));


                string status = LoadQuoteDetails();
                DataTable dtApprovals = new DataTable();
                string QuoteNumber = txtQuoteNum.Text;
                dtApprovals = obj.GetApprovaldata(QuoteNumber);
                string Approval1 = dtApprovals.Rows[0]["approval1"].ToString();
                //string Approval2 = dtApprovals.Rows[0]["approval2"].ToString();
                string Approval3 = dtApprovals.Rows[0]["approval3"].ToString();
                drpStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                if (UserRole == "Admin")
                {
                    if (status == "Approved")
                    {
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        btnUpdateVersion.Visible = true;
                        btnConfirmQuote.Visible = true;
                        drpStatus.Visible = true;
                        Label1.Visible = true;
                        txtUpdateStatusCmt.Visible = true;
                        btnUpdateVersion.Visible = true;
                        btnConfirmQuote.Visible = true;
                        btnExportToExcel.Visible = true;
                        btnExportToPDF.Visible = true;

                        txtComments.Visible = true;
                        //drpGroup.Enabled = false;
                        // DrpGroupName.Enabled = false;
                        // txtGroupDiscount.Enabled = false;
                        //  btnAddDiscount.Enabled = false;
                        btnAddNewItem.Enabled = false;
                        txtProjectName.Enabled = false;
                        txtOppurtunityId.Enabled = false;
                        //txtPaymentTerms.Enabled = false;
                        // txtPartialDelivery.Enabled = false;
                        txtCurrency.Enabled = false;
                        txtPreparedBy.Enabled = false;
                        txtSEEMail.Enabled = false;
                        txtSEPhone.Enabled = false;
                        txtSalesPerson.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSPPhone.Enabled = false;
                        txtCreationdate.Enabled = false;
                        txtExpirationDate.Enabled = false;
                        drpCarriageCharges.Enabled = false;
                        txtVersion.Enabled = false;
                        grdQuote.Enabled = false;

                    }
                    else if (status == "Pending Approval")
                    {
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        btnExportToExcel.Visible = true;
                        btnExportToPDF.Visible = false;
                        txtComments.Visible = true;
                        btnConfirmQuote.Visible = false;
                        drpStatus.Visible = false;
                        Label1.Visible = false;
                        txtUpdateStatusCmt.Visible = false;
                        grdQuote.Columns[15].Visible = true;
                        txtTotalGM.Visible = true;   //change by divya on 2/27/20
                        lblTotalGM.Visible = true;
                        if ((UserName == "Kirsty Anderson") || (UserRole == "Admin"))
                        {
                            btnDelete.Visible = true;
                        }
                        else
                        {
                            btnDelete.Visible = false;
                        }
                    }
                    else if (status == "Reject")
                    {
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        btnUpdateVersion.Visible = false;
                        btnConfirmQuote.Visible = false;
                        drpStatus.Visible = false;
                        Label1.Visible = false;
                        txtUpdateStatusCmt.Visible = false;
                        // btnAddDiscount.Enabled = false;
                        btnAddNewItem.Enabled = false;
                        btnExportToExcel.Visible = true;
                        btnExportToPDF.Visible = false;

                        txtProjectName.Enabled = false;
                        txtOppurtunityId.Enabled = false;
                        // txtPaymentTerms.Enabled = false;
                        // txtPartialDelivery.Enabled = false;
                        txtCurrency.Enabled = false;
                        txtPreparedBy.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSEPhone.Enabled = false;
                        txtSalesPerson.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSPPhone.Enabled = false;
                        txtCreationdate.Enabled = false;
                        txtExpirationDate.Enabled = false;
                        drpCarriageCharges.Enabled = false;
                        txtVersion.Enabled = false;

                        txtComments.Visible = true;
                        //drpGroup.Enabled = false;
                        //DrpGroupName.Enabled = false;
                        //txtGroupDiscount.Enabled = false;                        
                        grdQuote.Enabled = false;

                    }
                    else if (status == "Draft" || status == "New" || status == "Updated to New Version")
                    {
                        btnUpdateVersion.Visible = false;
                        btnConfirmQuote.Visible = false;
                        drpStatus.Visible = false;
                        Label1.Visible = false;
                        txtUpdateStatusCmt.Visible = false;
                        btnExportToExcel.Visible = true;
                        btnExportToPDF.Visible = false;
                        btnSubmit.Visible = false;
                        btnSave.Visible = false;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        txtComments.Visible = true;

                        btnAddNewItem.Enabled = false;
                        txtProjectName.Enabled = false;
                        txtOppurtunityId.Enabled = false;

                        txtCurrency.Enabled = false;
                        txtPreparedBy.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSEPhone.Enabled = false;
                        txtSalesPerson.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSPPhone.Enabled = false;
                        txtCreationdate.Enabled = false;
                        txtExpirationDate.Enabled = false;
                        drpCarriageCharges.Enabled = true;


                        txtVersion.Enabled = false;
                        grdQuote.Enabled = false;
                        if ((UserName == "Kirsty Anderson") || (UserRole == "Admin"))
                        {
                            btnDelete.Visible = true;
                        }
                        else
                        {
                            btnDelete.Visible = false;
                        }
                    }
                    else if (status == "INACTIVE")
                    {
                        btnUpdateVersion.Visible = false;
                        btnConfirmQuote.Visible = true;
                        drpStatus.Visible = true;
                        Label1.Visible = true;
                        txtUpdateStatusCmt.Visible = true;
                        btnExportToExcel.Visible = true;
                        btnExportToPDF.Visible = false;
                        btnSubmit.Visible = false;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        txtComments.Visible = false;


                        btnAddNewItem.Enabled = false;
                        txtProjectName.Enabled = false;
                        txtOppurtunityId.Enabled = false;

                        txtCurrency.Enabled = false;
                        txtPreparedBy.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSEPhone.Enabled = false;
                        txtSalesPerson.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSPPhone.Enabled = false;
                        txtCreationdate.Enabled = false;
                        txtExpirationDate.Enabled = false;
                        drpCarriageCharges.Enabled = false;
                        txtVersion.Enabled = false;
                        grdQuote.Enabled = false;
                    }
                    else
                    {
                        btnUpdateVersion.Visible = false;
                        btnConfirmQuote.Visible = false;
                        drpStatus.Visible = false;
                        Label1.Visible = false;
                        txtUpdateStatusCmt.Visible = false;
                        btnExportToExcel.Visible = true;
                        btnExportToPDF.Visible = false;
                        btnSubmit.Visible = false;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        txtComments.Visible = true;


                        btnAddNewItem.Enabled = true;
                        txtProjectName.Enabled = true;
                        txtOppurtunityId.Enabled = true;

                        txtCurrency.Enabled = false;
                        txtPreparedBy.Enabled = true;
                        txtSPEmail.Enabled = true;
                        txtSEPhone.Enabled = true;
                        txtSalesPerson.Enabled = true;
                        txtSPEmail.Enabled = true;
                        txtSPPhone.Enabled = true;
                        txtCreationdate.Enabled = true;
                        txtExpirationDate.Enabled = true;
                        drpCarriageCharges.Enabled = true;
                        txtVersion.Enabled = true;
                        grdQuote.Enabled = true;
                    }
                }
                else if (UserRole == "Activity Manager" || UserRole == "Regional Marketing Director" || UserRole == "Finance controller") //04-07-20 added new role finance controller
                {
                    if (status != "New" || status == "Updated to New Version")
                    {
                        txtProjectName.Enabled = false;
                        txtOppurtunityId.Enabled = false;
                        // txtPaymentTerms.Enabled = false;
                        // txtPartialDelivery.Enabled = false;
                        txtCurrency.Enabled = false;
                        txtPreparedBy.Enabled = false;
                        txtCreationdate.Enabled = false;
                        txtExpirationDate.Enabled = false;
                        drpCarriageCharges.Enabled = false;
                        txtVersion.Enabled = false;

                        txtComments.Visible = true;
                        //drpGroup.Enabled = false;
                        //DrpGroupName.Enabled = false;
                        //txtGroupDiscount.Enabled = false;
                        //btnAddDiscount.Enabled = false;
                        btnAddNewItem.Enabled = false;
                        grdQuote.Enabled = false;

                        btnExportToExcel.Visible = true;
                        btnExportToPDF.Visible = false;

                    }

                    btnExportToExcel.Visible = true;
                    txtComments.Visible = true;
                    txtComments.Enabled = false;
                    //btnExportToPDF.Visible = true;

                }
                else if (status == "Reject")
                {
                    txtProjectName.Enabled = false;
                    txtOppurtunityId.Enabled = false;
                    //txtPaymentTerms.Enabled = false;
                    // txtPartialDelivery.Enabled = false;
                    txtCurrency.Enabled = false;
                    txtPreparedBy.Enabled = false;
                    txtSPEmail.Enabled = false;
                    txtSEPhone.Enabled = false;
                    txtSalesPerson.Enabled = false;
                    txtSPEmail.Enabled = false;
                    txtSPPhone.Enabled = false;
                    txtCreationdate.Enabled = false;
                    txtExpirationDate.Enabled = false;
                    drpCarriageCharges.Enabled = false;
                    txtVersion.Enabled = false;

                    txtComments.Visible = true;
                    //drpGroup.Enabled = false;
                    // DrpGroupName.Enabled = false;
                    // txtGroupDiscount.Enabled = false;
                    // btnAddDiscount.Enabled = false;
                    btnAddNewItem.Enabled = false;
                    grdQuote.Enabled = false;

                }
                else
                {
                    if (status == "Pending Approval")

                    {
                        if ((UserName == "Kirsty Anderson") || (UserRole == "Admin"))
                        {
                            btnDelete.Visible = true;
                        }
                        else
                        {
                            btnDelete.Visible = false;
                        }
                        if (Approval1 == "Pending" && UserRole == "Sales Manager")
                        {
                            btnApprove.Visible = true;
                            btnReject.Visible = true;
                            btnExportToExcel.Visible = true;
                            btnExportToPDF.Visible = false;
                            txtComments.Visible = true;
                            btnConfirmQuote.Visible = false;
                            drpStatus.Visible = false;
                            Label1.Visible = false;
                            txtUpdateStatusCmt.Visible = false;
                            grdQuote.Columns[15].Visible = true;
                            grdQuote.Enabled = true;
                            txtTotalGM.Visible = true;
                            lblTotalGM.Visible = true;
                           
                        }
                        else if (Approval1 == "Approved" && Approval3 == "Pending" && UserRole == "GM") //03-11-20 removed BDM check
                        {
                            btnApprove.Visible = true;
                            btnReject.Visible = true;
                            btnExportToExcel.Visible = true;
                            btnExportToPDF.Visible = false;
                            txtComments.Visible = true;
                            grdQuote.Columns[15].Visible = true;
                            btnConfirmQuote.Visible = false;
                            drpStatus.Visible = false;
                            Label1.Visible = false;
                            txtUpdateStatusCmt.Visible = false;
                            btnExportToExcel.Visible = true;
                            txtTotalGM.Visible = true;   //change by divya on 2/27/20
                            lblTotalGM.Visible = true;
                        }

                        else
                        {

                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                            btnExportToExcel.Visible = false;
                            btnExportToPDF.Visible = false;
                            txtComments.Visible = true;
                            grdQuote.Columns[15].Visible = true;
                            txtTotalGM.Visible = true;   //change by divya on 2/27/20
                            lblTotalGM.Visible = true;

                            txtProjectName.Enabled = false;
                            txtOppurtunityId.Enabled = false;
                            // txtPaymentTerms.Enabled = false;
                            // txtPartialDelivery.Enabled = false;
                            txtCurrency.Enabled = false;
                            txtPreparedBy.Enabled = false;
                            txtSPEmail.Enabled = false;
                            txtSEPhone.Enabled = false;
                            txtSalesPerson.Enabled = false;
                            txtSPEmail.Enabled = false;
                            txtSPPhone.Enabled = false;
                            txtCreationdate.Enabled = false;
                            txtExpirationDate.Enabled = false;
                            drpCarriageCharges.Enabled = false;
                            txtVersion.Enabled = false;

                            //drpGroup.Enabled = false;
                            // DrpGroupName.Enabled = false;
                            // txtGroupDiscount.Enabled = false;
                            // btnAddDiscount.Enabled = false;
                            btnAddNewItem.Enabled = false;
                            grdQuote.Enabled = false;
                            txtComments.Visible = true;

                        }

                    }
                    else if (status == "Approved")
                    {
                        if (UserRole == "Sales Engineer")
                        {
                            btnUpdateVersion.Visible = true;
                            btnConfirmQuote.Visible = true;
                            drpStatus.Visible = true;
                            Label1.Visible = true;
                            txtUpdateStatusCmt.Visible = true;
                            btnExportToExcel.Visible = true;
                            btnExportToPDF.Visible = true;
                            btnSubmit.Visible = false;
                            grdQuote.Columns[15].Visible = true;
                            txtTotalGM.Visible = false;
                            lblTotalGM.Visible = false;

                            txtComments.Visible = true;

                            btnAddNewItem.Enabled = true;
                            txtProjectName.Enabled = false;
                            txtOppurtunityId.Enabled = false;

                            txtCurrency.Enabled = false;
                            txtPreparedBy.Enabled = false;
                            txtSPEmail.Enabled = false;
                            txtSEPhone.Enabled = false;
                            txtSalesPerson.Enabled = false;
                            txtSPEmail.Enabled = false;
                            txtSPPhone.Enabled = false;
                            txtCreationdate.Enabled = false;
                            txtExpirationDate.Enabled = false;
                            drpCarriageCharges.Enabled = false;
                            txtVersion.Enabled = false;
                            grdQuote.Enabled = true;

                        }
                        else if (UserRole == "Sales Manager") //03-11-20 made change to show GM% for SM
                        {
                            btnUpdateVersion.Visible = true;
                            btnConfirmQuote.Visible = true;
                            drpStatus.Visible = true;
                            Label1.Visible = true;
                            txtUpdateStatusCmt.Visible = true;
                            btnExportToExcel.Visible = true;
                            btnExportToPDF.Visible = true;
                            btnSubmit.Visible = false;
                            grdQuote.Columns[15].Visible = true;
                            txtTotalGM.Visible = true;
                            lblTotalGM.Visible = true;

                            txtComments.Visible = true;

                            btnAddNewItem.Enabled = true;
                            txtProjectName.Enabled = false;
                            txtOppurtunityId.Enabled = false;

                            txtCurrency.Enabled = false;
                            txtPreparedBy.Enabled = false;
                            txtSPEmail.Enabled = false;
                            txtSEPhone.Enabled = false;
                            txtSalesPerson.Enabled = false;
                            txtSPEmail.Enabled = false;
                            txtSPPhone.Enabled = false;
                            txtCreationdate.Enabled = false;
                            txtExpirationDate.Enabled = false;
                            drpCarriageCharges.Enabled = false;
                            txtVersion.Enabled = false;
                            grdQuote.Enabled = true;
                        }

                        else
                        {
                            btnUpdateVersion.Visible = false;
                            btnConfirmQuote.Visible = false;
                            drpStatus.Visible = false;
                            Label1.Visible = false;
                            txtUpdateStatusCmt.Visible = false;
                            btnExportToExcel.Visible = false;
                            btnExportToPDF.Visible = true; //per requirement received by Alan on 6/3/22
                            btnSubmit.Visible = false;

                            txtComments.Visible = true;

                            btnAddNewItem.Enabled = false;
                            txtProjectName.Enabled = false;
                            txtOppurtunityId.Enabled = false;

                            txtCurrency.Enabled = false;
                            txtPreparedBy.Enabled = false;
                            txtSPEmail.Enabled = false;
                            txtSEPhone.Enabled = false;
                            txtSalesPerson.Enabled = false;
                            txtSPEmail.Enabled = false;
                            txtSPPhone.Enabled = false;
                            txtCreationdate.Enabled = false;
                            txtExpirationDate.Enabled = false;
                            drpCarriageCharges.Enabled = false;
                            txtVersion.Enabled = false;
                            grdQuote.Enabled = false;
                        }
                    }
                    else if (status == "New" || status == "Updated to New Version")
                    {
                        //if (UserRole == "Sales Engineer")
                        //{
                        btnUpdateVersion.Visible = false;
                        btnConfirmQuote.Visible = false;
                        drpStatus.Visible = false;
                        Label1.Visible = false;
                        txtUpdateStatusCmt.Visible = false;
                        btnExportToExcel.Visible = false;
                        btnExportToPDF.Visible = false;
                        btnSubmit.Visible = true;
                        btnSave.Visible = true;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;

                        txtComments.Visible = true;
                        // drpGroup.Enabled = true;
                        // DrpGroupName.Enabled = true;
                        // txtGroupDiscount.Enabled = true;
                        // btnAddDiscount.Enabled = true;
                        btnAddNewItem.Enabled = true;
                        txtProjectName.Enabled = true;
                        txtOppurtunityId.Enabled = true;
                        //txtPaymentTerms.Enabled = true;
                        // txtPartialDelivery.Enabled = true;
                        txtCurrency.Enabled = false;
                        txtPreparedBy.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSEPhone.Enabled = false;
                        txtSalesPerson.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSPPhone.Enabled = false;
                        txtCreationdate.Enabled = true;
                        txtExpirationDate.Enabled = true;
                        drpCarriageCharges.Enabled = true;
                        txtVersion.Enabled = true;
                        grdQuote.Enabled = true;
                        if ((UserName == "Kirsty Anderson") || (UserRole == "Admin"))
                        {
                            btnDelete.Visible = true;
                        }
                        else
                        {
                            btnDelete.Visible = false;
                        }

                        //}
                    }
                    else if (status == "Confirmed")
                    {
                        txtProjectName.Enabled = false;
                        txtOppurtunityId.Enabled = false;
                        //txtPaymentTerms.Enabled = false;
                        //txtPartialDelivery.Enabled = false;
                        txtCurrency.Enabled = false;
                        txtPreparedBy.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSEPhone.Enabled = false;
                        txtSalesPerson.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSPPhone.Enabled = false;
                        txtCreationdate.Enabled = false;
                        txtExpirationDate.Enabled = false;
                        drpCarriageCharges.Enabled = false;
                        txtVersion.Enabled = false;
                        txtComments.Visible = true;

                        //drpGroup.Enabled = false;
                        //DrpGroupName.Enabled = false;
                        //txtGroupDiscount.Enabled = false;
                        //btnAddDiscount.Enabled = false;
                        btnAddNewItem.Enabled = false;
                        grdQuote.Enabled = false;
                        btnExportToExcel.Visible = true;
                        btnExportToPDF.Visible = true;  //per requirement received by Alan on 6/3/22
                    }
                    else if (status == "Draft" && UserName != txtPreparedBy.Text)
                    {
                        txtProjectName.Enabled = false;
                        txtOppurtunityId.Enabled = false;
                        txtCurrency.Enabled = false;
                        txtPreparedBy.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSEPhone.Enabled = false;
                        txtSalesPerson.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSPPhone.Enabled = false;
                        txtCreationdate.Enabled = false;
                        txtExpirationDate.Enabled = false;
                        drpCarriageCharges.Enabled = false;
                        txtVersion.Enabled = false;
                        txtComments.Visible = false;
                        btnAddNewItem.Enabled = false;
                        grdQuote.Enabled = false;
                        grdQuote.Columns[15].Visible = false;
                        txtTotalGM.Visible = false;   //change by divya on 2/27/20
                        lblTotalGM.Visible = false;

                        btnUpdateVersion.Visible = true;
                        btnConfirmQuote.Visible = true;
                        drpStatus.Visible = true;
                        Label1.Visible = true;
                        txtUpdateStatusCmt.Visible = true;
                        btnExportToExcel.Visible = false;
                        btnExportToPDF.Visible = true;
                        btnSubmit.Visible = false;
                        btnSave.Visible = false;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                        if ((UserName == "Kirsty Anderson") || (UserRole == "Admin"))
                        {
                            btnDelete.Visible = true;
                        }
                        else
                        {
                            btnDelete.Visible = false;
                        }
                    }
                    else if (status == "Draft")
                    {
                        txtProjectName.Enabled = true;
                        txtOppurtunityId.Enabled = true;
                        txtCurrency.Enabled = false;
                        txtPreparedBy.Enabled = true;
                        txtSPEmail.Enabled = true;
                        txtSEPhone.Enabled = true;
                        txtSalesPerson.Enabled = true;
                        txtSPEmail.Enabled = true;
                        txtSPPhone.Enabled = true;
                        txtCreationdate.Enabled = true;
                        txtExpirationDate.Enabled = true;
                        if ((UserName == "Kirsty Anderson") || (UserRole == "Admin"))
                        {
                            btnDelete.Visible = true;
                        }
                        else
                        {
                            btnDelete.Visible = false;
                        }


                        drpCarriageCharges.Enabled = true;

                        txtVersion.Enabled = true;
                        txtComments.Visible = true;
                        btnAddNewItem.Enabled = true;
                        grdQuote.Enabled = true;
                        grdQuote.Columns[15].Visible = true;
                        txtTotalGM.Visible = true;   //change by divya on 2/27/20
                        lblTotalGM.Visible = true;

                        btnUpdateVersion.Visible = false;
                        btnConfirmQuote.Visible = false;
                        drpStatus.Visible = false;
                        Label1.Visible = false;
                        txtUpdateStatusCmt.Visible = false;
                        btnExportToExcel.Visible = true;
                        btnExportToPDF.Visible = false;
                        btnSubmit.Visible = true;
                        btnSave.Visible = true;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                    }
                    else if (status == "INACTIVE")
                    {
                        btnUpdateVersion.Visible = false;
                        btnConfirmQuote.Visible = true;
                        drpStatus.Visible = true;
                        Label1.Visible = true;
                        txtUpdateStatusCmt.Visible = true;
                        btnExportToExcel.Visible = true;
                        btnExportToPDF.Visible = false;
                        btnSubmit.Visible = false;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        txtComments.Visible = false;


                        btnAddNewItem.Enabled = false;
                        txtProjectName.Enabled = false;
                        txtOppurtunityId.Enabled = false;

                        txtCurrency.Enabled = false;
                        txtPreparedBy.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSEPhone.Enabled = false;
                        txtSalesPerson.Enabled = false;
                        txtSPEmail.Enabled = false;
                        txtSPPhone.Enabled = false;
                        txtCreationdate.Enabled = false;
                        txtExpirationDate.Enabled = false;
                        drpCarriageCharges.Enabled = false;
                        txtVersion.Enabled = false;
                        grdQuote.Enabled = false;
                    }
                }

                if (status == "Approved" || status == "Confirmed" || status == "Reject")
                {
                    txtComments.Enabled = false;  //03-16-20 Comments textbox disabled

                }
            }

            if (UserRole == "Sales Engineer") // 03-11-20 removed SM per Business Request
            {
                grdQuote.Columns[15].Visible = true;
                txtTotalGM.Visible = false;   //change by divya on 2/27/20
                lblTotalGM.Visible = false;
            }

            if (chkPerforma.Checked == true)
            {
                if (txtCustomerName.Text.Contains("SUNDRY"))
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
            else if (drpCustBranch.SelectedItem.Text.ToUpper().Contains("SUNDRY"))
            {
                lblSundryBranch.Visible = true;
                txtSundryBranch.Visible = true;
                lblSundryBranch.Text = "Sundry Branch";
            }
            else
            {
                lblSundryBranch.Visible = false;
                txtSundryBranch.Visible = false;
            }






        }




        protected void btnAddNewItem_Click(object sender, EventArgs e)
        {
            Page.Validate("AddNewItem");
            if (Page.IsValid)
            {
                //DataTable dt = new DataTable();
                //if (ViewState["CurrentTable"] != null)
                //{
                //    dt = (DataTable)ViewState["CurrentTable"];
                //}
                //DataRow dr = null;

                DataTable dt = new DataTable();
                if (grdQuote.Rows.Count > 0)
                {

                    for (int i = 0; i < grdQuote.Columns.Count; i++)
                    {
                        string headername = grdQuote.Columns[i].HeaderText;
                        dt.Columns.Add(headername);
                    }
                    foreach (GridViewRow row in grdQuote.Rows)
                    {
                        DataRow dr = dt.NewRow();


                        for (int j = 0; j < grdQuote.Columns.Count; j++)
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
                        dr["UnitPrice"] = UnitPrice.Text;
                        TextBox AddDiscount = (TextBox)row.FindControl("txtDiscount");
                        dr["AdditionalDiscount"] = AddDiscount.Text;
                        dt.Rows.Add(dr);
                    }
                }
                //GridView1.DataBind();
                DataRow dr1 = dt.NewRow();
                dr1 = dt.NewRow(); // add last empty row
                dr1["QTY"] = "1";
                dt.Rows.Add(dr1);

                grdQuote.Columns[15].Visible = true;
                ViewState["CurrentTable"] = dt;
                grdQuote.DataSource = dt; // bind new datatable to grid
                grdQuote.DataBind();
                if (UserRole == "Sales Engineer") //03-11-20 removed SM per Business request
                {
                    grdQuote.Columns[16].Visible = true;
                }

                if (grdQuote.Rows.Count > 1)
                {
                    foreach (GridViewRow row in grdQuote.Rows)
                    {
                        TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                        PartNum.Focus();
                    }

                }
            }
        }
        private string LoadQuoteDetails()
        {
            DataTable dtSundry = new DataTable();
            string QuoteNo = Request["QuoteNo"];
            string sts = Request["Status"];
            dt = obj.LoadQuote(QuoteNo, sts);
            if (sts != "Draft" && sts != "Pending Approval" && sts.Contains("PENDING")==false && sts != "Approved" && sts != "Reject" && sts != "INACTIVE")
            {
                drpStatus.Visible = true;
                drpStatus.Enabled = false;
                drpStatus.Text = sts;

            }
            else if (sts == "INACTIVE" || sts.Contains("PENDING"))
            {
                drpStatus.Visible = true;
                drpStatus.Enabled = true;
                drpStatus.Text = sts;

            }
            else
            {
                drpStatus.Visible = false;
            }

            string ExportCust = dt.Rows[0]["ExportFlag"].ToString().Trim();
            if (ExportCust == "Yes")
            {
                chkExportCustomer.Checked = true;
                lblVAT.Visible = true;
                txtVAT.Visible = true;
            }
            else
            {
                chkExportCustomer.Checked = false;
                lblVAT.Visible = false;
                txtVAT.Visible = false;
            }



            txtVAT.Text = dt.Rows[0]["VATNo"].ToString().Trim();


            txtQuoteNum.Text = dt.Rows[0]["Quote Number"].ToString().Trim();
            txtCustomerName.Text = dt.Rows[0]["Customer Name"].ToString().Trim();
            DataTable dtCust = new DataTable();
            dtCust = objCreateQuoteBAL.GetTermsCode(txtCustomerName.Text);
            if (dt.Rows[0]["isPerforma"].ToString() == "Proforma")
            {
                lblPerforma.Text = "This is a Proforma customer";
                chkPerforma.Checked = true;
            }
            else
            {
                lblPerforma.Text = "";
                chkPerforma.Checked = false;
            }

            if (chkPerforma.Checked == true)
            {
                if (txtCustomerName.Text.Contains("SUNDRY"))
                {
                    lblSundryBranch.Visible = true;
                    txtSundryBranch.Visible = true;
                    lblSundryBranch.Text = "Delivery Address";
                    txtSundryBranch.Text = dt.Rows[0]["SundryBranch"].ToString().Trim();
                }
                else
                {
                    lblSundryBranch.Visible = true;
                    txtSundryBranch.Visible = true;
                }
            }
            if (dt.Rows[0]["Customer Number"].ToString() != string.Empty)
            {
                txtCustomerNumber.Text = dt.Rows[0]["Customer Number"].ToString().Trim();
                if (dt.Rows[0]["Customer Branch"].ToString() != string.Empty)
                {
                    drpCustBranch.SelectedItem.Text = dt.Rows[0]["Customer Branch"].ToString().Trim();
                }
                else
                {
                    drpCustBranch.SelectedItem.Text = "";
                }
                if ((drpCustBranch.SelectedItem.Text == "SUNDRY BRANCH"))
                {
                    lblSundryBranch.Visible = true;
                    txtSundryBranch.Visible = true;
                    txtSundryBranch.Text = dt.Rows[0]["SundryBranch"].ToString().Trim();
                }
            }
            else
            {
                txtCustomerNumber.Visible = false;
                lblCustNo.Visible = false;
                lblCustBranch.Visible = false;
                drpCustBranch.Visible = false;
                lblCustName.Visible = true;
                txtCustName.Visible = true;
                dtSundry = obj.LoadSundryCust(QuoteNo);
                txtCustName.Text = dtSundry.Rows[0][0].ToString() + dtSundry.Rows[0][1].ToString();
            }


            txtCustEmail.Text = dt.Rows[0]["Customer Email"].ToString().Trim();
            txtCustPhone.Text = dt.Rows[0]["Customer Phone"].ToString().Trim();
            //RFDrpCustNo.InitialValue = "1";
            txtProjectName.Text = dt.Rows[0]["Project Name"].ToString().Trim();
            txtOppurtunityId.Text = dt.Rows[0]["Oppurtunity Id"].ToString().Trim();
            txtCurrency.Text = dt.Rows[0]["Currency"].ToString().Trim();
            txtCreationdate.Text = Convert.ToDateTime(dt.Rows[0]["Creation Date"]).ToString("dd/MM/yyyy");
            txtExpirationDate.Text = Convert.ToDateTime(dt.Rows[0]["Expiration Date"]).ToString("dd/MM/yyyy");
            txtUpdateStatusCmt.Text = dt.Rows[0]["ChangeStatus_Comments"].ToString().Trim();
            if (dt.Rows[0]["CarriageCharge"].ToString() != string.Empty)
            {
                if (dt.Rows[0]["CarriageCharge"].ToString().Contains("/"))
                {
                    drpCarriageCharges.SelectedItem.Text = dt.Rows[0]["CarriageCharge"].ToString().Trim();
                    txtCarriage.Visible = false;
                }
                else
                {
                    txtCarriage.Visible = true;
                    drpCarriageCharges.SelectedItem.Text = "0/Add New/Carriage";
                    txtCarriage.Text = dt.Rows[0]["CarriageCharge"].ToString().Trim();
                }

            }
            else
            {
                drpCarriageCharges.SelectedItem.Text = string.Empty;
            }



            txtPreparedBy.Text = dt.Rows[0]["Prepared By"].ToString().Trim();
            txtSEEMail.Text = dt.Rows[0]["PreparedByEmail"].ToString().Trim();
            txtSEPhone.Text = dt.Rows[0]["PreparedByPhone"].ToString().Trim();
            txtSalesPerson.Text = dt.Rows[0]["SalesPerson"].ToString().Trim();
            txtSPEmail.Text = dt.Rows[0]["SPEmail"].ToString().Trim();
            txtSPPhone.Text = dt.Rows[0]["SPPhone"].ToString().Trim();
            txtVersion.Text = dt.Rows[0]["Version"].ToString().Trim();

            txtComments.Text = dt.Rows[0]["Comments"].ToString().Trim();


            grdQuote.Columns[15].Visible = true;
            ViewState["CurrentTable"] = dt;
            grdQuote.DataSource = dt;
            grdQuote.DataBind();
            if (UserRole == "Sales Engineer") //03-11-20 removed SM from here per Business request
            {
                grdQuote.Columns[15].Visible = true;
                txtTotalGM.Visible = false;
                lblTotalGM.Visible = false;
            }

            float GrandTotal = 0;
            bool status240Item=false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dtLine = new DataTable();
                dtLine = objCreateQuoteBAL.GetItemDetails(dt.Rows[i][19].ToString(), "NULL");
                if (dtLine.Rows.Count != 0)
                { 
                if (dtLine.Rows[0]["Status"].ToString() == "240")
                {
                    status240Item = true;
                }
                }
                string TotalPrice = dt.Rows[i]["Total Price after Extra Discount"].ToString();
                if (TotalPrice != string.Empty)
                {
                    GrandTotal = GrandTotal + float.Parse(TotalPrice);
                }

            }

            if (status240Item == true)
            {
                lblMessage.Text = "Quote contains status 240 item";
            }

            TxtGrandTotal.Text = GrandTotal.ToString("0,0.00");
            if (dt.Rows[0]["Total GM%"].ToString() != string.Empty) //added a check for non blank value 06/23/20
            {
                float fltGM = float.Parse(dt.Rows[0]["Total GM%"].ToString().Trim());
                txtTotalGM.Text = fltGM.ToString("0,0.00");
            }


            string Status = dt.Rows[0]["Status"].ToString().Trim();
            return Status;
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            Page.Validate("Reject");
            if (Page.IsValid)
            {
                string QuoteNo = txtQuoteNum.Text;
                string Comments = txtComments.Text;
                obj.RejectQuote(QuoteNo, Comments);
                //Response.Write("<script LANGUAGE='JavaScript' >alert('Quote rejected successfully')</script>");
                ////Server.Transfer("Dashboard.aspx");
                //Response.Redirect("Dashboard.aspx");

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Quote rejected successfully');window.location ='Dashboard.aspx';",
                true);
                SmtpClient smtpClient1 = new SmtpClient("smtp.watts.com");
                smtpClient1.UseDefaultCredentials = true;

                string Preparedby;
                if (txtPreparedBy.Text.ToUpper().Contains("ON BEHALF"))
                {
                    Preparedby = txtPreparedBy.Text.Substring(txtPreparedBy.Text.LastIndexOf(" ") + 1);
                }
                else
                {
                    Preparedby = txtPreparedBy.Text;

                }
                string MailTo1 = ConfigurationManager.AppSettings["Email" + Preparedby];
                MailMessage mail1 = new MailMessage("ukquotations@wattswater.com", MailTo1);
                mail1.Subject = QuoteNo + " is rejected";
                mail1.Body = "Your Quote " + QuoteNo + " is rejected.";
                MailAddress copy = new MailAddress(ConfigurationManager.AppSettings["EmailAdmin"]); //choprad send email to admin 2/4/20
                mail1.CC.Add(copy);
                smtpClient1.Send(mail1);
            }
        }

        protected void btnUpdateVersion_Click(object sender, EventArgs e)
        {
            Page.Validate("UpdateVersion");
            if (Page.IsValid)
            {
                //Get Control details
                string QuoteNumber = txtQuoteNum.Text;
                string CustomerName = txtCustomerName.Text;
                string CustomerNumber = string.Empty;
                string CustomerBranch = string.Empty;
                string SundryBranch = string.Empty;

                string CustomerEmail = txtCustEmail.Text;
                string CustomerPhone = txtCustPhone.Text;
                string ProjectName = txtProjectName.Text;
                string OppurtunityId = txtOppurtunityId.Text;
                //string PaymentTerms = txtPaymentTerms.Text;
                // string PartialDelivery = txtPartialDelivery.Text;
                string Currency = txtCurrency.Text;
                string PreparedBy = txtPreparedBy.Text;
                string PreparedByEmail = txtSEEMail.Text;
                string PreparedByPhone = txtSEPhone.Text;
                string SalesPerson = txtSalesPerson.Text;
                string SalesPersonEmail = txtSPEmail.Text;
                string SalesPersonPhone = txtSPPhone.Text;
                string UserRole = (string)Session["UserRole"];
                DateTime CreationDate = DateTime.ParseExact(txtCreationdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime ExpirationDate = DateTime.ParseExact(txtExpirationDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                string CarriageCharges = "";
                if (drpCarriageCharges.SelectedItem.Text.Contains("Add New"))
                {
                    CarriageCharges = txtCarriage.Text;
                }
                else
                {
                    CarriageCharges = drpCarriageCharges.SelectedItem.Text;
                }

                string Platform = "";
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
                string UnitPrice = "";
                string Discount = "";
                string AddDiscount = "";
                string UnitPriceAfterDiscount = "";
                string TotalPriceAfterDiscount = "";
                float GM = 0;
                string CostTotal = "";
                //string Country = txtCustomerCountry.Text;
                string Version = txtVersion.Text.Trim();
                string GetVersion = txtVersion.Text.Trim();
                string ActualVersion = "0";
                ActualVersion = GetVersion.Substring(GetVersion.IndexOf("_") + 1, 1);
                string VersionedQuote = "";
                try
                {
                    VersionedQuote = Version.Substring(0, Version.IndexOf('_'));
                }
                catch (Exception ex)
                {
                    VersionedQuote = Version;
                }
                if (ActualVersion != string.Empty)
                {
                    int Ver = 0;
                    try
                    {
                        Ver = Convert.ToInt32(ActualVersion) + 1;
                    }
                    catch (Exception ex)
                    {
                        Ver = 1;
                    }
                    // Version = QuoteNumber + "_" + Ver.ToString();
                    Version = VersionedQuote + "_" + Ver.ToString();
                }

                if (txtCustomerNumber.Visible == true)
                {
                    CustomerNumber = txtCustomerNumber.Text;
                    //string CustomerBranch = drpCustBranch.SelectedItem.Text;
                    CustomerBranch = string.Empty;
                    SundryBranch = string.Empty;
                    if (drpCustBranch.SelectedItem.Text != string.Empty && drpCustBranch.SelectedItem.Text != "SUNDRY BRANCH")
                    {
                        CustomerBranch = drpCustBranch.SelectedItem.Text;
                    }
                    else
                    {
                        CustomerBranch = "SUNDRY BRANCH";
                        SundryBranch = txtSundryBranch.Text;
                    }

                }
                else
                {
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
                        //Quote_Number=Version
                        objCreateQuoteBAL.SaveSundryCust(Version, strCustName, strCustAdd);
                    }
                }

                //objCreateQuoteBAL.DeleteExistingQuote(QuoteNumber);
                float AverageUnitPrice = 0;
                float AverageDiscount = 0;
                float AverageGM = 0;
                float AvgCost = 0;
                string total = TxtGrandTotal.Text;
                AverageUnitPrice = float.Parse(total);
                foreach (GridViewRow gr in grdQuote.Rows)
                {
                    // Platform = gr.Cells[1].Text;

                    if (gr.Cells[2].Text.Contains("&") == false && gr.Cells[2].Text.Contains("amp") == false && gr.Cells[2].Text.Contains("#160;") == false)
                    {
                        ProductFamily = gr.Cells[2].Text.Trim();
                    }
                    else
                    {
                        ProductFamily = "";
                    }
                    //ProductFamily = gr.Cells[2].Text;
                    if (gr.Cells[3].Text.Contains("&") == false && gr.Cells[3].Text.Contains("amp") == false && gr.Cells[3].Text.Contains("#160;") == false)
                    {
                        ItemNo = gr.Cells[3].Text.Trim();
                    }
                    else
                    {
                        ItemNo = "";
                    }

                    // ItemNo = gr.Cells[3].Text;
                    TextBox PartNoTextbox = (TextBox)gr.FindControl("txtPartNo");
                    PartNo = PartNoTextbox.Text;

                    if (PartNo.ToString().ToUpper().Contains("SUNDRY"))
                    {
                        blnSundryItem = true;
                    }
                    TextBox DescTextbox = (TextBox)gr.FindControl("txtDesc");
                    PartNo = PartNoTextbox.Text;
                    Desc = DescTextbox.Text;

                    TextBox txtQTYTextbox = (TextBox)gr.FindControl("txtQTY");
                    string Quantity = txtQTYTextbox.Text;
                    QTY = Convert.ToInt32(Quantity);


                    if (gr.Cells[7].Text.Contains("&") == false && gr.Cells[7].Text.Contains("amp") == false && gr.Cells[7].Text.Contains("#160;") == false)
                    {
                        AvailableQty = gr.Cells[7].Text.Trim();
                    }
                    else
                    {
                        AvailableQty = "";
                    }

                    if (gr.Cells[8].Text.Contains("&") == false && gr.Cells[8].Text.Contains("amp") == false && gr.Cells[8].Text.Contains("#160;") == false)
                    {
                        StockAvailable = gr.Cells[8].Text.Trim();
                    }
                    else
                    {
                        StockAvailable = "";
                    }


                    if (gr.Cells[9].Text.Contains("&") == false && gr.Cells[9].Text.Contains("amp") == false && gr.Cells[9].Text.Contains("#160;") == false)
                    {
                        MOQ = gr.Cells[9].Text.Trim();
                    }
                    else
                    {
                        MOQ = "";
                    }
                    TextBox txtLeadTime = (TextBox)gr.FindControl("txtLeadTime");
                    string strLeadtime = txtLeadTime.Text;
                    LeadTime = strLeadtime;
                    if (gr.Cells[11].Text.Contains("&") == false && gr.Cells[11].Text.Contains("amp") == false && gr.Cells[11].Text.Contains("#160;") == false)
                    {
                        SafetyStock = gr.Cells[11].Text.Trim();
                    }
                    else
                    {
                        SafetyStock = "";
                    }
                    if (gr.Cells[12].Text.Contains("&") == false && gr.Cells[12].Text.Contains("amp") == false && gr.Cells[12].Text.Contains("#160;") == false)
                    {
                        Weight = gr.Cells[12].Text.Trim();
                    }
                    else
                    {
                        Weight = "";
                    }

                    if (gr.Cells[13].Text.Contains("&") || gr.Cells[13].Text.Contains("amp") || gr.Cells[13].Text.Contains("#160;"))
                    {
                        ListPrice = "";
                    }
                    else
                    {
                        ListPrice = gr.Cells[13].Text.Trim();
                    }

                    if (gr.Cells[14].Text.Contains("&") || gr.Cells[14].Text.Contains("amp") || gr.Cells[14].Text.Contains("#160;"))
                    {
                        Discount = "";
                    }
                    else
                    {
                        Discount = gr.Cells[14].Text.Trim();
                    }

                    TextBox txtUnitPrice = (TextBox)gr.FindControl("txtUnitPrice");
                    UnitPrice = txtUnitPrice.Text;



                    TextBox DiscountTextbox = (TextBox)gr.FindControl("txtDiscount");
                    AddDiscount = DiscountTextbox.Text;
                    UnitPriceAfterDiscount = gr.Cells[17].Text;
                    TotalPriceAfterDiscount = gr.Cells[18].Text;
                    GM = float.Parse(gr.Cells[19].Text);
                    float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                    if (PartNo.ToString().ToUpper().Contains("SUNDRY"))
                    {
                        CostTotal = "0";
                    }
                    else
                    {
                        CostTotal = objCreateQuoteBAL.getCostPrice(PartNo);

                        if (CostTotal != "")
                        {
                            if(txtCurrency.Text == "EUR")
                            {
                                CostTotal = (float.Parse(CostTotal) * rate).ToString();
                            }
                        }

                        if (objCreateQuoteBAL.getCostPrice(PartNo) == "")
                        {
                            DataTable dtICO = new DataTable();
                            dtICO = objCreateQuoteBAL.GetICOItem(PartNo, string.Empty);
                            CostTotal = dtICO.Rows[0]["CostPrice"].ToString();
                            if (txtCurrency.Text == "EUR")
                            {
                                CostTotal = (float.Parse(CostTotal) * rate).ToString();
                            }
                        }
                    }
                    QuoteNumber = Version;



                    float ConvertedUnitPrice = 0;
                    float ConvertedDiscount = 0;
                    float ConvertedGM = 0;
                    float TotalCost = 0;



                    if (TotalPriceAfterDiscount.Trim() != string.Empty && TotalPriceAfterDiscount.Trim() != "0")
                    {
                        ConvertedUnitPrice = float.Parse(TotalPriceAfterDiscount);
                    }
                    if (AddDiscount.Trim() != string.Empty && AddDiscount.Trim() != "0")
                    {
                        ConvertedDiscount = float.Parse(AddDiscount);
                    }

                    if (CostTotal.Trim() != "" && CostTotal.Trim() != "0")
                    {
                        TotalCost = float.Parse(CostTotal) * QTY;
                    }
                    else
                        TotalCost = 0;


                    GM = ((ConvertedUnitPrice - TotalCost) * 100) / ConvertedUnitPrice;


                    AverageDiscount = AverageDiscount + ConvertedDiscount;

                    AvgCost = AvgCost + TotalCost;


                    //string total = TxtGrandTotal.Text;
                    if (AverageUnitPrice != 0)
                    {
                        if (PartNo.ToString().ToUpper().Contains("SUNDRY"))
                        {

                            AverageUnitPrice = AverageUnitPrice - ConvertedUnitPrice;

                        }

                    }

                    //chkPerforma.Checked = false;
                    string ExportFlag = "";
                    if (chkExportCustomer.Checked)
                    {
                        ExportFlag = "Yes";
                    }
                    else
                    {
                        ExportFlag = "No";
                    }

                    string type = "";
                    if (chkPerforma.Checked==true)
                    {
                        type = "Proforma";
                    }
                    else {
                        type = "Quote";
                    }

                    obj.UpdateQuote(QuoteNumber, CustomerName, CustomerNumber, CustomerBranch, CustomerEmail, CustomerPhone, ProjectName, OppurtunityId, string.Empty, string.Empty, PreparedBy, PreparedByEmail, PreparedByPhone, SalesPerson, SalesPersonEmail, SalesPersonPhone, Currency, UserRole, CreationDate, ExpirationDate, CarriageCharges, ProductFamily, ItemNo, PartNo, Desc, QTY, MOQ, LeadTime, AvailableQty, Weight, SafetyStock, ListPrice, Discount, UnitPrice, AddDiscount, UnitPriceAfterDiscount, TotalPriceAfterDiscount, GM.ToString("0.00"), Version, txtComments.Text, TxtGrandTotal.Text, string.Empty, CostTotal, SundryBranch, type, txtUpdateStatusCmt.Text, ExportFlag, txtVAT.Text,StockAvailable);

                }
                /*-------------------------------------------------------------------*/
                //03-18-20 for update version, approval matrix changes starts
                /*--------------------------------------------------------------------*/

                string Approval1 = "";
                //string Approval2 = "";
                string Approval3 = "";
                int SalesManagerFlag = 0;
                int GMFlag = 0;

                AverageGM = ((AverageUnitPrice - AvgCost) * 100) / AverageUnitPrice;


                AverageDiscount = AverageDiscount / grdQuote.Rows.Count;



                if ((AverageUnitPrice >= 2500 && AverageUnitPrice <= 5000 && UserName != "Richard Kleiser") || (AverageUnitPrice > 5000 && UserName == "Richard Kleiser"))
                {
                    SalesManagerFlag = 1;
                }
                else if ((AverageUnitPrice > 5000 || AverageGM < 25 || blnSundryItem == true) && (UserName != "Richard Kleiser"))
                {
                    GMFlag = 1;
                }



                if (GMFlag == 1)
                {
                    if (UserRole == "Sales Engineer")
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
                string Status = "Updated to New Version";
                //End of Matrix calculation
                if (UserRole == "Sales Manager" && SalesManagerFlag == 1 && Status == "Pending Approval") //03-17-20 to change status to approved only on Submit
                {
                    Status = "Approved";
                    obj.UpdateQuoteDetails(QuoteNumber, Status);
                }

                obj.UpdateMatrixDetails(Approval1, Approval3, AverageGM.ToString(), QuoteNumber);

                /*--------------------------------------------------------------------*/
                // approval matrix changes ends
                /*---------------------------------------------------------------------*/

                //Response.Write("<script LANGUAGE='JavaScript' >alert('Quote updated successfully')</script>");
                //Server.Transfer("Dashboard.aspx");

                ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Quote updated successfully');window.location ='Dashboard.aspx';",
               true);
            }
        }

        protected void txtPartNo_TextChanged(object sender, EventArgs e)
        {

            ////Read existing table data
            DataTable dt = new DataTable();
            if (grdQuote.Rows.Count > 0)
            {

                for (int i = 0; i < grdQuote.Columns.Count; i++)
                {
                    string headername = grdQuote.Columns[i].HeaderText;

                    dt.Columns.Add(headername);
                }
                foreach (GridViewRow row in grdQuote.Rows)
                {
                    DataRow dr = dt.NewRow();

                    for (int j = 0; j < grdQuote.Columns.Count; j++)
                    {
                        dr[j] = Server.HtmlDecode(row.Cells[j].Text);

                    }
                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                    dr["PartNo"] = PartNum.Text;
                    TextBox Desc = (TextBox)row.FindControl("txtDesc");
                    dr["Description"] = Desc.Text;
                    TextBox QTY = (System.Web.UI.WebControls.TextBox)row.FindControl("txtQTY");
                    dr["QTY"] = QTY.Text;

                    TextBox LeadTime = (System.Web.UI.WebControls.TextBox)row.FindControl("txtLeadTime");
                    dr["LeadTime"] = LeadTime.Text;

                    TextBox UnitPrice = (System.Web.UI.WebControls.TextBox)row.FindControl("txtUnitPrice");
                    dr["UnitPrice"] = UnitPrice.Text;

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
            dtLineItem = objCreateQuoteBAL.GetItemDetails(PartNo, "NULL");
            int RowId = currentRow.DataItemIndex;
            float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
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

                dt.Rows[RowId]["ProductFamily"] = objCreateQuoteBAL.ItemDiscGroup(dtLineItem.Rows[0]["ItemNo"].ToString());
                dt.Rows[RowId]["ItemNo"] = dtLineItem.Rows[0]["ItemNo"].ToString();
                dt.Rows[RowId]["PartNo"] = dtLineItem.Rows[0]["LegacyPartNo"].ToString();
                dt.Rows[RowId]["Description"] = dtLineItem.Rows[0]["Description1"].ToString();
                dt.Rows[RowId]["MOQ"] = dtLineItem.Rows[0]["MinOrderQty"].ToString();
                dt.Rows[RowId]["LeadTime"] = dtLineItem.Rows[0]["LeadTime"].ToString();
                dt.Rows[RowId]["AvailableQty"] = dtLineItem.Rows[0]["AvailableQty"].ToString();
                dt.Rows[RowId]["Weight"] = dtLineItem.Rows[0]["Weight"].ToString();
                float CostPrice = 0;
                CostPrice = float.Parse(dtLineItem.Rows[0]["CostPrice"].ToString());

                if (txtCurrency.Text == "EUR")
                {
                    CostPrice = CostPrice * rate;
                }

                dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                dt.Rows[RowId]["SafetyStock"] = dtLineItem.Rows[0]["SafetyStock"].ToString();


                DataTable dtAvailableStock = new DataTable();

                dtAvailableStock = objCreateQuoteBAL.GetStock(PartNo);

                if (dtAvailableStock.Rows.Count > 0)
                {


                    dt.Rows[RowId]["StockAvailability"] = dtAvailableStock.Rows[0]["Quantity Open"].ToString() + " on " + DateTime.Parse(dtAvailableStock.Rows[0]["Due Date"].ToString()).ToString("dd/MM/yy");
                }
                else
                {
                    dt.Rows[RowId]["StockAvailability"] = "";
                }


                string netprice = string.Empty;
                float UnitPrice = 0;
                
                netprice = objCreateQuoteBAL.GetNetPrice(txtCustomerNumber.Text, PartNo, string.Empty,txtCurrency.Text);

                if (netprice != string.Empty)
                {
                    dt.Rows[RowId]["ListPrice"] = string.Empty;
                    dt.Rows[RowId]["Discount"] = string.Empty;

                    //float Listprice = float.Parse(dtPrice.Rows[0]["ListPrice"].ToString());
                    //float Disc = float.Parse(dtPrice.Rows[0]["DiscountPerc"].ToString());
                    //UnitPrice = Listprice - (Listprice * Disc / 100);
                    UnitPrice = float.Parse(netprice);

                    dt.Rows[RowId]["UnitPrice"] = UnitPrice.ToString("0.00");

                    float AddDiscount = 0;
                    // AddDiscount = float.Parse(txtDisc.Text.ToString().Trim());

                    dt.Rows[RowId]["AdditionalDiscount"] = AddDiscount.ToString();
                    string Quantity = dt.Rows[RowId]["QTY"].ToString();
                    int Qty = 0;
                    float UnitPriceafterExtraDiscount = 0;
                    CostPrice = 0;
                    CostPrice = float.Parse(dtLineItem.Rows[0]["CostPrice"].ToString());

                    if(txtCurrency.Text == "EUR")
                    {
                        CostPrice = CostPrice * rate;
                    }

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
                    DataTable dtPrice = new DataTable();
                    dtPrice = objCreateQuoteBAL.GetPrice(PartNo, "NULL", "GBP");


                    if (dtPrice.Rows.Count != 0)
                    {

                        dt.Rows[RowId]["ListPrice"] = dtPrice.Rows[0]["ListPrice"].ToString();

                        DataTable dtDisc = new DataTable();
                        float Disc = 0;
                       
                        dtDisc = objCreateQuoteBAL.GetDiscount(dtLineItem.Rows[0]["ItemNo"].ToString(), txtCustomerNumber.Text);
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

                        UnitPrice = Listprice - (Listprice * Disc / 100);

                        dt.Rows[RowId]["UnitPrice"] = UnitPrice.ToString("0.00");




                        // TextBox txtDisc = (TextBox)currentRow.FindControl("txtDiscount");
                        float AddDiscount = 0;
                        // AddDiscount = float.Parse(txtDisc.Text.ToString().Trim());

                        dt.Rows[RowId]["AdditionalDiscount"] = AddDiscount.ToString();
                        string Quantity = dt.Rows[RowId]["QTY"].ToString();
                        int Qty = 0;
                        float UnitPriceafterExtraDiscount = 0;
                        CostPrice = 0;
                        CostPrice = float.Parse(dtLineItem.Rows[0]["CostPrice"].ToString());

                        if (txtCurrency.Text == "EUR")
                        {
                            CostPrice = CostPrice * rate;
                        }
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


                }




            }
            else
            {

                DataTable dtICOItem = new DataTable();
                dtICOItem = objCreateQuoteBAL.GetICOItem(PartNo, string.Empty);
                float UnitPrice = 0;
                float CostPrice = 0;
                if (dtICOItem.Rows.Count > 0)
                {
                    dt.Rows[RowId]["ProductFamily"] = "WOTH";
                    dt.Rows[RowId]["ItemNo"] = "";
                    dt.Rows[RowId]["PartNo"] = PartNo;
                    dt.Rows[RowId]["Description"] = dtICOItem.Rows[0]["Description"].ToString();
                    dt.Rows[RowId]["MOQ"] = "";
                    dt.Rows[RowId]["LeadTime"] = "";
                    dt.Rows[RowId]["AvailableQty"] = "";
                    dt.Rows[RowId]["Weight"] = "";

                    CostPrice = float.Parse(dtICOItem.Rows[0]["CostPrice"].ToString());

                    if (txtCurrency.Text == "EUR")
                    {
                        CostPrice = CostPrice * rate;
                    }

                    dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                    dt.Rows[RowId]["SafetyStock"] = "";




                    float ICOPrice = 0;
                    string ICODisc = string.Empty;

                    ICOPrice = float.Parse(dtICOItem.Rows[0]["Price"].ToString());
                    
                    ICODisc = objCreateQuoteBAL.GetICODiscount(txtCustomerNumber.Text);
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


                    dt.Rows[RowId]["UnitPrice"] = UnitPrice.ToString("0.00");


                    float AddDiscount = 0;
                    // AddDiscount = float.Parse(txtDisc.Text.ToString().Trim());

                    dt.Rows[RowId]["AdditionalDiscount"] = AddDiscount.ToString();

                    string Quantity = dt.Rows[RowId]["QTY"].ToString();
                    int Qty = 0;
                    float UnitPriceafterExtraDiscount = 0;
                    // CostPrice = 0;
                    CostPrice = float.Parse(dtICOItem.Rows[0]["CostPrice"].ToString());
                    if (txtCurrency.Text == "EUR")
                    {
                        CostPrice = CostPrice * rate;
                    }
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
                    /*dt.Rows[RowId]["ProductFamily"] = "";
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
                    dt.Rows[RowId]["UnitPrice"] = "";
                    dt.Rows[RowId]["AdditionalDiscount"] = "";
                    dt.Rows[RowId]["Unit Price After Extra Discount"] = "";
                    dt.Rows[RowId]["Total Price after Extra Discount"] = "";
                    dt.Rows[RowId]["GM"] = "";
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Invalid Part Number')</script>");*/
                }
            }

            ViewState["CurrentTable"] = dt;
            grdQuote.DataSource = dt; // bind new datatable to grid
                                      //GridView1.Columns[11].HeaderText = "Additional Discount%";
                                      //GridView1.Columns[14].HeaderText = "GM%";
            grdQuote.DataBind();
            ////obj.testmethod();
            float GrandTotal = 0;
            float COstTotal = 0;
            float TotalforGM = 0;
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
                string Cost = dt.Rows[i]["CostPrice"].ToString().Trim();
                if (Cost != "")
                {
                    COstTotal = COstTotal + float.Parse(Cost);
                }


            }

            float TotalGM = 0;
            TotalGM = ((TotalforGM - COstTotal) / TotalforGM) * 100;
            txtTotalGM.Text = TotalGM.ToString("0.00");




            /* string temp = drpCarriageCharges.SelectedItem.Text.Substring(drpCarriageCharges.SelectedItem.Text.IndexOf("£"));
             string Carriage = temp.Substring(1, temp.IndexOf("/") - 1);
             float CarriageVal = float.Parse(Carriage);
             TxtGrandTotal.Text = (GrandTotal + CarriageVal).ToString("0.00");*/
            TxtGrandTotal.Text = GrandTotal.ToString("0.00");
            TextBox setfocus = (TextBox)currentRow.FindControl("txtPartNo");
            setfocus.Focus();

        }
        protected void txtDesc_TextChanged(object sender, EventArgs e)
        {

            ////Read existing table data
            DataTable dt = new DataTable();
            if (grdQuote.Rows.Count > 0)
            {

                for (int i = 0; i < grdQuote.Columns.Count; i++)
                {
                    string headername = grdQuote.Columns[i].HeaderText;

                    dt.Columns.Add(headername);
                }
                foreach (GridViewRow row in grdQuote.Rows)
                {
                    DataRow dr = dt.NewRow();

                    for (int j = 0; j < grdQuote.Columns.Count; j++)
                    {
                        dr[j] = Server.HtmlDecode(row.Cells[j].Text);

                    }
                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                    dr["PartNo"] = PartNum.Text;
                    TextBox Desc = (TextBox)row.FindControl("txtDesc");
                    dr["Description"] = Desc.Text;
                    TextBox QTY = (System.Web.UI.WebControls.TextBox)row.FindControl("txtQTY");
                    dr["QTY"] = QTY.Text;
                    TextBox LeadTime = (System.Web.UI.WebControls.TextBox)row.FindControl("txtLeadTime");
                    dr["LeadTime"] = LeadTime.Text;
                    TextBox UnitPrice = (System.Web.UI.WebControls.TextBox)row.FindControl("txtUnitPrice");
                    dr["UnitPrice"] = UnitPrice.Text;
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
            dtLineItem = objCreateQuoteBAL.GetItemDetails("NULL", strDesc);
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

                dt.Rows[RowId]["ProductFamily"] = objCreateQuoteBAL.ItemDiscGroup(dtLineItem.Rows[0]["ItemNo"].ToString());
                dt.Rows[RowId]["ItemNo"] = dtLineItem.Rows[0]["ItemNo"].ToString();
                dt.Rows[RowId]["PartNo"] = dtLineItem.Rows[0]["LegacyPartNo"].ToString();
                dt.Rows[RowId]["Description"] = dtLineItem.Rows[0]["Description1"].ToString();
                dt.Rows[RowId]["MOQ"] = dtLineItem.Rows[0]["MinOrderQty"].ToString();
                dt.Rows[RowId]["LeadTime"] = dtLineItem.Rows[0]["LeadTime"].ToString();
                dt.Rows[RowId]["SafetyStock"] = dtLineItem.Rows[0]["SafetyStock"].ToString();


                DataTable dtAvailableStock = new DataTable();

                dtAvailableStock = objCreateQuoteBAL.GetStock(dtLineItem.Rows[0]["LegacyPartNo"].ToString());

                if (dtAvailableStock.Rows.Count > 0)
                {


                    dt.Rows[RowId]["StockAvailability"] = dtAvailableStock.Rows[0]["Quantity Open"].ToString() + " on " + DateTime.Parse(dtAvailableStock.Rows[0]["Due Date"].ToString()).ToString("dd/MM/yy");
                }
                else
                {
                    dt.Rows[RowId]["StockAvailability"] = "";
                }

                string netprice = string.Empty;
                float UnitPrice = 0;
               
                netprice = objCreateQuoteBAL.GetNetPrice(txtCustomerNumber.Text, string.Empty, strDesc,txtCurrency.Text);

                if (netprice != string.Empty)
                {
                    dt.Rows[RowId]["ListPrice"] = string.Empty;
                    dt.Rows[RowId]["Discount"] = string.Empty;

                    //float Listprice = float.Parse(dtPrice.Rows[0]["ListPrice"].ToString());
                    //float Disc = float.Parse(dtPrice.Rows[0]["DiscountPerc"].ToString());
                    //UnitPrice = Listprice - (Listprice * Disc / 100);
                    UnitPrice = float.Parse(netprice);

                    dt.Rows[RowId]["UnitPrice"] = UnitPrice.ToString("0.00");

                }
                else
                {
                    DataTable dtPrice = new DataTable();
                    dtPrice = objCreateQuoteBAL.GetPrice("NULL", strDesc, "GBP");


                    if (dtPrice.Rows.Count != 0)
                    {

                        dt.Rows[RowId]["ListPrice"] = dtPrice.Rows[0]["ListPrice"].ToString();
                        float Disc = 0;
                        DataTable dtDisc = new DataTable();

                        dtDisc = objCreateQuoteBAL.GetDiscount(dtLineItem.Rows[0]["ItemNo"].ToString(), txtCustomerNumber.Text);
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


                        UnitPrice = Listprice - (Listprice * Disc / 100);

                        dt.Rows[RowId]["UnitPrice"] = UnitPrice.ToString("0.00");
                    }

                }

                // TextBox txtDisc = (TextBox)currentRow.FindControl("txtDiscount");
                float AddDiscount = 0;
                // AddDiscount = float.Parse(txtDisc.Text.ToString().Trim());
                float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                dt.Rows[RowId]["AdditionalDiscount"] = AddDiscount.ToString();
                string Quantity = dt.Rows[RowId]["QTY"].ToString();
                int Qty = 0;
                float UnitPriceafterExtraDiscount = 0;
                float CostPrice = 0;
                CostPrice = float.Parse(dtLineItem.Rows[0]["CostPrice"].ToString());
                if (txtCurrency.Text == "EUR")
                {
                    CostPrice = CostPrice * rate;
                }
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
                DataTable dtICOItem = new DataTable();
                dtICOItem = objCreateQuoteBAL.GetICOItem(string.Empty, strDesc);
                if (dtICOItem.Rows.Count > 0)
                {
                    dt.Rows[RowId]["ProductFamily"] = "WOTH";
                    dt.Rows[RowId]["ItemNo"] = "";
                    dt.Rows[RowId]["PartNo"] = dtICOItem.Rows[0]["LegacyPartNo"].ToString();
                    dt.Rows[RowId]["Description"] = strDesc;
                    dt.Rows[RowId]["MOQ"] = "";
                    dt.Rows[RowId]["LeadTime"] = "";
                    dt.Rows[RowId]["AvailableQty"] = "";
                    dt.Rows[RowId]["Weight"] = "";
                    float CostPrice = 0;
                    float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                    CostPrice = float.Parse(dtICOItem.Rows[0]["CostPrice"].ToString());
                    if(txtCurrency.Text=="EUR")
                    {
                        CostPrice = CostPrice * rate;
                    }
                    dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");
                    dt.Rows[RowId]["SafetyStock"] = "";

                    float UnitPrice = 0;


                    float ICOPrice = 0;
                    string ICODisc = string.Empty;

                    ICOPrice = float.Parse(dtICOItem.Rows[0]["Price"].ToString());

                    ICODisc = objCreateQuoteBAL.GetICODiscount(txtCustomerNumber.Text);
                    float Listprice = float.Parse(ICOPrice.ToString("0.00"));
                    float Disc = float.Parse(ICODisc);

                    dt.Rows[RowId]["ListPrice"] = ICOPrice;
                    dt.Rows[RowId]["Discount"] = ICODisc;
                    UnitPrice = Listprice - (Listprice * Disc / 100);


                    dt.Rows[RowId]["UnitPrice"] = UnitPrice.ToString("0.00");


                    float AddDiscount = 0;
                    // AddDiscount = float.Parse(txtDisc.Text.ToString().Trim());

                    dt.Rows[RowId]["AdditionalDiscount"] = AddDiscount.ToString();
                    string Quantity = dt.Rows[RowId]["QTY"].ToString();
                    int Qty = 0;
                    float UnitPriceafterExtraDiscount = 0;
                    //float CostPrice = 0;
                    CostPrice = float.Parse(dtICOItem.Rows[0]["CostPrice"].ToString());
                    if (txtCurrency.Text == "EUR")
                    {
                        CostPrice = CostPrice * rate;
                    }
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
                   
                }

            }




            ViewState["CurrentTable"] = dt;
            grdQuote.DataSource = dt; // bind new datatable to grid
            //GridView1.Columns[11].HeaderText = "Additional Discount%";
            //GridView1.Columns[14].HeaderText = "GM%";
            grdQuote.DataBind();
            ////obj.testmethod();
            float GrandTotal = 0;
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
                }
                string Cost = dt.Rows[i]["CostPrice"].ToString().Trim();

                float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                if (txtCurrency.Text == "EUR")
                {
                    Cost = (float.Parse(Cost) * rate).ToString();
                }
                COstTotal = COstTotal + float.Parse(Cost);

            }

            float TotalGM = 0;
            TotalGM = ((GrandTotal - COstTotal) / GrandTotal) * 100;
            txtTotalGM.Text = TotalGM.ToString("0.00");

            /* string temp = drpCarriageCharges.SelectedItem.Text.Substring(drpCarriageCharges.SelectedItem.Text.IndexOf("£"));
             string Carriage = temp.Substring(1, temp.IndexOf("/") - 1);
             float CarriageVal = float.Parse(Carriage);
             TxtGrandTotal.Text = (GrandTotal + CarriageVal).ToString("0.00");*/
            TxtGrandTotal.Text = GrandTotal.ToString("0.00");
            TextBox setfocus = (TextBox)currentRow.FindControl("txtPartNo");
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
                lblMessage.Text = "Invalid Qty";
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
            float CostPrice = 0;
            float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
            if (grdQuote.Rows.Count > 0)
            {

                for (int i = 0; i < grdQuote.Columns.Count; i++)
                {
                    string headername = grdQuote.Columns[i].HeaderText;

                    dt.Columns.Add(headername);
                }
                foreach (GridViewRow row in grdQuote.Rows)
                {
                    DataRow dr = dt.NewRow();



                    for (int j = 0; j < grdQuote.Columns.Count; j++)
                    {
                        dr[j] = Server.HtmlDecode(row.Cells[j].Text);

                    }
                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                    dr["PartNo"] = PartNum.Text;
                    if (PartNum.Text.ToString().ToUpper().Contains("SUNDRY"))
                    {
                        CostPrice = 0;
                    }
                    else
                    {
                        CostPrice = float.Parse(objCreateQuoteBAL.getCostPrice(PartNum.Text));

                        if (objCreateQuoteBAL.getCostPrice(PartNum.Text) != "")
                        {
                            if (txtCurrency.Text == "EUR")
                            {
                                CostPrice = CostPrice * rate;
                            }
                        }
                        if (objCreateQuoteBAL.getCostPrice(PartNum.Text) == "")
                        {
                            DataTable dtICO = new DataTable();
                            dtICO = objCreateQuoteBAL.GetICOItem(PartNum.Text, string.Empty);
                            CostPrice = float.Parse(dtICO.Rows[0]["CostPrice"].ToString());
                            if (txtCurrency.Text == "EUR")
                            {
                                CostPrice = CostPrice * rate;
                            }
                        }
                    }
                    dr["CostPrice"] = CostPrice.ToString("0.00");
                    TextBox Desc = (TextBox)row.FindControl("txtDesc");
                    dr["Description"] = Desc.Text;
                    TextBox QTY = (TextBox)row.FindControl("txtQTY");
                    dr["QTY"] = QTY.Text;
                    TextBox LeadTime = (TextBox)row.FindControl("txtLeadTime");
                    dr["LeadTime"] = LeadTime.Text;
                    TextBox txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
                    dr["UnitPrice"] = txtUnitPrice.Text;
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
            string Adddisc = dt.Rows[RowId]["AdditionalDiscount"].ToString().Trim();
            float AddDiscount = 0;
            string UnitPrice = dt.Rows[RowId]["UnitPrice"].ToString().Trim();
            if (Adddisc != string.Empty)
            {
                AddDiscount = float.Parse(Adddisc);
            }
            if (UnitPrice != string.Empty)
            {
                float ConvUnitPrice = (float.Parse(UnitPrice));
                UnitPriceafterExtraDiscount = ConvUnitPrice - ((ConvUnitPrice * AddDiscount) / 100);
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
            string partno = dt.Rows[RowId]["PartNo"].ToString();
            if (partno.ToString().ToUpper().Contains("SUNDRY"))
            {
                CostPrice = 0;
            }
            else
            {
                CostPrice = float.Parse(objCreateQuoteBAL.getCostPrice(partno));

                if (objCreateQuoteBAL.getCostPrice(partno) != "")
                {
                    if (txtCurrency.Text == "EUR")
                    {
                        CostPrice = CostPrice * rate;
                    }
                }

                    if (objCreateQuoteBAL.getCostPrice(partno) == "")
                {
                    DataTable dtICO = new DataTable();
                    dtICO = objCreateQuoteBAL.GetICOItem(partno, string.Empty);
                    CostPrice = float.Parse(dtICO.Rows[0]["CostPrice"].ToString());
                    if (txtCurrency.Text == "EUR")
                    {
                        CostPrice = CostPrice * rate;
                    }
                }
            }

            dt.Rows[RowId]["CostPrice"] = CostPrice.ToString("0.00");




            float TotalCost = CostPrice * Quantity;



            float GM = 0;


            GM = (((TotalPriceafterExtraDiscount - TotalCost) * 100) / TotalPriceafterExtraDiscount);

            dt.Rows[RowId]["GM"] = GM.ToString("0,0.00");

            grdQuote.Columns[16].Visible = true;
            ViewState["CurrentTable"] = dt;
            grdQuote.DataSource = dt; // bind new datatable to grid
                                      // grdQuote.Columns[10].HeaderText = "Discount%";
                                      //grdQuote.Columns[13].HeaderText = "GM%";
            grdQuote.DataBind();
            if (UserRole == "Sales Engineer")  //03-11-20 removed SM per Business request
            {
                grdQuote.Columns[15].Visible = true;
            }
            float GrandTotal = 0;
            float TotalforGM = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string TotalPrice = dt.Rows[i]["Total Price after Extra Discount"].ToString().Trim();
                if (TotalPrice != string.Empty)
                {
                    GrandTotal = GrandTotal + float.Parse(TotalPrice);
                    if (dt.Rows[i]["PartNo"].ToString().ToUpper().Contains("SUNDRY") == false)
                    {
                        TotalforGM = TotalforGM + float.Parse(TotalPrice);
                    }
                }

            }

            //03-16-20 change for calculaing total GM
            float TotalUnitCost = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string part = dt.Rows[i]["PartNo"].ToString();


                if (UnitPrice != string.Empty)
                {
                    TotalUnitCost = TotalUnitCost + float.Parse(dt.Rows[i]["CostPrice"].ToString()) * Convert.ToInt32(dt.Rows[i]["Qty"].ToString());
                }


            }


            float TotalGM = 0;

            TotalGM = (((TotalforGM - TotalUnitCost) * 100) / TotalforGM);



            //
            /* string temp = drpCarriageCharges.SelectedItem.Text.Substring(drpCarriageCharges.SelectedItem.Text.IndexOf("£"));
             string Carriage = temp.Substring(1, temp.IndexOf("/") - 1);
             float CarriageVal = float.Parse(Carriage);*/
            //TxtGrandTotal.Text = (GrandTotal + CarriageVal).ToString("0.00");
            TxtGrandTotal.Text = GrandTotal.ToString("0.00");
            txtTotalGM.Text = TotalGM.ToString("0.00");
            TextBox setfocus = (TextBox)currentRow.FindControl("txtPartNo");
            setfocus.Focus();
        }

        protected void SelectCheckBox_OnCheckedChanged(object sender, EventArgs e)
        {

            ////Read existing table data
            DataTable dt = new DataTable();

            if (grdQuote.Rows.Count > 0)
            {

                for (int i = 0; i < grdQuote.Columns.Count; i++)
                {
                    string headername = grdQuote.Columns[i].HeaderText;

                    dt.Columns.Add(headername);
                }
                foreach (GridViewRow row in grdQuote.Rows)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < grdQuote.Columns.Count; j++)
                    {
                        dr[j] = Server.HtmlDecode(row.Cells[j].Text);

                    }
                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
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
            grdQuote.Columns[15].Visible = true;
            ViewState["CurrentTable"] = dt;
            grdQuote.DataSource = dt; // bind new datatable to grid
           
            grdQuote.DataBind();
            if (UserRole == "Sales Engineer") //03-11-20 removed SM per Business request
            {
                grdQuote.Columns[15].Visible = true;
            }

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

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {

            GetDiscountDetails(sender);
            //grdQuote.AllowPaging = true;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Page.Validate("Approve");
            if (Page.IsValid)
            {
                string QuoteNumber = txtQuoteNum.Text;
                string UserRole = (string)Session["UserRole"];
                string UserName = (string)Session["UserName"];
                //string Status = obj.ApproveQuote(UserRole, QuoteNumber);
                string Status = "Pending Approval";
                //SaveQuoteDetails(Status);
                SaveApprovedQuoteDetails(Status);
                string CurrentStatus = obj.ApproveQuote(UserRole, QuoteNumber);

                DataTable dtApprovals = new DataTable();
                dtApprovals = obj.GetApprovaldata(QuoteNumber);


                string Approval1 = "";
                //string Approval2 = "";
                string Approval3 = "";

                if (dtApprovals.Rows.Count > 0)
                {
                    Approval1 = dtApprovals.Rows[0]["approval1"].ToString();
                    //Approval2 = dtApprovals.Rows[0]["approval2"].ToString();
                    Approval3 = dtApprovals.Rows[0]["approval3"].ToString();
                }


                string MailTo = ConfigurationManager.AppSettings["Email" + UserName];
                string Role = "";
                if (Approval1 == "Approved" && Approval3 == "NA")
                {

                    MailTo = ConfigurationManager.AppSettings["Email" + UserName];  //03-11-20 removed BDM from approval matrix
                    Role = "Sales Manager";
                    //MailTo = "divya.chopra@wattswater.com";
                }
                else if (Approval1 == "Approved" && Approval3 == "Pending")
                {
                    MailTo = ConfigurationManager.AppSettings["EmailKerry Harris"];
                    Role = "GM";
                }
                if (CurrentStatus == "Pending Approval")
                {
                    SmtpClient smtpClient = new SmtpClient("smtp.watts.com");
                    smtpClient.UseDefaultCredentials = true;

                    MailMessage mail = new MailMessage("ukquotations@wattswater.com", MailTo);
                    mail.Subject = "Quote " + QuoteNumber + " Approval Request";
                    string MailStatus = "Pending Approval";
                    CurrentStatus = CurrentStatus.Replace(" ", "%20");
                    UserName = UserName.Replace(" ", "%20");
                    Role = Role.Replace(" ", "%20");
                    string QuoteURL = "https://ukrpaquotetool.wattswater.com/Quote.aspx?QuoteNo=" + QuoteNumber + "&Status=" + CurrentStatus + "&UserName=" + UserName + "&UserRole=" + Role;
                    //string QuoteURL = "https://rpaquotationtooldubai.wattswater.com/login";
                    mail.Body = @"New Quote Available to review and approve. Click the link below to approve the quote" + Environment.NewLine + QuoteURL;
                    MailAddress copy = new MailAddress(ConfigurationManager.AppSettings["EmailAlan Fahy"]); //choprad send email to admin 2/4/20
                    mail.Bcc.Add(copy);
                    smtpClient.Send(mail);
                }

                if (CurrentStatus == "Approved")
                {
                    SmtpClient smtpClient1 = new SmtpClient("smtp.watts.com");
                    smtpClient1.UseDefaultCredentials = true;
                    string UserName1 = (string)Session["UserName"];
                    string Preparedby;
                    if (txtPreparedBy.Text.ToUpper().Contains("ON BEHALF"))
                    {
                        Preparedby = txtPreparedBy.Text.Substring(txtPreparedBy.Text.LastIndexOf(" ") + 1);
                    }
                    else
                    {
                        Preparedby = txtPreparedBy.Text;

                    }



                    DataTable dt = new DataTable();
                    if (grdQuote.Rows.Count > 0)
                    {

                        for (int i = 0; i < grdQuote.Columns.Count; i++)
                        {
                            string headername = grdQuote.Columns[i].HeaderText;

                            dt.Columns.Add(headername);
                        }
                        foreach (GridViewRow row in grdQuote.Rows)
                        {
                            DataRow dr = dt.NewRow();

                            for (int j = 0; j < grdQuote.Columns.Count; j++)
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
                    dt.Columns.Remove(dt.Columns[18]);
                    dt.Columns.Remove(dt.Columns[17]);
                    dt.Columns.Remove(dt.Columns[16]);
                   // dt.Columns.Remove(dt.Columns[15]);

                    dt.Columns.Remove(dt.Columns[5]);
                    dt.Columns.Remove(dt.Columns[5]);
                    dt.Columns.Remove(dt.Columns[6]);
                    dt.Columns.Remove(dt.Columns[6]);
                    dt.Columns.Remove(dt.Columns[6]);
                    dt.Columns.Remove(dt.Columns[6]);
                    dt.Columns.Remove(dt.Columns[6]);
                    dt.Columns.Remove(dt.Columns[6]);


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
                    pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               WATTS INDUSTRIES UK LTD", fontAdd));
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

                                pdfDoc.Add(new Phrase("\r\n                                                  OUR VAT No. " + txtVAT.Text + "\r\n", font));

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
                    if (txtCustomerNumber.Visible == true)
                    {



                        if (drpCustBranch.SelectedItem.Text == "Select" || drpCustBranch.SelectedItem.Text == "")
                        {
                            dtAddress = objCreateQuoteBAL.GetCustNameAddress(string.Empty, txtCustomerNumber.Text);

                        }
                        else if (drpCustBranch.SelectedItem.Text != "SUNDRY BRANCH")
                        {
                            dtAddress = objCreateQuoteBAL.GetCustNameAddress(drpCustBranch.SelectedItem.Text, txtCustomerNumber.Text);
                        }

                    }

                        if (chkPerforma.Checked == false)
                        {
                            if (txtCustomerNumber.Visible == true)
                            {
                                string Name;
                                string Address;
                                string City;
                                string Zip;
                                string Country;


                                if (drpCustBranch.SelectedItem.Text == "Select" || drpCustBranch.SelectedItem.Text == "")
                                {
                                    dtAddress = objCreateQuoteBAL.GetCustNameAddress(string.Empty, txtCustomerNumber.Text);

                                }
                                else if (drpCustBranch.SelectedItem.Text != "SUNDRY BRANCH")
                                {
                                    dtAddress = objCreateQuoteBAL.GetCustNameAddress(drpCustBranch.SelectedItem.Text, txtCustomerNumber.Text);
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

                            if (txtCustomerName.Text.Contains("SUNDRY") == false)
                            {
                                DataTable dtCustAdd = new DataTable();
                                dtCustAdd = objCreateQuoteBAL.GetCustomerAddress(txtCustomerName.Text);


                                //pdfDoc.Add(new Phrase("\r\n" + "Invoice Address:".PadRight(115- "Invoice Address:".Length) +"Delivery Address:", font));
                                if (drpCustBranch.SelectedItem.Text.Contains("SUNDRY"))
                                {
                                    string[] strSundryBranch = txtSundryBranch.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                    int i = strSundryBranch.Length;

                                    PdfPCell cellI1 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
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



                                }
                                else if (drpCustBranch.SelectedItem.Text == "Select" || drpCustBranch.SelectedItem.Text == "")
                                {
                                    PdfPCell cellI1 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
                                    cellI1.HorizontalAlignment = Element.ALIGN_LEFT;
                                    cellI1.Border = 0;
                                    tableAdd.AddCell(cellI1);

                                    PdfPCell cellI2 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
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
                                    dtAddress = objCreateQuoteBAL.GetCustNameAddress(drpCustBranch.SelectedItem.Text, txtCustomerNumber.Text);

                                    PdfPCell cellI1 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
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


                            }

                            pdfDoc.Add(tableAdd);
                        }


                        pdfDoc.Add(new Phrase("\r\n \r\n\r\n", font));

                        PdfPTable table1 = new PdfPTable(2);

                        if (chkPerforma.Checked == false)
                        {
                            table1.WidthPercentage = 100;
                            PdfPCell cell1 = new PdfPCell(new Phrase("Quotation Ref: " + this.txtQuoteNum.Text.Trim(), font));
                            cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell1.Border = 0;
                            table1.AddCell(cell1);
                        }
                        else
                        {
                            table1.WidthPercentage = 100;
                            PdfPCell cell1 = new PdfPCell(new Phrase("Proforma Invoice: " + this.txtQuoteNum.Text.Trim(), font));
                            cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell1.Border = 0;
                            table1.AddCell(cell1);
                        }

                        PdfPCell cell2 = new PdfPCell(new Phrase("Offer Creation Date  : " + txtCreationdate.Text, font));
                        cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell2.Border = 0;

                        table1.AddCell(cell2);


                        PdfPCell cell14 = new PdfPCell(new Phrase("Customer : " + txtCustomerName.Text, font));
                        cell14.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell14.Border = 0;
                        table1.AddCell(cell14);

                        PdfPCell cell3 = new PdfPCell(new Phrase("Offer Expiry Date  : " + txtExpirationDate.Text, font));
                        cell3.Colspan = 2;
                        cell3.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell3.Border = 0;
                        table1.AddCell(cell3);

                        string CustomerNo = "";
                        if (txtCustomerNumber.Text.Trim() == "Select")
                        {
                            CustomerNo = "";
                        }
                        else
                            CustomerNo = txtCustomerNumber.Text.Trim();


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


                        PdfPCell cell12 = new PdfPCell(new Phrase("Prepared by  : " + this.txtPreparedBy.Text, font));
                        cell12.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell12.Border = 0;
                        table1.AddCell(cell12);

                        PdfPCell cell13 = new PdfPCell(new Phrase("Currency: "+this.txtCurrency.Text, font));
                        cell13.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell13.Border = 0;
                        table1.AddCell(cell13);

                    float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
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

                        pdfDoc.Add(new Phrase("\r\n\r\n                                                                                                                       Carriage Charge : "+currencysymbol + CarriageVal, font));

                        if (chkPerforma.Checked == false)
                        {
                            pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total Excl VAT : "+ currencysymbol + Total.ToString("0.##"), font));//03-16-20 remove GM% from any output file
                        }
                        else
                        {
                            if (chkExportCustomer.Checked == true)
                            {
                                pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total : "+ currencysymbol + Total.ToString("0.##"), font));
                            }
                            else
                            {
                                float VAT = (20 * Total) / 100;
                                pdfDoc.Add(new Phrase("\r\n                                                                                                                       VAT : "+ currencysymbol + VAT.ToString("0.##"), font));
                                pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total Incl VAT : "+ currencysymbol + (Total + VAT).ToString("0.##"), font));
                            }
                        }                                                                                                                                                                                                      //}

                        if (chkPerforma.Checked == true)
                        {
                            pdfDoc.Add(new Phrase("\r\n\r\n Payment can be made by credit/debit card - please phone your card details through to Accounts\r\n", font));
                            pdfDoc.Add(new Phrase("on 01480-407074 Email:ukaccounts@wattswater.com. You can also pay by bank transfer\r\n", font));
                            pdfDoc.Add(new Phrase("However we must receive the amount in full and all bank charges are to be paid by your Company.", fontRed));
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
                        pdfDoc.Add(new Phrase("\r\n Lead Times are based on date of quotation and are subject to change’ \r\n", font));
                        pdfDoc.Add(new Phrase("\r\n This is subjected to the standard Watts UK Terms and Conditions as attached \r\n", font));
                        if (chkPerforma.Checked == false)
                        {
                            pdfDoc.Add(new Phrase("\r\n When Placing a Purchase Order could you please include the Quotation Number for our Reference \r\n", font));
                        }



                        htmlparser.Parse(sr);
                        writer.CloseStream = false;
                        pdfDoc.Close();

                        //byte[] bytes = memoryStream.ToArray();
                        memoryStream.Position = 0;
                        if (chkPerforma.Checked == true)
                        {
                            string Customer = "";
                            if (txtCustomerName.Text.Contains("SUNDRY"))
                            {
                                string[] strSundryCust = txtCustName.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                Customer = strSundryCust[0];
                            }
                            else
                            {
                                Customer = txtCustomerName.Text;
                            }
                            // WriteProformatoExcel(DateTime.Now.ToString("dd/MM/yyyy"), QuoteNumber, Customer, Total.ToString("0.##"));

                            SavePDFFile(@"C:\UK_RPAQuoteTool_Deploy\App_Data\Proformas\" + QuoteNumber + ".pdf", memoryStream);
                        }



                        //create pdf ends
                        SmtpClient smtpClient = new SmtpClient("smtp.watts.com");
                        smtpClient.UseDefaultCredentials = true;
                        string MailTo1 = txtCustEmail.Text.ToString();

                        string MailTo2 = ConfigurationManager.AppSettings["Email" + Preparedby];
                        MailMessage mail1 = new MailMessage("ukquotations@wattswater.com", MailTo2);
                        mail1.Subject = QuoteNumber + " is approved";
                        mail1.IsBodyHtml = true;
                        if (chkPerforma.Checked == false)
                        {
                            mail1.Body = "Your Quote " + QuoteNumber + " is Approved and sent to the customer.";
                        }
                        else
                        {
                            mail1.Body = "Your Proforma " + QuoteNumber + " is Approved and sent to the customer.";
                        }
                        MailAddress copy = new MailAddress(ConfigurationManager.AppSettings["EmailAlan Fahy"]); //choprad send email to admin 2/4/20
                        mail1.Bcc.Add(copy);
                        mail1.CC.Add(txtSPEmail.Text);

                        smtpClient1.Send(mail1);

                        MailMessage mail = new MailMessage("ukquotations@wattswater.com", MailTo1);
                        mail.Subject = "Watts Industries UK Quote-" + QuoteNumber;
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



                        MailAddress copy1 = new MailAddress(ConfigurationManager.AppSettings["EmailAlan Fahy"]); //choprad send email to admin 2/4/20
                        mail.Bcc.Add(copy1);
                        mail.CC.Add(new MailAddress(MailTo2));
                        mail.CC.Add("rpa@wattswater.com");
                        mail.CC.Add(txtSPEmail.Text);
                        smtpClient.Send(mail);

                        objCreateQuoteBAL.UpdateEmail(QuoteNumber);


                    

                    Response.Redirect("Dashboard.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Quote is approved');window.location ='Dashboard.aspx';",
                   true);
                }
            }
        }
        private void SaveApprovedQuoteDetails(string status)
        {
            //Get Control details
            string QuoteNumber = txtQuoteNum.Text;
            string CustomerName = txtCustomerName.Text;
            string CustomerNumber = txtCustomerNumber.Text;
            //string CustomerBranch = drpCustBranch.SelectedItem.Text;
            string CustomerBranch = string.Empty;
            string SundryBranch = string.Empty;
            if (drpCustBranch.SelectedItem.Text != string.Empty && drpCustBranch.SelectedItem.Text != "SUNDRY BRANCH")
            {
                CustomerBranch = drpCustBranch.SelectedItem.Text;
            }
            else
            {
                CustomerBranch = "SUNDRY BRANCH";
                SundryBranch = txtSundryBranch.Text;
            }
            string CustomerEmail = txtCustEmail.Text;
            string CustomerPhone = txtCustPhone.Text;
            string ProjectName = txtProjectName.Text;
            string OppurtunityId = txtOppurtunityId.Text;
            //string PaymentTerms = txtPaymentTerms.Text;
            // string PartialDelivery = txtPartialDelivery.Text;
            string Currency = txtCurrency.Text;
            string PreparedBy = txtPreparedBy.Text;
            string PreparedByEmail = txtSEEMail.Text;
            string PreparedByPhone = txtSEPhone.Text;
            string SalesPerson = txtSalesPerson.Text;
            string SalesPersonEmail = txtSPEmail.Text;
            string SalesPersonPhone = txtSPPhone.Text;
            string UserRole = (string)Session["UserRole"];
            //if (UserRole == "Admin")
            //{
            //    PreparedBy = "On Behalf Of " + PreparedBy;
            //}

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

            DateTime CreationDate = DateTime.ParseExact(txtCreationdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
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
            string AddDiscount = "";
            string UnitPriceAfterDiscount = "";
            string TotalPriceAfterDiscount = "";
            float GM = 0;
            string CostPrice = "";
            //string Country = txtCustomerCountry.Text;
            string Version = txtQuoteNum.Text;
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(Version))
            {
                Version = txtQuoteNum.Text + "_0";
            }
            // string SerialNo = ViewState["SerialNo"].ToString();
            //Delete existing records
            float AverageUnitPrice = 0;
            float AverageDiscount = 0;
            float AverageGM = 0;
            float TotalCost = 0;
            float AvgCost = 0;
            string Comments = txtComments.Text;
            int GMFlag = 0;

            DataTable dtApprovals = new DataTable();
            dtApprovals = obj.GetApprovaldata(QuoteNumber);
            string Approval1 = "";
            //string Approval2 = "";
            string Approval3 = "";

            if (dtApprovals.Rows.Count > 0)
            {
                Approval1 = dtApprovals.Rows[0]["approval1"].ToString();
                //Approval2 = dtApprovals.Rows[0]["approval2"].ToString();
                Approval3 = dtApprovals.Rows[0]["approval3"].ToString();
            }

            obj.DeleteExistingQuote(QuoteNumber);

            foreach (GridViewRow gr in grdQuote.Rows)
            {

                // Platform = gr.Cells[1].Text;
                if (gr.Cells[2].Text.Contains("&") == false && gr.Cells[2].Text.Contains("amp") == false && gr.Cells[2].Text.Contains("#160;") == false)
                {
                    ProductFamily = gr.Cells[2].Text;
                }
                //ProductFamily = gr.Cells[1].Text;
                if (gr.Cells[3].Text.Contains("&") == false && gr.Cells[3].Text.Contains("amp") == false && gr.Cells[3].Text.Contains("#160;") == false)
                {
                    ItemNo = gr.Cells[3].Text;
                }
                //ItemNo = gr.Cells[2].Text;
                TextBox PartNoTextbox = (TextBox)gr.FindControl("txtPartNo");
                PartNo = PartNoTextbox.Text;

                if (PartNo.ToString().ToUpper().Contains("SUNDRY"))
                {
                    blnSundryItem = true;
                }

                TextBox DescTextbox = (TextBox)gr.FindControl("txtDesc");
                Desc = DescTextbox.Text;
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

                /////////////////////////////////////////////

                if (gr.Cells[8].Text.Contains("&") == false && gr.Cells[8].Text.Contains("amp") == false && gr.Cells[8].Text.Contains("#160;") == false)
                {
                    StockAvailable = gr.Cells[8].Text;
                }





                //////////////////////////////////////////////




                if (gr.Cells[9].Text.Contains("&") == false && gr.Cells[9].Text.Contains("amp") == false && gr.Cells[9].Text.Contains("#160;") == false)
                {
                    MOQ = gr.Cells[9].Text;
                }
                // MOQ = gr.Cells[6].Text;
                TextBox LeadTimetext = (TextBox)gr.FindControl("txtLeadTime");
                LeadTime = LeadTimetext.Text;
                //LeadTime = gr.Cells[7].Text;
                if (gr.Cells[11].Text.Contains("&") == false && gr.Cells[11].Text.Contains("amp") == false && gr.Cells[11].Text.Contains("#160;") == false)
                {
                    SafetyStock = gr.Cells[11].Text;
                }
                //SafetyStock = gr.Cells[8].Text;

                if (gr.Cells[12].Text.Contains("&") == false && gr.Cells[12].Text.Contains("amp") == false && gr.Cells[12].Text.Contains("#160;") == false)
                {
                    Weight = gr.Cells[12].Text;
                }


                if (gr.Cells[13].Text.Contains("&") == false && gr.Cells[13].Text.Contains("amp") == false && gr.Cells[13].Text.Contains("#160;") == false)
                {
                    ListPrice = gr.Cells[13].Text;
                }
                //ListPrice = gr.Cells[9].Text;
                if (gr.Cells[14].Text.Contains("&") == false && gr.Cells[14].Text.Contains("amp") == false && gr.Cells[14].Text.Contains("#160;") == false)
                {
                    Discount = gr.Cells[14].Text;
                }
                // Discount = gr.Cells[10].Text;

                TextBox UnitPriceTextbox = (TextBox)gr.FindControl("txtUnitPrice");
                UnitPrice = UnitPriceTextbox.Text;



                TextBox DiscountTextbox = (TextBox)gr.FindControl("txtDiscount");
                AddDiscount = DiscountTextbox.Text.Trim();
                UnitPriceAfterDiscount = gr.Cells[17].Text;
                TotalPriceAfterDiscount = gr.Cells[18].Text;
                float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                if (PartNo.ToString().ToUpper().Contains("SUNDRY"))
                {
                    CostPrice = "0";
                }
                else
                {
                    CostPrice = objCreateQuoteBAL.getCostPrice(PartNo);

                    if (CostPrice != string.Empty)
                    {
                        if (txtCurrency.Text == "EUR")
                        {
                            CostPrice = (float.Parse(CostPrice) * rate).ToString();
                        }
                    }

                    if (CostPrice == string.Empty)
                    {
                        DataTable dtICO = new DataTable();
                        dtICO = objCreateQuoteBAL.GetICOItem(PartNo, string.Empty);
                        CostPrice = dtICO.Rows[0]["CostPrice"].ToString();
                        if (txtCurrency.Text == "EUR")
                        {
                            CostPrice = (float.Parse(CostPrice) * rate).ToString();
                        }
                    }
                }
                //Matrix Calculation
                float ConvertedUnitPrice = 0;
                float ConvertedDiscount = 0;
                float ConvertedGM = 0;
                // float ConvertedStdCost = 0;

                if (TotalPriceAfterDiscount.Trim() != string.Empty && TotalPriceAfterDiscount.Trim() != "0")
                {
                    ConvertedUnitPrice = float.Parse(TotalPriceAfterDiscount);
                }
                if (AddDiscount.Trim() != string.Empty && AddDiscount.Trim() != "0")
                {
                    ConvertedDiscount = float.Parse(AddDiscount);
                }

                TotalCost = float.Parse(CostPrice) * QTY;


                GM = ((ConvertedUnitPrice - TotalCost) * 100) / ConvertedUnitPrice;



                AverageUnitPrice = AverageUnitPrice + ConvertedUnitPrice;
                AverageDiscount = AverageDiscount + ConvertedDiscount;
                AvgCost = AvgCost + TotalCost;

                string isPerforma = string.Empty;
                if (chkPerforma.Checked == true)
                {
                    isPerforma = "Proforma";
                }
                else
                {
                    isPerforma = "Quote";
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

                obj.SaveQuote(QuoteNumber, CustomerName, CustomerNumber, CustomerBranch, CustomerEmail, CustomerPhone, ProjectName, OppurtunityId, string.Empty, string.Empty, Currency, PreparedBy, PreparedByEmail, PreparedByPhone, SalesPerson, SalesPersonEmail, SalesPersonPhone, ProductFamily, ItemNo, PartNo, Desc, QTY, MOQ, LeadTime, AvailableQty, Weight, SafetyStock, ListPrice, Discount, UnitPrice, AddDiscount, UnitPriceAfterDiscount, TotalPriceAfterDiscount, GM.ToString("0,0.00"), status, CreationDate, ExpirationDate, CarriageCharge, Version, Comments, TxtGrandTotal.Text, string.Empty, CostPrice, SundryBranch, isPerforma, txtUpdateStatusCmt.Text, ExportFlag, txtVAT.Text,StockAvailable);

            }


            AverageGM = ((AverageUnitPrice - AvgCost) * 100) / AverageUnitPrice;


            AverageDiscount = AverageDiscount / grdQuote.Rows.Count;

            if ((AverageUnitPrice > 5000 || AverageGM < 25 || blnSundryItem == true) && (UserName != "Richard Kleiser"))
            {
                GMFlag = 1;
            }



            if (UserRole == "Sales Manager")
            {
                Approval1 = "Approved";
                //Approval2 = "NA";
                //Approval3 = "NA";
            }
            else if (UserRole == "GM")
            {
                //Approval1 = "Approved";
                //Approval2 = "Approved";
                Approval3 = "Approved";
            }


            obj.UpdateMatrixDetails(Approval1, Approval3, AverageGM.ToString("0,0.00"), QuoteNumber);

        }

        private void SaveQuoteDetails(string Status)
        {
            //Get Control details
            string QuoteNumber = txtQuoteNum.Text;
            string CustomerName = txtCustomerName.Text;
            string CustomerNumber = string.Empty;
            string CustomerBranch = string.Empty;
            string isPerforma = string.Empty;
            if (chkPerforma.Checked == true)
            {
                isPerforma = "Proforma";
            }
            else
            {
                isPerforma = "Quote";
            }
            string CustName = string.Empty;
            if (txtCustomerNumber.Visible == true)
            {
                CustomerNumber = txtCustomerNumber.Text;
                CustomerBranch = drpCustBranch.SelectedItem.Text;

            }
            else
            {
                CustomerNumber = "";
                CustomerBranch = "";
                CustName = txtCustomerName.Text;

            }

            string CustomerEmail = txtCustEmail.Text;
            string CustomerPhone = txtCustPhone.Text;
            string ProjectName = txtProjectName.Text;
            string OppurtunityId = txtOppurtunityId.Text;
            //string PaymentTerms = txtPaymentTerms.Text;
            //string PartialDelivery = txtPartialDelivery.Text;
            string Currency =txtCurrency.Text;
            string PreparedBy = txtPreparedBy.Text;
            string PreparedByEmail = txtSEEMail.Text;
            string PreparedByPhone = txtSEPhone.Text;
            string SalesPerson = txtSalesPerson.Text;
            string SalesPersonEmail = txtSPEmail.Text;
            string SalesPersonPhone = txtSPPhone.Text;
            string UserRole = (string)Session["UserRole"];

            if (FileUpload1.HasFile)
            {
                string folderName = @"C:\UK_RPAQuoteTool_Deploy\App_Data\AdditonalDocs";

                string pathString = System.IO.Path.Combine(folderName, QuoteNumber);
                System.IO.Directory.CreateDirectory(pathString);

                FileUpload1.SaveAs(System.IO.Path.Combine(pathString, FileUpload1.FileName));

            }

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

            // DateTime startDate = DateTime.Parse(txtCreationdate.Text);
            //DateTime expiryDate = startDate.AddMonths(1);
            //txtExpirationDate.Text = expiryDate.ToString("dd/MM/yyyy");

            DateTime CreationDate = DateTime.ParseExact(txtCreationdate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
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
            string AvailableQty="";
            string stockAvailable = "";
            string Weight = "";
            string SafetyStock = "";
            string Listprice = "";
            string Discount = "";
            string UnitPrice = "";
            string AddDiscount = "";
            string UnitPriceAfterDiscount = "";
            string TotalPriceAfterDiscount = "";
            float GM = 0;
            string CostPrice="";
            //string Country = txtCustomerCountry.Text;
            string Version = txtQuoteNum.Text;
            int SalesManagerFlag = 0;
           
            int GMFlag = 0;
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(Version))
            {
                Version = txtQuoteNum.Text+"_0";
            }
            // string SerialNo = ViewState["SerialNo"].ToString();
            //Delete existing records
            float AverageUnitPrice = 0;
            float AverageDiscount = 0;
            float AverageGM = 0;
            float AvgCost = 0; //added by Divya
            float TotalCost = 0;
            string Comments = txtComments.Text;

            DataTable dtApprovals = new DataTable();
            dtApprovals = obj.GetApprovaldata(QuoteNumber);
            string Approval1 = "";
            //string Approval2 = "";
            string Approval3 = "";

            if (dtApprovals.Rows.Count > 0)
            {
                Approval1 = dtApprovals.Rows[0]["approval1"].ToString();
                //Approval2 = dtApprovals.Rows[0]["approval2"].ToString();
                Approval3 = dtApprovals.Rows[0]["approval3"].ToString();
            }

            obj.DeleteExistingQuote(QuoteNumber);

            foreach (GridViewRow gr in grdQuote.Rows)
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
                //ItemNo = gr.Cells[2].Text.Trim();
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
                //Desc = gr.Cells[4].Text;
                TextBox DescTextbox = (TextBox)gr.FindControl("txtDesc");
                Desc = DescTextbox.Text;

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


                ///////////////////////////////
                if (gr.Cells[8].Text.Contains("&") == false && gr.Cells[8].Text.Contains("amp") == false && gr.Cells[8].Text.Contains("#160;") == false)
                {
                    stockAvailable = gr.Cells[8].Text;
                }
                else
                    stockAvailable = String.Empty;



                ////////////////////////////////


                if (gr.Cells[9].Text.Contains("&") == false && gr.Cells[9].Text.Contains("amp") == false && gr.Cells[9].Text.Contains("#160;") == false)
                {
                    MOQ = gr.Cells[9].Text;
                }
                else
                    MOQ = String.Empty;
                //MOQ = gr.Cells[6].Text;

                TextBox LeadTimeTextbox = (TextBox)gr.FindControl("txtLeadTime");
                LeadTime = LeadTimeTextbox.Text;

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
                    Listprice = gr.Cells[13].Text;
                }
                else
                    Listprice = String.Empty;
                //ListPrice = gr.Cells[9].Text;

                if (gr.Cells[14].Text.Contains("&") == false && gr.Cells[14].Text.Contains("amp") == false && gr.Cells[14].Text.Contains("#160;") == false)
                {
                    Discount = gr.Cells[14].Text;
                }
                else
                    Discount = String.Empty;

               
                TextBox UPTextbox = (TextBox)gr.FindControl("txtUnitPrice");
                UnitPrice = UPTextbox.Text;
                

                TextBox DiscountTextbox = (TextBox)gr.FindControl("txtDiscount");
                AddDiscount = DiscountTextbox.Text.Trim();
                UnitPriceAfterDiscount = gr.Cells[17].Text;
                TotalPriceAfterDiscount = gr.Cells[18].Text;
                float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                if (PartNo.ToString().ToUpper().Contains("SUNDRY"))
                {
                    CostPrice = "0";
                }
                else
                {
                    CostPrice = objCreateQuoteBAL.getCostPrice(PartNo);

                    if (CostPrice != string.Empty)
                    {
                        if (txtCurrency.Text == "EUR")
                        {
                            CostPrice =(float.Parse(CostPrice) * rate).ToString();
                        }
                    }

                    if (CostPrice == "")
                    {
                        DataTable dtICO = new DataTable();
                        dtICO = objCreateQuoteBAL.GetICOItem(PartNo, string.Empty);
                        CostPrice = dtICO.Rows[0]["CostPrice"].ToString();
                        if (txtCurrency.Text == "EUR")
                        {
                            CostPrice = (float.Parse(CostPrice) * rate).ToString();
                        }
                    }
                    
                }
                

                //Matrix Calculation
                float ConvertedUnitPrice = 0;
                float ConvertedDiscount = 0;
                float ConvertedGM = 0;
                

                if (TotalPriceAfterDiscount.Trim() != string.Empty && TotalPriceAfterDiscount.Trim() != "0")
                {
                    ConvertedUnitPrice = float.Parse(TotalPriceAfterDiscount);
                }
                if (AddDiscount.Trim() != "" && AddDiscount.Trim() != "0")
                {
                    ConvertedDiscount = float.Parse(AddDiscount);
                }

              
              
                    TotalCost = float.Parse(CostPrice) * QTY;
               
                    GM = ((ConvertedUnitPrice - TotalCost) * 100) / ConvertedUnitPrice;


                if (PartNo.ToString().ToUpper().Contains("SUNDRY"))
                {
                    //ConvertedUnitPrice = 0;
                    TotalCost = 0;
                }
                AverageUnitPrice = AverageUnitPrice + ConvertedUnitPrice;
                AverageDiscount = AverageDiscount + ConvertedDiscount;

                AvgCost = AvgCost + TotalCost;
                string SundryBranch = string.Empty;
                if (txtSundryBranch.Text!="")
                {
                    SundryBranch = txtSundryBranch.Text;
                    
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

                obj.SaveQuote(QuoteNumber, CustomerName, CustomerNumber,CustomerBranch,CustomerEmail,CustomerPhone, ProjectName, OppurtunityId, string.Empty, string.Empty, Currency, PreparedBy,PreparedByEmail,PreparedByPhone,SalesPerson,SalesPersonEmail,SalesPersonPhone, ProductFamily,ItemNo, PartNo, Desc, QTY, MOQ, LeadTime,AvailableQty,Weight, SafetyStock,Listprice,Discount, UnitPrice, AddDiscount, UnitPriceAfterDiscount, TotalPriceAfterDiscount, GM.ToString("0,0.00"), Status, CreationDate, ExpirationDate, CarriageCharge,Version, Comments,TxtGrandTotal.Text,string.Empty,CostPrice,SundryBranch,isPerforma,txtUpdateStatusCmt.Text,ExportFlag,txtVAT.Text,stockAvailable);
                
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

                    objCreateQuoteBAL.SaveSundryCust(QuoteNumber, strCustName, strCustAdd);
                }
            }

                      
                AverageGM = ((AverageUnitPrice - AvgCost) * 100) / AverageUnitPrice;
          

            AverageDiscount = AverageDiscount / grdQuote.Rows.Count;




            if ((AverageUnitPrice >= 2500 && AverageUnitPrice <= 5000 && UserName != "Richard Kleiser") || (AverageUnitPrice > 5000 && UserName == "Richard Kleiser"))
            {
                SalesManagerFlag = 1;
            }
            else if ((AverageUnitPrice > 5000 || AverageGM < 25 || blnSundryItem == true) && (UserName != "Richard Kleiser"))
            {
                GMFlag = 1;
            }


            if (GMFlag == 1)
            {
                if (UserRole == "Sales Engineer")
                {
                    Approval1 = "Pending";
                    Approval3 = "Pending";
                }
                else if (UserRole == "Sales Manager")
                {
                    Approval1 = "Approved";
                    Approval3 = "Pending";

                }
            }
            else if (SalesManagerFlag == 1)
            {
                if ((UserRole == "Sales Engineer" && UserName != "Richard Kleiser") || UserRole == "Admin")
                {
                    Approval1 = "Pending";
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
                    //Approval2 = "NA";
                    Approval3 = "NA";
                }
            }
            else
            {
                Approval1 = "NA";
                //Approval2 = "NA";
                Approval3 = "NA";
            }

            
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


            //obj.SaveQuote(QuoteNumber, CustomerName, CustomerNumber, ProjectName, OppurtunityId, PaymentTerms, PartialDelivery, Currency, PreparedBy, CreationDate, ExpirationDate, IncoTerms, Platform, ProductGroup, PartNo, Desc, QTY, MOQ, LeadTime, QtyImpact, UnitPrice, Discount, UnitPriceAfterDiscount, TotalPriceAfterDiscount, GM, Status, Country, Version, StandardCost, Comments);
            obj.UpdateMatrixDetails(Approval1,Approval3,AverageGM.ToString("0,0.00"), QuoteNumber); 


        }



        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            grdQuote.Columns[0].Visible = false;

            string Carriage = "";
            if(drpCarriageCharges.SelectedItem.Text.Contains("Add New"))
            {
                Carriage = txtCarriage.Text;
            }
            else
            {
                Carriage = drpCarriageCharges.SelectedItem.Text;

            }
          
            grdQuote.Columns[18].Visible = false;
            string text = "<b>"+" Quote Number :"+"  </b>" + txtQuoteNum.Text + "<br/>" + "<b>" + " Customer : " + "</b>" + txtCustomerName.Text
                        + "<br/>" + "<b>" + " Customer Number :" + "</b>" + txtCustomerNumber.Text +"<br/>"+"<b>"+"Customer Branch : "+"</b>"+drpCustBranch.SelectedItem.Text
                        + "<br/>" + "<b>" + " Customer Email :" + "</b>" + txtCustEmail.Text + "<br/>" + "<b>" + " Customer Phone : " + "</b>" + txtCustPhone.Text
                        + "<br/>" + "<b>" + " Carriage Charges : " + "</b>" + Carriage + "<br/>" + "<b>" + " Project Name : " + "</b>" + txtProjectName.Text + "<br/>" + "<b>" + " Oppurtunity Id :" + "</b>" + txtOppurtunityId.Text
                         + "<br/>" + "<b>" + " Currency :" + "</b>" +txtCurrency.Text + "<br/>" 
                         + "<b>" + "Prepared By : " + "</b>" + txtPreparedBy.Text + "<br/>" + "<b>" + " Email: " + "</b>" + txtSEEMail.Text + "<br/>" + "<b>" + " Phone :" + "</b>" + txtSEPhone.Text + "<br/>"
                          + "<b>" + "Sales Person : " + "</b>" + txtSalesPerson.Text + "<br/>" + "<b>" + " Email: " + "</b>" + txtSPEmail.Text + "<br/>" + "<b>" + " Phone :" + "</b>" + txtSPPhone.Text + "<br/>"
                         + "<br/>"+ "<b>" + " Creation Date : " + "</b>" + txtCreationdate.Text + "<br/>" + "<b>" + " Expiration Date :" + "</b>" + "</b>" + txtExpirationDate.Text + "<br/>" + "<b>" + " Version : " + "</b>" + txtVersion.Text+ "<br/>" + "<br/>" + "<br/>";

            //Response.Clear();
            //Response.AddHeader("content-disposition", "attachment; filename = Quote.xls");
            //Response.ContentType = "application/vnd.xls";
            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);


            ////divExport.RenderControl(htmlWrite);
            //grdQuote.RenderControl(htmlWrite);
            //Response.Write(text);
            //Response.Write(dt);
            ////pdfDoc.Add(table);
            ////tblTotal.RenderControl(htmlWrite);
            //Response.Write(stringWrite.ToString());
            //string totalText = "<br/>" + "<b>" + " Grand Total :" + "  </b>" + TxtGrandTotal.Text + "<br/>";
            //Response.Write(totalText);
            ////Response.Write(table);
            //Response.Write("\n");
            //Response.End();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                

               
                foreach (TableCell cell in grdQuote.HeaderRow.Cells)
                {
                    cell.BackColor = grdQuote.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdQuote.Rows)
                {
                    
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdQuote.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdQuote.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                        List<Control> controls = new List<Control>();

                        //Add controls to be removed to Generic List
                        foreach (Control control in cell.Controls)
                        {
                            controls.Add(control);
                        }

                        //Loop through the controls to be removed and replace then with Literal
                        foreach (Control control in controls)
                        {
                            switch (control.GetType().Name)
                            {                               
                                case "TextBox":
                                    cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                    break;                                
                            }
                            cell.Controls.Remove(control);
                        }
                    }
                }

                Response.Write(text);
                grdQuote.RenderControl(hw);

                ////style to format numbers to string
                //string style = @"<style> .textmode { } </style>";
                //Response.Write(style);
                Response.Output.Write(sw.ToString());
                string totalText = string.Empty;               
                
               // if (UserRole == "Sales Engineer") //03-11-20 to hide GM% for SE in excel
                //{
                    totalText = "<br/>" + "<b>" + " Grand Total :" + "  </b>" + TxtGrandTotal.Text + "<br/>";  //03-16-20 remove GM% from all outout files
                //}
                //else
                //{
                  //  totalText = "<br/>" + "<b>" + " Grand Total :" + "  </b>" + TxtGrandTotal.Text + "<b>" + " Total GM% :" + "  </b>" + txtTotalGM.Text + "<br/>";
                //}
                Response.Write(totalText);
                //Response.Write(table);
                Response.Write("\n");
                Response.Flush();
                Response.End();

            }
        }
        private iTextSharp.text.Font font = FontFactory.GetFont("Times Roman", 11, iTextSharp.text.Font.TIMES_ROMAN);
        private iTextSharp.text.Font fontAdd = FontFactory.GetFont("Times Roman", 8, iTextSharp.text.Font.NORMAL);
        private iTextSharp.text.Font fontRed = FontFactory.GetFont("Times Roman", 11, Color.RED);
        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
           
            DataTable dt = new DataTable();
            if (grdQuote.Rows.Count > 0)
            {

                for (int i = 0; i < grdQuote.Columns.Count; i++)
                {
                    string headername = grdQuote.Columns[i].HeaderText;
                    
                    dt.Columns.Add(headername);
                }
                foreach (GridViewRow row in grdQuote.Rows)
                {
                    DataRow dr = dt.NewRow();

                    for (int j = 0; j < grdQuote.Columns.Count; j++)
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
                   // TextBox Discount = (TextBox)row.FindControl("txtDiscount");
                    //dr["AdditionalDiscount"] = Discount.Text;
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
                else if(cellText == "QTY")
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

                    
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                   
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

            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 10f, 50f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter writer=PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
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
            pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               WATTS INDUSTRIES UK LTD", fontAdd));
            pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               COLMWORTH BUSINESS PARK", fontAdd));
            pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               EATON SOCON", fontAdd));
            pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               ST NEOTS PE19 8YX, UK. ", fontAdd));
            pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               Tel : +44 (0) 1480 407074 \r\n\r\n", fontAdd));

            if (chkPerforma.Checked == true )
            {
                pdfDoc.Add(new Phrase("\r\n                                                            Proforma Invoice\r\n", font));
                if (chkExportCustomer.Checked == true)
                {
                    if (txtVAT.Text != "")
                    {
                        pdfDoc.Add(new Phrase("\r\n                                                  VAT No. " + txtVAT.Text + "\r\n", font));
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
                if (txtCustomerNumber.Visible == true)
                {

                    if (drpCustBranch.SelectedItem.Text == "Select" || drpCustBranch.SelectedItem.Text == "")
                    {
                        dtAddress = objCreateQuoteBAL.GetCustNameAddress(string.Empty, txtCustomerNumber.Text);

                    }
                    else if (drpCustBranch.SelectedItem.Text != "SUNDRY BRANCH")
                    {
                        dtAddress = objCreateQuoteBAL.GetCustNameAddress(drpCustBranch.SelectedItem.Text, txtCustomerNumber.Text);
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

                if (txtCustomerName.Text.Contains("SUNDRY") == false)
                {
                    DataTable dtCustAdd = new DataTable();
                    dtCustAdd = objCreateQuoteBAL.GetCustomerAddress(txtCustomerName.Text);


                    //pdfDoc.Add(new Phrase("\r\n" + "Invoice Address:".PadRight(115- "Invoice Address:".Length) +"Delivery Address:", font));
                    if (drpCustBranch.SelectedItem.Text.Contains("SUNDRY"))
                    {
                        string[] strSundryBranch = txtSundryBranch.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        int i = strSundryBranch.Length;

                        PdfPCell cellI1 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
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



                    }
                    else if (drpCustBranch.SelectedItem.Text == "Select" || drpCustBranch.SelectedItem.Text == "")
                    {
                        PdfPCell cellI1 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
                        cellI1.HorizontalAlignment = Element.ALIGN_LEFT;
                        cellI1.Border = 0;
                        tableAdd.AddCell(cellI1);

                        PdfPCell cellI2 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
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
                        dtAddress = objCreateQuoteBAL.GetCustNameAddress(drpCustBranch.SelectedItem.Text, txtCustomerNumber.Text);

                        PdfPCell cellI1 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
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


                }

                pdfDoc.Add(tableAdd);
            }
            pdfDoc.Add(new Phrase("\r\n \r\n\r\n", font));

            PdfPTable table1 = new PdfPTable(2);

            if (chkPerforma.Checked == false)
            {
                table1.WidthPercentage = 100;
                PdfPCell cell1 = new PdfPCell(new Phrase("Quotation Ref: " + this.txtQuoteNum.Text.Trim(), font));
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                cell1.Border = 0;
                table1.AddCell(cell1);
            }
            else
            {
                table1.WidthPercentage = 100;
                PdfPCell cell1 = new PdfPCell(new Phrase("Proforma Invoice: " + this.txtQuoteNum.Text.Trim(), font));
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                cell1.Border = 0;
                table1.AddCell(cell1);
            }
            
            
           
            PdfPCell cell2 = new PdfPCell(new Phrase("Offer Creation Date  : " + txtCreationdate.Text, font));
            cell2.HorizontalAlignment = Element.ALIGN_LEFT;
            cell2.Border = 0;
           
            table1.AddCell(cell2);

            
            PdfPCell cell14 = new PdfPCell(new Phrase("Customer : "+txtCustomerName.Text,font));
            cell14.HorizontalAlignment = Element.ALIGN_LEFT;
            cell14.Border = 0;
            table1.AddCell(cell14);

            PdfPCell cell3 = new PdfPCell(new Phrase("Offer Expiry Date  : " + txtExpirationDate.Text, font));
            cell3.Colspan = 2;
            cell3.HorizontalAlignment = Element.ALIGN_LEFT;
            cell3.Border = 0;
            table1.AddCell(cell3);

            string CustomerNo = "";
            if (txtCustomerNumber.Text.Trim() == "Select")
            {
                CustomerNo = "";
            }
            else
                CustomerNo = txtCustomerNumber.Text.Trim();

            PdfPCell cell4 = new PdfPCell(new Phrase("Customer No : " + CustomerNo,font));
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

            PdfPCell cell10= new PdfPCell(new Phrase("Project Name  : " + this.txtProjectName.Text.Trim(), font));
            cell10.HorizontalAlignment = Element.ALIGN_LEFT;
            cell10.Border = 0;
            table1.AddCell(cell10);

            PdfPCell cell11 = new PdfPCell(new Phrase("Customer Reference : " + this.txtOppurtunityId.Text.Trim(), font));
            cell11.HorizontalAlignment = Element.ALIGN_LEFT;
            cell11.Border = 0;
            table1.AddCell(cell11);

          
            PdfPCell cell12 = new PdfPCell(new Phrase("Prepared by  : " + this.txtPreparedBy.Text, font));
            cell12.HorizontalAlignment = Element.ALIGN_LEFT;
            cell12.Border = 0;
            table1.AddCell(cell12);

            PdfPCell cell13 = new PdfPCell(new Phrase("Currency: "+this.txtCurrency.Text,font));
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

           pdfDoc.Add(new Phrase("\r\n\r\n                                                                                                                       Carriage Charge : "+currencysymbol + CarriageVal, font));

            if (chkPerforma.Checked == false)
            {
                pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total Excl VAT : "+ currencysymbol + Total.ToString("0.##"), font));//03-16-20 remove GM% from any output file
            }
            else
            {
                if (chkExportCustomer.Checked == true)
                {
                    pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total : "+ currencysymbol + Total.ToString("0.##"), font));
                }
                else
                {
                    float VAT = (20 * Total) / 100;
                    pdfDoc.Add(new Phrase("\r\n                                                                                                                       VAT : "+ currencysymbol + VAT.ToString("0.##"), font));
                    pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total Incl VAT : "+ currencysymbol + (Total + VAT).ToString("0.##"), font));
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



            htmlparser.Parse(sr);
            pdfDoc.Close();

            Response.Write(pdfDoc);

            Response.End();
         


                      

        }

       

        protected void btnConfirmQuote_Click(object sender, EventArgs e)
        {
            Page.Validate("Confirm");
            if(Page.IsValid)
            {

                //create pdf

                string QuoteNo = txtQuoteNum.Text;
            SmtpClient smtpClient = new SmtpClient("smtp.watts.com");
            smtpClient.UseDefaultCredentials = true;
            obj.ConfirmQuote(QuoteNo,drpStatus.SelectedItem.Text,txtUpdateStatusCmt.Text);

                       
            string MailTo = txtSEEMail.Text;
            MailMessage mail = new MailMessage("ukquotations@wattswater.com", MailTo);
            mail.Subject = QuoteNo + " "+ drpStatus.SelectedItem.Text.ToString(); //divya added space
             

                if (txtSPEmail.Text != string.Empty)
                {
                    mail.CC.Add(new MailAddress(txtSPEmail.Text));
                }
                // string QuoteURL = "https://rpaquotationtooldubai.wattswater.com/Quote.aspx?QuoteNo=" + QuoteNumber + "&Status=" + CurrentStatus + "&UserName=" + UserName + "&UserRole=" + Role;
                string CurrentStatus = drpStatus.SelectedItem.Text;
            string Role = "Sales Engineer"; 
            string site = "ukrpaquotetool.wattswater.com/Quote.aspx?QuoteNo=" + QuoteNo + "&Status=" + CurrentStatus + "&UserName=" + UserName + "&UserRole=" + Role;
            //string QuoteURL = "https://rpaquotationtooldubai.wattswater.com/login";
            //mail.Body = @"New Quote is pending for Approval. Click the link below to approve the quote  < a href = ""https://"+site+"> Quote </a>";
            CurrentStatus = CurrentStatus.Replace(" ", "%20");
            UserName = UserName.Replace(" ", "%20");
            Role = Role.Replace(" ", "%20");
           
                // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), QuoteNo+".pdf", "application/pdf"));
                

               

            smtpClient.Send(mail);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Quote is confirmed');window.location ='Dashboard.aspx';", true);
                // Response.Redirect("Dashboard.aspx");

            }
        }

        //protected void grdQuote_PageIndexChanged(object sender, EventArgs e)
        //{

        //    grdQuote.PageIndex = e.new;
        //    BindData();
        //}

        private void BindData()
        {
            ////Read existing table data
            DataTable dt = new DataTable();
            if (grdQuote.Rows.Count > 0)
            {

                for (int i = 0; i < grdQuote.Columns.Count; i++)
                {
                    string headername = grdQuote.Columns[i].HeaderText;
                    if (headername == "Discount%")
                    {
                        headername = "Discount";
                    }
                    else if (headername == "GM%")
                    {
                        headername = "GM";
                    }
                    dt.Columns.Add(headername);
                }
                foreach (GridViewRow row in grdQuote.Rows)
                {
                    DataRow dr = dt.NewRow();



                    for (int j = 0; j < grdQuote.Columns.Count; j++)
                    {
                        dr[j] = Server.HtmlDecode(row.Cells[j].Text);

                    }
                    TextBox PartNum = (TextBox)row.FindControl("txtPartNumber");
                    dr["PartNo"] = PartNum.Text;

                    TextBox Desc = (TextBox)row.FindControl("txtDesc");
                    dr["Description"] = Desc.Text;

                    TextBox QTY = (TextBox)row.FindControl("txtQTY");
                    dr["QTY"] = QTY.Text;

                    TextBox LeadTime = (TextBox)row.FindControl("txtLeadTime");
                    dr["LeadTime"] = LeadTime.Text;

                    TextBox Dist = (TextBox)row.FindControl("txtDiscount");
                    dr["Discount"] = Dist.Text;
                    dt.Rows.Add(dr);
                }
            }
            grdQuote.Columns[15].Visible = true;
            ViewState["CurrentTable"] = dt;
            grdQuote.DataSource = dt; // bind new datatable to grid
           // grdQuote.Columns[10].HeaderText = "Discount%";
            //grdQuote.Columns[13].HeaderText = "GM%";
            grdQuote.DataBind();
            if (UserRole == "Sales Engineer") //03-11-20 removed SM from here per Business Request
            {
                grdQuote.Columns[15].Visible = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("Save");
            if(Page.IsValid)
            {
                if (chkPerforma.Checked == true && txtCustomerName.Text == "SUNDRY ACCOUNT" && (txtCustName.Text.Trim() == "" || txtSundryBranch.Text.Trim() == ""))
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the customer address')</script>");
                    lblMessage.Text = "Please enter the customer address";
                    return;
                }
                if (drpCarriageCharges.SelectedItem.Text.Contains("Add New") && txtCarriage.Text.Trim() == "")
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the carriage charges')</script>");
                    lblMessage.Text = "Please enter the carriage charges";
                    return;
                }


                bool blnQtyZero = false;
                string Unit_Price = "";
                    foreach (GridViewRow gr in grdQuote.Rows)
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
                    lblMessage.Text = "Please enter the carriage charges";
                        return;
                    }
                    else if (Unit_Price == "" || Unit_Price == "0.00" || Unit_Price == "0")
                    {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the Unit Price')</script>");
                    lblMessage.Text = "Please enter the Unit Price";
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
                        ////Response.Write("<script LANGUAGE='JavaScript' >alert('Quote saved successfully')</script>");
                        ///
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('" + Message + "');window.location ='Dashboard.aspx';",
                       true);
                    }
                }
                
            
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate("Submit");
            if (Page.IsValid)
            {
                if (chkPerforma.Checked == true && txtCustomerName.Text == "SUNDRY ACCOUNT" && (txtCustName.Text.Trim() == "" || txtSundryBranch.Text.Trim() == ""))
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the customer address')</script>");
                    lblMessage.Text = "Please enter the customer address";
                    return;
                }
                if (drpCarriageCharges.SelectedItem.Text.Contains("Add New") && txtCarriage.Text.Trim() == "")
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the carriage charges')</script>");
                    lblMessage.Text = "Please enter the carriage charges";
                    return;
                }
                if (drpCustBranch.SelectedItem.Text.Contains("SUNDRY") && txtSundryBranch.Text.Trim() == "")
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the carriage charges')</script>");
                    lblMessage.Text = "Please enter the Sundry Branch";
                    return;
                }


                bool blnQtyZero = false;
                string Unit_Price = "";
                foreach (GridViewRow gr in grdQuote.Rows)
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
                    lblMessage.Text = "Invalid Qty";
                    return;
                }
                else if (txtSalesPerson.Text == string.Empty || txtSPEmail.Text == string.Empty)
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Enter Sales Person details')</script>");
                    lblMessage.Text = "Enter Sales Person details";
                    return;
                }
                else if (Unit_Price == "" || Unit_Price == "0.00" || Unit_Price == "0")
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Please enter the Unit Price')</script>");
                    lblMessage.Text = "Please enter the Unit Price";
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


                    if (UserRole == "Sales Engineer")
                    {

                        //string MailTo = ConfigurationManager.AppSettings["Email" + Manager];
                        string MailTo = txtSPEmail.Text;
                        MailMessage mail = new MailMessage("ukquotations@wattswater.com", MailTo);
                        mail.Subject = QuoteNumber + "Pending Approval";
                        MailAddress copy = new MailAddress(ConfigurationManager.AppSettings["EmailAlan Fahy"]); //choprad send email to admin 2/4/20
                        mail.Bcc.Add(copy);
                        // string QuoteURL = "https://rpaquotationtooldubai.wattswater.com/Quote.aspx?QuoteNo=" + QuoteNumber + "&Status=" + CurrentStatus + "&UserName=" + UserName + "&UserRole=" + Role;
                        string CurrentStatus = "Pending Approval";
                        string Role = "Sales Manager";
                        string site = "ukrpaquotetool.wattswater.com/Quote.aspx?QuoteNo=" + QuoteNumber + "&Status=" + CurrentStatus + "&UserName=" + UserName + "&UserRole=" + Role;

                        CurrentStatus = CurrentStatus.Replace(" ", "%20");
                        UserName = UserName.Replace(" ", "%20");
                        Role = Role.Replace(" ", "%20");
                        mail.Body = "https://ukrpaquotetool.wattswater.com/Quote.aspx?QuoteNo=" + QuoteNumber + "&Status=" + CurrentStatus + "&UserName=" + UserName + "&UserRole=" + Role;


                        smtpClient.Send(mail);
                    }

                    SmtpClient smtpClient1 = new SmtpClient("smtp.watts.com");



                    string Message = "Quote submitted successfully with quote number " + QuoteNumber;


                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('" + Message + "');window.location ='Dashboard.aspx';",
                   true);


                    DataTable dtApprovals = new DataTable();
                    dtApprovals = obj.GetApprovaldata(QuoteNumber);


                    string Approval1 = "";
                    //string Approval2 = "";
                    string Approval3 = "";

                    if (dtApprovals.Rows.Count > 0)
                    {
                        Approval1 = dtApprovals.Rows[0]["approval1"].ToString();
                        //Approval2 = dtApprovals.Rows[0]["approval2"].ToString();
                        Approval3 = dtApprovals.Rows[0]["approval3"].ToString();
                    }

                    if ((Approval1 == "NA" && Approval3 == "NA") || (UserRole == "Sales Manager" && Approval1 == "Approved" && Approval3 == "NA"))
                    {
                        DataTable dt = new DataTable();
                        if (grdQuote.Rows.Count > 0)
                        {

                            for (int i = 0; i < grdQuote.Columns.Count; i++)
                            {
                                string headername = grdQuote.Columns[i].HeaderText;

                                dt.Columns.Add(headername);
                            }
                            foreach (GridViewRow row in grdQuote.Rows)
                            {
                                DataRow dr = dt.NewRow();

                                for (int j = 0; j < grdQuote.Columns.Count; j++)
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
                        /*dt.Columns.Remove(dt.Columns[0]);
                        dt.Columns.Remove(dt.Columns[0]);
                        dt.Columns.Remove(dt.Columns[0]);
                        // dt.Columns.Remove(dt.Columns[16]);
                        dt.Columns.Remove(dt.Columns[17]);
                        dt.Columns.Remove(dt.Columns[16]);
                        dt.Columns.Remove(dt.Columns[15]);

                        dt.Columns.Remove(dt.Columns[5]);
                        dt.Columns.Remove(dt.Columns[6]);
                        dt.Columns.Remove(dt.Columns[6]);
                        dt.Columns.Remove(dt.Columns[6]);
                        dt.Columns.Remove(dt.Columns[6]);
                        dt.Columns.Remove(dt.Columns[6mailto
                        dt.Columns.Remove(dt.Columns[6]);*/

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

                                if (j == 4) //if AvailableQty <=0, make it blank
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
                        pdfDoc.Add(new Phrase("\r\n                                                                                                                                                                                               WATTS INDUSTRIES UK LTD", fontAdd));
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


                                   
                                    pdfDoc.Add(new Phrase("\r\n                                                  OUR VAT No. " + txtVAT.Text + "\r\n", font));

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

                        if (txtCustomerNumber.Visible == true || txtCustomerNumber.Visible == false)
                        {

                            DataTable dtAddress = new DataTable();

                            if (drpCustBranch.SelectedItem.Text == "Select" || drpCustBranch.SelectedItem.Text == "")
                            {
                                dtAddress = objCreateQuoteBAL.GetCustNameAddress(string.Empty, txtCustomerNumber.Text);

                            }
                            else if (drpCustBranch.SelectedItem.Text != "SUNDRY BRANCH")
                            {
                                dtAddress = objCreateQuoteBAL.GetCustNameAddress(drpCustBranch.SelectedItem.Text, txtCustomerNumber.Text);
                            }

                            if (chkPerforma.Checked == false)
                            {
                                if (txtCustomerNumber.Visible == true)
                                {
                                    string Name;
                                    string Address;
                                    string City;
                                    string Zip;
                                    string Country;


                                    if (drpCustBranch.SelectedItem.Text == "Select" || drpCustBranch.SelectedItem.Text == "")
                                    {
                                        dtAddress = objCreateQuoteBAL.GetCustNameAddress(string.Empty, txtCustomerNumber.Text);

                                    }
                                    else if (drpCustBranch.SelectedItem.Text != "SUNDRY BRANCH")
                                    {
                                        dtAddress = objCreateQuoteBAL.GetCustNameAddress(drpCustBranch.SelectedItem.Text, txtCustomerNumber.Text);
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

                                if (txtCustomerName.Text.Contains("SUNDRY") == false)
                                {
                                    DataTable dtCustAdd = new DataTable();
                                    dtCustAdd = objCreateQuoteBAL.GetCustomerAddress(txtCustomerName.Text);


                                    //pdfDoc.Add(new Phrase("\r\n" + "Invoice Address:".PadRight(115- "Invoice Address:".Length) +"Delivery Address:", font));
                                    if (drpCustBranch.SelectedItem.Text.Contains("SUNDRY"))
                                    {
                                        string[] strSundryBranch = txtSundryBranch.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                        int i = strSundryBranch.Length;

                                        PdfPCell cellI1 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
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



                                    }
                                    else if (drpCustBranch.SelectedItem.Text == "Select" || drpCustBranch.SelectedItem.Text == "")
                                    {
                                        PdfPCell cellI1 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
                                        cellI1.HorizontalAlignment = Element.ALIGN_LEFT;
                                        cellI1.Border = 0;
                                        tableAdd.AddCell(cellI1);

                                        PdfPCell cellI2 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
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
                                        dtAddress = objCreateQuoteBAL.GetCustNameAddress(drpCustBranch.SelectedItem.Text, txtCustomerNumber.Text);

                                        PdfPCell cellI1 = new PdfPCell(new Phrase(txtCustomerName.Text.Trim(), font));
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


                                }

                                pdfDoc.Add(tableAdd);
                            }


                            pdfDoc.Add(new Phrase("\r\n \r\n\r\n", font));

                            PdfPTable table1 = new PdfPTable(2);

                            if (chkPerforma.Checked == false)
                            {
                                table1.WidthPercentage = 100;
                                PdfPCell cell1 = new PdfPCell(new Phrase("Quotation Ref: " + this.txtQuoteNum.Text.Trim(), font));
                                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell1.Border = 0;
                                table1.AddCell(cell1);
                            }
                            else
                            {
                                table1.WidthPercentage = 100;
                                PdfPCell cell1 = new PdfPCell(new Phrase("Proforma Invoice: " + this.txtQuoteNum.Text.Trim(), font));
                                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell1.Border = 0;
                                table1.AddCell(cell1);
                            }

                            PdfPCell cell2 = new PdfPCell(new Phrase("Offer Creation Date  : " + txtCreationdate.Text, font));
                            cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell2.Border = 0;

                            table1.AddCell(cell2);


                            PdfPCell cell14 = new PdfPCell(new Phrase("Customer : " + txtCustomerName.Text, font));
                            cell14.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell14.Border = 0;
                            table1.AddCell(cell14);

                            PdfPCell cell3 = new PdfPCell(new Phrase("Offer Expiry Date  : " + txtExpirationDate.Text, font));
                            cell3.Colspan = 2;
                            cell3.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell3.Border = 0;
                            table1.AddCell(cell3);

                            string CustomerNo = "";
                            if (txtCustomerNumber.Text.Trim() == "Select")
                            {
                                CustomerNo = "";
                            }
                            else
                                CustomerNo = txtCustomerNumber.Text.Trim();


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


                            PdfPCell cell12 = new PdfPCell(new Phrase("Prepared by  : " + this.txtPreparedBy.Text, font));
                            cell12.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell12.Border = 0;
                            table1.AddCell(cell12);

                            PdfPCell cell13 = new PdfPCell(new Phrase("Currency: "+txtCurrency.Text, font));
                            cell13.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell13.Border = 0;
                            table1.AddCell(cell13);


                            float CarriageVal = 0;
                            string currencysymbol = "£";

                            if (drpCarriageCharges.SelectedItem.Text.Contains("Add New"))
                            {
                                CarriageVal =float.Parse(txtCarriage.Text);
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

                            pdfDoc.Add(new Phrase("\r\n\r\n                                                                                                                       Carriage Charge : "+currencysymbol + CarriageVal, font));

                            if (chkPerforma.Checked == false)
                            {
                                pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total Excl VAT : "+ currencysymbol + Total.ToString("0.##"), font));//03-16-20 remove GM% from any output file
                            }
                            else
                            {
                                    if (chkExportCustomer.Checked == true)
                                    {
                                        pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total : "+ currencysymbol + Total.ToString("0.##"), font));
                                    }
                                    else
                                    {
                                        float VAT = (20 * Total) / 100;
                                        pdfDoc.Add(new Phrase("\r\n                                                                                                                       VAT : "+ currencysymbol + VAT.ToString("0.##"), font));
                                        pdfDoc.Add(new Phrase("\r\n                                                                                                                       Total Incl VAT : "+ currencysymbol + (Total + VAT).ToString("0.##"), font));
                                    }
                            }                                                                                                                                                                                                      //}

                            if (chkPerforma.Checked == true)
                            {
                                pdfDoc.Add(new Phrase("\r\n\r\n Payment can be made by credit/debit card - please phone your card details through to Accounts\r\n", font));
                                pdfDoc.Add(new Phrase("on 01480-407074 Email:ukaccounts@wattswater.com. You can also pay by bank transfer\r\n", font));
                                pdfDoc.Add(new Phrase("However we must receive the amount in full and all bank charges are to be paid by your Company.", fontRed));
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
                            pdfDoc.Add(new Phrase("\r\n Lead Times are based on date of quotation and are subject to change’ \r\n", font));
                            pdfDoc.Add(new Phrase("\r\n This is subjected to the standard Watts UK Terms and Conditions as attached \r\n", font));
                            if (chkPerforma.Checked == false)
                            {
                                pdfDoc.Add(new Phrase("\r\n When Placing a Purchase Order could you please include the Quotation Number for our Reference \r\n", font));
                            }



                            htmlparser.Parse(sr);
                            writer.CloseStream = false;
                            pdfDoc.Close();


                            memoryStream.Position = 0;

                            if (chkPerforma.Checked == true)
                            {
                                string Customer = "";
                                if (txtCustomerName.Text.Contains("SUNDRY"))
                                {
                                    string[] strSundryCust = txtCustName.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                    Customer = strSundryCust[0];
                                }
                                else
                                {
                                    Customer = txtCustomerName.Text;
                                }
                                // WriteProformatoExcel(DateTime.Now.ToString("dd/MM/yyyy"), QuoteNumber, Customer, Total.ToString("0.##"));

                                SavePDFFile(@"C:\UK_RPAQuoteTool_Deploy\App_Data\Proformas\" + QuoteNumber + ".pdf", memoryStream);
                            }

                            //create pdf ends
                            smtpClient = new SmtpClient("smtp.watts.com");
                            smtpClient.UseDefaultCredentials = true;
                            string MailTo = txtCustEmail.Text.ToString();

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
                            MailAddress copy1 = new MailAddress(ConfigurationManager.AppSettings["EmailAlan Fahy"]); //choprad send email to admin 2/4/20
                            mail.Bcc.Add(copy1);
                            mail.CC.Add(new MailAddress(ConfigurationManager.AppSettings["Email" + txtPreparedBy.Text]));
                            mail.CC.Add("rpa@wattswater.com");
                            mail.CC.Add(txtSPEmail.Text);
                            smtpClient.Send(mail);

                            objCreateQuoteBAL.UpdateEmail(QuoteNumber);

                            string MailTo2 = ConfigurationManager.AppSettings["Email" + txtPreparedBy.Text];
                            MailMessage mail1 = new MailMessage("ukquotations@wattswater.com", MailTo2);
                            mail1.Subject = QuoteNumber + " is approved";
                            mail1.Body = "Your Quote " + QuoteNumber + " is Approved and sent to the customer.";
                            MailAddress copy = new MailAddress(ConfigurationManager.AppSettings["EmailAlan Fahy"]); //choprad send email to admin 2/4/20
                            mail1.Bcc.Add(copy);
                            mail1.CC.Add(txtSPEmail.Text);
                            smtpClient.Send(mail1);

                            Response.Redirect("Dashboard.aspx");
                        }


                    }

                    //   HTMLHelper.jsAlertAndRedirect(this.Page, Message, ResolveUrl("Dashboard.aspx"));
                
            }
           
        }
        }
                 

        private void GetQuoteNumber()
        {
            string QuoteNumber = "";
            string UserName = (string)Session["UserName"];
            string ID = obj.GetQuoteNumber(UserName);
            //ViewState["SerialNo"] = QuoteNumber;
            UserName = UserName.ToString().ToUpper().Substring(0, 3);
            QuoteNumber = UserName + ID;
            txtQuoteNum.Text = QuoteNumber;
            obj.UpdateQuoteNo(QuoteNumber, ID);
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
       /*public void WriteProformatoExcel(string Date, string Proforma, string Customer, string Value)
        {
            string path = @"C:\UK_RPAQuoteTool_Deploy\App_Data\Proformas\Proforma.xlsx";
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;
            oXL.DisplayAlerts = false;
            mWorkBook = oXL.Workbooks.Open(path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            //Get all the sheets in the workbook
            mWorkSheets = mWorkBook.Worksheets;
            //Get the allready exists sheet
            mWSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)mWorkSheets.get_Item("Sheet1");
            Microsoft.Office.Interop.Excel.Range range = mWSheet1.UsedRange;
            int colCount = range.Columns.Count;
            int rowCount = range.Rows.Count;

            mWSheet1.Cells[rowCount + 1, 1] = Date;
            mWSheet1.Cells[rowCount + 1, 2] = Proforma;
            mWSheet1.Cells[rowCount + 1, 3] = Customer;
            mWSheet1.Cells[rowCount + 1, 4] = Value;

            ///mWorkBook.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault,System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
            mWorkBook.Save();
            mWorkBook.Close(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
            mWSheet1 = null;
            mWorkBook = null;
            oXL.Quit();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();


        }*/

        protected void drpGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            // string GroupName = drpGroup.SelectedItem.Text.Trim();
            string GroupName = "";
            if (GroupName == "Product Group")
            {
                GroupName = "ProductGrp";
            }
            else if (GroupName == "Platform")
            {
                GroupName = "Platform_Name";
            }
          //  DataTable dt = new DataTable();
           // dt = obj.GetGroupDetails(GroupName);
            //DrpGroupName.DataSource = dt;
            //DrpGroupName.DataValueField = "GroupName";
           // DrpGroupName.DataBind();
           // DrpGroupName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

        }

        protected void drpCustBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtSP = new DataTable();
            if (drpCustBranch.SelectedItem.Value != "0")
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
                    dtSP = objCreateQuoteBAL.GetSalesPersonfromBranch(drpCustBranch.SelectedItem.Text, txtCustomerNumber.Text);
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

        protected void grdQuote_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = ViewState["CurrentTable"] as DataTable;

            //dt.Rows[index].Delete();
            dt.Rows.RemoveAt(index);
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
                dr["StockAvailability"] = string.Empty;
                dr["Weight"] = string.Empty;
                dr["LeadTime"] = string.Empty;
                dr["SafetyStock"] = string.Empty;
                dr["ListPrice"] = string.Empty;
                dr["Discount"] = string.Empty;
                dr["Unit Price"] = string.Empty;
                dr["QTY"] = string.Empty;
                dr["AdditionalDiscount"] = "0";
                //dr["CostTotal"] = 0;

                dt.Rows.Add(dr);
                ViewState["CurrentTable"] = dt;
                grdQuote.DataSource = dt; // bind new datatable to grid
                grdQuote.DataBind();
            }
            else
            {
                ViewState["CurrentTable"] = dt;
                grdQuote.DataSource = dt;
                grdQuote.DataBind();
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
                            TotalforGM=TotalforGM+ float.Parse(TotalPrice);
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
                if (txtCustomerName.Text.Contains("SUNDRY"))
                {
                    lblSundryBranch.Visible = true;
                    txtSundryBranch.Visible = true;
                    lblSundryBranch.Text = "Delivery Address";
                }

            }
            else if (drpCustBranch.SelectedItem.Text.ToUpper().Contains("SUNDRY"))
            {
                lblSundryBranch.Visible = true;
                txtSundryBranch.Visible = true;
                lblSundryBranch.Text = "Sundry Branch";
            }
            else
            {
                lblSundryBranch.Visible = false;
                txtSundryBranch.Visible = false;
            }

        }

        protected void drpCarriageCharges_SelectedIndexChanged(object sender, EventArgs e)
        {

           
            if (drpCarriageCharges.SelectedItem.Text.Contains("Add New"))
            {
                txtCarriage.Visible = true;
            }
            else
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
            if (TxtGrandTotal.Text != "0" && TxtGrandTotal.Text != string.Empty)
            {
                float rate = float.Parse(ConfigurationManager.AppSettings["GBPtoEUR"]);
                TxtGrandTotal.Text = float.Parse(TxtGrandTotal.Text).ToString("0.00");
                float TotalOverallCost = 0;
                foreach (GridViewRow row in grdQuote.Rows)
                {


                    TextBox PartNum = (TextBox)row.FindControl("txtPartNo");
                    TextBox Qty = (TextBox)row.FindControl("txtQty");

                    if (PartNum.Text == string.Empty)
                    {
                        continue;
                    }
                    string CostPrice = objCreateQuoteBAL.getCostPrice(PartNum.Text);
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
                        dtICO = objCreateQuoteBAL.GetICOItem(PartNum.Text, string.Empty);
                        CostPrice = dtICO.Rows[0]["CostPrice"].ToString();
                        if (txtCurrency.Text == "EUR")
                        {
                            CostPrice = (float.Parse(CostPrice) * rate).ToString();
                        }
                    }

                    TotalOverallCost = TotalOverallCost + (float.Parse(CostPrice) * Convert.ToInt32(Qty.Text));



                }

                float TotalGM = (((float.Parse(TxtGrandTotal.Text) - TotalOverallCost) * 100) / float.Parse(TxtGrandTotal.Text));
                txtTotalGM.Text = TotalGM.ToString("0.00");


            }
        }

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
                string test = "github";
            }
        }

        protected void grdQuote_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            obj.DeleteExistingQuote(txtQuoteNum.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Quote deleted successfully');window.location ='Dashboard.aspx';",
                true);
        }



        //protected void grdQuote_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{

        //    // BindData();
        //    grdQuote.PageIndex = e.NewPageIndex;
        //    //BindData();
        //    DataTable dt1 = new DataTable();
        //    dt1 = (DataTable)ViewState["CurrentTable"];
        //    grdQuote.DataSource = dt1; // bind new datatable to grid
        //    grdQuote.Columns[10].HeaderText = "Discount%";
        //    grdQuote.Columns[13].HeaderText = "GM%";
        //    grdQuote.DataBind();

        //}




    }

    public class PDFFooter : PdfPageEventHelper
    {
        // write on top of document
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            PdfPCell cell;
            tabFot.TotalWidth = 300F;
            cell = new PdfPCell(new Phrase("Footer"));
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }

}