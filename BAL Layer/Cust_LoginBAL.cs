using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Layer;

namespace BAL_Layer
{
    public class Cust_LoginBAL
    {
        Cust_LoginDAL obj = new Cust_LoginDAL();

        public int ValidateCredential(string username, string password)
        {
            int count = obj.ValidateCredential(username, password);
            return count;
        }
    }
}
