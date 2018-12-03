using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            if (LoginUserNameTxtBox.Text.Equals("Username"))
                LoginUserNameTxtBox.Text = String.Empty;
            LoginUserNameTxtBox.Foreground = Brushes.Black;
        }

        private void LoginPasswordTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
           // if (LoginPasswordTxtBox.Text.Equals("Password"))
               // LoginPasswordTxtBox.Text = String.Empty;
            LoginPasswordTxtBox.Foreground = Brushes.Black;
        }

        private void LoginUserNameTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginUserNameTxtBox.Text.Equals(String.Empty))
                LoginUserNameTxtBox.Text = "Username";
            LoginUserNameTxtBox.Foreground = Brushes.LightGray;
        }

        private void LoginPasswordTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (LoginPasswordTxtBox.Text.Equals(String.Empty))
                //LoginPasswordTxtBox.Text = "Password";
            LoginPasswordTxtBox.Foreground = Brushes.LightGray;
        }
    }
}
