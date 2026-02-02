using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccessLayer
{
    public class clsLicenseClassesData
    {
        private static void _FillLicenseClassInfoFromReader(SqlDataReader reader, ref int classID, ref string className, ref string classDescription, ref int minimumAllowedAge, ref int defaultValidityLength, ref float classFees)
        {
            // Use Convert to avoid InvalidCastException if DB types are tinyint/smallint
            classID = Convert.ToInt32(reader["LicenseClassID"]);
            className = reader["ClassName"].ToString();

            // Using the exact spelling 'ClassDescription' from your DB image
            classDescription = reader["ClassDescription"].ToString();

            minimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
            defaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);

            // float in C# matches 'real' in SQL; 'float' or 'money' in SQL often requires Convert.ToSingle
            classFees = Convert.ToSingle(reader["ClassFees"]);
        }

        public static DataTable GetLicenseClassesData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetLicenseClassesData", connection))
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

        public static int AddNewLicenseClass(string className, string classDescription, int minimumAllowedAge, int defaultValidityLength, float classFees)
        {
            int ClassID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddNewLicenseClass", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClassName", className);
                command.Parameters.AddWithValue("@ClassDescription", classDescription);
                command.Parameters.AddWithValue("@MinimumAllowedAge", minimumAllowedAge);
                command.Parameters.AddWithValue("@DefaultValidityLength", defaultValidityLength);
                command.Parameters.AddWithValue("@ClassFees", classFees);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        ClassID = insertedID;
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return ClassID;
        }

        public static bool UpdateLicenseClass(int classID, string className, string classDescription, int minimumAllowedAge, int defaultValidityLength, float classFees)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateLicenseClass", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClassID", classID);
                command.Parameters.AddWithValue("@ClassName", className);
                command.Parameters.AddWithValue("@ClassDescription", classDescription);
                command.Parameters.AddWithValue("@MinimumAllowedAge", minimumAllowedAge);
                command.Parameters.AddWithValue("@DefaultValidityLength", defaultValidityLength);
                command.Parameters.AddWithValue("@ClassFees", classFees);

                try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); return false; }
            }
            return rowsAffected > 0;
        }

        public static bool GetLicenseClassInfoByID(int classID, ref string className, ref string classDescription, ref int minimumAllowedAge, ref int defaultValidityLength, ref float classFees)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetLicenseClassInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClassID", classID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            _FillLicenseClassInfoFromReader(reader, ref classID, ref className, ref classDescription, ref minimumAllowedAge, ref defaultValidityLength, ref classFees);
                        }
                    }
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); isFound = false; }
            }
            return isFound;
        }

        public static bool GetLicenseClassInfoByName(string className, ref int classID, ref string classDescription, ref int minimumAllowedAge, ref int defaultValidityLength, ref float classFees)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetLicenseClassInfoByName", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClassName", className);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            _FillLicenseClassInfoFromReader(reader, ref classID, ref className, ref classDescription, ref minimumAllowedAge, ref defaultValidityLength, ref classFees);
                        }
                    }
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); isFound = false; }
            }
            return isFound;
        }
    }
}