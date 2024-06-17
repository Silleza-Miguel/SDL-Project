using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WpfApp4.Views
{
    /// <summary>
    /// Interaction logic for FloorView.xaml
    /// </summary>
    public partial class FloorView : UserControl
    {

        public string roomName;

        public FloorView()
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

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = (ToggleButton)sender;

            roomName = (string)toggle.Content;

            object wantedNode = FindElementByName<Path>(contentPath, roomName);
            if (wantedNode is Path)
            {
                // Following executed if Text element was found.
                Path wantedChild = wantedNode as Path;

                wantedChild.Fill = new SolidColorBrush(Colors.Transparent);

                ColorAnimation animation = new ColorAnimation((Color)ColorConverter.ConvertFromString("#15CDCA"), TimeSpan.FromSeconds(0.3));

                wantedChild.Fill.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = (ToggleButton)sender;

            object wantedNode = FindElementByName<Path>(contentPath, roomName);
            if (wantedNode is Path)
            {
                // Following executed if Text element was found.
                Path wantedChild = wantedNode as Path;

                wantedChild.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#15CDCA"));

                ColorAnimation animation = new ColorAnimation(Colors.Transparent, TimeSpan.FromSeconds(0.3));

                wantedChild.Fill.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            }

            roomName = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object wantedNode = FindElementByName<Path>(contentPath, roomName);
            if (wantedNode is Path)
            {
                // Following executed if Text element was found.
                Path wantedChild = wantedNode as Path;
                wantedChild.Fill = Brushes.Transparent;
            }

            roomName = null;
        }

        public T FindElementByName<T>(FrameworkElement element, string sChildName) where T : FrameworkElement
        {
            T childElement = null;
            var nChildCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < nChildCount; i++)
            {
                FrameworkElement child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;

                if (child == null)
                    continue;

                if (child is T && child.Name.Equals(sChildName))
                {
                    childElement = (T)child;
                    break;
                }

                childElement = FindElementByName<T>(child, sChildName);

                if (childElement != null)
                    break;
            }
            return childElement;
        }
    }
}
