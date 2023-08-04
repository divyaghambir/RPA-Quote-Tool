using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Layer;

namespace BAL_Layer
{
    public class QuoteBAL
    {
        DataTable dt = new DataTable();
        QuoteDAL obj = new QuoteDAL();
        public DataTable LoadQuote(string quoteNo, string sts)
        {
            dt = obj.LoadQuote(quoteNo, sts);
            return dt;
        }
        public void SaveQuote(string QuoteNumber, string CustomerName, string CustomerNumber,string CustomerBranch,string CustEmail, string CustPhone, string ProjectName, string OppurtunityId, string PaymentTerms, string PartialDelivery, string Currency, string PreparedBy, string PreparedByEmail, string PreparedByPhone, string SalesPerson, string SalesPersonEmail, string SalesPersonPhone, string ProductFamily, string ItemNo, string PartNo, string Desc, int QTY, string MOQ, string LeadTime,string availableqty,string weight, string SafetyStock, string ListPrice, string Discount, string UnitPrice, string AddDiscount, string UnitPriceAfterDiscount, string TotalPriceAfterDiscount, string GM, string Status, DateTime CreationDate, DateTime ExpirationDate, string carriagecharge, string Version,string Comments, string GrandTotal, string GrossGM,string CostTotal,string SundryBranch,string Performa,string status_comment,string ExportFlag,string VATNo,string stock)
        {
            obj.SaveQuote(QuoteNumber, CustomerName, CustomerNumber,CustomerBranch, CustEmail, CustPhone, ProjectName, OppurtunityId, PaymentTerms, PartialDelivery, Currency, PreparedBy, PreparedByEmail, PreparedByPhone, SalesPerson, SalesPersonEmail, SalesPersonPhone, ProductFamily, ItemNo, PartNo, Desc, QTY, MOQ, LeadTime,availableqty,weight, SafetyStock, ListPrice, Discount, UnitPrice, AddDiscount, UnitPriceAfterDiscount, TotalPriceAfterDiscount, GM, Status, CreationDate, ExpirationDate, carriagecharge, Version,Comments, GrandTotal, GrossGM,CostTotal, SundryBranch,Performa, status_comment,ExportFlag,VATNo,stock);
        }

        public string GetQuoteNumber(string userName)
        {
            string QuoteNo = obj.GetQuoteNumber(userName);
            return QuoteNo;
        }

        public DataTable GetGroupDetails(string groupName)
        {
            dt = obj.GetGroupDetails(groupName);
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

        public void UpdateQuote(string quoteNumber, string customerName, string customerNumber,string CustomerBranch,string customeremail,string customerphone, string projectName, string oppurtunityId, string paymentTerms, string partialDelivery, string preparedBy,string preparedbyEmail, string preparedbyPhone,string SalesPerson,string SalesPersonEmail,string SalesPersonPhone, string currency, string userRole, DateTime creationDate, DateTime expirationDate, string carriagecharge,string productFamily,string itemno, string partNo, string desc, int qTY, string mOQ, string leadTime,string AvailableQty,string weight, string safetyStock, string listPrice, string discount, string unitPrice, string addDiscount, string unitPriceAfterDiscount, string totalPriceAfterDiscount, string gM, string version,string Comments,string GrandTotal,string GrossGM,string CostTotal,string SundryBranch,string isPerforma,string status_comment,string ExportFlag,string VATNo,string Stock)
        {
            obj.UpdateQuote(quoteNumber, customerName, customerNumber, CustomerBranch,customeremail, customerphone, projectName, oppurtunityId, paymentTerms, partialDelivery, preparedBy,preparedbyEmail ,preparedbyPhone,SalesPerson,SalesPersonEmail,SalesPersonPhone,currency, userRole, creationDate, expirationDate, carriagecharge, productFamily,itemno, partNo, desc, qTY, mOQ, leadTime,AvailableQty,weight, safetyStock,listPrice,discount, unitPrice, addDiscount, unitPriceAfterDiscount, totalPriceAfterDiscount, gM, version, Comments,GrandTotal,GrossGM,CostTotal, SundryBranch,isPerforma, status_comment,ExportFlag,VATNo,Stock);
        }
        public DataTable LoadSundryCust(string QuoteNo)
        {
            DataTable dt = new DataTable();
            dt = obj.LoadSundryCust(QuoteNo);
            return dt;
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
            obj.UpdateMatrixDetails(approval1, approval3,GrossGM, quoteNumber); //03-11-20 removed BDM apprval
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
            dtApprovals = obj.GetApprovaldata(quoteNumber);
            return dtApprovals;
            //public void UpdateQuote(string QuoteNumber, string platform, string productGroup, string partNo, string desc, int qTY, string mOQ, string leadTime, string qtyImpact, string unitPrice, string discount, string unitPriceAfterDiscount, string totalPriceAfterDiscount, string gM, string version)
            //{
            //    obj.UpdateQuote(QuoteNumber, platform, productGroup, partNo, desc, qTY, mOQ, leadTime, qtyImpact, unitPrice, discount, unitPriceAfterDiscount, totalPriceAfterDiscount, gM, version);
            //}
        }
    }
}