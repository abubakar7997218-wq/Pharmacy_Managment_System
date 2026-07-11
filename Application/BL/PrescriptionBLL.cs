using Application.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BL
{
    internal class PrescriptionBLL : BaseBLL  
    {
        IPrescriptionDAL dal = new PrescriptionDLL();  

        public DataTable GetAllPrescriptions()
        {
            return dal.GetAllPrescriptions();
        }

        public int TotalPrescriptions()
        {
            return dal.TotalPrescriptions();
        }

        public int TotalPatients()
        {
            return dal.TotalPatients();
        }

        public int ScannedToday()
        {
            return dal.ScannedToday();
        }

        public DataTable SearchPrescription(string patient)
        {
            return dal.SearchPrescription(patient);
        }
        public bool DeletePrescription(int prescriptionID)
        {
            return dal.DeletePrescription(prescriptionID);
        }
        public bool DeletePrescription(int prescriptionID, bool showMessage)
        {
            bool deleted = dal.DeletePrescription(prescriptionID);

            if (deleted && showMessage)
                ShowSuccess("Prescription Deleted Successfully");  
            else if (!deleted)
                ShowError("Could not delete. Try again.");         

            return deleted;
        }
    }
}
