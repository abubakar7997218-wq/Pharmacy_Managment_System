using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.DL
{
    internal class StaffDashboardDLL
    {
        public int GetAvailableMedicines()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q = @"
SELECT COUNT(*)
FROM medicines
WHERE IsAvailable=1";

                MySqlCommand cmd =
                    new MySqlCommand(q, con);

                con.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar());
            }
        }

        public int GetLowStockCount()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q = @"
SELECT COUNT(*)
FROM medicines
WHERE StockQty <= MinStockLevel
AND StockQty > 0";

                MySqlCommand cmd =
                    new MySqlCommand(q, con);

                con.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar());
            }
        }

        public int GetExpiryCount()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q = @"
SELECT COUNT(*)
FROM medicines
WHERE ExpiryDate <=
DATE_ADD(CURDATE(),INTERVAL 30 DAY)";

                MySqlCommand cmd =
                    new MySqlCommand(q, con);

                con.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar());
            }
        }

        public decimal GetTodaySales()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q = @"
SELECT IFNULL(
SUM(GrandTotal),0)
FROM bills
WHERE DATE(BillDate)=CURDATE()";

                MySqlCommand cmd =
                    new MySqlCommand(q, con);

                con.Open();

                return Convert.ToDecimal(
                    cmd.ExecuteScalar());
            }
        }
        public DataTable GetRecentBills()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q = @"
SELECT
'Sale' AS Type,
BillID,
CustomerName,
BillDate,
GrandTotal,
Status
FROM bills
ORDER BY BillID DESC
LIMIT 10";

                MySqlDataAdapter da =
                    new MySqlDataAdapter(q, con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                return dt;
            }
        }
        public DataTable GetWeeklySales()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q = @"
SELECT
DAYNAME(BillDate) AS DayName,
SUM(GrandTotal) AS TotalSales
FROM bills
GROUP BY DayName
ORDER BY FIELD(
DayName,
'Monday',
'Tuesday',
'Wednesday',
'Thursday',
'Friday',
'Saturday',
'Sunday'
);";

                MySqlDataAdapter da =
                    new MySqlDataAdapter(q, con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                return dt;
            }
        }
    }
}
