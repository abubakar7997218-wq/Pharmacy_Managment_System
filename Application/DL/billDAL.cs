using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal class billDAL : IBillDAL  
    {
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(Database.con);
        }

        public int SaveBill(
            string customer,
            string phone,
            decimal subTotal,
            decimal discount,
            decimal tax,
            decimal grandTotal)
        {
            using (MySqlConnection con = GetConnection())
            {
                con.Open();

                string query = @"
INSERT INTO Bills
(
    CustomerName,
    CustomerPhone,
    BillDate,
    SubTotal,
    DiscountAmount,
    GSTAmount,
    GrandTotal,
    Status
)
VALUES
(
    @customer,
    @phone,
    NOW(),
    @sub,
    @discount,
    @tax,
    @grand,
    'Completed'
);

SELECT LAST_INSERT_ID();";

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@customer", customer);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@sub", subTotal);
                cmd.Parameters.AddWithValue("@discount", discount);
                cmd.Parameters.AddWithValue("@tax", tax);
                cmd.Parameters.AddWithValue("@grand", grandTotal);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void SaveBillItem(
            int billID,
            int medicineID,
            int qty,
            decimal price,
            decimal discount,
            decimal total)
        {
            using (MySqlConnection con = GetConnection())
            {
                con.Open();

                string query = @"
INSERT INTO billitems
(
    BillID,
    MedicineID,
    Quantity,
    UnitPrice,
    TotalPrice
)
VALUES
(
    @bill,
    @med,
    @qty,
    @price,
    @total
)";

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.AddWithValue("@bill", billID);
                cmd.Parameters.AddWithValue("@med", medicineID);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@discount", discount);
                cmd.Parameters.AddWithValue("@total", total);

                cmd.ExecuteNonQuery();
            }
        }

        public DataTable SearchMedicine(string name)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT *
FROM Medicines
WHERE MedicineName = @name";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", name);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
    }
}
    

