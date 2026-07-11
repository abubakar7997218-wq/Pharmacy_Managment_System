using Application.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BL
{
    internal class MedicineBLL : BaseBLL
    {
        IMedicineDAL dal = new MedicineDL(); 

        public int TotalMedicines { get { return dal.GetTotalMedicines(); } }
        public int InStockMedicines { get { return dal.GetInStockMedicines(); } }
        public int LowStockMedicines { get { return dal.GetLowStockMedicines(); } }
        public int OutOfStockMedicines { get { return dal.GetOutOfStockMedicines(); } }
        public int ExpiredMedicines { get { return dal.GetExpiredMedicines(); } }


        public int GetTotalMedicines() => TotalMedicines;
        public int GetInStockMedicines() => InStockMedicines;
        public int GetLowStockMedicines() => LowStockMedicines;
        public int GetOutOfStockMedicines() => OutOfStockMedicines;
        public int GetExpiredMedicines() => ExpiredMedicines;


        public DataTable GetAllMedicines() => dal.GetAllMedicines();
        public DataTable GetCategories() => dal.GetCategories();
        public DataTable GetSuppliers() => dal.GetSuppliers();
        public DataTable GetCategoryOverview() => dal.GetCategoryOverview();
        public DataTable GetLowStockMedicinesList() => dal.GetLowStockMedicinesList();
        public DataTable GetExpiredMedicinesList() => dal.GetExpiredMedicinesList();
        public bool DeleteMedicine(int id) => dal.DeleteMedicine(id);

        public DataTable SearchMedicines(string name, string category, string status)
            => dal.SearchMedicines(name, category, status);

        public bool AddMedicine(
            int categoryID, int supplierID, string medicineName,
            string genericName, decimal purchasePrice, decimal sellingPrice,
            decimal unitPrice, int stockQty, int minStock,
            DateTime expiryDate, bool available, int addedBy)
            => dal.AddMedicine(categoryID, supplierID, medicineName, genericName,
               purchasePrice, sellingPrice, unitPrice, stockQty, minStock,
               expiryDate, available, addedBy);

        public bool AddMedicine(
            int categoryID, int supplierID, string medicineName,
            string genericName, decimal purchasePrice, decimal sellingPrice,
            decimal unitPrice, int stockQty, int minStock,
            DateTime expiryDate, bool available)
            => dal.AddMedicine(categoryID, supplierID, medicineName, genericName,
               purchasePrice, sellingPrice, unitPrice, stockQty, minStock,
               expiryDate, available, 1);

        public bool UpdateMedicine(
            int medicineID, int categoryID, int supplierID, string medicineName,
            string genericName, decimal purchasePrice, decimal sellingPrice,
            decimal unitPrice, int stockQty, int minStock,
            DateTime expiryDate, bool available)
            => dal.UpdateMedicine(medicineID, categoryID, supplierID, medicineName,
               genericName, purchasePrice, sellingPrice, unitPrice, stockQty,
               minStock, expiryDate, available);
    }
}
