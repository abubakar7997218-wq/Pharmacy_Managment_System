using Application.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BL
{
    internal class stockdashboardBLL
    {
        supplierdashboarrdDLL dl = new supplierdashboarrdDLL();
        public int GetTotalDeliveries()
        {
            return dl.GetTotalDeliveries();
        }

        public int GetTotalItemsDelivered()
        {
            return dl.GetTotalItemsDelivered();
        }

        public int GetTotalSuppliers()
        {
            return dl.GetTotalSuppliers();
        }

        public decimal GetTotalDeliveryValue()
        {
            return dl.GetTotalDeliveryValue();
        }

        public DataTable GetMonthlyDeliveries()
        {
            return dl.GetMonthlyDeliveries();
        }

        public DataTable GetLowStockMedicines()
        {
            return dl.GetLowStockMedicines();
        }

        public DataTable GetRecentDeliveries()
        {
            return dl.GetRecentDeliveries();
        }
    }
}
