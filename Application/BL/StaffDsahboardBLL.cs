using Application.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BL
{
    internal class StaffDsahboardBLL
    {
        StaffDashboardDLL dl = new StaffDashboardDLL(); 
        public int GetAvailableMedicines()
        {
            return dl.GetAvailableMedicines();
        }

        public int GetLowStockCount()
        {
            return dl.GetLowStockCount();
        }

        public int GetExpiryCount()
        {
            return dl.GetExpiryCount();
        }

        public decimal GetTodaySales()
        {
            return dl.GetTodaySales();
        }
        public DataTable GetRecentBills()
        {
            return dl.GetRecentBills();
        }
        public DataTable GetWeeklySales()
        {
            return dl.GetWeeklySales();
        }
    }
}
