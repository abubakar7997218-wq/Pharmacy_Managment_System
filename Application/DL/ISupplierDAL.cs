using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal interface ISupplierDAL
    {
        int GetTotalSuppliers();
        int GetActiveSuppliers();
        int GetNewSuppliersThisMonth();
        DataTable GetAllSuppliers();
        DataTable GetSupplierNames();
        
        bool DeactivateSupplier(int supplierID);
        bool IsSupplierUsed(int supplierID);
        DataTable SearchSuppliers(string contactName, string status, string supplier);
        bool AddSupplier(string supplierName, string contactName, string phone, string email, 
            string address, bool active);
        bool UpdateSupplier(int id, string supplierName, string contactName, string phone, 
            string email, string address, bool active);
        bool DeleteSupplier(int supplierID);
    }
}
