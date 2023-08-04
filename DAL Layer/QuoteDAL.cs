using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL_Layer
{
    public class QuoteDAL
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DevDb"].ConnectionString;
        public DataTable LoadQuote(string quoteNo, string sts)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_LoadQuote", _con))

                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@QuoteNo", SqlDbType.VarChar).Value = quoteNo.Trim();
                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = sts.Trim();
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

        public DataTable LoadSundryCust(string QuoteNo)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select [Sundry CustomerName],[Sundry CustomerAddress] from [dbo].[Sundry_Customers] where  [Quote Number] ='" + QuoteNo + "'";

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


        public void SaveQuote(string QuoteNumber, string CustomerName, string CustomerNumber,string CustomerBranch, string CustEmail, string CustPhone, string ProjectName, string OppurtunityId, string PaymentTerms, string PartialDelivery, string Currency, string PreparedBy, string PreparedByEmail, string PreparedByPhone, string SalesPerson, string SalesPersonEmail, string SalesPersonPhone, string ProductFamily, string ItemNo, string PartNo, string Desc, int QTY, string MOQ, string LeadTime,string AvailableQTy,string Weight, string SafetyStock, string ListPrice, string Discount, string UnitPrice, string AddDiscount, string UnitPriceAfterDiscount, string TotalPriceAfterDiscount, string GM, string Status, DateTime CreationDate, DateTime ExpirationDate, string CarriageCHarge, string Version,string Comments, string GrandTotal, string GrossGM,string CostTotal,string SundryBranch,string Performa,string Status_Comments,string ExportFlag, string VATNo,string Stock)
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
                        cmd.Parameters.Add("@CustomerBranch", SqlDbType.VarChar).Value = CustomerBranch.Trim();
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
                        cmd.Parameters.Add("@AvailableQty", SqlDbType.VarChar).Value = AvailableQTy.Trim();
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
                        cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = CreationDate;
                        cmd.Parameters.Add("@ExpirationDate", SqlDbType.Date).Value = ExpirationDate;
                        cmd.Parameters.Add("@CarriageCharge", SqlDbType.VarChar).Value = CarriageCHarge.Trim();
                        cmd.Parameters.Add("@Version", SqlDbType.VarChar).Value = Version.Trim();
                        cmd.Parameters.Add("@Comments", SqlDbType.VarChar).Value = Comments.Trim();
                        cmd.Parameters.Add("@GrandTotal", SqlDbType.VarChar).Value = GrandTotal.Trim();
                        cmd.Parameters.Add("@TotalGM", SqlDbType.VarChar).Value = GrossGM.Trim();
                        cmd.Parameters.Add("@CostTotal", SqlDbType.VarChar).Value = CostTotal.Trim();
                        cmd.Parameters.Add("@SundryBranch", SqlDbType.VarChar).Value = SundryBranch.Trim();
                        cmd.Parameters.Add("@isPerforma", SqlDbType.VarChar).Value = Performa.Trim();
                        cmd.Parameters.Add("@ChangeStatus_Comments", SqlDbType.VarChar).Value = Status_Comments.Trim();
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
                        cmd.Parameters.Add("@ProjectName", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@OppurtunityId", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@PaymentTerms", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@PartialDelivery", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Platform", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@ProductGroup", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@PartNo", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@QTY", SqlDbType.Int).Value = 5;
                        cmd.Parameters.Add("@MOQ", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@LeadTime", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@QtyImpact", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@UnitPrice", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Discount", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@UnitPriceAfterDiscount", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@TotalPriceAfterDiscount", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@GM", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@CreationDate", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@ExpirationDate", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@IncoTerms", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@PreparedBy", SqlDbType.VarChar).Value = test.Trim();
                        cmd.Parameters.Add("@Comments", SqlDbType.VarChar).Value = test.Trim();
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

        public void UpdateQuoteNo(string quoteNumber, string ID)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
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

        public DataTable GetGroupDetails(string groupName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select distinct " + groupName + " as GroupName from tblLineItems where " + groupName + " ! = ''";

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


        public void UpdateQuote(string quoteNumber, string customerName, string customerNumber,string CUstomerBranch, string customerEmail, string customerPhone, string projectName, string oppurtunityId, string paymentTerms, string partialDelivery, string preparedBy,string preparedbyEmail,string preparedbyPhone,string SalesPerson,string SalesPersonEmail,string SalesPersonPhone, string currency, string userRole, DateTime creationDate, DateTime expirationDate, string carriagecharge, string productFamily,string itemNo, string partNo, string desc, int qTY, string mOQ, string leadTime,string Availableqty,string Weight, string safetyStock,string listPrice,string discount ,string unitPrice, string addDiscount, string unitPriceAfterDiscount, string totalPriceAfterDiscount, string gM, string version,string Comments, string GrandTotal,string GrossGM,string CostTotal,string SundryBranch,string Performa, string Status_Comment,string ExportFlag, string VATNo,string Stock)
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

                        cmd.Parameters.Add("@QuoteNumber", SqlDbType.VarChar).Value = quoteNumber.Trim();
                        cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = customerName.Trim();
                        cmd.Parameters.Add("@CustomerNumber", SqlDbType.VarChar).Value = customerNumber.Trim();
                        cmd.Parameters.Add("@CustomerBranch", SqlDbType.VarChar).Value = CUstomerBranch.Trim();
                        cmd.Parameters.Add("@CustomerEmail", SqlDbType.VarChar).Value = customerEmail.Trim();
                        cmd.Parameters.Add("@CustomerPhone", SqlDbType.VarChar).Value = customerPhone.Trim();
                        cmd.Parameters.Add("@ProjectName", SqlDbType.VarChar).Value = projectName.Trim();
                        cmd.Parameters.Add("@OppurtunityId", SqlDbType.VarChar).Value = oppurtunityId.Trim();
                        cmd.Parameters.Add("@PaymentTerms", SqlDbType.VarChar).Value = paymentTerms.Trim();
                        cmd.Parameters.Add("@PartialDelivery", SqlDbType.VarChar).Value = partialDelivery.Trim();
                        cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = currency.Trim();
                        cmd.Parameters.Add("@PreparedBy", SqlDbType.VarChar).Value = preparedBy.Trim();
                        cmd.Parameters.Add("@PreparedByEmail", SqlDbType.VarChar).Value = preparedbyEmail.Trim();
                        cmd.Parameters.Add("@PreparedByPhone", SqlDbType.VarChar).Value = preparedbyPhone.Trim();
                        cmd.Parameters.Add("@SalesPerson", SqlDbType.VarChar).Value = SalesPerson.Trim();
                        cmd.Parameters.Add("@SalesPersonEmail", SqlDbType.VarChar).Value = SalesPersonEmail.Trim();
                        cmd.Parameters.Add("@SalesPersonPhone", SqlDbType.VarChar).Value = SalesPersonPhone.Trim();
                        cmd.Parameters.Add("@ProductFamily", SqlDbType.VarChar).Value = productFamily.Trim();
                        cmd.Parameters.Add("@ItemNo", SqlDbType.VarChar).Value = itemNo.Trim();
                        cmd.Parameters.Add("@PartNo", SqlDbType.VarChar).Value = partNo.Trim();
                        cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = desc.Trim();
                        cmd.Parameters.Add("@QTY", SqlDbType.Int).Value = qTY;
                        cmd.Parameters.Add("@MOQ", SqlDbType.VarChar).Value = mOQ.Trim();
                        cmd.Parameters.Add("@LeadTime", SqlDbType.VarChar).Value = leadTime.Trim();
                        cmd.Parameters.Add("@AvailableQty", SqlDbType.VarChar).Value = Availableqty.Trim();
                        cmd.Parameters.Add("@Weight", SqlDbType.VarChar).Value = Weight.Trim();
                        cmd.Parameters.Add("@SafetyStock", SqlDbType.VarChar).Value = safetyStock.Trim();
                        cmd.Parameters.Add("@ListPrice", SqlDbType.VarChar).Value = listPrice.Trim();
                        cmd.Parameters.Add("@Discount", SqlDbType.VarChar).Value = discount.Trim();
                        cmd.Parameters.Add("@UnitPrice", SqlDbType.VarChar).Value = unitPrice.Trim();
                        cmd.Parameters.Add("@AddDiscount", SqlDbType.VarChar).Value = addDiscount.Trim();
                        cmd.Parameters.Add("@UnitPriceAfterDiscount", SqlDbType.VarChar).Value = unitPriceAfterDiscount.Trim();
                        cmd.Parameters.Add("@TotalPriceAfterDiscount", SqlDbType.VarChar).Value = totalPriceAfterDiscount.Trim();
                        cmd.Parameters.Add("@GM", SqlDbType.VarChar).Value = gM.Trim();
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Updated to New Version".Trim();
                        cmd.Parameters.Add("@CreationDate", SqlDbType.VarChar).Value = creationDate;
                        cmd.Parameters.Add("@ExpirationDate", SqlDbType.VarChar).Value = expirationDate;
                        cmd.Parameters.Add("@CarriageCharge", SqlDbType.VarChar).Value = carriagecharge.Trim();
                        cmd.Parameters.Add("@Version", SqlDbType.VarChar).Value = version.Trim();
                        cmd.Parameters.Add("@Comments", SqlDbType.VarChar).Value = Comments.Trim();
                        cmd.Parameters.Add("@GrandTotal", SqlDbType.VarChar).Value = GrandTotal.Trim();
                        cmd.Parameters.Add("@TotalGM", SqlDbType.VarChar).Value = GrossGM.Trim();
                        cmd.Parameters.Add("@CostTotal", SqlDbType.VarChar).Value = CostTotal.Trim();
                        cmd.Parameters.Add("@SundryBranch", SqlDbType.VarChar).Value = SundryBranch.Trim();
                        cmd.Parameters.Add("@isPerforma", SqlDbType.VarChar).Value = Performa.Trim();
                        cmd.Parameters.Add("@ChangeStatus_Comments", SqlDbType.VarChar).Value = Status_Comment.Trim();
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

        public DataTable GetApprovaldata(string quoteNumber)
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString)) //03-11-20 removed BDM approval
                {
                    string queryStatement = "select approval1, approval3 from tblQuoteDetails where [Quote Number] = '"+ quoteNumber+"'";

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

        public void UpdateMatrixDetails(string approval1, string approval3,string GrossGM, string quoteNumber)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateMatrixDetails", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        string test = "";

                        cmd.Parameters.Add("@Approval1", SqlDbType.VarChar).Value = approval1.Trim();
                        //cmd.Parameters.Add("@Approval2", SqlDbType.VarChar).Value = approval2.Trim(); //03-11-20 removed BDM approval
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

        
        /*public void SaveQuote(string quoteNumber, string customerName, string customerNumber, string projectName, string oppurtunityId, string paymentTerms, string partialDelivery, string currency, string preparedBy, string creationDate, string expirationDate, string incoTerms, string platform, string productGroup, string partNo, string desc, int qTY, string mOQ, string leadTime, string qtyImpact, string unitPrice, string discount, string unitPriceAfterDiscount, string totalPriceAfterDiscount, string gM, string Status, string Country, string Version, string StandardCost,string Comments,string GrandTotal,string GrossGM)
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

                        cmd.Parameters.Add("@QuoteNumber", SqlDbType.VarChar).Value = quoteNumber.Trim();
                        cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = customerName.Trim();
                        cmd.Parameters.Add("@CustomerNumber", SqlDbType.VarChar).Value = customerNumber.Trim();
                        cmd.Parameters.Add("@ProjectName", SqlDbType.VarChar).Value = projectName.Trim();
                        cmd.Parameters.Add("@OppurtunityId", SqlDbType.VarChar).Value = oppurtunityId.Trim();
                        cmd.Parameters.Add("@PaymentTerms", SqlDbType.VarChar).Value = paymentTerms.Trim();
                        cmd.Parameters.Add("@PartialDelivery", SqlDbType.VarChar).Value = partialDelivery.Trim();
                        cmd.Parameters.Add("@Currency", SqlDbType.VarChar).Value = currency.Trim();
                        cmd.Parameters.Add("@Platform", SqlDbType.VarChar).Value = platform.Trim();
                        cmd.Parameters.Add("@ProductGroup", SqlDbType.VarChar).Value = productGroup.Trim();
                        cmd.Parameters.Add("@PartNo", SqlDbType.VarChar).Value = partNo.Trim();
                        cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = desc.Trim();
                        cmd.Parameters.Add("@QTY", SqlDbType.Int).Value = qTY;
                        cmd.Parameters.Add("@MOQ", SqlDbType.VarChar).Value = mOQ.Trim();
                        cmd.Parameters.Add("@LeadTime", SqlDbType.VarChar).Value = leadTime.Trim();
                        cmd.Parameters.Add("@QtyImpact", SqlDbType.VarChar).Value = qtyImpact.Trim();
                        cmd.Parameters.Add("@UnitPrice", SqlDbType.VarChar).Value = unitPrice.Trim();
                        cmd.Parameters.Add("@Discount", SqlDbType.VarChar).Value = discount.Trim();
                        cmd.Parameters.Add("@UnitPriceAfterDiscount", SqlDbType.VarChar).Value = unitPriceAfterDiscount.Trim();
                        cmd.Parameters.Add("@TotalPriceAfterDiscount", SqlDbType.VarChar).Value = totalPriceAfterDiscount.Trim();
                        cmd.Parameters.Add("@GM", SqlDbType.VarChar).Value = gM.Trim();
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status.Trim();
                        cmd.Parameters.Add("@CreationDate", SqlDbType.VarChar).Value = creationDate.Trim();
                        cmd.Parameters.Add("@ExpirationDate", SqlDbType.VarChar).Value = expirationDate.Trim();
                        cmd.Parameters.Add("@IncoTerms", SqlDbType.VarChar).Value = incoTerms.Trim();
                        cmd.Parameters.Add("@PreparedBy", SqlDbType.VarChar).Value = preparedBy.Trim();
                        cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = Country.Trim();
                        cmd.Parameters.Add("@Version", SqlDbType.VarChar).Value = Version.Trim();
                        cmd.Parameters.Add("@standardcost", SqlDbType.VarChar).Value = StandardCost.Trim();
                        cmd.Parameters.Add("@Comments", SqlDbType.VarChar).Value = Comments.Trim();
                        cmd.Parameters.Add("@GrandTotal", SqlDbType.VarChar).Value = GrandTotal.Trim();
                        cmd.Parameters.Add("@TotalGM", SqlDbType.VarChar).Value = GrossGM.Trim();
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

    }*/
    public void DeleteExistingQuote(string quoteNumber)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "delete from tblQuoteDetails where [Quote Number] = '" + quoteNumber + "'";

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

        public void ConfirmQuote(string quoteNo,string status,string StatusComment)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "UPDATE tblQuoteDetails SET Status ='"+ status + "',ChangeStatus_Comments='"+StatusComment+"' where [Quote Number] ='" + quoteNo + "'";

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

        public string ApproveQuote(string userRole, string QuoteNumber)
        {
            string Status = "";
            try
            {
                
                DataTable dt = new DataTable();
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateApproval", _con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QuoteNumber", SqlDbType.VarChar).Value = QuoteNumber.Trim();
                        cmd.Parameters.Add("@UserRole", SqlDbType.VarChar).Value = userRole.Trim();
                        _con.Open();
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.Fill(dt);
                        }
                        _con.Close();
                    }
                }
                Status = dt.Rows[0][0].ToString();
               
            }
            catch (Exception ex)
            {
            }
            return Status;
        }

        public void RejectQuote(string quoteNo, string Comments)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))  //03-11-20 removed approval 2 for BDM
                { 
                    string queryStatement = "UPDATE tblQuoteDetails SET Status ='Reject' , Approval1 ='Reject',Approval3 ='Reject', [Comments] = '" + Comments+"' where [Quote Number] ='"+quoteNo+"'" ;

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
    }
}
