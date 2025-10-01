using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsTestTypesData
    {
        public static DataTable GetTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes";
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

        public static int AddNewTestType(string Title, string Description, float Fees)
        {
            int ApplicationTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into TestTypes (TestTypeTitle, TestTypeDescription,TestTypeFees)
                            Values (@Title, @Description, @Fees)
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Description", Description);
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

        public static bool UpdateTestType(int ID, string Title, string Description, int Fees)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE TestTypes SET TestTypeTitle = @Title, TestTypeDescription = @Description, TestTypeFees = @Fees WHERE TestTypeId = @ID";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@Fees", Fees);

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

        public static bool GetTestTypeInfoByID(int ID, ref string Title, ref string Description, ref int Fees)
        {
            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * From TestTypes WHERE TestTypeId = @ID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Title = reader["TestTypeTitle"].ToString();
                    Description = reader["TestTypeDescription"].ToString();
                    Fees = Convert.ToInt32(reader["TestTypeFees"]);
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