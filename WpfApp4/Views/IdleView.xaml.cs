using System.Windows.Controls;

namespace WpfApp4.Views
{
    /// <summary>
    /// Interaction logic for IdleView.xaml
    /// </summary>
    public partial class IdleView : UserControl
    {
        public IdleView()
        {
            InitializeComponent();
        }

        private void MediaElement_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {
            MediaPlayer.Position = TimeSpan.FromMilliseconds(1);
        }
    }
}
