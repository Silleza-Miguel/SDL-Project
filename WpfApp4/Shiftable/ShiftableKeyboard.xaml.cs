using System.Windows;
using System.Windows.Controls;

namespace WpfApp4.Shiftable
{
    /// <summary>
    /// Interaction logic for ShiftableKeyboard.xaml
    /// </summary>
    public partial class ShiftableKeyboard : UserControl
    {
        private bool isCapitilaized;
        private bool isShifted;
        public ShiftableKeyboard()
        {
            InitializeComponent();

            isCapitilaized = false;
            isShifted = false;

            lowerCaseKeyboard.Visibility = Visibility.Visible;
            shiftedKeyboard.Visibility = Visibility.Collapsed;
            capsKeyboard.Visibility = Visibility.Collapsed;
        }

        private void ModifierChanged(object sender, VisualKeyboard.Control.ModifierChangedEventArgs e)
        {
            if (e.virtualKeyCode == VisualKeyboard.Control.VirtualKeyCode.Shift)
            {
                isShifted = e.IsInEffect;
            }
            else if (e.virtualKeyCode == VisualKeyboard.Control.VirtualKeyCode.Capital)
            {
                isCapitilaized = e.IsInEffect;
            }

            if (isShifted && isCapitilaized) //show lower case keyboard
            {
                lowerCaseKeyboard.Visibility = Visibility.Visible;
                shiftedKeyboard.Visibility = Visibility.Collapsed;
                capsKeyboard.Visibility = Visibility.Collapsed;
            }
            else if (isShifted) //show shifted keyboard
            {
                lowerCaseKeyboard.Visibility = Visibility.Collapsed;
                shiftedKeyboard.Visibility = Visibility.Visible;
                capsKeyboard.Visibility = Visibility.Collapsed;
            }
            else if (isCapitilaized) // show caps keyboar
            {
                lowerCaseKeyboard.Visibility = Visibility.Collapsed;
                shiftedKeyboard.Visibility = Visibility.Collapsed;
                capsKeyboard.Visibility = Visibility.Visible;
            }
            else // show lowercase keyboard
            {
                lowerCaseKeyboard.Visibility = Visibility.Visible;
                shiftedKeyboard.Visibility = Visibility.Collapsed;
                capsKeyboard.Visibility = Visibility.Collapsed;
            }

            lowerCaseKeyboard.SynchroniseModifierKeyState();
            shiftedKeyboard.SynchroniseModifierKeyState();
            capsKeyboard.SynchroniseModifierKeyState();
        }
    }
}
