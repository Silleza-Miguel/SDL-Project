using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.Stores;
using WpfApp4.Views.RoomNav;

namespace WpfApp4.ViewModels
{
    public class FloorViewModel : ViewModelBase
    {
        public ICommand HomeNavigateCommand { get; }

        public ICommand VideoSearchbarNavigateCommand { get; }

        public ICommand VideoNavigateCommand { get; }

        public ICommand FloorSwitchCommand { get; }

        public ICommand RoomSelectCommand { get; }

        public BuildingStore _buildingStore;

        public int? BuildingID => _buildingStore.CurrentBuilding;

        public string? FloorBuildingName => _buildingStore.CurrentBuildingName?.ToUpper();

        public ObservableCollection<Floor>? Floors { get; set; }

        private Object? _test;
        public Object? Test
        {
            get
            {
                return _test;
            }
            set
            {
                _test = value;
                OnPropertyChanged(nameof(Test));
            }
        }

        private Floor? _selectedFloor;
        public Floor? SelectedFloor
        {
            get
            {
                return _selectedFloor;
            }
            set
            {
                _selectedFloor = value;
                OnPropertyChanged(nameof(SelectedFloor));

                _buildingStore.CurrentFloor = SelectedFloor?.FloorLevel;

                FloorTitle = $"{AddOrdinal(SelectedFloor?.FloorLevel)} Floor".ToUpper();
                FloorMap = SelectedFloor?.FloorMap;


                try
                {
                    Test = FloorAssign.testDict[SelectedFloor?.FloorID];
                }

                catch(Exception e) 
                {
                    Test = FloorAssign.testDict[0];
                }

                Debug.WriteLine($"Selected floor is F{SelectedFloor?.FloorLevel}");

                RoomManager.getDatabase(_buildingStore);
                RoomManager.checkDatabase();

                Rooms = RoomManager.returnDatabase();
            }
        }

        private string? _floorMap;
        public string? FloorMap
        {
            get
            {
                return _floorMap;
            }
            set
            {
                _floorMap = value;
                OnPropertyChanged(nameof(FloorMap));
                //Test = FloorAssign.myDictionary[FloorMap];
            }
        }

        private Room? _selectedRoom;
        public Room? SelectedRoom
        {
            get
            {
                return _selectedRoom;
            }
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));

                RoomName = SelectedRoom?.RoomName;
            }
        }

        private string? _roomName;
        public string? RoomName
        {
            get
            {
                return _roomName;
            }
            set
            {
                _roomName = value;
                OnPropertyChanged(nameof(RoomName));
            }
        }

        private ObservableCollection<Room>? _rooms;
        public ObservableCollection<Room>? Rooms
        {
            get
            {
                return _rooms;
            }
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        private ObservableCollection<Room>? _roomQuery;
        public ObservableCollection<Room>? RoomQuery
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

        private string? _query;
        public string? Query
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

        private string _floorTitle;
        public string FloorTitle
        {
            get
            {
                return _floorTitle;
            }
            set
            {
                _floorTitle = value;
                OnPropertyChanged(nameof(FloorTitle));
            }
        }

        public FloorViewModel(NavigationStore navigationStore, BuildingStore buildingStore)
        {
            _buildingStore = buildingStore;

            //Test commit

            FloorManager.getDatabase(_buildingStore);
            FloorManager.checkDatabase();

            Floors = FloorManager.returnDatabase();

            SelectedFloor = Floors[0];

            VideoSearchbarNavigateCommand = new NavigateCommand<VideoViewModel>(buildingStore, new NavigationService<VideoViewModel>(navigationStore, () => new VideoViewModel(navigationStore, buildingStore)));

            HomeNavigateCommand = new NavigateCommand<HomeViewModel>(buildingStore, new NavigationService<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore, buildingStore)));

            VideoNavigateCommand = new ConfirmFloorNavigateCommand<VideoViewModel>(this, buildingStore, new NavigationService<VideoViewModel>(navigationStore, () => new VideoViewModel(navigationStore, buildingStore)));

            FloorSwitchCommand = new SelectFloorCommand(this);

            RoomSelectCommand = new SelectRoomCommand(this);

            //Test = FloorAssign.myDictionary[FloorMap];

        }

        public static string AddOrdinal(int? num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }

        ~FloorViewModel() { }
    }
}
