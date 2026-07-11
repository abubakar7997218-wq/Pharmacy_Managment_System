using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal interface IPrescriptionDAL
    {
        DataTable GetAllPrescriptions();
        int TotalPrescriptions();
        int TotalPatients();
        int ScannedToday();
        DataTable SearchPrescription(string patient);
        bool DeletePrescription(int prescriptionID);
    }
}
