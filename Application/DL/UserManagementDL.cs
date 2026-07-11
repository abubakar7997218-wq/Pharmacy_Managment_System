using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal class UserManagementDL : IUserManagementDAL  
    {
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(Database.con);
        }
        private int ExecuteScalarInt(string query)
        {
            using (MySqlConnection con = GetConnection())
            {
                con.Open();
                return Convert.ToInt32(
                    new MySqlCommand(query, con).ExecuteScalar());
            }
        }
        private DataTable ExecuteQuery(string query)
        {
            using (MySqlConnection con = GetConnection())
            {
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        private bool ExecuteNonQuery(string query, string paramName, object paramValue)
        {
            using (MySqlConnection con = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue(paramName, paramValue);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int GetTotalUsers() => ExecuteScalarInt("SELECT COUNT(*) FROM users");
        public int GetActiveUsers() => ExecuteScalarInt("SELECT COUNT(*) FROM users WHERE IsActive = 1");
        public int GetInactiveUsers() => ExecuteScalarInt("SELECT COUNT(*) FROM users WHERE IsActive = 0");
        public int GetRolesCount() => ExecuteScalarInt("SELECT COUNT(*) FROM roles");

        public DataTable GetRoles()
        {
            return ExecuteQuery("SELECT RoleID, RoleName FROM roles");
        }

        public DataTable GetAllUsers()
        {
            return ExecuteQuery(@"
SELECT
    u.UserID,
    u.Username,
    u.FullName,
    u.Email,
    u.CNIC,
    r.RoleName,
    CASE WHEN u.IsActive = 1 THEN 'Active' ELSE 'Inactive' END AS Status,
    u.CreatedAt
FROM users u
INNER JOIN roles r ON u.RoleID = r.RoleID
ORDER BY u.UserID DESC");
        }

        public DataTable GetRoleOverview()
        {
            return ExecuteQuery(@"
SELECT
    r.RoleName,
    COUNT(u.UserID) AS TotalUsers
FROM roles r
LEFT JOIN users u ON r.RoleID = u.RoleID
GROUP BY r.RoleID, r.RoleName");
        }

        public DataTable SearchUsers(string search, string role, string status)
        {
            using (MySqlConnection con = GetConnection())
            {
                string q = @"
SELECT
    u.UserID,
    u.Username,
    u.FullName,
    u.Email,
    r.RoleName,
    CASE WHEN u.IsActive = 1 THEN 'Active' ELSE 'Inactive' END AS Status,
    u.CreatedAt
FROM users u
INNER JOIN roles r ON u.RoleID = r.RoleID
WHERE 1=1";

                if (!string.IsNullOrEmpty(search))
                    q += " AND (u.FullName LIKE @search OR u.Username LIKE @search OR u.Email LIKE @search)";

                if (!string.IsNullOrEmpty(role))
                    q += " AND r.RoleName = @role";

                if (status == "Active")
                    q += " AND u.IsActive = 1";
                else if (status == "Inactive")
                    q += " AND u.IsActive = 0";

                MySqlCommand cmd = new MySqlCommand(q, con);

                if (!string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                if (!string.IsNullOrEmpty(role))
                    cmd.Parameters.AddWithValue("@role", role);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool AddUser(
            string fullName, string username, string email,
            string password, string cnic, int roleID, bool active)
        {
            using (MySqlConnection con = GetConnection())
            {
                string q = @"
INSERT INTO users
(FullName, Username, Email, PasswordHash, CNIC, RoleID, IsActive)
VALUES
(@FullName, @Username, @Email, @Password, @CNIC, @RoleID, @Active)";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@CNIC", cnic);
                cmd.Parameters.AddWithValue("@RoleID", roleID);
                cmd.Parameters.AddWithValue("@Active", active);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateUser(
            int userID, string fullName, string username,
            string email, string cnic, int roleID, bool active)
        {
            using (MySqlConnection con = GetConnection())
            {
                string q = @"
UPDATE users SET
    FullName = @FullName,
    Username = @Username,
    Email    = @Email,
    CNIC     = @CNIC,
    RoleID   = @RoleID,
    IsActive = @Active
WHERE UserID = @UserID";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@CNIC", cnic);
                cmd.Parameters.AddWithValue("@RoleID", roleID);
                cmd.Parameters.AddWithValue("@Active", active);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool MakeUserInactive(int userID)
        {
            return ExecuteNonQuery(
                "UPDATE users SET IsActive = 0 WHERE UserID = @UserID",
                "@UserID", userID);
        }

        public bool DeleteUser(int userID)
        {
            return ExecuteNonQuery(
                "DELETE FROM users WHERE UserID = @UserID",
                "@UserID", userID);
        }
    }
}
