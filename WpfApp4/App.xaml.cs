using System.Windows;
using System.Windows.Threading;
using WpfApp4.Stores;
using WpfApp4.ViewModels;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        private readonly BuildingStore _buildingStore;

        private DispatcherTimer _timer;

        public App()
        {
            _navigationStore = new NavigationStore();
            _buildingStore = new BuildingStore();
            _timer = new DispatcherTimer();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //Set the ViewModel of the View you want to start with as default
            _navigationStore.CurrentViewModel = new IdleViewModel(_navigationStore, _buildingStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, _timer, _buildingStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
