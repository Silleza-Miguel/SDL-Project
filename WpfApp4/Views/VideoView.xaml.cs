using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp4.Views
{
    /// <summary>
    /// Interaction logic for VideoView.xaml
    /// </summary>
    public partial class VideoView : UserControl
    {
        public VideoView()
        {
            InitializeComponent();
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
    }
}
