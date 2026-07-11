using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DL
{
    internal class PrescriptionDLL : IPrescriptionDAL  
    {
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(Database.con);
        }
        private int ExecuteScalar(string query)
        {
            using (MySqlConnection con = GetConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public DataTable GetAllPrescriptions()
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT
    PrescriptionID,
    CustomerName,
    CustomerPhone,
    DoctorName,
    PrescriptionDate,
    RecordedBy,
    CreatedAt,
    Notes
FROM Prescriptions
ORDER BY PrescriptionDate DESC";

                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int TotalPrescriptions()
        {
            return ExecuteScalar(
                "SELECT COUNT(*) FROM Prescriptions");
        }

        public int TotalPatients()
        {
            return ExecuteScalar(
                "SELECT COUNT(DISTINCT CustomerName) FROM Prescriptions");
        }

        public int ScannedToday()
        {
            return ExecuteScalar(
                "SELECT COUNT(*) FROM Prescriptions WHERE DATE(CreatedAt) = CURDATE()");
        }

        public DataTable SearchPrescription(string patient)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query = @"
SELECT
    PrescriptionID,
    CustomerName,
    CustomerPhone,
    DoctorName,
    PrescriptionDate,
    Notes
FROM Prescriptions
WHERE CustomerName LIKE @name";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", "%" + patient + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool DeletePrescription(int prescriptionID)
        {
            using (MySqlConnection con = GetConnection())
            {
                string query =
                    "DELETE FROM prescriptions WHERE PrescriptionID = @id";

                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", prescriptionID);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
