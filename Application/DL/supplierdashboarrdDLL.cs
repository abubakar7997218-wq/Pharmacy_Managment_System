using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal class supplierdashboarrdDLL
    {
        public int GetTotalDeliveries()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q =
                    "SELECT COUNT(*) FROM stockdeliverymaster";

                MySqlCommand cmd =
                    new MySqlCommand(q, con);

                con.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar());
            }
        }
        public int GetTotalItemsDelivered()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q = @"
SELECT IFNULL(
SUM(QtyReceived),0)
FROM stockdeliverydetails";

                MySqlCommand cmd =
                    new MySqlCommand(q, con);

                con.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar());
            }
        }
        public int GetTotalSuppliers()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q =
                    "SELECT COUNT(*) FROM suppliers";

                MySqlCommand cmd =
                    new MySqlCommand(q, con);

                con.Open();

                return Convert.ToInt32(
                    cmd.ExecuteScalar());
            }
        }
        public decimal GetTotalDeliveryValue()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q = @"
SELECT IFNULL(
SUM(QtyReceived * UnitCost),0)
FROM stockdeliverydetails";

                MySqlCommand cmd =
                    new MySqlCommand(q, con);

                con.Open();

                return Convert.ToDecimal(
                    cmd.ExecuteScalar());
            }
        }
        public DataTable GetMonthlyDeliveries()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q = @"
SELECT
MONTHNAME(DeliveryDate)
AS MonthName,

COUNT(*)
AS TotalDeliveries

FROM stockdeliverymaster

GROUP BY
MONTH(DeliveryDate),
MONTHNAME(DeliveryDate)

ORDER BY
MONTH(DeliveryDate)";

                MySqlDataAdapter da =
                    new MySqlDataAdapter(q, con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                return dt;
            }
        }
        public DataTable GetLowStockMedicines()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q = @"
SELECT
MedicineName,
StockQty
FROM medicines
WHERE StockQty <= MinStockLevel
ORDER BY StockQty ASC
LIMIT 10";

                MySqlDataAdapter da =
                    new MySqlDataAdapter(q, con);

                DataTable dt =
                    new DataTable();

                da.Fill(dt);

                return dt;
            }
        }
        public DataTable GetRecentDeliveries()
        {
            using (MySqlConnection con =
                new MySqlConnection(Database.con))
            {
                string q = @"
SELECT
m.DeliveryID,
m.InvoiceNo,
s.SupplierName,
m.DeliveryDate,

COUNT(d.DetailID)
AS Items,

SUM(
d.QtyReceived *
d.UnitCost)
AS Amount

FROM stockdeliverymaster m

INNER JOIN suppliers s
ON m.SupplierID=s.SupplierID

INNER JOIN stockdeliverydetails d
ON m.DeliveryID=d.DeliveryID

GROUP BY
m.DeliveryID

ORDER BY
m.DeliveryID DESC

LIMIT 10";

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
