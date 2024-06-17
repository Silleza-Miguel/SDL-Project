using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using WpfApp4.Services;

namespace WpfApp4.Models
{
    public class BuildingManager
    {
        public static ObservableCollection<Building> DatabaseBuilding = new ObservableCollection<Building>();

        public static void getDatabase()
        {
            DatabaseBuilding.Clear();

            using (SqlCommand command = new SqlCommand("SELECT b.*, COUNT(DISTINCT r.roomID) AS roomAmount, COUNT(DISTINCT f.floorID) AS floorAmount " +
                                                       "FROM test.dbo.Building b " +
                                                       "LEFT JOIN test.dbo.Room r ON b.buildingID = r.roomBuilding " +
                                                       "LEFT JOIN test.dbo.Floor f ON b.buildingID = f.floorBuilding " +
                                                       "GROUP BY b.buildingID, b.buildingName, b.buildingImage", DatabaseService.connection()))
            {
                // Execute the query and retrieve the result
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Loop through the result and print database names
                    while (reader.Read())
                    {
                        DatabaseBuilding.Add(new Building
                        {
                            BuildingID = Convert.ToInt32(reader["buildingID"]),

                            BuildingName = Convert.ToString(reader["buildingName"]),

                            BuildingFloorAmount = Convert.ToInt32(reader["floorAmount"]),

                            BuildingRoomAmount = Convert.ToInt32(reader["roomAmount"]),

                            BuildingImage = Convert.ToString(reader["buildingImage"] is DBNull ? new Uri("../Resources/Buffer.png", UriKind.Relative) : reader["buildingImage"]), 

                            BuildingActive = Convert.ToInt32(reader["roomAmount"]) > 0 && Convert.ToInt32(reader["floorAmount"]) > 0 ? true : false
                        });
                    }

                    DatabaseService.connectionClose();
                }
            }
        }

        public static void checkDatabase()
        {
            for (int i = 0; i < DatabaseBuilding.Count; i++)
            {

                Debug.WriteLine($"{DatabaseBuilding[i].BuildingRoomAmount}");
            }
        }

        public static ObservableCollection<Building> returnDatabase()
        {
            return DatabaseBuilding;
        }
    }
}
