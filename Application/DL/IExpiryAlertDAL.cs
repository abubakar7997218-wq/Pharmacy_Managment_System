using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal interface IExpiryAlertDAL
    {
        DataTable GetExpiryMedicines();
        int Get7DaysCount();
        int Get30DaysCount();
        int Get90DaysCount();
        int GetAbove90DaysCount();
        
        DataTable GetCategories();
        DataTable GetSuppliers();
        DataTable FilterExpiryMedicines(int categoryID, int supplierID, DateTime fromDate, DateTime toDate);
    }
}
