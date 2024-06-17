using System.Data.SqlClient;

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
    }
}
