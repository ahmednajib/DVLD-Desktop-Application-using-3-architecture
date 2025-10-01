using System;
using System.Configuration;

namespace DVLD_DataAccessLayer
{
    static class clsDataAccessSettings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
    }
}