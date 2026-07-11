using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal interface IBillDAL
    {
        DataTable SearchMedicine(string name);
        

        int SaveBill(
            string customer,
            string phone,
            decimal sub,
            decimal discount,
            decimal tax,
            decimal grand);

        void SaveBillItem(
            int billID,
            int medID,
            int qty,
            decimal price,
            decimal discount,
            decimal total);
    }
}
