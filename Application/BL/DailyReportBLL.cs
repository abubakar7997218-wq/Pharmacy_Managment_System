using Application.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BL
{
    internal class DailyReportBLL : BaseBLL 
    {
        IDailyReportDAL dal = new DailyReportDL();  

        public decimal TotalSales
        {
            get { return dal.GetTotalSales(); }
        }

        public int TotalBills
        {
            get { return dal.GetTotalBills(); }
        }

        public int ItemsSold
        {
            get { return dal.GetItemsSold(); }
        }

        public int CustomerCount
        {
            get { return dal.GetCustomerCount(); }
        }

        public decimal AverageBill
        {
            get { return dal.GetAverageBill(); }
        }


        public decimal GetTotalSales() => TotalSales;
        public int GetTotalBills() => TotalBills;
        public int GetItemsSold() => ItemsSold;
        public int GetCustomerCount() => CustomerCount;
        public decimal GetAverageBill() => AverageBill;


        public DataTable GetSalesDetails() => dal.GetSalesDetails();
        public DataTable GetSalesTrend() => dal.GetSalesTrend();
        public DataTable GetPaymentSummary() => dal.GetPaymentSummary();
        public DataTable GetPaymentMethods() => dal.GetPaymentMethods();
        public DataTable GetCustomers() => dal.GetCustomers();
        public DataTable GetStatuses() => dal.GetStatuses();

        public DataTable GetFilteredSales(
            DateTime date, string payment,
            string customer, string status)
            => dal.GetFilteredSales(date, payment, customer, status);

        public DataTable GetFilteredSales(DateTime date)
            => dal.GetFilteredSales(date, "", "", "");

        public DataTable GetFilteredSales(DateTime date, string payment)
            => dal.GetFilteredSales(date, payment, "", "");
    }
}
