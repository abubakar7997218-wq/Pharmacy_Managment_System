using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Application.DL
{
    internal class DashboardDAL
    {
        public int GetTotalUsers()
        {
            using (MySqlConnection con = new MySqlConnection(Database.con))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE IsActive=1";

                MySqlCommand cmd = new MySqlCommand(query, con);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetTotalMedicines()
        {
            using (MySqlConnection con = new MySqlConnection(Database.con))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM Medicines";

                MySqlCommand cmd = new MySqlCommand(query, con);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetTotalSuppliers()
        {
            using (MySqlConnection con = new MySqlConnection(Database.con))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM Suppliers";

                MySqlCommand cmd = new MySqlCommand(query, con);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetLowStock()
        {
            MySqlConnection con = new MySqlConnection(Database.con);

            string query = @"SELECT COUNT(*)
                     FROM medicines
                     WHERE StockQty <= MinStockLevel";

            MySqlCommand cmd = new MySqlCommand(query, con);

            con.Open();

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            con.Close();

            return count;
        }

        public decimal GetTodaySales()
        {
            using (MySqlConnection con = new MySqlConnection(Database.con))
            {
                con.Open();

                string query =
@"SELECT IFNULL(SUM(GrandTotal),0)
  FROM Bills
  WHERE DATE(BillDate)=CURDATE()";

                MySqlCommand cmd = new MySqlCommand(query, con);

                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        
            public DataTable GetMonthlySales()
        {
            using (MySqlConnection con = new MySqlConnection(Database.con))
            {
                string query = @"
        SELECT MONTH(BillDate) MonthNo,
               SUM(GrandTotal) Total
        FROM Bills
        GROUP BY MONTH(BillDate)
        ORDER BY MonthNo";

                MySqlDataAdapter da =
                    new MySqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }


        }
        public DataTable GetRecentTransactions()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string query = @"
        SELECT
            BillID AS ID,
            'Sale' AS Type,
            CustomerName AS User,
            BillDate AS Date,
            GrandTotal AS Amount,
            Status
        FROM Bills
        ORDER BY BillDate DESC
        LIMIT 10";

                MySqlDataAdapter da =
                    new MySqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                return dt;
            }
        }
    }
    }
