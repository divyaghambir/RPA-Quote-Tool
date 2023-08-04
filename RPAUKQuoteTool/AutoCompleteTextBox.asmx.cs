using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using BAL_Layer;
namespace RPADubaiQuoteTool
{
    /// <summary>
    /// Summary description for AutoCompleteTextBox
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutoCompleteTextBox : System.Web.Services.WebService
    {

        CreateQuoteBAL obj = new CreateQuoteBAL();
        [WebMethod]
        public string[] GetCompletionList(string prefixText, int count)
        {

            if (count == 0)
            {
                count = 10;
            }
            DataTable dt = GetRecords(prefixText);
            List<string> items = new List<string>(count);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strPartNo = dt.Rows[i]["LegacyPartNo"].ToString();
                items.Add(strPartNo);
            }
            return items.ToArray();
        }

        public DataTable GetRecords(string strPartNo)
        {
            DataTable dtItems = new DataTable();
            dtItems = obj.GetItems(strPartNo);

            return dtItems;
        }
    }
}
