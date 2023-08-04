using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Layer;
namespace BAL_Layer
{
    public class DashboardBAL
    {
        DataTable dt = new DataTable();
        DashboardDAL obj = new DashboardDAL();
        public void testmethod()
        {
            
            obj.testmethod();
        }

        public DataTable LoadDashboard(string username, bool search)
        {
            dt = obj.LoadDashboard(username,search);
            return dt;
        }
        public DataTable LoadAllQuoteDetails()
        {
            dt = obj.LoadAllQuoteDetails();
            return dt;
        }
        public DataTable GetCustomerName(string QuoteNo)
        {
            dt = obj.GetCustomerName(QuoteNo);
            return dt;
        }

        public DataTable LoadFilteredDashboard(string columnName, string columnValue)
        {
            dt = obj.LoadFilteredDashboard(columnName, columnValue);
            return dt;
        }

        public DataTable LoadDashboard(string userName, string preparedBy)
        {
            dt = obj.LoadDashboard(userName, preparedBy);
            return dt;
        }
    }
}
