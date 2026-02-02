using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccessLayer
{
    public class clsLicenseData
    {
        private static void _FillLicenseInfoFromReader(SqlDataReader reader, ref int applicationID, ref int driverID, ref int licenseClass, ref DateTime issueDate, ref DateTime expirationDate, ref string notes, ref float paidFees, ref bool isActive, ref int issueReason, ref int createdByUserID)
        {
            applicationID = Convert.ToInt32(reader["ApplicationID"]);
            driverID = Convert.ToInt32(reader["DriverID"]);
            licenseClass = Convert.ToInt32(reader["LicenseClass"]);
            issueDate = Convert.ToDateTime(reader["IssueDate"]);
            expirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
            notes = reader["Notes"] != DBNull.Value ? Convert.ToString(reader["Notes"]) : "No Notes";
            paidFees = Convert.ToSingle(reader["PaidFees"]);
            isActive = Convert.ToBoolean(reader["IsActive"]);
            issueReason = Convert.ToInt32(reader["IssueReason"]);
            createdByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
        }

        public static DataTable GetAllLicensesData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllLicenses", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) dt.Load(reader);
                    }
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return dt;
        }

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddNewLicense", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Notes) ? DBNull.Value : (object)Notes);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueReason", IssueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        LicenseID = insertedID;
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return LicenseID;
        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateLicense", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Notes) ? DBNull.Value : (object)Notes);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueReason", IssueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); return false; }
            }
            return rowsAffected > 0;
        }

        public static bool FindLicenseByLicenseID(int licenseID, ref int applicationID, ref int driverID, ref int licenseClass, ref DateTime issueDate, ref DateTime expirationDate, ref string notes, ref float paidFees, ref bool isActive, ref int issueReason, ref int createdByUserID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetLicenseInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@licenseID", licenseID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            _FillLicenseInfoFromReader(reader, ref applicationID, ref driverID, ref licenseClass, ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID);
                        }
                    }
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return isFound;
        }

        public static DataTable GetLicensesByDriverID(int driverID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetLicensesByDriverID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DriverID", driverID);
                try { connection.Open(); using (SqlDataReader reader = command.ExecuteReader()) if (reader.HasRows) dt.Load(reader); }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return dt;
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetActiveLicenseIDByPersonID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null) LicenseID = Convert.ToInt32(result);
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return LicenseID;
        }

        public static bool DeactivateLicense(int LicenseID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeactivateLicense", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); return false; }
            }
            return rowsAffected > 0;
        }
    }
}