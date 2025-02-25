using System;
using System.Data;
using System.Data.SqlClient;

namespace WinFormsMVC.Model
{
    public class Database
    {
        private static readonly string connectionString = "Server=ADMIN-PC;Database=Suppermaket;Trusted_Connection=True;";


        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}