using System.Diagnostics;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.Stores;
using WpfApp4.ViewModels;

namespace WpfApp4.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _navigationService;

        private BuildingStore _buildingStore;

        public NavigateCommand(BuildingStore buildingStore, NavigationService<TViewModel> navigationService)
        {
            _buildingStore = buildingStore;

            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            Debug.WriteLine($"Parameter is: {parameter}");

            if (parameter != null)
            {
                if (parameter.GetType() == typeof(Building))
                {
                    Building building = (Building)parameter;

                    Debug.WriteLine($"OBJECT IS BUILDING");
                    _buildingStore.CurrentBuilding = building.BuildingID;

                    _buildingStore.CurrentBuildingName = building.BuildingName;
                }

                else if (parameter.GetType() == typeof(Room))
                {
                    Room room = (Room)parameter;

                    Debug.WriteLine($"Room name is {room.RoomName}");

                    _buildingStore.CurrentRoom = room.RoomName;

                    _buildingStore.CurrentBuilding = room.RoomBuildingID;

                    _buildingStore.CurrentBuildingName = room.RoomBuildingName;

                    _buildingStore.CurrentVideo = room.RoomVideoID;

                    _buildingStore.CurrentVideoQR = room.RoomQR;
                }
            }

            _navigationService.Navigate();
        }
    }
}
