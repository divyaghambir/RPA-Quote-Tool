using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL_Layer
{
    public class Cust_CreateQuoteDAL
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBQuote"].ConnectionString;
        string connectionString1 = System.Configuration.ConfigurationManager.ConnectionStrings["DBCustQuote"].ConnectionString;
        

        public string GetQuoteNumber(string userName)
        {
            string id = "";
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString1))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SaveQuoteDetails", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        string test = "";

                        cmd.Parameters.Add("@QuoteNumber", SqlDbType.VarChar).Value = userName.Trim();
                        cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@CustomerNumber", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@CustomerEmail", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@CustomerPhone", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@ProjectName", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = test.Trim();
                        //cmd.Parameters.Add("@PaymentTerms", SqlDbType.VarChar).Value = test.Trim();
                        //cmd.Parameters.Add("@PartialDelivery", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@PreparedBy", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@PreparedByEmail", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@PreparedByPhone", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@SalesPerson", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@SalesPersonEmail", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@SalesPersonPhone", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@ProductFamily", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@ItemNo", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@PartNo", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@QTY", SqlDbType.Int).Value = 5;
                        cmd.Parameters.Add("@MOQ", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@LeadTime", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@SafetyStock", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@ListPrice", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Discount", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@CostPrice", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@CreationDate", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@ExpirationDate", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Version", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@GrandTotal", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        _con.Open();
                       
                        cmd.ExecuteNonQuery();
                        id = cmd.Parameters["@id"].Value.ToString();
                       // lblMessage.Text = "Record inserted successfully. ID = " + id;
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return id;
        }

       



        public void UpdateEmail(string quoteNumber)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateEmail", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.Add("@QuoteNumber", SqlDbType.VarChar).Value = quoteNumber.Trim();

                        _con.Open();

                        cmd.ExecuteNonQuery();
                       
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
       

      

      

        public void DeleteExistingQuote(string QuoteNumber)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString1))
                {
                    string queryStatement = "delete from tblQuoteDetails where [Quote Number] = '" +QuoteNumber+"'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _cmd.ExecuteNonQuery();
                        _con.Close();

                    }

                }
            }
            catch (Exception ex)
            {
            }

        }

        public void SaveQuote(string QuoteNumber, string CustomerName, string CustomerNumber, string CustEmail, string CustPhone, string ProjectName, string RefNo, string Currency, string PreparedBy, string PreparedByEmail, string PreparedByPhone, string SalesPerson, string SalesPersonEmail, string SalesPersonPhone, string ProductFamily, string ItemNo, string PartNo, string Desc, int QTY, string MOQ, string LeadTime,string AvailableQty,string Weight, string SafetyStock, string ListPrice, string Discount, string CostPrice,string total,string Status, DateTime CreationDate, DateTime ExpirationDate, string CarriageCharge, string Version,string Comments, string GrandTotal, string Stock)
        {

            string id = "";
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString1))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SaveQuoteDetails", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        string test = "";

                        cmd.Parameters.Add("@QuoteNumber", SqlDbType.VarChar).Value = QuoteNumber.Trim();
                        cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = CustomerName.Trim();
                        cmd.Parameters.Add("@CustomerNumber", SqlDbType.VarChar).Value = CustomerNumber.Trim();
                        cmd.Parameters.Add("@CustomerEmail", SqlDbType.VarChar).Value = CustEmail.Trim();
                        cmd.Parameters.Add("@CustomerPhone", SqlDbType.VarChar).Value = CustPhone.Trim();
                        cmd.Parameters.Add("@ProjectName", SqlDbType.VarChar).Value = ProjectName.Trim();
                        cmd.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = RefNo.Trim();
                        //cmd.Parameters.Add("@PaymentTerms", SqlDbType.VarChar).Value = PaymentTerms.Trim();
                        //cmd.Parameters.Add("@PartialDelivery", SqlDbType.VarChar).Value = PartialDelivery.Trim();
                        cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = Currency.Trim();
                        cmd.Parameters.Add("@PreparedBy", SqlDbType.VarChar).Value = PreparedBy.Trim();
                        cmd.Parameters.Add("@PreparedByEmail", SqlDbType.VarChar).Value = PreparedByEmail.Trim();
                        cmd.Parameters.Add("@PreparedByPhone", SqlDbType.VarChar).Value = PreparedByPhone.Trim();
                        cmd.Parameters.Add("@SalesPerson", SqlDbType.VarChar).Value = SalesPerson.Trim();
                        cmd.Parameters.Add("@SalesPersonEmail", SqlDbType.VarChar).Value = SalesPersonEmail.Trim();
                        cmd.Parameters.Add("@SalesPersonPhone", SqlDbType.VarChar).Value = SalesPersonPhone.Trim();
                        cmd.Parameters.Add("@ProductFamily", SqlDbType.VarChar).Value = ProductFamily.Trim();
                        cmd.Parameters.Add("@ItemNo", SqlDbType.VarChar).Value = ItemNo.Trim();
                        cmd.Parameters.Add("@PartNo", SqlDbType.VarChar).Value = PartNo.Trim();
                        cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = Desc.Trim();
                        cmd.Parameters.Add("@QTY", SqlDbType.Int).Value = QTY;
                        cmd.Parameters.Add("@MOQ", SqlDbType.VarChar).Value = MOQ.Trim();
                        cmd.Parameters.Add("@LeadTime", SqlDbType.VarChar).Value = LeadTime.Trim();
                        cmd.Parameters.Add("@AvailableQty", SqlDbType.VarChar).Value = AvailableQty.Trim();
                        cmd.Parameters.Add("@Weight", SqlDbType.VarChar).Value = Weight.Trim();
                        cmd.Parameters.Add("@SafetyStock", SqlDbType.VarChar).Value = SafetyStock.Trim();
                        cmd.Parameters.Add("@ListPrice", SqlDbType.VarChar).Value = ListPrice.Trim();
                        cmd.Parameters.Add("@Discount", SqlDbType.VarChar).Value = Discount.Trim();
                        cmd.Parameters.Add("@CostPrice", SqlDbType.VarChar).Value = CostPrice.Trim();
                        cmd.Parameters.Add("@Total", SqlDbType.VarChar).Value = total.Trim();
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status.Trim();
                        cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value =  CreationDate;
                        cmd.Parameters.Add("@ExpirationDate", SqlDbType.Date).Value = ExpirationDate;
                        cmd.Parameters.Add("@CarriageCharge", SqlDbType.VarChar).Value = CarriageCharge.Trim();
                        cmd.Parameters.Add("@Version", SqlDbType.VarChar).Value = Version.Trim(); 
                        cmd.Parameters.Add("@Comments", SqlDbType.VarChar).Value = Comments.Trim();
                        cmd.Parameters.Add("@GrandTotal", SqlDbType.VarChar).Value = GrandTotal.Trim();
                        cmd.Parameters.Add("@StockAvailability", SqlDbType.VarChar).Value = Stock.Trim();
                        cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        _con.Open();

                        cmd.ExecuteNonQuery();
                        id = cmd.Parameters["@id"].Value.ToString();
                        // lblMessage.Text = "Record inserted successfully. ID = " + id;
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            
        }

       
       
        public DataTable GetItems(string partNo)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select * from ((select a.LegacyPartNo as LegacyPartNo from WattsItemBrowse a,WattsUKItemBrowse b where a.ItemNo = b.ItemNo) union(select LegacyPartNo from ICO_Price) union (select LegacyPartNo from NetPrice)) AS U where U.LegacyPartNo like '" + partNo +"%'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _dap.Fill(dt);
                        _con.Close();

                    }

                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public DataTable GetItemDesc(string strDesc)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select * from ((select a.Description1 as Description from WattsItemBrowse a,WattsUKItemBrowse b where a.ItemNo = b.ItemNo) union(select Description from ICO_Price) union (select Description from NetPrice)) AS U where U.Description like '" + strDesc + "%'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _dap.Fill(dt);
                        _con.Close();

                    }

                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public DataTable GetStock(string partno)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from [UK_Quotations].[dbo].[StockCheck] where [Legacy Number]='"+partno+"'", _con))
                    {
                       
                        _con.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;
                            da.Fill(dt);
                        }
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public DataTable GetCompany(string User)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("select [Company] from [UK_Customer_Quote].dbo.[UserLogin] where UserName='" + User + "'", _con))
                    {

                        _con.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;
                            da.Fill(dt);
                        }
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public DataTable GetItemDetails(string partNo, string Desc)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[ItemMasterData]", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@LegacyPartNo", SqlDbType.VarChar).Value = partNo;
                        cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = Desc;
                        _con.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.Fill(dt);
                        }
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public DataTable GetNetItemDetails(string partNo,string Desc)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "";
                    if(Desc==string.Empty)
                        queryStatement= " select * from WattsItemBrowse a,WattsUKItemBrowse b where a.ItemNo=b.ItemNo and a.LegacyPartNo= '" + partNo + "'";
                    else if (partNo == string.Empty)
                        queryStatement = " select * from WattsItemBrowse a,WattsUKItemBrowse b where a.ItemNo=b.ItemNo and a.Description1= '" + Desc + "'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _dap.Fill(dt);
                        _con.Close();

                    }

                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

       
        public DataTable GetPrice(string PartNo,string Desc,string currency)
        {   
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[PriceMasterData]", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@LegacyPartNo", SqlDbType.VarChar).Value = PartNo;
                        cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = Desc;
                        cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value ="GBP" ;
                        _con.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.Fill(dt);
                        }
                        _con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public DataTable GetDiscount(string ItemNo, string CustNo)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "";

                    queryStatement = "select a.DiscountPerc,c.ItemDiscountGrp from DiscountPerc a ,CustDiscGroup b ,ItemDiscGroup c where a.CustDiscCategory=b.CustDiscCategory and a.ItemDiscountGrp=c.ItemDiscountGrp and c.ItemNo='" + ItemNo + "'" + "and b.CustomerNumber='" + CustNo + "'"; 
                                        
                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _dap.Fill(dt);
                        _con.Close();

                    }

                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
       
        public string GetNetPrice(string CustNo,string PartNo,string Desc,string currency)
        {

            string Netprice = string.Empty;
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    if (currency == "Select")
                    {
                        currency = "GBP";
                    }

                    string queryStatement="";
                    if(Desc==string.Empty)
                        queryStatement= "select NetPrice from NetPrice where [LegacyPartNo] = '" + PartNo + "' and [CustomerNumber] = '" + CustNo + "' and [Currency] ='"+ currency + "'" ;
                    else if(PartNo==string.Empty)
                        queryStatement = "select NetPrice from NetPrice where [Description] = '" + Desc + "' and [CustomerNumber] = '" + CustNo + "' and [Currency] ='" + currency + "'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        _con.Open();
                        if (_cmd.ExecuteScalar() != null)
                        {
                            Netprice = _cmd.ExecuteScalar().ToString();
                        }

                    }

                }

            }
            catch (Exception ex)
            {
            }
            return Netprice;
        }
       
        public DataTable GetICOItem(string PartNo,string Desc)
        {

            DataTable ICOItem = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "";
                    if(Desc==string.Empty)
                        queryStatement= "select * from ICO_Price where [LegacyPartNo] = '" + PartNo + "'";
                    else if (PartNo == string.Empty)
                        queryStatement = "select * from ICO_Price where [Description] = '" + Desc + "'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _dap.Fill(ICOItem);
                        _con.Close();

                    }

                }

            }
            catch (Exception ex)
            {
            }
            return ICOItem;
        }

        public string GetICODiscount(string CustNo)
        {
            string ICODisc = string.Empty;
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select a.DiscountPerc from DiscountPerc a ,CustDiscGroup b  where a.CustDiscCategory=b.CustDiscCategory and a.ItemDiscountGrp='WOTH' and b.CustomerNumber= '" + CustNo + "'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        _con.Open();
                        ICODisc = _cmd.ExecuteScalar().ToString();

                    }

                }

            }
            catch (Exception ex)
            {
            }
            return ICODisc;

        }

        public void UpdateQuoteNo(string quoteNumber, string ID)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString1))
                {
                    string queryStatement = "UPDATE tblQuoteDetails SET [Quote Number] ='" + quoteNumber + "' where [SerialNo] = " + ID;

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _cmd.ExecuteNonQuery();
                        //_dap.Fill(dt);
                        _con.Close();

                    }

                }
            }
            catch (Exception ex)
            {
            }

        }


        public DataTable GetTermCode(string Customer)
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select TermsCode from CustomerBrowse where CustomerName = '" + Customer + "'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _dap.Fill(dt);
                        _con.Close();

                    }

                }

            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public DataTable GetUserInfo(string User)
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString1))
                {
                    string queryStatement = "select * from UserLogin where UserName = '" + User + "'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _dap.Fill(dt);
                        _con.Close();

                    }

                }

            }
            catch (Exception ex)
            {
            }
            return dt;
        }


        public string ItemDiscGroup(string ItemNo)
        {
            string itemdiscountgrp = string.Empty;
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "SELECT ItemDiscountGrp FROM [UK_Quotations].[dbo].[ItemDiscGroup] where ItemNo='"+ItemNo+"'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {
                        _con.Open();
                        itemdiscountgrp = _cmd.ExecuteScalar().ToString();
                    }

                }

            }
            catch (Exception ex)
            {
            }
            return itemdiscountgrp;

        }
       



    }
}
