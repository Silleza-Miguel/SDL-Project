using System.Windows;
using System.Windows.Threading;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.Stores;
using WpfApp4.Views.RoomNav;

namespace WpfApp4.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationService<IdleViewModel> _navigationService;

        private readonly NavigationStore _navigationStore;

        private readonly BuildingStore _buildingStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        private DispatcherTimer _timer;

        public MainViewModel(NavigationStore navigationStore, DispatcherTimer timer, BuildingStore buildingStore)
        {
            _navigationStore = navigationStore;

            _buildingStore = buildingStore;

            _timer = timer;

            _navigationStore.CurrentViewModelChanged += _navigationStore_CurrentViewModelChanged;

            _navigationService = new NavigationService<IdleViewModel>(_navigationStore, () => new IdleViewModel(_navigationStore, _buildingStore));
        }

        private void _navigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));

            _timer.Tick -= timer_Tick;

            if (_navigationStore.CurrentViewModel.GetType() != typeof(IdleViewModel))
            {

                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += timer_Tick;
                _timer.Start();
            }

            else
            {
                _timer.Stop();
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            var idleTime = IdleTimeService.GetIdleTimeInfo();

            if (idleTime.IdleTime.TotalSeconds >= 10)
            {
                //MessageBox.Show($"APP IS IDLE AIIEEEEE \n{idleTime.IdleTime.TotalSeconds} and {TimeSpan.FromMilliseconds(idleTime.SystemUptimeMilliseconds)}\nCurrent ViewModel: {_navigationStore.CurrentViewModel}");

                //_navigationStore.CurrentViewModel = new IdleViewModel(_navigationStore, _buildingStore);

                _navigationService.Navigate();
            }
        }
    }
}
