using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccessLayer
{
    public class clsApplicationTypesData
    {
        public static int AddNewApplicationType(string Title, float Fees)
        {
            int ApplicationTypeID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddNewApplicationType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Title", Title);
                command.Parameters.AddWithValue("@Fees", Fees);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        ApplicationTypeID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                }
            }
            return ApplicationTypeID;
        }

        public static DataTable GetApplicationTypes()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllApplicationTypes", connection))
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
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                }
            }
            return dt;
        }

        public static bool UpdateApplicationType(int applicationTypeId, string applicationTypeTitle, float applicationFees)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateApplicationType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ApplicationTypeID", applicationTypeId);
                command.Parameters.AddWithValue("@Title", applicationTypeTitle);
                command.Parameters.AddWithValue("@Fees", applicationFees);

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
            return rowsAffected > 0;
        }

        public static bool GetApplicationTypeInfoByID(int applicationTypeId, ref string applicationTypeTitle, ref float applicationFees)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetApplicationTypeInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ApplicationTypeID", applicationTypeId);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            applicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                            applicationFees = Convert.ToSingle(reader["ApplicationFees"]);
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
    }
}