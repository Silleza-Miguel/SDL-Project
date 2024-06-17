using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using WpfApp4.Services;
using WpfApp4.Stores;

namespace WpfApp4.Models
{
    public class RoomManager
    {
        public static ObservableCollection<Room> DatabaseRoom = new ObservableCollection<Room>();

        public static BuildingStore _buildingStore;

        public static void getDatabase(BuildingStore buildingStore)
        {
            _buildingStore = buildingStore;

            DatabaseRoom.Clear();

            using (SqlCommand command = new SqlCommand($"select * from test.dbo.Room where roomBuilding = {_buildingStore.CurrentBuilding} and roomFloor = {_buildingStore.CurrentFloor}", DatabaseService.connection()))
            {
                // Execute the query and retrieve the result
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Loop through the result and print database names
                    while (reader.Read())
                    {
                        DatabaseRoom.Add(new Room
                        {
                            RoomID = Convert.ToInt32(reader["roomID"]),

                            RoomName = Convert.ToString(reader["roomName"]),

                            RoomFloor = Convert.ToInt32(reader["roomFloor"]),

                            RoomBuildingID = Convert.ToInt32(reader["roomBuilding"]),

                            RoomVideoID = Convert.ToString(reader["roomVideo"] is DBNull ? null : reader["roomVideo"]),

                            RoomQR = Convert.ToString(reader["roomQR"] is DBNull ? new Uri("../Resources/Buffer.png", UriKind.Relative) : reader["roomQR"])
                        });

                        Debug.WriteLine($"SHIT HAS BEEN ADDED");
                    }

                    DatabaseService.connectionClose();
                }
            }
        }

        public static void checkDatabase()
        {
            for (int i = 0; i < DatabaseRoom.Count; i++)
            {
                var room = DatabaseRoom[i];

                Debug.WriteLine($"{room.RoomID}, {room.RoomName}, {room.RoomFloor}, {room.RoomBuildingID}, {room.RoomVideoID}");
            }
        }

        public static ObservableCollection<Room> returnDatabase()
        {
            return DatabaseRoom;
        }
    }
}
