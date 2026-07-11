using Application.DL;
using System.Data;

namespace Application.BL
{
    internal class DashboardBLL
    {
        DashboardDAL dal = new DashboardDAL();

        public int TotalUsers()
        {
            return dal.GetTotalUsers();
        }

        public int TotalMedicines()
        {
            return dal.GetTotalMedicines();
        }

        public int TotalSuppliers()
        {
            return dal.GetTotalSuppliers();
        }

        public int LowStock()
        {
            return dal.GetLowStock();
        }

        public decimal TodaySales()
        {
            return dal.GetTodaySales();
        }

        
        public DataTable MonthlySales()
        {
            DashboardDAL dal = new DashboardDAL();
            return dal.GetMonthlySales();
        }

        public DataTable RecentTransactions()
        {
            DashboardDAL dal = new DashboardDAL();
            return dal.GetRecentTransactions();
        }
    }
}