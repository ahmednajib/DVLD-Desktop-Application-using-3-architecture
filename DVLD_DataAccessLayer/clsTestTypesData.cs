using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccessLayer
{
    public class clsTestTypesData
    {
        public static DataTable GetTestTypes()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetTestTypes", connection))
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

        public static int AddNewTestType(string Title, string Description, float Fees)
        {
            int insertedID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddNewTestType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Title", Title);
                command.Parameters.AddWithValue("@Description", Description);
                command.Parameters.AddWithValue("@Fees", Fees);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        insertedID = id;
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                }
            }
            return insertedID;
        }

        public static bool UpdateTestType(int ID, string Title, string Description, float Fees)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateTestType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@Title", Title);
                command.Parameters.AddWithValue("@Description", Description);
                command.Parameters.AddWithValue("@Fees", Fees);

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

        public static bool GetTestTypeInfoByID(int ID, ref string Title, ref string Description, ref float Fees)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetTestTypeInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", ID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            Title = Convert.ToString(reader["TestTypeTitle"]);
                            Description = Convert.ToString(reader["TestTypeDescription"]);
                            Fees = Convert.ToSingle(reader["TestTypeFees"]);
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