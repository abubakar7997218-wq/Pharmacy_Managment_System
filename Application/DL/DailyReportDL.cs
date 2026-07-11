using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal class DailyReportDL : IDailyReportDAL 
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
        private decimal ExecuteScalarDecimal(string query)
        {
            using (MySqlConnection con = GetConnection())
            {
                con.Open();
                return Convert.ToDecimal(
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

        public decimal GetTotalSales()
        {
            return ExecuteScalarDecimal(@"
SELECT IFNULL(SUM(GrandTotal), 0)
FROM bills
WHERE DATE(BillDate) = CURDATE()");
        }

        public int GetTotalBills()
        {
            return ExecuteScalarInt(@"
SELECT COUNT(*) FROM bills
WHERE DATE(BillDate) = CURDATE()");
        }

        public int GetItemsSold()
        {
            return ExecuteScalarInt(@"
SELECT IFNULL(SUM(Quantity), 0)
FROM billitems");
        }

        public int GetCustomerCount()
        {
            return ExecuteScalarInt(@"
SELECT COUNT(DISTINCT CustomerName)
FROM bills");
        }

        public decimal GetAverageBill()
        {
            return ExecuteScalarDecimal(@"
SELECT IFNULL(AVG(GrandTotal), 0)
FROM bills");
        }

        public DataTable GetSalesDetails()
        {
            return ExecuteQuery(@"
SELECT
    BillID,
    BillDate,
    CustomerName,
    PaymentMethod,
    GrandTotal
FROM bills
ORDER BY BillDate DESC");
        }

        public DataTable GetSalesTrend()
        {
            return ExecuteQuery(@"
SELECT
    HOUR(BillDate) AS HourNo,
    SUM(GrandTotal) AS Sales
FROM bills
GROUP BY HOUR(BillDate)
ORDER BY HourNo");
        }

        public DataTable GetPaymentSummary()
        {
            return ExecuteQuery(@"
SELECT
    PaymentMethod,
    SUM(GrandTotal) AS Amount
FROM bills
GROUP BY PaymentMethod");
        }

        public DataTable GetPaymentMethods()
        {
            return ExecuteQuery(
                "SELECT DISTINCT PaymentMethod FROM bills");
        }

        public DataTable GetCustomers()
        {
            return ExecuteQuery(@"
SELECT DISTINCT CustomerName
FROM bills
WHERE CustomerName IS NOT NULL");
        }

        public DataTable GetStatuses()
        {
            return ExecuteQuery(
                "SELECT DISTINCT Status FROM bills");
        }

        public DataTable GetFilteredSales(
            DateTime date,
            string payment,
            string customer,
            string status)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT
    BillID,
    BillDate,
    CustomerName,
    PaymentMethod,
    Status,
    GrandTotal
FROM bills
WHERE DATE(BillDate) = @date";

                if (!string.IsNullOrEmpty(payment))
                    query += " AND PaymentMethod = @payment";

                if (!string.IsNullOrEmpty(customer))
                    query += " AND CustomerName = @customer";

                if (!string.IsNullOrEmpty(status))
                    query += " AND Status = @status";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));

                if (!string.IsNullOrEmpty(payment))
                    cmd.Parameters.AddWithValue("@payment", payment);

                if (!string.IsNullOrEmpty(customer))
                    cmd.Parameters.AddWithValue("@customer", customer);

                if (!string.IsNullOrEmpty(status))
                    cmd.Parameters.AddWithValue("@status", status);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
