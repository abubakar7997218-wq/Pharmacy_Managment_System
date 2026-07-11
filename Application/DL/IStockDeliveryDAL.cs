using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal interface IStockDeliveryDAL
    {
        int GetTotalDeliveries();
        int GetItemsReceived();
        int GetActiveSuppliers();
        DataTable GetSuppliers();
        DataTable GetMedicines();
        DataTable GetAllDeliveries();
        DataTable SearchDeliveries(string search);
        DataTable GetDeliveryByID(int deliveryID);
        int SaveDeliveryMaster(int supplierID, string invoiceNo, DateTime deliveryDate, int receivedBy, string notes);
        bool SaveDeliveryDetail(int deliveryID, int medicineID, string batchNo, DateTime expiryDate, int qty, decimal unitCost);
        bool UpdateMedicineStock(int medicineID, int qty);
        bool DeleteDelivery(int deliveryID);
    }
}
