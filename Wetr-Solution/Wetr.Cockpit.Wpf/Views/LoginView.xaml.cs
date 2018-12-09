using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Wetr.Cockpit.Wpf.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginUserNameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginUserNameTxtBox.Foreground = Brushes.Black;
        }

        private void LoginPasswordTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LoginPasswordTxtBox.Foreground = Brushes.Black;
        }

        private void LoginUserNameTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            LoginUserNameTxtBox.Foreground = Brushes.LightGray;
        }

        private void LoginPasswordTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            LoginPasswordTxtBox.Foreground = Brushes.LightGray;
        }
    }
}
