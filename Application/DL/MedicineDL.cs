using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal class MedicineDL : IMedicineDAL 
    {
        private MySqlConnection GetConnection()
            => new MySqlConnection(Database.con);

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

        private bool ExecuteNonQuery(string query, string paramName, object value)
        {
            using (MySqlConnection con = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue(paramName, value);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int GetTotalMedicines() => ExecuteScalarInt("SELECT COUNT(*) FROM medicines");
        public int GetInStockMedicines() => ExecuteScalarInt("SELECT COUNT(*) FROM medicines WHERE StockQty > MinStockLevel");
        public int GetLowStockMedicines() => ExecuteScalarInt("SELECT COUNT(*) FROM medicines WHERE StockQty <= MinStockLevel AND StockQty > 0");
        public int GetOutOfStockMedicines() => ExecuteScalarInt("SELECT COUNT(*) FROM medicines WHERE StockQty = 0");
        public int GetExpiredMedicines() => ExecuteScalarInt("SELECT COUNT(*) FROM medicines WHERE ExpiryDate < CURDATE()");

        public DataTable GetCategories()
            => ExecuteQuery("SELECT CategoryID, CategoryName FROM medicinecategories");

        public DataTable GetSuppliers()
            => ExecuteQuery("SELECT SupplierID, SupplierName FROM suppliers ORDER BY SupplierName");

        public DataTable GetLowStockMedicinesList()
            => ExecuteQuery("SELECT * FROM medicines WHERE StockQty <= MinStockLevel");

        public DataTable GetExpiredMedicinesList()
            => ExecuteQuery("SELECT * FROM medicines WHERE ExpiryDate < CURDATE()");

        public DataTable GetAllMedicines()
        {
            return ExecuteQuery(@"
SELECT
    m.MedicineID, m.CategoryID, m.SupplierID,
    m.MedicineName, m.GenericName, m.UnitPrice,
    m.PurchasePrice, m.SellingPrice, m.StockQty,
    m.MinStockLevel, m.ExpiryDate, m.IsAvailable,
    c.CategoryName,
    CASE WHEN m.IsAvailable = 1 THEN 'Available' ELSE 'Unavailable' END AS Status
FROM medicines m
INNER JOIN medicinecategories c ON m.CategoryID = c.CategoryID
ORDER BY m.MedicineID DESC");
        }

        public DataTable GetCategoryOverview()
        {
            return ExecuteQuery(@"
SELECT
    c.CategoryName,
    COUNT(m.MedicineID) AS TotalMedicines
FROM medicinecategories c
LEFT JOIN medicines m ON c.CategoryID = m.CategoryID
GROUP BY c.CategoryID, c.CategoryName
ORDER BY c.CategoryName");
        }

        public DataTable SearchMedicines(string name, string category, string status)
        {
            using (MySqlConnection con = GetConnection())
            {
                string q = @"
SELECT
    m.MedicineID, m.MedicineName, m.GenericName,
    c.CategoryName, m.UnitPrice, m.PurchasePrice,
    m.SellingPrice, m.StockQty, m.ExpiryDate,
    CASE WHEN m.IsAvailable = 1 THEN 'Available' ELSE 'Unavailable' END AS Status
FROM medicines m
INNER JOIN medicinecategories c ON m.CategoryID = c.CategoryID
WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(name))
                    q += " AND m.MedicineName LIKE @name";

                if (!string.IsNullOrWhiteSpace(category))
                    q += " AND c.CategoryName = @category";

                if (status == "In Stock") q += " AND m.StockQty > m.MinStockLevel";
                else if (status == "Low Stock") q += " AND m.StockQty <= m.MinStockLevel AND m.StockQty > 0";
                else if (status == "Out Of Stock") q += " AND m.StockQty = 0";
                else if (status == "Expired") q += " AND m.ExpiryDate < CURDATE()";

                MySqlCommand cmd = new MySqlCommand(q, con);

                if (!string.IsNullOrWhiteSpace(name))
                    cmd.Parameters.AddWithValue("@name", "%" + name + "%");

                if (!string.IsNullOrWhiteSpace(category))
                    cmd.Parameters.AddWithValue("@category", category);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool AddMedicine(
            int categoryID, int supplierID, string medicineName,
            string genericName, decimal purchasePrice, decimal sellingPrice,
            decimal unitPrice, int stockQty, int minStock,
            DateTime expiryDate, bool available, int addedBy)
        {
            using (MySqlConnection con = GetConnection())
            {
                string q = @"
INSERT INTO medicines
(CategoryID, SupplierID, MedicineName, GenericName, PurchasePrice,
 SellingPrice, UnitPrice, StockQty, MinStockLevel, ExpiryDate, IsAvailable, AddedBy)
VALUES
(@CategoryID, @SupplierID, @MedicineName, @GenericName, @PurchasePrice,
 @SellingPrice, @UnitPrice, @StockQty, @MinStockLevel, @ExpiryDate, @IsAvailable, @AddedBy)";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                cmd.Parameters.AddWithValue("@MedicineName", medicineName);
                cmd.Parameters.AddWithValue("@GenericName", genericName);
                cmd.Parameters.AddWithValue("@PurchasePrice", purchasePrice);
                cmd.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                cmd.Parameters.AddWithValue("@StockQty", stockQty);
                cmd.Parameters.AddWithValue("@MinStockLevel", minStock);
                cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                cmd.Parameters.AddWithValue("@IsAvailable", available);
                cmd.Parameters.AddWithValue("@AddedBy", addedBy);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateMedicine(
            int medicineID, int categoryID, int supplierID, string medicineName,
            string genericName, decimal purchasePrice, decimal sellingPrice,
            decimal unitPrice, int stockQty, int minStock,
            DateTime expiryDate, bool available)
        {
            using (MySqlConnection con = GetConnection())
            {
                string q = @"
UPDATE medicines SET
    CategoryID    = @CategoryID,    SupplierID  = @SupplierID,
    MedicineName  = @MedicineName,  GenericName = @GenericName,
    PurchasePrice = @PurchasePrice, SellingPrice= @SellingPrice,
    UnitPrice     = @UnitPrice,     StockQty    = @StockQty,
    MinStockLevel = @MinStockLevel, ExpiryDate  = @ExpiryDate,
    IsAvailable   = @IsAvailable
WHERE MedicineID = @MedicineID";

                MySqlCommand cmd = new MySqlCommand(q, con);
                cmd.Parameters.AddWithValue("@MedicineID", medicineID);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                cmd.Parameters.AddWithValue("@MedicineName", medicineName);
                cmd.Parameters.AddWithValue("@GenericName", genericName);
                cmd.Parameters.AddWithValue("@PurchasePrice", purchasePrice);
                cmd.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                cmd.Parameters.AddWithValue("@StockQty", stockQty);
                cmd.Parameters.AddWithValue("@MinStockLevel", minStock);
                cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                cmd.Parameters.AddWithValue("@IsAvailable", available);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteMedicine(int medicineID)
            => ExecuteNonQuery(
                "DELETE FROM medicines WHERE MedicineID = @id",
                "@id", medicineID);
    }
}
