using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal class StockAlertDL : IStockAlertDAL  
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
                MySqlCommand cmd = new MySqlCommand(query, con);
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

        public DataTable GetAllAlerts()
        {
            return ExecuteQuery(@"
SELECT
    MedicineID,
    MedicineName,
    Brand,
    StockQty,
    CASE
        WHEN StockQty = 0             THEN 'Out Of Stock'
        WHEN StockQty <= MinStockLevel THEN 'Low Stock'
        WHEN StockQty >= 100          THEN 'High Stock'
        ELSE 'Normal'
    END AS Status
FROM Medicines");
        }

        public int GetLowStockCount()
        {
            return ExecuteScalar(@"
SELECT COUNT(*) FROM Medicines
WHERE StockQty <= MinStockLevel AND StockQty > 0");
        }

        public int GetOutStockCount()
        {
            return ExecuteScalar(
                "SELECT COUNT(*) FROM Medicines WHERE StockQty = 0");
        }

        public int GetHighStockCount()
        {
            return ExecuteScalar(
                "SELECT COUNT(*) FROM Medicines WHERE StockQty >= 100");
        }

        public int GetNormalStockCount()
        {
            return ExecuteScalar(@"
SELECT COUNT(*) FROM Medicines
WHERE StockQty > MinStockLevel AND StockQty < 100");
        }

        public int GetTotalMedicineCount()
        {
            return ExecuteScalar(
                "SELECT COUNT(*) FROM Medicines");
        }

        public int GetSupplierCount()
        {
            return ExecuteScalar(
                "SELECT COUNT(*) FROM Suppliers");
        }

        public DataTable GetLowStockMedicines()
        {
            return ExecuteQuery(@"
SELECT * FROM Medicines
WHERE StockQty <= MinStockLevel AND StockQty > 0");
        }

        public DataTable GetOutOfStockMedicines()
        {
            return ExecuteQuery(
                "SELECT * FROM Medicines WHERE StockQty = 0");
        }

        public DataTable GetHighStockMedicines()
        {
            return ExecuteQuery(
                "SELECT * FROM Medicines WHERE StockQty >= 100");
        }

        public DataTable GetNormalStockMedicines()
        {
            return ExecuteQuery(@"
SELECT * FROM Medicines
WHERE StockQty > MinStockLevel AND StockQty < 100");
        }

        public DataTable GetCategories()
        {
            return ExecuteQuery(
                "SELECT CategoryID, CategoryName FROM medicinecategories");
        }

        public DataTable SearchStock(string medicine, int categoryID)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT * FROM medicines
WHERE MedicineName LIKE @name
AND (@cat = 0 OR CategoryID = @cat)";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", "%" + medicine + "%");
                cmd.Parameters.AddWithValue("@cat", categoryID);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable SearchMedicines(string medicine, int categoryID, string status)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT * FROM medicines
WHERE (@medicine = '' OR MedicineName LIKE @medicine)
AND   (@category = 0  OR CategoryID = @category)
AND
(
    @status = 'All'
    OR (@status = 'Available'   AND IsAvailable = 1)
    OR (@status = 'Unavailable' AND IsAvailable = 0)
)";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@medicine", "%" + medicine + "%");
                cmd.Parameters.AddWithValue("@category", categoryID);
                cmd.Parameters.AddWithValue("@status", status);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
