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
    internal class ExpiryAlertDL : IExpiryAlertDAL
    {
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(Database.con);
        }
        private int ExecuteScalar(string query)
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

        public DataTable GetExpiryMedicines()
        {
            return ExecuteQuery(@"
SELECT
    MedicineID,
    MedicineName,
    CategoryID,
    BatchNumber,
    ExpiryDate,
    StockQty,
    SupplierID,
    DATEDIFF(ExpiryDate, CURDATE()) AS DaysLeft
FROM medicines
WHERE ExpiryDate >= CURDATE()
ORDER BY ExpiryDate");
        }

        public int Get7DaysCount()
        {
            return ExecuteScalar(@"
SELECT COUNT(*) FROM medicines
WHERE DATEDIFF(ExpiryDate, CURDATE()) BETWEEN 0 AND 7");
        }

        public int Get30DaysCount()
        {
            return ExecuteScalar(@"
SELECT COUNT(*) FROM medicines
WHERE DATEDIFF(ExpiryDate, CURDATE()) BETWEEN 8 AND 30");
        }

        public int Get90DaysCount()
        {
            return ExecuteScalar(@"
SELECT COUNT(*) FROM medicines
WHERE DATEDIFF(ExpiryDate, CURDATE()) BETWEEN 31 AND 90");
        }

        public int GetAbove90DaysCount()
        {
            return ExecuteScalar(@"
SELECT COUNT(*) FROM medicines
WHERE DATEDIFF(ExpiryDate, CURDATE()) > 90");
        }

        public DataTable FilterExpiryMedicines(
            int categoryID,
            int supplierID,
            DateTime fromDate,
            DateTime toDate)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT
    MedicineID,
    MedicineName,
    CategoryID,
    BatchNumber,
    ExpiryDate,
    StockQty,
    SupplierID,
    DATEDIFF(ExpiryDate, CURDATE()) AS DaysLeft
FROM medicines
WHERE
    (@cat = 0 OR CategoryID = @cat)
    AND (@sup = 0 OR SupplierID = @sup)
    AND ExpiryDate BETWEEN @from AND @to
ORDER BY ExpiryDate";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@cat", categoryID);
                cmd.Parameters.AddWithValue("@sup", supplierID);
                cmd.Parameters.AddWithValue("@from", fromDate);
                cmd.Parameters.AddWithValue("@to", toDate);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetCategories()
        {
            return ExecuteQuery(
                "SELECT CategoryID, CategoryName FROM medicinecategories");
        }

        public DataTable GetSuppliers()
        {
            return ExecuteQuery(
                "SELECT SupplierID, SupplierName FROM suppliers");
        }
    }
}

