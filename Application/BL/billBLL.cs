using Application.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BL
{
    internal class billBLL : BaseBLL   
    {
        IBillDAL dal = new billDAL();

        public DataTable SearchMedicine(string name)
        {
            return dal.SearchMedicine(name);
        }

        public int SaveBill(
            string customer,
            string phone,
            decimal sub,
            decimal discount,
            decimal tax,
            decimal grand)
        {
            if (string.IsNullOrWhiteSpace(customer))
            {
                ShowError("Customer name required!");  
                return -1;
            }

            return dal.SaveBill(customer, phone, sub, discount, tax, grand);
        }

        public void SaveBillItem(
            int billID,
            int medID,
            int qty,
            decimal price,
            decimal discount,
            decimal total)
        {
            dal.SaveBillItem(billID, medID, qty, price, discount, total);
        }
    }

}

