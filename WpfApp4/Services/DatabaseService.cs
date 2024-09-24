using System.Data.SqlClient;
using System.Diagnostics;
using WpfApp4.Models;
using WpfApp4.Stores;

namespace WpfApp4.Services
{
    public class DatabaseService
    {
        private static SqlConnection Connection { get; set; }

        private static string db = "TAIGA\\SQLEXPRESS";

        public static SqlConnection connection()
        {
            Connection = new SqlConnection($"Data Source={db}; Integrated Security=True");
            Connection.Open();

            Console.WriteLine($"Connection Opened: {Connection.State}");

            return Connection;
        }

        public static void connectionClose()
        {
            Connection.Dispose();

            Console.WriteLine($"Connection Closed: {Connection.State}");
        }

        public static void getConnectionState()
        {
            Console.WriteLine($"Connection State: {Connection.State}");
        }

        public static void UpdateDatabaseValue(string table, string columnName, List<string> newValue, List<string> oldValue)
        {
            for(int i = 0; i < newValue.Count(); i++)
            {
                using (SqlCommand command = new SqlCommand($"update test.dbo.{table} set {columnName} = '{newValue[i]}' where {columnName} = '{oldValue[i]}'", connection()))
                {
                    command.ExecuteNonQuery();
                    connectionClose();
                }
            }
        }
    }
}
