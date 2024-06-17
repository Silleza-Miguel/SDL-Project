using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.Stores;

namespace WpfApp4.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand FloorNavigateCommand { get; }

        public ICommand VideoSearchbarNavigateCommand { get; }

        public ICommand CampusMapNavigateCommand { get; }

        public BuildingStore _buildingStore;

        public string _buildingName => _buildingStore.CurrentBuildingName;

        private ObservableCollection<Building> _buildingList;
        public ObservableCollection<Building> BuildingList
        {
            get
            {
                return _buildingList;
            }
            set
            {
                _buildingList = value;
                OnPropertyChanged(nameof(BuildingList));
            }
        }

        private ObservableCollection<Room> _roomQuery;
        public ObservableCollection<Room> RoomQuery
        {
            get
            {
                return _roomQuery;
            }
            set
            {
                _roomQuery = value;
                OnPropertyChanged(nameof(RoomQuery));
            }
        }

        private string _query;
        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
                Debug.WriteLine(Query);

                RoomSearch.getDatabase(Query);
                // RoomSearch.checkDatabase();

                RoomQuery = RoomSearch.returnDatabase();
            }
        }

        public HomeViewModel(NavigationStore navigationStore, BuildingStore buildingStore)
        {

            _buildingStore = buildingStore;

            BuildingManager.getDatabase();
            BuildingManager.checkDatabase();
            DatabaseService.getConnectionState();

            BuildingList = BuildingManager.returnDatabase();


            FloorNavigateCommand = new ConfirmBuildingNavigateCommand<FloorViewModel>(this, buildingStore, new NavigationService<FloorViewModel>(navigationStore, () => new FloorViewModel(navigationStore, buildingStore)));

            CampusMapNavigateCommand = new NavigateCommand<MapViewModel>(buildingStore, new NavigationService<MapViewModel>(navigationStore, () => new MapViewModel(navigationStore, buildingStore)));

            VideoSearchbarNavigateCommand = new NavigateCommand<VideoViewModel>(buildingStore, new NavigationService<VideoViewModel>(navigationStore, () => new VideoViewModel(navigationStore, buildingStore)));
        }

        ~HomeViewModel() { }
    }
}
