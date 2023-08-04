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
    public class CreateQuoteDAL
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DevDb"].ConnectionString;
        public DataTable GetCustomerNumber(string customerName,string City)
        {
            
            DataTable dt = new DataTable();
            try
            {                
                     using (SqlConnection _con = new SqlConnection(connectionString))
                     {
                    string queryStatement = "select [CustomerNumber] from CustomerBrowse where [CustomerName] = '" + customerName + "' and [City]='"+City+"'";

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

        public string GetQuoteNumber(string userName)
        {
            string id = "";
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
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
                        cmd.Parameters.Add("@OppurtunityId", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@PaymentTerms", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@PartialDelivery", SqlDbType.VarChar).Value = test.Trim();
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
                        cmd.Parameters.Add("@UnitPrice", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@AddDiscount", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@UnitPriceAfterDiscount", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@TotalPriceAfterDiscount", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@GM", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@CreationDate", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@ExpirationDate", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@IncoTerms", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Version", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@GrandTotal", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@TotalGM", SqlDbType.VarChar).Value = test.Trim();
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

        public string getCostPrice(string partno)
        {
            string CostPrice = "";
            string queryStatement = "";
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    queryStatement = "select b.CostPrice from [WattsItemBrowse] a,[WattsUKItemBrowse] b where a.ItemNo=b.ItemNo and a.LegacyPartNo = '" + partno + "'";

                    SqlCommand cmd = new SqlCommand(queryStatement, _con);
                    //cmd.Parameters.Add("@Name", SqlDbType.VarChar);
                    //cmd.Parameters["@name"].Value = newName;

                    _con.Open();
                    if (cmd.ExecuteScalar() != null)
                    {
                        CostPrice =(string)cmd.ExecuteScalar();
                    }
                    else
                    {
                        string queryStatement1 = "select CostPrice from ICO_Price where LegacyPartNo = '" + partno + "'";

                        SqlCommand cmd1 = new SqlCommand(queryStatement1, _con);
                        //cmd.Parameters.Add("@Name", SqlDbType.VarChar);
                        //cmd.Parameters["@name"].Value = newName;

                        
                        CostPrice = (string)cmd1.ExecuteScalar();
                        if (CostPrice == string.Empty)
                        {
                            throw new Exception("Costprice not found");
                        }
                    }

                    }
                }

            
            catch (Exception ex)
            {
               
                
            }
            return CostPrice;
        }

        //public int ValidatePartNo(string partno)
        //{
        //    int RowCount = 0;
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        using (SqlConnection _con = new SqlConnection(connectionString))
        //        {
        //            string queryStatement = "select count(PartNo) from tblLineItems where PartNo = '" +partno+"'";
        //            using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
        //            {

        //                SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
        //                _con.Open();
        //                _dap.Fill(dt);
        //                _con.Close();

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    if(dt.Rows.Count>0)
        //    {
        //        string count = dt.Rows[0][0].ToString();
        //        RowCount = Convert.ToInt32(count); 
        //    }
        //    return RowCount;
        //}

        public void UpdateMatrixDetails(string approval1, string approval3,string GrossGM, string quoteNumber)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateMatrixDetails", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //string test = "";

                        cmd.Parameters.Add("@Approval1", SqlDbType.VarChar).Value = approval1.Trim();
                        //cmd.Parameters.Add("@Approval2", SqlDbType.VarChar).Value = approval2.Trim();
                        cmd.Parameters.Add("@Approval3", SqlDbType.VarChar).Value = approval3.Trim();
                        cmd.Parameters.Add("@GrossGM", SqlDbType.VarChar).Value = GrossGM.Trim();
                        cmd.Parameters.Add("@QuoteNumber", SqlDbType.VarChar).Value = quoteNumber.Trim();
                                               
                        _con.Open();

                        cmd.ExecuteNonQuery();
                       // id = cmd.Parameters["@id"].Value.ToString();
                        // lblMessage.Text = "Record inserted successfully. ID = " + id;
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }

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
        public void UpdateQuoteDetails(string quoteNumber, string Status)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateQuoteDetails", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                       

                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status.Trim();
                        cmd.Parameters.Add("@QuoteNumber", SqlDbType.VarChar).Value = quoteNumber.Trim();

                        _con.Open();

                        cmd.ExecuteNonQuery();
                        // id = cmd.Parameters["@id"].Value.ToString();
                        // lblMessage.Text = "Record inserted successfully. ID = " + id;
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        public string GetPaymentTerms(string customerName, string custNo)
        {
            string PaymentTerms = "";
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select [PaymentTerms] from CustomerDetails where [CustomerName] = '" + customerName + "'" + "and [CustomerNo] = '" + custNo + "'";

                    SqlCommand cmd = new SqlCommand(queryStatement, _con);
                    //cmd.Parameters.Add("@Name", SqlDbType.VarChar);
                    //cmd.Parameters["@name"].Value = newName;

                    _con.Open();
                    PaymentTerms = cmd.ExecuteScalar().ToString();
                }

            }
            catch (Exception ex)
            {
            }
            return PaymentTerms;
        }

        public void UpdateQuoteNo(string quoteNumber,string ID)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "UPDATE tblQuoteDetails SET [Quote Number] ='" +quoteNumber+ "' where [SerialNo] = " + ID;

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

        public void DeleteExistingQuote(string QuoteNumber)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
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

        public void SaveQuote(string QuoteNumber, string CustomerName, string CustomerNumber, string CUstomerBranch,string CustEmail, string CustPhone, string ProjectName, string OppurtunityId, string PaymentTerms, string PartialDelivery, string Currency, string PreparedBy, string PreparedByEmail, string PreparedByPhone, string SalesPerson, string SalesPersonEmail, string SalesPersonPhone, string ProductFamily, string ItemNo, string PartNo, string Desc, int QTY, string MOQ, string LeadTime,string AvailableQty,string Weight, string SafetyStock, string ListPrice, string Discount, string UnitPrice, string AddDiscount, string UnitPriceAfterDiscount, string TotalPriceAfterDiscount, string GM, string Status, DateTime CreationDate, DateTime ExpirationDate, string CarriageCharge, string Version,string Comments, string GrandTotal, string GrossGM,string CostPrice,string SundryBranch,string isPerforma,string statusComment,string ExportFlag, string VATNo,string Stock)
        {

            string id = "";
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SaveQuoteDetails", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        string test = "";

                        cmd.Parameters.Add("@QuoteNumber", SqlDbType.VarChar).Value = QuoteNumber.Trim();
                        cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = CustomerName.Trim();
                        cmd.Parameters.Add("@CustomerNumber", SqlDbType.VarChar).Value = CustomerNumber.Trim();
                        cmd.Parameters.Add("@CustomerBranch", SqlDbType.VarChar).Value = CUstomerBranch.Trim();
                        cmd.Parameters.Add("@CustomerEmail", SqlDbType.VarChar).Value = CustEmail.Trim();
                        cmd.Parameters.Add("@CustomerPhone", SqlDbType.VarChar).Value = CustPhone.Trim();
                        cmd.Parameters.Add("@ProjectName", SqlDbType.VarChar).Value = ProjectName.Trim();
                        cmd.Parameters.Add("@OppurtunityId", SqlDbType.VarChar).Value = OppurtunityId.Trim();
                        cmd.Parameters.Add("@PaymentTerms", SqlDbType.VarChar).Value = PaymentTerms.Trim();
                        cmd.Parameters.Add("@PartialDelivery", SqlDbType.VarChar).Value = PartialDelivery.Trim();
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
                        cmd.Parameters.Add("@UnitPrice", SqlDbType.VarChar).Value = UnitPrice.Trim();
                        cmd.Parameters.Add("@AddDiscount", SqlDbType.VarChar).Value = AddDiscount.Trim();
                        cmd.Parameters.Add("@UnitPriceAfterDiscount", SqlDbType.VarChar).Value = UnitPriceAfterDiscount.Trim();
                        cmd.Parameters.Add("@TotalPriceAfterDiscount", SqlDbType.VarChar).Value = TotalPriceAfterDiscount.Trim();
                        cmd.Parameters.Add("@GM", SqlDbType.VarChar).Value = GM.Trim();
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status.Trim();
                        cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value =  CreationDate;
                        cmd.Parameters.Add("@ExpirationDate", SqlDbType.Date).Value = ExpirationDate;
                        cmd.Parameters.Add("@CarriageCharge", SqlDbType.VarChar).Value = CarriageCharge.Trim();
                        cmd.Parameters.Add("@Version", SqlDbType.VarChar).Value = Version.Trim(); 
                        cmd.Parameters.Add("@Comments", SqlDbType.VarChar).Value = Comments.Trim();
                        cmd.Parameters.Add("@GrandTotal", SqlDbType.VarChar).Value = GrandTotal.Trim();
                        cmd.Parameters.Add("@TotalGM", SqlDbType.VarChar).Value = GrossGM.Trim();
                        cmd.Parameters.Add("@CostTotal", SqlDbType.VarChar).Value = CostPrice.Trim();
                        cmd.Parameters.Add("@SundryBranch", SqlDbType.VarChar).Value = SundryBranch.Trim();
                        cmd.Parameters.Add("@isPerforma", SqlDbType.VarChar).Value = isPerforma.Trim();
                        cmd.Parameters.Add("@ChangeStatus_Comments", SqlDbType.VarChar).Value = statusComment.Trim();
                        cmd.Parameters.Add("@ExportFlag", SqlDbType.VarChar).Value = ExportFlag.Trim();
                        cmd.Parameters.Add("@VATNo", SqlDbType.VarChar).Value = VATNo.Trim();
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

        public void SaveSundryCust(string QuoteNumber, string CustName, string CustAddress)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "Insert into Sundry_Customers values ('"+QuoteNumber+"','"+CustName+"','"+CustAddress+"')";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {
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

        public DataTable GetGroupDetails(string groupName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select distinct "+ groupName + " as GroupName from tblLineItems where "+ groupName+" ! = ''";

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

        public DataTable LoadCustomer(string CustNo)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("[CustomerMasterData]", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@CustNo", SqlDbType.VarChar).Value = CustNo;
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
        public DataTable getPreparedByList()
        {

            DataTable dtPrepby = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select Name from SalesPerson where Role= 'SE'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _dap.Fill(dtPrepby);
                        _con.Close();

                    }
                }

            }
            catch (Exception ex)
            {
            }
            return dtPrepby;
        }

        public DataTable GetSEEmailPhone(string strName)
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select Email,Phone from SalesPerson where [Name] = '" + strName + "'";

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

                    string queryStatement="";
                    if(Desc==string.Empty)
                        queryStatement= "select NetPrice from NetPrice where [LegacyPartNo] = '" + PartNo + "' and [CustomerNumber] = '" + CustNo + "' and [Currency] ='"+ currency + "'" ;
                    else if(PartNo==string.Empty)
                        queryStatement = "select NetPrice from NetPrice where [Description] = '" + Desc + "' and [CustomerNumber] = '" + CustNo + "' and [Currency] ='" + currency + "'";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {

                        _con.Open();
                        Netprice = _cmd.ExecuteScalar().ToString();

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

        public DataTable GetSalesPersonData(string CustNo)
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select c.Currency,b.Name,b.Email,b.Phone from BusinessRelationBrowse a,SalesPerson b,CustomerBrowse c where a.CustomerNumber = '" + CustNo + "' and a.SalesPerson=b.Region and a.CustomerNumber=c.CustomerNumber";

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
        public DataTable GetSalesPersonFromBranch(string Branch,string CustNo)
        {

            DataTable dt = new DataTable();
            try
            {

                string City=Branch.Substring(0,Branch.LastIndexOf("-"));
                string Zip=Branch.Substring(Branch.LastIndexOf("-")+1);

                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select b.Name,b.Email,b.Phone from [CustomerBranch] a,SalesPerson b where a.City = '" + City + "' and a.PostalCode = '" + Zip + "' and a.SalesPerson1=b.Region and a.CustomerNumber='" + CustNo +"'";

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
        

        public DataTable GetCustNameAddress(string Branch, string CustNo)
        {

            DataTable dt = new DataTable();
            try
            {
                if (Branch != "")
                {

                    string City = Branch.Substring(0, Branch.LastIndexOf("-"));
                    string Zip = Branch.Substring(Branch.LastIndexOf("-") + 1);

                    using (SqlConnection _con = new SqlConnection(connectionString))
                    {
                        string queryStatement = "select Name,Address1,City,PostalCode,Country from [CustomerBranch] where CustomerNumber='"+CustNo+"' and City='"+City+"' and PostalCode='"+Zip+"'"  ; 

                        using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                        {

                            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                            _con.Open();
                            _dap.Fill(dt);
                            _con.Close();

                        }

                    }
                }
                else
                {
                    using (SqlConnection _con = new SqlConnection(connectionString))
                    {
                        string queryStatement = "select CustomerName,Address1,City,PostalCode,Country from [CustomerBrowse] where CustomerNumber = '" + CustNo + "'";

                        using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                        {

                            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                            _con.Open();
                            _dap.Fill(dt);
                            _con.Close();

                        }

                    }

                }

            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public DataTable GetCustomerAddress(string Cust)
        {

            DataTable dt = new DataTable();
            try
            {
                if (Cust != "")
                {
                    Cust = Cust.Substring(0, Cust.LastIndexOf("|"));

                    using (SqlConnection _con = new SqlConnection(connectionString))
                    {
                        string queryStatement = "select Address1,City,PostalCode,Country from [CustomerBrowse] where CustomerName='" + Cust + "'";

                        using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                        {

                            SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                            _con.Open();
                            _dap.Fill(dt);
                            _con.Close();

                        }

                    }
                }
               

            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        public DataTable GetPGMDAta()
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select distinct Description from PGM_Desc";

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

        public DataTable GetCustomerBranch(string CustNo)
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select ([City]+'-'+[PostalCode]) as Branch from [UK_Quotations].[dbo].[CustomerBranch] where CustomerNumber='" + CustNo+ "' order by [City]";

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
        public DataTable GetCarriageCharge()
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "SELECT (CAST(Number as varchar(10))+'/'+Charge+'/'+ Description) as Charge FROM [CarriageCharges]";

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



    }
}
