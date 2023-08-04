using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Layer;

namespace BAL_Layer
{
    public class LoginBAL
    {
        LoginDAL obj = new LoginDAL();

        public int ValidateCredential(string username, string password)
        {
            int count = obj.ValidateCredential(username, password);
            return count;
        }
    }
}
