using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace InsuranceManagement.Util
{
    public class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            try
            {
                string connectionString = DBPropertyUtil.GetConnectionString();
                if (connectionString != null)
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    Console.WriteLine("Database connection established.");
                    return connection;
                }
                return null;
            }
            catch (SystemException ex)
            {
                Console.WriteLine($"Error establishing database connection: {ex.Message}");
                return null;
            }
        }
    }
}
    



