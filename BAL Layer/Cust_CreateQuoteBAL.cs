using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Layer;
using System.Data;

namespace BAL_Layer
{
    public class Cust_CreateQuoteBAL
    {
        DataTable dt = new DataTable();
        Cust_CreateQuoteDAL obj = new Cust_CreateQuoteDAL();
       
        
        public DataTable GetStock(string partno)
        {
            dt = obj.GetStock(partno);
            return dt;

        }
        public DataTable GetTermsCode(string Customer)
        {
            dt = obj.GetTermCode(Customer);
            return dt;

        }

        public void testtt()
        {
            throw new NotImplementedException();
        }

      
        public DataTable GetPrice(string PartNo, string Desc,string currency)
        {
            dt = obj.GetPrice(PartNo, Desc,currency);
            return dt;
        }

        public DataTable GetDiscount(string ItemNo, string CustNo)
        {
            dt = obj.GetDiscount(ItemNo, CustNo);
            return dt;
        }
        public DataTable GetICOItem(string PartNo,string Desc)
        {
            dt = obj.GetICOItem(PartNo,Desc);
            return dt;
        }
        public string GetICODiscount(string CustNo)
        {
            string ICODisc = obj.GetICODiscount(CustNo);
            return ICODisc;
        }

        public string GetNetPrice(string CustNo, string PartNo,string Desc,string currency)
        {
            string NetPrice = obj.GetNetPrice(CustNo, PartNo,Desc,currency);
            return NetPrice;
        }
        public string ItemDiscGroup(string ItemNo)
        {
            string ItemDsicGrp = obj.ItemDiscGroup(ItemNo);
            return ItemDsicGrp;
        }
        public DataTable GetItems(string partNo)
        {
            dt = obj.GetItems(partNo);
            return dt;
        }
        public DataTable GetItemDesc(string strDesc)
        {
            dt = obj.GetItemDesc(strDesc);
            return dt;
        }


        public DataTable GetItemDetails(string partNo,string Desc)
        {
            dt = obj.GetItemDetails(partNo,Desc);
            return dt;
        }
      

        public DataTable GetNetItemDetails(string partNo,string Desc)
        {
            dt = obj.GetNetItemDetails(partNo,Desc);
            return dt;
        }

        public DataTable GetGroupDetails(string groupName)
        {
           // dt = obj.GetGroupDetails(groupName);
            return dt;
        }

        public string GetQuoteNumber(string userName)
        {
           string QuoteNo = obj.GetQuoteNumber(userName);
            return QuoteNo;
        }

        public void SaveQuote(string QuoteNumber, string CustomerName, string CustomerNumber,  string CustEmail, string CustPhone, string ProjectName, string RefNo, string Currency, string PreparedBy, string PreparedByEmail, string PreparedByPhone, string SalesPerson, string SalesPersonEmail, string SalesPersonPhone, string ProductFamily, string ItemNo, string PartNo, string Desc, int QTY, string MOQ, string LeadTime, string AVaialbleQty, string Weight, string SafetyStock, string ListPrice, string Discount, string CostPrice,string total,  string Status, DateTime CreationDate, DateTime ExpirationDate, string carriagcharge, string Version, string Comments, string GrandTotal, string Stock)
        {
            obj.SaveQuote(QuoteNumber, CustomerName, CustomerNumber,CustEmail,CustPhone, ProjectName, RefNo,  Currency, PreparedBy,PreparedByEmail,PreparedByPhone,SalesPerson,SalesPersonEmail,SalesPersonPhone,ProductFamily,ItemNo, PartNo, Desc, QTY, MOQ, LeadTime,AVaialbleQty,Weight, SafetyStock,ListPrice,Discount, CostPrice, total, Status, CreationDate, ExpirationDate, carriagcharge,Version,Comments,GrandTotal,Stock);
        }
        public DataTable GetCompany(string User)
        {
            DataTable dt = new DataTable();
            dt = obj.GetCompany(User);
            return dt;
        }

        public void UpdateQuoteNo(string quoteNumber,string ID)
        {
            obj.UpdateQuoteNo(quoteNumber,ID);
        }
        public void UpdateEmail(string quoteNumber)
        {
            obj.UpdateEmail(quoteNumber);
        }


        public void DeleteExistingQuote(string QuoteNumber)
        {
            obj.DeleteExistingQuote(QuoteNumber);
        }


        public DataTable GetUserInfo(string User)
        {
            DataTable dt = new DataTable();
            dt = obj.GetUserInfo(User);
            return dt;
        }


    }
}
