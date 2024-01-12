using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace InsuranceManagement.Util
{
    public class DBPropertyUtil
    {

        public static string GetConnectionString()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Insurance"].ConnectionString;
                return connectionString;
            }
            catch (SystemException ex)
            {
                Console.WriteLine($"Error reading connection string from config file: {ex.Message}");
                return null;
            }

        }
    }
}

