using System.ComponentModel;
using System.Diagnostics;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.Stores;
using WpfApp4.ViewModels;

namespace WpfApp4.Commands
{
    class ConfirmBuildingNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _navigationService;

        private BuildingStore _buildingStore;

        private HomeViewModel _homeViewModel;

        public ConfirmBuildingNavigateCommand(HomeViewModel homeViewModel, BuildingStore buildingStore, NavigationService<TViewModel> navigationService)
        {
            _buildingStore = buildingStore;

            _navigationService = navigationService;

            _homeViewModel = homeViewModel;
        }

        private void OnFloorViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FloorViewModel.SelectedRoom))
            {
                OnExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            Debug.WriteLine($"Room name is {parameter}");

            Building building = (Building)parameter;

            if (building.BuildingRoomAmount == 0 || building.BuildingFloorAmount == 0)
            {
                return false;
            }

            else
            {
                return true;
            }

        }

        public override void Execute(object parameter)
        {
            Debug.WriteLine($"Room name is {parameter}");

            Building building = (Building)parameter;

            Debug.WriteLine($"OBJECT IS BUILDING");
            _buildingStore.CurrentBuilding = building.BuildingID;

            _buildingStore.CurrentBuildingName = building.BuildingName;

            _navigationService.Navigate();
        }
    }
}
