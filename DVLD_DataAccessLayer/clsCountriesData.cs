using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DVLD_DataAccessLayer
{
    public class clsCountriesData
    {
        public static bool GetCountryIDByName(string CountryName, ref int CountryID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT CountryID FROM Countries WHERE CountryName = @CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            bool IsFound = false;

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    IsFound = true;
                    CountryID = Convert.ToInt32(result);
                }
                return IsFound;
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
        }

        public static bool GetCountryNameByID(int CountryID, ref string CountryName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT CountryName FROM Countries WHERE CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            bool IsFound = false;

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    IsFound = true;
                    CountryName = result.ToString();
                }
                return IsFound;
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
        }

        public static DataTable GetAllCountries()
        {
            DataTable dtCountries = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT CountryID, CountryName FROM Countries Order By CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dtCountries.Load(reader);

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
            return dtCountries;
        }
    }
}