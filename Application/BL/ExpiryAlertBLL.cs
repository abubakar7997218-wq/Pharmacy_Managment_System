using Application.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BL
{
    internal class ExpiryAlertBLL : BaseBLL 
    {
        IExpiryAlertDAL dal = new ExpiryAlertDL();  
        public int Days7Count
        {
            get { return dal.Get7DaysCount(); }
        }

        public int Days30Count
        {
            get { return dal.Get30DaysCount(); }
        }

        public int Days90Count
        {
            get { return dal.Get90DaysCount(); }
        }

        public int Above90Count
        {
            get { return dal.GetAbove90DaysCount(); }
        }

        public int TotalExpiryCount
        {
            get
            {
                return Days7Count + Days30Count
                     + Days90Count + Above90Count;
            }
        }


        public int Get7DaysCount() => Days7Count;
        public int Get30DaysCount() => Days30Count;
        public int Get90DaysCount() => Days90Count;
        public int GetAbove90DaysCount() => Above90Count;


        public DataTable GetExpiryMedicines()
            => dal.GetExpiryMedicines();

        public DataTable GetCategories()
            => dal.GetCategories();

        public DataTable GetSuppliers()
            => dal.GetSuppliers();

        public DataTable FilterExpiryMedicines(
            int categoryID, int supplierID,
            DateTime fromDate, DateTime toDate)
            => dal.FilterExpiryMedicines(
                categoryID, supplierID, fromDate, toDate);

        public DataTable FilterExpiryMedicines(
            DateTime fromDate, DateTime toDate)
            => dal.FilterExpiryMedicines(0, 0, fromDate, toDate);
    }
}

