using System.Collections.ObjectModel;
using System.Data.SqlClient;
using WpfApp4.Services;
using WpfApp4.Stores;

namespace WpfApp4.Models
{
    public class FloorManager
    {
        public static ObservableCollection<Floor> DatabaseFloor = new ObservableCollection<Floor>();

        public static BuildingStore _buildingStore;

        public static void getDatabase(BuildingStore buildingStore)
        {
            _buildingStore = buildingStore;

            DatabaseFloor.Clear();

            using (SqlCommand command = new SqlCommand($"select * from test.dbo.floor where floorBuilding = {_buildingStore.CurrentBuilding}", DatabaseService.connection()))
            {
                // Execute the query and retrieve the result
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Loop through the result and print database names
                    while (reader.Read())
                    {
                        DatabaseFloor.Add(new Floor
                        {
                            FloorID = Convert.ToInt32(reader["floorID"]),

                            FloorLevel = Convert.ToInt32(reader["floorLevel"]),

                            FloorBuilding = Convert.ToInt32(reader["floorBuilding"]),

                            FloorMap = Convert.ToString(reader["floorMap"] is DBNull ? new Uri("../Resources/FloorBuffer.png", UriKind.Relative) : reader["floorMap"])
                        });

                        Console.WriteLine($"SHIT HAS BEEN ADDED");
                    }

                    DatabaseService.connectionClose();
                }
            }
        }

        public static void checkDatabase()
        {
            for (int i = 0; i < DatabaseFloor.Count; i++)
            {

                Console.WriteLine($"{DatabaseFloor[i]}");
            }
        }

        public static ObservableCollection<Floor> returnDatabase()
        {
            return DatabaseFloor;
        }

    }
}
