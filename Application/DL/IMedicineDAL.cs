using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal interface IMedicineDAL
    {
        int GetTotalMedicines();
        int GetInStockMedicines();
        int GetLowStockMedicines();
        int GetOutOfStockMedicines();
        int GetExpiredMedicines();
        DataTable GetAllMedicines();
        DataTable GetCategories();
        DataTable GetSuppliers();
        DataTable GetCategoryOverview();
        DataTable GetLowStockMedicinesList();
        DataTable GetExpiredMedicinesList();
        DataTable SearchMedicines(string name, string category, string status);
        bool AddMedicine(int categoryID, int supplierID, string medicineName, string genericName, decimal purchasePrice, decimal sellingPrice, decimal unitPrice, int stockQty, int minStock, DateTime expiryDate, bool available, int addedBy);
        bool UpdateMedicine(int medicineID, int categoryID, int supplierID, string medicineName, string genericName, decimal purchasePrice, decimal sellingPrice, decimal unitPrice, int stockQty, int minStock, DateTime expiryDate, bool available);
        bool DeleteMedicine(int medicineID);
    }
}
