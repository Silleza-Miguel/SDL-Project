using System.ComponentModel;
using System.Diagnostics;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.Stores;
using WpfApp4.ViewModels;

namespace WpfApp4.Commands
{
    class ConfirmFloorNavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        private readonly NavigationService<TViewModel> _navigationService;

        private BuildingStore _buildingStore;

        private FloorViewModel _floorViewModel;
        public ConfirmFloorNavigateCommand(FloorViewModel floorViewModel, BuildingStore buildingStore, NavigationService<TViewModel> navigationService)
        {
            _buildingStore = buildingStore;

            _navigationService = navigationService;

            _floorViewModel = floorViewModel;

            _floorViewModel.PropertyChanged += OnFloorViewModelPropertyChanged;
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

            if (_floorViewModel.SelectedRoom is null)
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
            Room room = _floorViewModel.SelectedRoom;

            Debug.WriteLine($"Room name is {room.RoomName}");

            _buildingStore.CurrentRoom = room.RoomName;

            _buildingStore.CurrentVideo = room.RoomVideo;

            _buildingStore.CurrentVideoQR = room.RoomQR;

            _navigationService.Navigate();
        }
    }
}