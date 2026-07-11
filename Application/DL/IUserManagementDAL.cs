using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal interface IUserManagementDAL
    {
        int GetTotalUsers();
        int GetActiveUsers();
        int GetInactiveUsers();
        int GetRolesCount();
        DataTable GetRoles();
        DataTable GetAllUsers();
        DataTable SearchUsers(string search, string role, string status);
        DataTable GetRoleOverview();
       
        bool MakeUserInactive(int userID);
        bool DeleteUser(int userID);
        bool AddUser(string fullName, string username, string email, string password, string cnic, int roleID, bool active);
        bool UpdateUser(int userID, string fullName, string username, string email, string cnic, int roleID, bool active);
    }
}
