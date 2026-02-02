using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccessLayer
{
    public class clsTestAppointmentData
    {
        // Private mapper to handle data extraction safely
        private static void _FillTestAppointmentInfoFromReader(SqlDataReader reader, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
            LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
            AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
            CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
            PaidFees = Convert.ToSingle(reader["PaidFees"]);
            IsLocked = Convert.ToBoolean(reader["IsLocked"]);
            RetakeTestApplicationID = (reader["RetakeTestApplicationID"] == DBNull.Value) ? -1 : Convert.ToInt32(reader["RetakeTestApplicationID"]);
        }

        public static bool GetTestAppointmentInfoByID(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetTestAppointmentInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            _FillTestAppointmentInfoFromReader(reader, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID);
                        }
                    }
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return isFound;
        }

        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllTestAppointments", connection))
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

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LDLApplicationID, int TestTypeID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetApplicationTestAppointmentsPerTestType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
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

        public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddNewTestAppointment", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@RetakeTestApplicationID", (RetakeTestApplicationID == -1) ? DBNull.Value : (object)RetakeTestApplicationID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        TestAppointmentID = insertedID;
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return TestAppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateTestAppointment", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@IsLocked", IsLocked);
                command.Parameters.AddWithValue("@RetakeTestApplicationID", (RetakeTestApplicationID == -1) ? DBNull.Value : (object)RetakeTestApplicationID);

                try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); return false; }
            }
            return rowsAffected > 0;
        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetTestIDByAppointmentID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int foundID))
                        TestID = foundID;
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return TestID;
        }
    }
}