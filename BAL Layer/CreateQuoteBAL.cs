using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Layer;
using System.Data;

namespace BAL_Layer
{
    public class CreateQuoteBAL
    {
        DataTable dt = new DataTable();
        CreateQuoteDAL obj = new CreateQuoteDAL();
        public DataTable GetCustomerNumber(string CustomerName,string City)
        {
            dt = obj.GetCustomerNumber(CustomerName,City);
            return dt;

        }

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

        public DataTable LoadCustomer(string CustNo)
        {
            dt = obj.LoadCustomer(CustNo);
            return dt;
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
        public DataTable GetCustomerBranch(string CustNo)
        {
            dt = obj.GetCustomerBranch(CustNo);
            return dt;
        }

        public DataTable GetCarriageCharge()
        {
            dt = obj.GetCarriageCharge();
            return dt;
        }
        public DataTable GetNetItemDetails(string partNo,string Desc)
        {
            dt = obj.GetNetItemDetails(partNo,Desc);
            return dt;
        }

        public DataTable GetGroupDetails(string groupName)
        {
            dt = obj.GetGroupDetails(groupName);
            return dt;
        }

        public string GetQuoteNumber(string userName)
        {
           string QuoteNo = obj.GetQuoteNumber(userName);
            return QuoteNo;
        }

        public void SaveQuote(string QuoteNumber, string CustomerName, string CustomerNumber, string CustomerBranch, string CustEmail, string CustPhone, string ProjectName, string OppurtunityId, string PaymentTerms, string PartialDelivery, string Currency, string PreparedBy, string PreparedByEmail, string PreparedByPhone, string SalesPerson, string SalesPersonEmail, string SalesPersonPhone, string ProductFamily, string ItemNo, string PartNo, string Desc, int QTY, string MOQ, string LeadTime, string AVaialbleQty, string Weight, string SafetyStock, string ListPrice, string Discount, string UnitPrice, string AddDiscount, string UnitPriceAfterDiscount, string TotalPriceAfterDiscount, string GM, string Status, DateTime CreationDate, DateTime ExpirationDate, string carriagcharge, string Version, string Comments, string GrandTotal, string GrossGM, string CostTotal, string SundryBranch, string isPerforma, string status_comment, string ExportFlag,string VATNo,string Stock)
        {
            obj.SaveQuote(QuoteNumber, CustomerName, CustomerNumber,CustomerBranch,CustEmail,CustPhone, ProjectName, OppurtunityId, PaymentTerms, PartialDelivery, Currency, PreparedBy,PreparedByEmail,PreparedByPhone,SalesPerson,SalesPersonEmail,SalesPersonPhone,ProductFamily,ItemNo, PartNo, Desc, QTY, MOQ, LeadTime,AVaialbleQty,Weight, SafetyStock,ListPrice,Discount, UnitPrice, AddDiscount, UnitPriceAfterDiscount, TotalPriceAfterDiscount, GM, Status, CreationDate, ExpirationDate, carriagcharge,Version,Comments,GrandTotal,GrossGM,CostTotal, SundryBranch,isPerforma, status_comment,ExportFlag,VATNo,Stock);
        }

        public void UpdateQuoteNo(string quoteNumber,string ID)
        {
            obj.UpdateQuoteNo(quoteNumber,ID);
        }
        public void UpdateEmail(string quoteNumber)
        {
            obj.UpdateEmail(quoteNumber);
        }


        public void UpdateQuoteDetails(string quoteNumber, string Status)
        {

            obj.UpdateQuoteDetails(quoteNumber, Status);
        }
        public void DeleteExistingQuote(string QuoteNumber)
        {
            obj.DeleteExistingQuote(QuoteNumber);
        }

        public string GetPaymentTerms(string customerName, string custNo)
        {
            string Currency = obj.GetPaymentTerms(customerName, custNo);
            return Currency;
        }

        public void UpdateMatrixDetails(string approval1, string approval3,string GrossGM, string quoteNumber)
        {
            obj.UpdateMatrixDetails(approval1, approval3, GrossGM, quoteNumber);
        }

        //public DataTable ValidatePartNo(string partno)
        //{
        //    DataTable Newdt = new DataTable();
        //    Newdt = obj.ValidatePartNo(partno);
        //    return Newdt;
        //}

        public string getCostPrice(string partno)
        {
            string CostPrice = obj.getCostPrice(partno);
            return CostPrice;
        }

        public DataTable getPreparedByList()
        {
            DataTable dtPrepBy = obj.getPreparedByList();
            return dtPrepBy;
        }

        public DataTable GetSEEmailPhone(string strName)
        {
            DataTable dtSEEMailPhone = obj.GetSEEmailPhone(strName);
            return dtSEEMailPhone;
            
        }

        public DataTable GetSalesPersonData(string CustNo)
        {
            DataTable dtSalesPerson = obj.GetSalesPersonData(CustNo);
            return dtSalesPerson;
        }
        public DataTable GetSalesPersonfromBranch(string Branch,string CustNo)
        {
            DataTable dtSalesPerson = obj.GetSalesPersonFromBranch(Branch,CustNo);
            return dtSalesPerson;
        }
        public void SaveSundryCust(string QuoteNumber, string CustName, string CustAddress)
        {
            obj.SaveSundryCust(QuoteNumber, CustName, CustAddress);
        }
        public DataTable GetCustNameAddress(string Branch, string CustNo)
        {
            DataTable dtSalesPerson = obj.GetCustNameAddress(Branch, CustNo);
            return dtSalesPerson;
        }
        public DataTable GetCustomerAddress(string Cust)
        {
            DataTable dt = obj.GetCustomerAddress(Cust);
            return dt;
        }


        public DataTable GetPGMDAta()
        {
            DataTable dtPGM = obj.GetPGMDAta();
            return dtPGM;
        }
    }
}
