using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DL;

namespace Application.BL
{
    internal class searchmedicineBLL : BaseBLL  
    {
        ISearchMedicineDAL dal = new searchmedicineDAL(); 

        public DataTable SearchMedicine(
            string keyword,
            string category,
            string company,
            string stockStatus)
        {
            return dal.SearchMedicine(keyword, category, company, stockStatus);
        }

        public DataTable GetAllMedicines()
        {
            return dal.GetAllMedicines();
        }
        public bool DeleteMedicine(int medicineID)
        {
            return dal.DeleteMedicine(medicineID);
        }
        public bool DeleteMedicine(int medicineID, bool showConfirmation)
        {
            bool deleted = dal.DeleteMedicine(medicineID);

            if (deleted && showConfirmation)
                ShowSuccess("Medicine Deleted Successfully");  
            else if (!deleted)
                ShowError("Could not delete. Medicine may be in use.");  

            return deleted;
        }
    }
}
