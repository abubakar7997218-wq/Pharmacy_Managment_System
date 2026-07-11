using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal class searchmedicineDAL : ISearchMedicineDAL 
    { 
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(Database.con);
        }

        public DataTable SearchMedicine(
            string keyword,
            string category,
            string company,
            string stockStatus)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT
    MedicineID,
    MedicineName,
    CategoryID,
    Brand,
    StockQty,
    UnitPrice,
    ExpiryDate,
    CASE
        WHEN StockQty = 0 THEN 'Out Of Stock'
        WHEN StockQty <= MinStockLevel THEN 'Low Stock'
        ELSE 'In Stock'
    END AS Status
FROM Medicines
WHERE
    (@keyword = '' OR MedicineName LIKE CONCAT('%',@keyword,'%'))
    AND (@category = 'All Categories' OR CategoryID = @category)
    AND (@company  = 'All Companies'  OR Brand = @company)";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@keyword", keyword);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@company", company);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (stockStatus != "All Status")
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = $"Status = '{stockStatus}'";
                    return dv.ToTable();
                }

                return dt;
            }
        }

        public DataTable GetAllMedicines()
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT
    MedicineID,
    MedicineName,
    Brand,
    StockQty,
    UnitPrice,
    ExpiryDate
FROM Medicines
ORDER BY MedicineName";

                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public bool DeleteMedicine(int medicineID)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query =
                    "DELETE FROM medicines WHERE MedicineID = @MedicineID";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MedicineID", medicineID);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
