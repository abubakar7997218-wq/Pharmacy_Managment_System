using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

 
namespace Application.DL
    {
    internal class StockDeliveryDL : IStockDeliveryDAL 
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

        public int GetTotalDeliveries()
        {
            return ExecuteScalarInt(
                "SELECT COUNT(*) FROM stockdeliverymaster");
        }

        public int GetItemsReceived()
        {
            return ExecuteScalarInt(
                "SELECT IFNULL(SUM(QtyReceived), 0) FROM stockdeliverydetails");
        }

        public int GetActiveSuppliers()
        {
            return ExecuteScalarInt(
                "SELECT COUNT(*) FROM suppliers WHERE IsActive = 1");
        }

        public DataTable GetSuppliers()
        {
            return ExecuteQuery(@"
SELECT SupplierID, SupplierName
FROM suppliers
WHERE IsActive = 1
ORDER BY SupplierName");
        }

        public DataTable GetMedicines()
        {
            return ExecuteQuery(@"
SELECT MedicineID, MedicineName
FROM medicines
WHERE IsAvailable = 1
ORDER BY MedicineName");
        }

        public DataTable GetAllDeliveries()
        {
            return ExecuteQuery(@"
SELECT
    m.DeliveryID,
    s.SupplierName,
    m.InvoiceNo,
    m.DeliveryDate,
    COUNT(d.DetailID)            AS Items,
    SUM(d.QtyReceived * d.UnitCost) AS Amount
FROM stockdeliverymaster m
INNER JOIN suppliers s           ON m.SupplierID = s.SupplierID
INNER JOIN stockdeliverydetails d ON m.DeliveryID  = d.DeliveryID
GROUP BY m.DeliveryID, s.SupplierName, m.InvoiceNo, m.DeliveryDate
ORDER BY m.DeliveryID DESC");
        }

        public DataTable SearchDeliveries(string search)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT
    m.DeliveryID,
    s.SupplierName,
    m.InvoiceNo,
    m.DeliveryDate,
    COUNT(d.DetailID)            AS Items,
    SUM(d.QtyReceived * d.UnitCost) AS Amount
FROM stockdeliverymaster m
INNER JOIN suppliers s           ON m.SupplierID = s.SupplierID
INNER JOIN stockdeliverydetails d ON m.DeliveryID  = d.DeliveryID
WHERE s.SupplierName LIKE @Search OR m.InvoiceNo LIKE @Search
GROUP BY m.DeliveryID, s.SupplierName, m.InvoiceNo, m.DeliveryDate
ORDER BY m.DeliveryID DESC";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Search", "%" + search + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetDeliveryByID(int deliveryID)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT
    m.DeliveryID, m.SupplierID, m.InvoiceNo,
    m.DeliveryDate, m.ReceivedBy, m.Notes,
    d.DetailID, d.MedicineID, d.BatchNumber,
    d.ExpiryDate, d.QtyReceived, d.UnitCost
FROM stockdeliverymaster m
INNER JOIN stockdeliverydetails d ON m.DeliveryID = d.DeliveryID
WHERE m.DeliveryID = @id";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", deliveryID);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int SaveDeliveryMaster(
            int supplierID, string invoiceNo,
            DateTime deliveryDate, int receivedBy, string notes)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
INSERT INTO stockdeliverymaster
(SupplierID, InvoiceNo, DeliveryDate, ReceivedBy, Notes)
VALUES
(@SupplierID, @InvoiceNo, @DeliveryDate, @ReceivedBy, @Notes);
SELECT LAST_INSERT_ID();";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                cmd.Parameters.AddWithValue("@InvoiceNo", invoiceNo);
                cmd.Parameters.AddWithValue("@DeliveryDate", deliveryDate);
                cmd.Parameters.AddWithValue("@ReceivedBy", receivedBy);
                cmd.Parameters.AddWithValue("@Notes", notes);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool SaveDeliveryDetail(
            int deliveryID, int medicineID, string batchNo,
            DateTime expiryDate, int qty, decimal unitCost)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
INSERT INTO stockdeliverydetails
(DeliveryID, MedicineID, BatchNumber, ExpiryDate, QtyReceived, UnitCost)
VALUES
(@DeliveryID, @MedicineID, @BatchNumber, @ExpiryDate, @QtyReceived, @UnitCost)";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DeliveryID", deliveryID);
                cmd.Parameters.AddWithValue("@MedicineID", medicineID);
                cmd.Parameters.AddWithValue("@BatchNumber", batchNo);
                cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                cmd.Parameters.AddWithValue("@QtyReceived", qty);
                cmd.Parameters.AddWithValue("@UnitCost", unitCost);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateMedicineStock(int medicineID, int qty)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
UPDATE medicines
SET StockQty = StockQty + @Qty
WHERE MedicineID = @MedicineID";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Qty", qty);
                cmd.Parameters.AddWithValue("@MedicineID", medicineID);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteDelivery(int deliveryID)
        {
            using (MySqlConnection con = GetConnection())
            {
                con.Open();
                MySqlTransaction tr = con.BeginTransaction();

                try
                {
                    MySqlCommand cmd1 = new MySqlCommand(
                        "DELETE FROM stockdeliverydetails WHERE DeliveryID = @ID",
                        con, tr);
                    cmd1.Parameters.AddWithValue("@ID", deliveryID);
                    cmd1.ExecuteNonQuery();

                    MySqlCommand cmd2 = new MySqlCommand(
                        "DELETE FROM stockdeliverymaster WHERE DeliveryID = @ID",
                        con, tr);
                    cmd2.Parameters.AddWithValue("@ID", deliveryID);
                    bool result = cmd2.ExecuteNonQuery() > 0;

                    tr.Commit();
                    return result;
                }
                catch
                {
                    tr.Rollback();
                    throw;
                }
            }
        }
    }
}




