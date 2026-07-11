using Application.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BL
{
    internal class UserManagementBLL : BaseBLL      {
        IUserManagementDAL dal = new UserManagementDL();  

        public int TotalUsers
        {
            get { return dal.GetTotalUsers(); }
        }

        public int ActiveUsers
        {
            get { return dal.GetActiveUsers(); }
        }

        public int InactiveUsers
        {
            get { return dal.GetInactiveUsers(); }
        }

        public int RolesCount
        {
            get { return dal.GetRolesCount(); }
        }


        public int GetTotalUsers() => TotalUsers;
        public int GetActiveUsers() => ActiveUsers;
        public int GetInactiveUsers() => InactiveUsers;
        public int GetRolesCount() => RolesCount;

        public DataTable GetRoles() => dal.GetRoles();
        public DataTable GetAllUsers() => dal.GetAllUsers();
        public DataTable GetRoleOverview() => dal.GetRoleOverview();
        public bool MakeUserInactive(int id) => dal.MakeUserInactive(id);
        public bool DeleteUser(int id) => dal.DeleteUser(id);

        public DataTable SearchUsers(string search, string role, string status)
            => dal.SearchUsers(search, role, status);
        public bool AddUser(
            string fullName, string username, string email,
            string password, string cnic, int roleID, bool active)
            => dal.AddUser(fullName, username, email, password, cnic, roleID, active);
        public bool AddUser(
            string fullName, string username, string email,
            string password, string cnic, int roleID)
            => dal.AddUser(fullName, username, email, password, cnic, roleID, true);
        public bool UpdateUser(
            int userID, string fullName, string username,
            string email, string cnic, int roleID, bool active)
            => dal.UpdateUser(userID, fullName, username, email, cnic, roleID, active);
        public bool UpdateUser(
            int userID, string fullName, string username,
            string email, string cnic, int roleID)
            => dal.UpdateUser(userID, fullName, username, email, cnic, roleID, true);
    }
}

