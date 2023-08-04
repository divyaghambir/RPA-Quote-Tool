using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL_Layer
{
    public class CreateReportDAL
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DevDb"].ConnectionString;
        public DataTable CreateReport(string CreateStartDt, string CreateEnddt, string Status)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GenerateReport", _con))

                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (CreateStartDt != "" && CreateEnddt != "")
                        {
                            cmd.Parameters.Add("@CreateStartDt", SqlDbType.VarChar).Value = CreateStartDt;
                            cmd.Parameters.Add("@CreateEndDt", SqlDbType.VarChar).Value = CreateEnddt;
                        }
                        if (Status != "")
                        {
                            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;
                        }
                    

                       
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

    }
}
