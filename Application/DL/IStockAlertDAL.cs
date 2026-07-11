using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal interface IStockAlertDAL
    {
        DataTable GetAllAlerts();
        int GetLowStockCount();
        int GetOutStockCount();
        int GetHighStockCount();
        int GetNormalStockCount();
        int GetTotalMedicineCount();
        int GetSupplierCount();
        DataTable GetLowStockMedicines();
        DataTable GetOutOfStockMedicines();
        DataTable GetHighStockMedicines();
        DataTable GetNormalStockMedicines();
        DataTable GetCategories();
        DataTable SearchStock(string medicine, int categoryID);
        DataTable SearchMedicines(string medicine, int categoryID, string status);
    }
}
