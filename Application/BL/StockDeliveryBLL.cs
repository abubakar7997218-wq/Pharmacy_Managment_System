using Application.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BL
{
    internal class StockDeliveryBLL : BaseBLL 
    {
        IStockDeliveryDAL dal = new StockDeliveryDL(); 


        public int TotalDeliveries
        {
            get { return dal.GetTotalDeliveries(); }
        }

        public int ItemsReceived
        {
            get { return dal.GetItemsReceived(); }
        }

        public int ActiveSuppliers
        {
            get { return dal.GetActiveSuppliers(); }
        }

        public int GetTotalDeliveries() => TotalDeliveries;
        public int GetItemsReceived() => ItemsReceived;
        public int GetActiveSuppliers() => ActiveSuppliers;


        public DataTable GetSuppliers() => dal.GetSuppliers();
        public DataTable GetMedicines() => dal.GetMedicines();
        public DataTable GetAllDeliveries() => dal.GetAllDeliveries();
        public DataTable SearchDeliveries(string search) => dal.SearchDeliveries(search);
        public DataTable GetDeliveryByID(int id) => dal.GetDeliveryByID(id);
        public bool UpdateMedicineStock(int medicineID, int qty) => dal.UpdateMedicineStock(medicineID, qty);
        public bool DeleteDelivery(int deliveryID) => dal.DeleteDelivery(deliveryID);

        public int SaveDeliveryMaster(
            int supplierID, string invoiceNo,
            DateTime deliveryDate, int receivedBy, string notes)
            => dal.SaveDeliveryMaster(supplierID, invoiceNo, deliveryDate, receivedBy, notes);

        public int SaveDeliveryMaster(
            int supplierID, string invoiceNo,
            DateTime deliveryDate, int receivedBy)
            => dal.SaveDeliveryMaster(supplierID, invoiceNo, deliveryDate, receivedBy, "");

        public bool SaveDeliveryDetail(
            int deliveryID, int medicineID, string batchNo,
            DateTime expiryDate, int qty, decimal unitCost)
            => dal.SaveDeliveryDetail(deliveryID, medicineID, batchNo, expiryDate, qty, unitCost);
    }
}


