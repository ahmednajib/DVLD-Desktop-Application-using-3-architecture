using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccessLayer
{
    public class clsTestData
    {
        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetTestInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestID", TestID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                            TestResult = Convert.ToBoolean(reader["TestResult"]);
                            Notes = reader["Notes"] == DBNull.Value ? "" : Convert.ToString(reader["Notes"]);
                            CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                    isFound = false;
                }
            }
            return isFound;
        }

        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddNewTest", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);
                command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Notes) ? DBNull.Value : (object)Notes);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        TestID = insertedID;
                    }
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return TestID;
        }

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateTest", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestID", TestID);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);
                command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Notes) ? DBNull.Value : (object)Notes);
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

        public static byte GetPassedTestCount(int LDLApplicationID)
        {
            byte PassedTestCount = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetPassedTestCount", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApplicationID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        PassedTestCount = Convert.ToByte(result);
                    }
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return PassedTestCount;
        }
    }
}