using System.Diagnostics;
using WpfApp4.Models;
using WpfApp4.ViewModels;

namespace WpfApp4.Commands
{
    public class SelectRoomCommand : CommandBase
    {
        private FloorViewModel _floorViewModel;

        public SelectRoomCommand(FloorViewModel floorViewModel)
        {
            _floorViewModel = floorViewModel;
        }

        public override void Execute(object parameter)
        {
            Room room = (Room)parameter;

            if (_floorViewModel.SelectedRoom == null || room.RoomID != _floorViewModel.SelectedRoom.RoomID)
            {
                _floorViewModel.SelectedRoom = room;
                Debug.WriteLine($"{room.RoomName} IS CLICKED, select is {_floorViewModel.SelectedRoom}");
            }

            else
            {
                _floorViewModel.SelectedRoom = null;
                Debug.WriteLine($"{room.RoomName} IS UNCLICKED, select is {_floorViewModel.SelectedRoom}");
            }
        }
    }
}
