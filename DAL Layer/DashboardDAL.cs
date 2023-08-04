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
    public class DashboardDAL
    {
        //string queryString = "select * from tblCustomerDetails where PaymentTerms = 003";
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DevDb"].ConnectionString;


        public void testmethod()
        {
           // string connectionString = "server=USWA-RPADB;database=Dubai_Quotations;uid=rpadbdev;password=W@tts2019!@;";

            using (SqlConnection _con = new SqlConnection(connectionString))
            {
                string queryStatement = "select * from tblCustomerDetails where PaymentTerms = 003";

                using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                {
                    DataTable customerTable = new DataTable();

                    SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                    _con.Open();
                    _dap.Fill(customerTable);
                    _con.Close();

                }
            }

        }

        public DataTable LoadDashboard(string userName, string preparedBy)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    //string queryStatement = "SELECT DISTINCT[Quote Number] as QuoteNo, [Prepared By] as SalesEngineer,[Customer Name] as CustomerName , [Customer Number] as CustomerNo,convert(varchar,[Creation Date],103) as CreationDate,convert(varchar,[Expiration Date],103) as ExpirationDate, [Status],GrandTotal  from [dbo].[tblQuoteDetails] where" +preparedBy + "ORDER BY CreationDate desc,QuoteNo desc";
                    string queryStatement = " select  QuoteNo,[Prepared By] as SalesEngineer,CustomerName , CustomerNo ,Email, convert(varchar,test.[Creation Date],103) as CreationDate,ExpirationDate, [Status],[EmailSent],GrandTotal,isPerforma from (SELECT DISTINCT [Quote Number] as QuoteNo,[Prepared By],[Customer Name] as CustomerName , [Customer Number] as CustomerNo,[Customer Email] as Email ,[Creation Date],convert(varchar, [Expiration Date],103) as ExpirationDate, [Status],[EmailSent],GrandTotal,isPerforma from [dbo].[tblQuoteDetails]) test  where " + preparedBy + " order by test.[Creation Date] desc,test.QuoteNo desc";
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

        public DataTable GetCustomerName(string QuoteNo)
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select [Sundry CustomerName] from Sundry_Customers where [Quote Number] = '" + QuoteNo + "'";

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

        public DataTable LoadDashboard(string username, bool search)
        {
            DataTable dt = new DataTable();
            try
            {
                string queryStatement = "";
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    if (search == true)
                    {
                         queryStatement = "select  QuoteNo,[Prepared By] as SalesEngineer,CustomerName , CustomerNo ,Email, convert(varchar,test.[Creation Date],103) as CreationDate,ExpirationDate, [Status],[EmailSent],GrandTotal,isPerforma from (SELECT DISTINCT [Quote Number] as QuoteNo,[Prepared By],[Customer Name] as CustomerName , [Customer Number] as CustomerNo,[Customer Email] as Email, [Creation Date],convert(varchar, [Expiration Date],103) as ExpirationDate, [Status],[EmailSent],GrandTotal,isPerforma from [dbo].[tblQuoteDetails]) test order by test.[Creation Date] desc,test.QuoteNo desc";
                    }
                    else
                    {
                         queryStatement = "select  QuoteNo,[Prepared By] as SalesEngineer,CustomerName , CustomerNo ,Email, convert(varchar,test.[Creation Date],103) as CreationDate,ExpirationDate, [Status],[EmailSent],GrandTotal,isPerforma from (SELECT DISTINCT [Quote Number] as QuoteNo,[Prepared By],[Customer Name] as CustomerName , [Customer Number] as CustomerNo,[Customer Email] as Email, [Creation Date],convert(varchar, [Expiration Date],103) as ExpirationDate, [Status],[EmailSent],GrandTotal,isPerforma from [dbo].[tblQuoteDetails] where [Status]<>'INACTIVE') test order by test.[Creation Date] desc,test.QuoteNo desc";
                    }
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
        public DataTable LoadAllQuoteDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "SELECT [Quote Number],[Customer Name],[Customer Number],[Customer Branch],[Customer Email],[Customer Phone],[Project Name],[Oppurtunity Id] ,[Payment Terms],[Partial Delivery],[Currency] ,[Prepared By]  ,[PreparedByEmail],[PreparedByPhone],[SalesPerson],[SPEmail],[SPPhone],[ProductFamily],[ItemNo] ,[PartNo] ,[Description] ,[QTY] ,[MOQ] ,[LeadTime]  ,[AvailableQty] ,[Weight],[SafetyStock],[ListPrice],[Discount] ,[UnitPrice] ,[AdditionalDiscount] ,[Unit Price after Extra Discount]  ,[Total Price after Extra Discount],[GM],[Status],[Creation Date]  ,[Expiration Date]  ,[CarriageCharge],[Version],[Comments] ,[GrandTotal]  ,[Total GM%],[CostPrice]  ,[EmailSent] ,[SundryBranch] ,[isPerforma],[ChangeStatus_Comments] FROM[UK_Quotations].[dbo].[tblQuoteDetails] order by[Creation Date] desc,[Quote Number] desc";

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
        public DataTable LoadFilteredDashboard(string columnName, string columnValue)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "";
                   /* if (columnValue.ToString().ToUpper() != "INACTIVE")
                    {
                         queryStatement = "select  QuoteNo,[Prepared By] as SalesEngineer,CustomerName , CustomerNo , convert(varchar,test.[Creation Date],103) as CreationDate,ExpirationDate, [Status],[EmailSent],GrandTotal,isPerforma from (SELECT DISTINCT [Quote Number] as QuoteNo,[Prepared By],[Customer Name] as CustomerName , [Customer Number] as CustomerNo, [Creation Date],convert(varchar, [Expiration Date],103) as ExpirationDate, [Status],[EmailSent],GrandTotal,isPerforma from [dbo].[tblQuoteDetails] where [Status]<>'INACTIVE') test where test.[" + columnName + "] like'%" + columnValue + "%' order by test.[Creation Date] desc,test.QuoteNo desc";
                    }
                    else
                    {*/
                         queryStatement = "select  QuoteNo,[Prepared By] as SalesEngineer,CustomerName , CustomerNo ,Email, convert(varchar,test.[Creation Date],103) as CreationDate,ExpirationDate, [Status],[EmailSent],GrandTotal,isPerforma from (SELECT DISTINCT [Quote Number] as QuoteNo,[Prepared By],[Customer Name] as CustomerName , [Customer Number] as CustomerNo,[Customer Email] as Email, [Creation Date],convert(varchar, [Expiration Date],103) as ExpirationDate, [Status],[EmailSent],GrandTotal,isPerforma from [dbo].[tblQuoteDetails]) test where test.[" + columnName + "] like'%" + columnValue + "%' order by test.[Creation Date] desc,test.QuoteNo desc";
                   // }
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
