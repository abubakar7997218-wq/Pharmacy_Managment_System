using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal class suppliersDL : ISupplierDAL  
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
        private int ExecuteScalarInt(string query, MySqlCommand cmd)
        {
            using (MySqlConnection con = GetConnection())
            {
                cmd.Connection = con;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
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

        public int GetTotalSuppliers()
        {
            return ExecuteScalarInt(
                "SELECT COUNT(*) FROM suppliers");
        }

        public int GetActiveSuppliers()
        {
            return ExecuteScalarInt(
                "SELECT COUNT(*) FROM suppliers WHERE IsActive = 1");
        }

        public int GetNewSuppliersThisMonth()
        {
            return ExecuteScalarInt(@"
SELECT COUNT(*) FROM suppliers
WHERE MONTH(CreatedAt) = MONTH(CURDATE())
AND YEAR(CreatedAt) = YEAR(CURDATE())");
        }

        public DataTable GetAllSuppliers()
        {
            return ExecuteQuery(@"
SELECT
    SupplierID,
    SupplierName,
    ContactName,
    Phone,
    Email,
    Address,
    CASE WHEN IsActive = 1 THEN 'Active' ELSE 'Inactive' END AS Status
FROM suppliers");
        }

        public DataTable GetSupplierNames()
        {
            return ExecuteQuery(
                "SELECT SupplierID, SupplierName FROM suppliers");
        }

        public DataTable SearchSuppliers(
            string contactName,
            string status,
            string supplier)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT
    SupplierID,
    SupplierName,
    ContactName,
    Phone,
    Email,
    Address,
    CASE WHEN IsActive = 1 THEN 'Active' ELSE 'Inactive' END AS Status
FROM suppliers
WHERE 1=1";

                if (!string.IsNullOrEmpty(contactName))
                    query += " AND ContactName LIKE @contact";

                if (!string.IsNullOrEmpty(supplier))
                    query += " AND SupplierName = @supplier";

                if (status == "Active")
                    query += " AND IsActive = 1";
                else if (status == "Inactive")
                    query += " AND IsActive = 0";

                MySqlCommand cmd = new MySqlCommand(query, con);

                if (!string.IsNullOrEmpty(contactName))
                    cmd.Parameters.AddWithValue("@contact", "%" + contactName + "%");

                if (!string.IsNullOrEmpty(supplier))
                    cmd.Parameters.AddWithValue("@supplier", supplier);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool AddSupplier(
            string supplierName, string contactName,
            string phone, string email,
            string address, bool active)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
INSERT INTO suppliers
(SupplierName, ContactName, Phone, Email, Address, IsActive)
VALUES
(@name, @contact, @phone, @email, @address, @active)";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", supplierName);
                cmd.Parameters.AddWithValue("@contact", contactName);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@active", active);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateSupplier(
            int id, string supplierName, string contactName,
            string phone, string email,
            string address, bool active)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
UPDATE suppliers SET
    SupplierName = @name,
    ContactName  = @contact,
    Phone        = @phone,
    Email        = @email,
    Address      = @address,
    IsActive     = @active
WHERE SupplierID = @id";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", supplierName);
                cmd.Parameters.AddWithValue("@contact", contactName);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@active", active);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteSupplier(int supplierID)
        {
            using (MySqlConnection con = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(
                    "DELETE FROM suppliers WHERE SupplierID = @id", con);
                cmd.Parameters.AddWithValue("@id", supplierID);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeactivateSupplier(int supplierID)
        {
            using (MySqlConnection con = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(
                    "UPDATE suppliers SET IsActive = 0 WHERE SupplierID = @id", con);
                cmd.Parameters.AddWithValue("@id", supplierID);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool IsSupplierUsed(int supplierID)
        {
            using (MySqlConnection con = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM medicines WHERE SupplierID = @id", con);
                cmd.Parameters.AddWithValue("@id", supplierID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
    }

}
