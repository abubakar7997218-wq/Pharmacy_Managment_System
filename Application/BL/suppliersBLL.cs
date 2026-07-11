using Application.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BL
{
    internal class suppliersBLL : BaseBLL 
    {
        ISupplierDAL dal = new suppliersDL(); 
        public int TotalSuppliers
        {
            get { return dal.GetTotalSuppliers(); }
        }

        public int ActiveSuppliers
        {
            get { return dal.GetActiveSuppliers(); }
        }

        public int NewThisMonth
        {
            get { return dal.GetNewSuppliersThisMonth(); }
        }

        public int GetTotalSuppliers() => TotalSuppliers;
        public int GetActiveSuppliers() => ActiveSuppliers;
        public int GetNewSuppliersThisMonth() => NewThisMonth;

        public DataTable GetAllSuppliers() => dal.GetAllSuppliers();
        public DataTable GetSupplierNames() => dal.GetSupplierNames();

        public DataTable SearchSuppliers(string name, string status, string supplier)
            => dal.SearchSuppliers(name, status, supplier);

        public bool IsSupplierUsed(int supplierID)
            => dal.IsSupplierUsed(supplierID);

        public bool DeactivateSupplier(int supplierID)
            => dal.DeactivateSupplier(supplierID);

        public bool DeleteSupplier(int supplierID)
            => dal.DeleteSupplier(supplierID);

        public bool AddSupplier(
            string supplierName, string contactName,
            string phone, string email,
            string address, bool active)
            => dal.AddSupplier(supplierName, contactName, phone, email, address, active);

        public bool AddSupplier(
            string supplierName, string contactName,
            string phone, string email, string address)
            => dal.AddSupplier(supplierName, contactName, phone, email, address, true);

        public bool UpdateSupplier(
            int id, string supplierName, string contactName,
            string phone, string email,
            string address, bool active)
            => dal.UpdateSupplier(id, supplierName, contactName, phone, email, address, active);

        public bool UpdateSupplier(
            int id, string supplierName, string contactName,
            string phone, string email, string address)
            => dal.UpdateSupplier(id, supplierName, contactName, phone, email, address, true);
    }

}
