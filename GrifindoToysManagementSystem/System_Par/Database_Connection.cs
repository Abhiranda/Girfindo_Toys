using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrifindoToysManagementSystem.System_Par
{
    class Database_Connection 
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=ABHIRANDA\SQLEXPRESS01;;Initial Catalog=GrifindoToysSystem;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
