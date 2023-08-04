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
    /// Summary description for AutoCompleteDesc
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutoCompleteDesc : System.Web.Services.WebService
    {
        CreateQuoteBAL obj = new CreateQuoteBAL();

        [WebMethod]
        public string[] GetDescList(string prefixText, int count)
        {

            if (count == 0)
            {
                count = 10;
            }
            DataTable dt = GetDesc(prefixText);
            List<string> items = new List<string>(count);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strDesc = dt.Rows[i]["Description"].ToString();
                items.Add(strDesc);
            }
            return items.ToArray();
        }

        public DataTable GetDesc(string strDesc)
        {
            DataTable dtItems = new DataTable();
            dtItems = obj.GetItemDesc(strDesc);

            return dtItems;
        }

    }
}
