using Application.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BL
{
    internal class UserBLL
    {
        public DataTable Login(string username, string password, string role)
        {
            UserDAL dal = new UserDAL();
            return dal.Login(username, password, role);
        }
        public bool VerifyIdentity(string user, string cnic)
        {
            UserDAL dal = new UserDAL();

            DataTable dt = dal.VerifyIdentity(user, cnic);

            return dt.Rows.Count > 0;
        }
    }
}
