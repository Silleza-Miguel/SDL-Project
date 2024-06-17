using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.Stores;

namespace WpfApp4.ViewModels
{
    public class VideoViewModel : ViewModelBase
    {
        public ICommand FloorNavigateCommand { get; }

        public ICommand VideoSearchbarNavigateCommand { get; }

        public BuildingStore _buildingStore;

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

        private string? _video;
        public string? Video
        {
            get
            {
                return _video;
            }
            set
            {
                _video = value;
                OnPropertyChanged(nameof(Video));
            }
        }

        private string? _qr;
        public string? QR
        {
            get
            {
                return _qr;
            }
            set
            {
                _qr = value;
                OnPropertyChanged(nameof(QR));
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

        public VideoViewModel(NavigationStore navigationStore, BuildingStore buildingStore)
        {
            _buildingStore = buildingStore;

            Video = _buildingStore.CurrentVideo;

            QR = _buildingStore.CurrentVideoQR;

            RoomName = $"{buildingStore.CurrentRoom.ToUpper()}";

            //Video = "C:\\Users\\schumarkie\\Videos\\pythondownload\\【アニメ】起こすな危険！.mp4";

            FloorNavigateCommand = new NavigateCommand<FloorViewModel>(buildingStore, new NavigationService<FloorViewModel>(navigationStore, () => new FloorViewModel(navigationStore, buildingStore)));

            VideoSearchbarNavigateCommand = new NavigateCommand<VideoViewModel>(buildingStore, new NavigationService<VideoViewModel>(navigationStore, () => new VideoViewModel(navigationStore, buildingStore)));
        }

        ~VideoViewModel() { }
    }
}
