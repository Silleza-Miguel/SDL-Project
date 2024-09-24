using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp4.ViewModels;

namespace WpfApp4.Views
{
    /// <summary>
    /// Interaction logic for VideoView.xaml
    /// </summary>
    public partial class VideoView : UserControl
    {
        private static bool _isVideoPlaying;

        public static event Action VideoStatusChanged;

        public static bool IsVideoPlaying 
        {
            get => _isVideoPlaying;
            set
            {
                _isVideoPlaying = value;
                OnVideoStatusChanged();
            }
        }

        public VideoView()
        {
            InitializeComponent();
        }

        private void test()
        {
            // Access the ViewModel from the DataContext
            var viewModel = (VideoViewModel)this.DataContext;

            viewModel.Test = 1;
        }

        private void OnManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frame.Focus();

            if (tb.Text != "")
            {
                tb.PlaceholderText = tb.Text;
                tb.Text = "";
            }

            else
            {
                tb.PlaceholderText = "Search";
            }
        }

        private void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tb.PlaceholderText.ToLower() != "search")
            {
                tb.Text = tb.PlaceholderText;
                tb.PlaceholderText = "Search";
            }
        }

        private void VideoFrame_MediaOpened(object sender, RoutedEventArgs e)
        {
            IsVideoPlaying = true;
            test();
        }

        private void VideoFrame_MediaEnded(object sender, RoutedEventArgs e)
        {
            IsVideoPlaying = false;
        }

        private  static void OnVideoStatusChanged()
        {
            VideoStatusChanged?.Invoke();
        }
    }
}
