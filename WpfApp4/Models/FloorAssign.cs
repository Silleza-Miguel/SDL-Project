using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp4.Views.RoomNav;

namespace WpfApp4.Models
{
    public static class FloorAssign
    {
        // Use floorID as the int

        public static Dictionary<int?, UserControl> testDict = new Dictionary<int?, UserControl>()
        {
            {0, new RoomBase()}, //DO NOT REMOVE THIS, THIS IS THE DEFAULT VALUE IF A FLOOR BUTTON EXIST BUT HAS NO ROOM HIGHLIGHTS

            {1, new OZFloor1() },
            {2, new OZFloor2() },
            {3, new OZFloor3() },
            {4, new OZFloor4() },

            {5, new CTFloor1() },
            {6, new CTFloor2() },
            {7, new CTFloor3() },
            {8, new CTFloor4() },
            {9, new CTFloor5() },
            {10, new CTFloor6() },
            {11, new CTFloor7() },
            {12, new CTFloor8() },

            {14, new STFloor1() },
            {15, new STFloor2() },
            {16, new STFloor3() },
            {17, new STFloor4() },
        };
    }
}
