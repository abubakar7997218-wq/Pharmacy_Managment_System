using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal interface IDailyReportDAL
    {
        decimal GetTotalSales();
        int GetTotalBills();
        int GetItemsSold();
        int GetCustomerCount();
        decimal GetAverageBill();
        DataTable GetSalesDetails();
        DataTable GetSalesTrend();
        DataTable GetPaymentSummary();
        DataTable GetFilteredSales(DateTime date, string payment, string customer, string status);
        DataTable GetPaymentMethods();
        DataTable GetCustomers();
        DataTable GetStatuses();
    }
}
