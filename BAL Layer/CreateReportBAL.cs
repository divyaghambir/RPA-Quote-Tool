using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Layer;
using System.Data;

namespace BAL_Layer
{

    public class CreateReportBAL
    {

        CreateReportDAL obj = new CreateReportDAL();

        public DataTable CreateReport(string CreateStartDt, string CreateEnddt, string Status)
        {
            DataTable dtReport = new DataTable();
            dtReport = obj.CreateReport(CreateStartDt, CreateEnddt, Status);
            return dtReport;
        }
    }
}
