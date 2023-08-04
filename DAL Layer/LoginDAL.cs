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
    public class LoginDAL
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DevDb"].ConnectionString;

        public int ValidateCredential(string username, string password)
        {
            int RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    string queryStatement = "select count([UserName]) from UserLogin where [UserName] ='"+username+"' and [Password] = '"+password+"'";
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
            if (dt.Rows.Count > 0)
            {
                string count = dt.Rows[0][0].ToString();
                RowCount = Convert.ToInt32(count);
            }
            return RowCount;
        }
    }
}
