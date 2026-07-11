using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal interface ISearchMedicineDAL
    {
        DataTable SearchMedicine(
            string keyword,
            string category,
            string company,
            string stockStatus);

        DataTable GetAllMedicines();

        bool DeleteMedicine(int medicineID);
    }
}
