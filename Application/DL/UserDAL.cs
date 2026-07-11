using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application.DL
{
    internal class UserDAL
    {

        public DataTable Login(string username, string password, string role)
        {
            MySqlConnection con = new MySqlConnection(Database.con);

            string query = @"
    SELECT u.*, r.RoleName
    FROM users u
    INNER JOIN roles r ON u.RoleID = r.RoleID
    WHERE (u.Username=@user OR u.Email=@user)
    AND u.PasswordHash=@pass
    AND r.RoleName=@role
    AND u.IsActive=1";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@pass", password);
            cmd.Parameters.AddWithValue("@role", role);

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public DataTable VerifyIdentity(string user, string cnic)
        {
            MySqlConnection con = new MySqlConnection(Database.con);

            string query = @"SELECT *
                     FROM users
                     WHERE (Username=@user OR Email=@user)
                     AND CNIC=@cnic
                     AND IsActive=1";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@cnic", cnic);

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public bool UpdatePassword(string user, string password)
        {
            MySqlConnection con = new MySqlConnection(Database.con);

            string query = @"UPDATE users
                     SET PasswordHash=@pass
                     WHERE Username=@user
                     OR Email=@user";

            MySqlCommand cmd = new MySqlCommand(query, con);

            cmd.Parameters.AddWithValue("@pass", password);
            cmd.Parameters.AddWithValue("@user", user);

            con.Open();

            int rows = cmd.ExecuteNonQuery();

            con.Close();

            return rows > 0;
        }
    }
}
