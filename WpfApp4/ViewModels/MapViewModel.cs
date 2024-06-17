using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApp4.Commands;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.Stores;

namespace WpfApp4.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        public ICommand HomeNavigateCommand { get; }

        public ICommand VideoSearchbarNavigateCommand { get; }

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

        private string _campusMap;
        public string CampusMap
        {
            get
            {
                return _campusMap;
            }
            set
            {
                _campusMap = value;
                OnPropertyChanged(nameof(CampusMap));
            }
        }

        public MapViewModel(NavigationStore navigationStore, BuildingStore buildingStore)
        {
            //var bitMap = new BitmapImage();

            //var stream = File.OpenRead("C:\\Users\\schumarkie\\Downloads\\WpfApp4\\WpfApp4\\Resources\\campusmap.jpg");

            //bitMap.BeginInit();
            //bitMap.StreamSource = stream;
            //bitMap.DecodePixelHeight = 625;
            //bitMap.CacheOption = BitmapCacheOption.OnLoad;
            //bitMap.EndInit();
            //stream.Close();
            //stream.Dispose();
            //bitMap.Freeze();

            //Image = bitMap;

            CampusMap = "C:\\Users\\schumarkie\\Downloads\\WpfApp4\\WpfApp4\\Resources\\campusmap.jpg";

            VideoSearchbarNavigateCommand = new NavigateCommand<VideoViewModel>(buildingStore, new NavigationService<VideoViewModel>(navigationStore, () => new VideoViewModel(navigationStore, buildingStore)));

            HomeNavigateCommand = new NavigateCommand<HomeViewModel>(buildingStore, new NavigationService<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore, buildingStore)));
        }

        ~MapViewModel() { }
    }
}
