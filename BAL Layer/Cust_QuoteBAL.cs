using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Layer;

namespace BAL_Layer
{
    public class Cust_QuoteBAL
    {
        DataTable dt = new DataTable();
        Cust_QuoteDAL obj = new Cust_QuoteDAL();
        public DataTable LoadQuote(string quoteNo, string sts)
        {
            dt = obj.LoadQuote(quoteNo, sts);
            return dt;
        }
        public void SaveQuote(string QuoteNumber, string CustomerName, string CustomerNumber,string CustEmail, string CustPhone, string ProjectName, string RefNo, string Currency, string PreparedBy, string PreparedByEmail, string PreparedByPhone, string SalesPerson, string SalesPersonEmail, string SalesPersonPhone, string ProductFamily, string ItemNo, string PartNo, string Desc, int QTY, string MOQ, string LeadTime,string availableqty,string weight, string SafetyStock, string ListPrice, string Discount, string CostPrice, string total, string Status, DateTime CreationDate, DateTime ExpirationDate, string carriagecharge, string Version,string Comments, string GrandTotal, string stock)
        {
            obj.SaveQuote(QuoteNumber, CustomerName, CustomerNumber, CustEmail, CustPhone, ProjectName, RefNo, Currency, PreparedBy, PreparedByEmail, PreparedByPhone, SalesPerson, SalesPersonEmail, SalesPersonPhone, ProductFamily, ItemNo, PartNo, Desc, QTY, MOQ, LeadTime,availableqty,weight, SafetyStock, ListPrice, Discount, CostPrice,total, Status, CreationDate, ExpirationDate, carriagecharge, Version,Comments, GrandTotal,stock);
        }

        public string GetQuoteNumber(string userName)
        {
            string QuoteNo = obj.GetQuoteNumber(userName);
            return QuoteNo;
        }

        public DataTable GetGroupDetails(string groupName)
        {
           // dt = obj.GetGroupDetails(groupName);
            return dt;
        }

        public void UpdateQuoteNo(string quoteNumber, string ID)
        {
            obj.UpdateQuoteNo(quoteNumber, ID);
        }
        public void RejectQuote(string quoteNo, string Comments)
        {
            obj.RejectQuote(quoteNo, Comments);
        }

        public void UpdateQuote(string quoteNumber, string customerName, string customerNumber, string customerEmail, string customerPhone, string projectName, string RefNo, string currency, string preparedBy, string preparedbyEmail, string preparedbyPhone, string SalesPerson, string SalesPersonEmail, string SalesPersonPhone, string productFamily, string itemNo, string partNo, string desc, int qTY, string mOQ, string leadTime, string Availableqty, string Weight, string safetyStock, string listPrice, string discount, string CostPrice, string total, string status, DateTime creationDate, DateTime expirationDate, string carriagecharge, string version, string Comments, string GrandTotal, string Stock)
        {
           obj.UpdateQuote(quoteNumber, customerName, customerNumber, customerEmail, customerPhone, projectName, RefNo, currency,preparedBy,preparedbyEmail ,preparedbyPhone,SalesPerson,SalesPersonEmail,SalesPersonPhone, productFamily,itemNo, partNo, desc, qTY, mOQ, leadTime,Availableqty,Weight, safetyStock,listPrice,discount, CostPrice, total,status,creationDate, expirationDate, carriagecharge,version,Comments,GrandTotal,Stock);
        }
      

        public string ApproveQuote(string userRole, string QuoteNumber)
        {
            string Status = obj.ApproveQuote(userRole, QuoteNumber);
            return Status;
        }

        public void ConfirmQuote(string quoteNo,string status,string StatusComment)
        {
            obj.ConfirmQuote(quoteNo,status,StatusComment);
        }

        public void DeleteExistingQuote(string quoteNumber)
        {
            obj.DeleteExistingQuote(quoteNumber);
        }
       

        public void UpdateMatrixDetails(string approval1, string approval3,string GrossGM, string quoteNumber)
        {
          //  obj.UpdateMatrixDetails(approval1, approval3,GrossGM, quoteNumber); //03-11-20 removed BDM apprval
        }
        /*public string getStandardCost(string partno)
        {
           // string StdCost = obj.getStandardCost(partno);
            //return StdCost;
        }*/

        public void UpdateQuoteDetails(string quoteNumber, string Status)
        {

            obj.UpdateQuoteDetails(quoteNumber, Status);
        }
      
        public DataTable GetApprovaldata(string quoteNumber)
        {
            DataTable dtApprovals = new DataTable();
           // dtApprovals = obj.GetApprovaldata(quoteNumber);
            return dtApprovals;
            //public void UpdateQuote(string QuoteNumber, string platform, string productGroup, string partNo, string desc, int qTY, string mOQ, string leadTime, string qtyImpact, string unitPrice, string discount, string unitPriceAfterDiscount, string totalPriceAfterDiscount, string gM, string version)
            //{
            //    obj.UpdateQuote(QuoteNumber, platform, productGroup, partNo, desc, qTY, mOQ, leadTime, qtyImpact, unitPrice, discount, unitPriceAfterDiscount, totalPriceAfterDiscount, gM, version);
            //}
        }
    }
}