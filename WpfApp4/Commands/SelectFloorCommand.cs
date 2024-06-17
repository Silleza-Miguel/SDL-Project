using System.Diagnostics;
using WpfApp4.Models;
using WpfApp4.ViewModels;

namespace WpfApp4.Commands
{
    internal class SelectFloorCommand : CommandBase
    {
        private FloorViewModel _floorViewModel;

        public SelectFloorCommand(FloorViewModel floorViewModel)
        {
            _floorViewModel = floorViewModel;
        }

        public override void Execute(object parameter)
        {
            Floor floor = (Floor)parameter;
            Debug.WriteLine($"parameter is {floor.FloorID}");

            if (_floorViewModel.SelectedFloor != floor) 
            {
                _floorViewModel.SelectedRoom = null;
                _floorViewModel.SelectedFloor = floor;
            }
        }
    }
}
