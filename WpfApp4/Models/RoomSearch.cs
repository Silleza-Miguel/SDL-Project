using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using WpfApp4.Services;

namespace WpfApp4.Models
{
    public class RoomSearch
    {
        public static ObservableCollection<Room> DatabaseRoom = new ObservableCollection<Room>();

        public static void getDatabase(string query)
        {
            DatabaseRoom.Clear();

            if (query.Length != 0)
            {
                using (SqlCommand command = new SqlCommand($"select top 4 * from test.dbo.Room r join test.dbo.Building b on r.roomBuilding=b.buildingID where roomName like '{query}%' and roomVideo is not null", DatabaseService.connection()))
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

                                RoomBuildingName = Convert.ToString(reader["buildingName"]),

                                RoomVideoID = Convert.ToString(reader["roomVideo"] is DBNull ? null : reader["roomVideo"]),

                                RoomQR = Convert.ToString(reader["roomQR"] is DBNull ? new Uri("../Resources/Buffer.png", UriKind.Relative) : reader["roomQR"])
                            });

                            Console.WriteLine($"SHIT HAS BEEN ADDED");
                        }

                        DatabaseService.connectionClose();
                    }
                }
            }
        }
        public static void checkDatabase()
        {

            if (DatabaseRoom.Count > 0)
            {
                for (int i = 0; i < DatabaseRoom.Count; i++)
                {
                    var room = DatabaseRoom[i];

                    Debug.WriteLine($"{room.RoomID}, {room.RoomName}, {room.RoomFloor}, {room.RoomBuildingID}, {room.RoomVideoID}");
                }
            }
        }

        public static ObservableCollection<Room> returnDatabase()
        {
            return DatabaseRoom;
        }
    }
}
