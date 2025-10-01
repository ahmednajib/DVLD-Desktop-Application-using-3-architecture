using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;

namespace DVLD_DataAccessLayer
{
    public class clsPersonData
    {
        public static DataTable GetAllPeople()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT 'Person ID'=PersonID, 'National No'= NationalNo, 'First Name'=FirstName, 'Second Name'=SecondName, " +
                "'Third Name'=ThirdName,'Last Name'=LastName, 'Date Of Birth'=DateOfBirth, Gender = CASE Gender \r\nWHEN 0 THEN 'Male' \r\nWHEN 1 THEN 'Female' \r\nEND," +
                " Address, Phone, Email, Nationality=C.CountryName, 'Image Path'=ImagePath\r\n  FROM People P join Countries C ON (P.NationalityCountryID = C.CountryID)";

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

        public static bool GetPersonInfoByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
            ref int Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true; // PersonID found
                    NationalNo = reader["NationalNo"].ToString();
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    ThirdName = reader["ThirdName"] != DBNull.Value ? reader["ThirdName"].ToString() : string.Empty;
                    LastName = reader["LastName"].ToString();
                    Gender = reader["Gender"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Gender"]);
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty;
                    NationalityCountryID = (int)reader["NationalityCountryID"];
                    ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty; // Handle null value for ImagePath
                }
                else
                {
                    isFound = false; // PersonID not found
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                isFound = false; // In case of error, we assume the person was not found
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool GetPersonInfoByNationalNO(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
           ref int Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    ThirdName = reader["ThirdName"] != DBNull.Value ? reader["ThirdName"].ToString() : string.Empty;
                    LastName = reader["LastName"].ToString();
                    Gender = reader["Gender"] == DBNull.Value ? -1 : Convert.ToInt32(reader["Gender"]);
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty;
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                    ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty;
                }
                else
                {
                    isFound = false; // PersonID not found
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                isFound = false; // In case of error, we assume the person was not found
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static int AddNewPerson(string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth,
                          int gender, string address, string phone, string email, int nationalityCountryID, string imagePath) 
        {
            int NewPersonID = -1; // Default value for new person ID

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, 
                            Address, Phone, Email, NationalityCountryID, ImagePath) 
                            VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gender, 
                            @Address, @Phone, @Email, @NationalityCountryID, @ImagePath); SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", nationalNo);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@SecondName", secondName);
            
            if (!string.IsNullOrEmpty(thirdName))
                command.Parameters.AddWithValue("@ThirdName", thirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@Gender", gender);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@Phone", phone);

            if (!string.IsNullOrEmpty(email))
                command.Parameters.AddWithValue("@Email", email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            
            command.Parameters.AddWithValue("@NationalityCountryID", nationalityCountryID);
            
            if (imagePath != "" && imagePath != null)
                command.Parameters.AddWithValue("@ImagePath", imagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    NewPersonID = insertedID;
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

            return NewPersonID; // Return the newly created PersonID
        }

        public static bool UpdatePerson(int personID, string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth,
                          int gender, string address, string phone, string email, int nationalityCountryID, string imagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE People SET NationalNo = @NationalNo, FirstName = @FirstName, SecondName = @SecondName, 
                            ThirdName = @ThirdName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, 
                            Address = @Address, Phone = @Phone, Email = @Email, NationalityCountryID = @NationalityCountryID, ImagePath = @ImagePath
                            WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personID);
            command.Parameters.AddWithValue("@NationalNo", nationalNo);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@SecondName", secondName);

            if (!string.IsNullOrEmpty(thirdName))
                command.Parameters.AddWithValue("@ThirdName", thirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@Gender", gender);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@Phone", phone);

            if (!string.IsNullOrEmpty(email))
                command.Parameters.AddWithValue("@Email", email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", nationalityCountryID);

            if (imagePath != "" && imagePath != null)
                command.Parameters.AddWithValue("@ImagePath", imagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

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
            return rowsAffected > 0; // Return true if at least one row was updated

        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete People where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}