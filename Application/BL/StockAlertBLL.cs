using Application.DL;
using System.Data;
namespace Application.BL
{ internal class StockAlertBLL : BaseBLL  
    {
        IStockAlertDAL dal = new StockAlertDL(); 
        public int GetLowStockCount() => dal.GetLowStockCount();
        public int GetOutStockCount() => dal.GetOutStockCount();
        public int GetHighStockCount() => dal.GetHighStockCount();
        public int GetNormalStockCount() => dal.GetNormalStockCount();
        public int GetTotalMedicineCount() => dal.GetTotalMedicineCount();
        public int GetSupplierCount() => dal.GetSupplierCount();
        public DataTable GetAllAlerts() => dal.GetAllAlerts();
        public DataTable GetLowStockMedicines() => dal.GetLowStockMedicines();
        public DataTable GetOutOfStockMedicines() => dal.GetOutOfStockMedicines();
        public DataTable GetHighStockMedicines() => dal.GetHighStockMedicines();
        public DataTable GetNormalStockMedicines() => dal.GetNormalStockMedicines();
        public DataTable GetCategories() => dal.GetCategories();

        public DataTable SearchStock(string medicine, int categoryID)
            => dal.SearchStock(medicine, categoryID);
        public DataTable SearchMedicines(string medicine, int categoryID, string status)
            => dal.SearchMedicines(medicine, categoryID, status);

        public DataTable SearchMedicines(string medicine, string status)
            => dal.SearchMedicines(medicine, 0, status);
        public DataTable SearchMedicines(string medicine)
            => dal.SearchMedicines(medicine, 0, "All");
    }
}