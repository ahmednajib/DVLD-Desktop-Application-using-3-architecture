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

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into ApplicationTypes (ApplicationTypeTitle,ApplicationFees)
                            Values (@Title,@Fees)
                            
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

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
            finally
            {
                connection.Close();
            }

            return ApplicationTypeID;
        }

        public static DataTable GetApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes";
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return dt;
        }

        public static bool UpdateApplicationType(int applicationTypeId, string applicationTypeTitle, int applicationFees)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE ApplicationTypes SET ApplicationTypeTitle = @ApplicationTypeTitle, ApplicationFees = @ApplicationFees WHERE ApplicationTypeId = @ApplicationTypeId";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ApplicationTypeId", applicationTypeId);
            cmd.Parameters.AddWithValue("@ApplicationTypeTitle", applicationTypeTitle);
            cmd.Parameters.AddWithValue("@ApplicationFees", applicationFees);

            try
            {
                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return rowsAffected > 0;
        }

        public static bool GetApplicationTypeInfoByID(int applicationTypeId, ref string applicationTypeTitle, ref int applicationFees)
        {
            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * From ApplicationTypes WHERE ApplicationTypeId = @ApplicationTypeId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ApplicationTypeId", applicationTypeId);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    applicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                    applicationFees = Convert.ToInt32(reader["ApplicationFees"]);
                    isfound = true;
                }
                else
                {
                    isfound = false;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return isfound;
        }
    }
}