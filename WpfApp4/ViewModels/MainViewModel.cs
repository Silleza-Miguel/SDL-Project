using System.Windows;
using System.Windows.Threading;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.Stores;
using WpfApp4.Views;
using WpfApp4.Views.RoomNav;

namespace WpfApp4.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationService<IdleViewModel> _navigationService;

        private readonly NavigationStore _navigationStore;

        private readonly BuildingStore _buildingStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public bool isPLaying => VideoView.IsVideoPlaying;

        private DispatcherTimer _timer;

        private bool istouched = false;

        private TimeSpan newIdleTime;

        public MainViewModel(NavigationStore navigationStore, DispatcherTimer timer, BuildingStore buildingStore)
        {
            _navigationStore = navigationStore;

            _buildingStore = buildingStore;

            _timer = timer;

            _navigationStore.CurrentViewModelChanged += CurrentViewModelChanged;

            VideoView.VideoStatusChanged += IsVideoPlayingChanged;

            _navigationService = new NavigationService<IdleViewModel>(_navigationStore, () => new IdleViewModel(_navigationStore, _buildingStore));
        }

        private void IsVideoPlayingChanged()
        {
            OnPropertyChanged(nameof(isPLaying));
            _timer.Tick -= timer_Tick;

            if (isPLaying == false)
            {
                var idleTime = IdleTimeService.GetIdleTimeInfo();

                newIdleTime = idleTime.IdleTime;

                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += timer_Tick;
                _timer.Start();
            }

            else
            {
                _timer.Stop();
            }

        }

        private void CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));

            _timer.Tick -= timer_Tick;

            if (CurrentViewModel.GetType() == typeof(IdleViewModel))
            {
                _timer.Stop();
            }

            else if (CurrentViewModel.GetType() == typeof(VideoViewModel))
            {
                _timer.Stop();
            }

            else
            {
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += timer_Tick;
                _timer.Start();
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            var idleTime = IdleTimeService.GetIdleTimeInfo();

            int threshold = 30;

            if (CurrentViewModel.GetType() == typeof(VideoViewModel))
            {
                if (idleTime.IdleTime.TotalSeconds < newIdleTime.TotalSeconds || istouched == true)
                {
                    istouched = true;

                    if(idleTime.IdleTime.TotalSeconds >= threshold)
                    {
                        //MessageBox.Show($"APP IS IDLE AIIEEEEE \n{idleTime.IdleTime.TotalSeconds} and {TimeSpan.FromMilliseconds(idleTime.SystemUptimeMilliseconds)}\nCurrent ViewModel: {_navigationStore.CurrentViewModel}");

                        //_navigationStore.CurrentViewModel = new IdleViewModel(_navigationStore, _buildingStore);
                        istouched = false;
                        _navigationService.Navigate();
                    }
                }

                else if ((idleTime.IdleTime.TotalSeconds - newIdleTime.TotalSeconds) >=  + threshold && istouched == false)
                {
                    //MessageBox.Show($"APP IS IDLE AIIEEEEE \n{idleTime.IdleTime.TotalSeconds} and {TimeSpan.FromMilliseconds(idleTime.SystemUptimeMilliseconds)}\nCurrent ViewModel: {_navigationStore.CurrentViewModel}");

                    //_navigationStore.CurrentViewModel = new IdleViewModel(_navigationStore, _buildingStore);

                    _navigationService.Navigate();
                }
            }
            else
            {
                if (idleTime.IdleTime.TotalSeconds >= threshold)
                {
                    //MessageBox.Show($"APP IS IDLE AIIEEEEE \n{idleTime.IdleTime.TotalSeconds} and {TimeSpan.FromMilliseconds(idleTime.SystemUptimeMilliseconds)}\nCurrent ViewModel: {_navigationStore.CurrentViewModel}");

                    //_navigationStore.CurrentViewModel = new IdleViewModel(_navigationStore, _buildingStore);

                    _navigationService.Navigate();
                }
            }
        }
    }
}
