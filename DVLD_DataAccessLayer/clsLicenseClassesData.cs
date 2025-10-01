using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccessLayer
{
    public class clsLicenseClassesData
    {
        public static DataTable GetLicenseClassesData()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LicenseClasses order by LicenseClassID";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
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
                connection.Close();
            }

            return dt;
        }

        public static int AddNewLicenseClass(string className, string classDiscription, int minimumAllowedAge, int defaultValidityLength, float classFees)
        {
            int ClassID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO LicenseClasses (ClassName, ClassDiscription, MinimumAllowedAge, DefaultValidityLength, ClassFees) " +
                           "VALUES (@ClassName, @ClassDiscription, @MinimumAllowedAge, @DefaultValidityLength, @ClassFees); " +
                           "SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", className);
            command.Parameters.AddWithValue("@ClassDiscription", classDiscription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", minimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", defaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", classFees);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ClassID = insertedID;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                return -1;
            }
            finally
            {
                connection.Close();
            }
            return ClassID;
        }

        public static bool UpdateLicenseClass(int classID, string className, string classDiscription, int minimumAllowedAge, int defaultValidityLength, float classFees)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE LicenseClasses SET ClassName = @ClassName, ClassDiscription = @ClassDiscription, " +
                           "MinimumAllowedAge = @MinimumAllowedAge, DefaultValidityLength = @DefaultValidityLength, ClassFees = @ClassFees " +
                           "WHERE LicenseClassID = @ClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassID", classID);
            command.Parameters.AddWithValue("@ClassName", className);
            command.Parameters.AddWithValue("@ClassDiscription", classDiscription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", minimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", defaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", classFees);
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
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0;
        }
        
        public static bool GetLicenseClassInfoByID(int classID, ref string className, ref string classDescription, ref int minimumAllowedAge, ref int defaultValidityLength, ref float classFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees FROM LicenseClasses WHERE LicenseClassID = @ClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassID", classID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    className = reader["ClassName"].ToString();
                    classDescription = reader["ClassDescription"].ToString();
                    minimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    defaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                    classFees = Convert.ToSingle(reader["ClassFees"]);
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                return false;
            }
            finally
            {
                connection.Close();
            }
            return false;
        }

        public static bool GetLicenseClassInfoByName(string className, ref int classID, ref string classDescription, ref int minimumAllowedAge, ref int defaultValidityLength, ref float classFees)
        {
            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT LicenseClassID, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees FROM LicenseClasses WHERE ClassName = @ClassName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", className);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    classID = Convert.ToInt32(reader["LicenseClassID"]);
                    classDescription = reader["ClassDescription"].ToString();
                    minimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    defaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                    classFees = Convert.ToSingle(reader["ClassFees"]);
                    isfound = true;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                return isfound;
            }
            finally
            {
                connection.Close();
            }
            return isfound;
        }
    }
}