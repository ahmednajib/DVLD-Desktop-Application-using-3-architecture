using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccessLayer
{
    public class clsDetainedLicenseData
    {
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllDetainedLicenses", connection))
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

        private static bool _FillDetainedInfoFromReader(SqlDataReader reader, ref int DetainID, ref int LicenseID, ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            DetainID = (int)reader["DetainID"];
            LicenseID = (int)reader["LicenseID"];
            DetainDate = (DateTime)reader["DetainDate"];
            FineFees = Convert.ToSingle(reader["FineFees"]);
            CreatedByUserID = (int)reader["CreatedByUserID"];
            IsReleased = (bool)reader["IsReleased"];
            ReleaseDate = (reader["ReleaseDate"] == DBNull.Value) ? DateTime.MaxValue : (DateTime)reader["ReleaseDate"];
            ReleasedByUserID = (reader["ReleasedByUserID"] == DBNull.Value) ? -1 : (int)reader["ReleasedByUserID"];
            ReleaseApplicationID = (reader["ReleaseApplicationID"] == DBNull.Value) ? -1 : (int)reader["ReleaseApplicationID"];
            return true;
        }

        public static bool GetDetainedLicenseInfoByID(int DetainID, ref int LicenseID, ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetDetainedLicenseInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DetainID", DetainID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) isFound = _FillDetainedInfoFromReader(reader, ref DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID);
                    }
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return isFound;
        }

        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetDetainedLicenseInfoByLicenseID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) isFound = _FillDetainedInfoFromReader(reader, ref DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID);
                    }
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return isFound;
        }

        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int DetainID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddNewDetainedLicense", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                command.Parameters.AddWithValue("@FineFees", FineFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID)) DetainID = insertedID;
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return DetainID;
        }

        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateDetainedLicense", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@DetainID", DetainID);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                command.Parameters.AddWithValue("@FineFees", FineFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                    return false;
                }
            }

            return (rowsAffected > 0);
        }

        public static bool ReleaseDetainedLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_ReleaseDetainedLicense", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DetainID", DetainID);
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
                try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return rowsAffected > 0;
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_IsLicenseDetained", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null) IsDetained = true;
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return IsDetained;
        }
    }
}