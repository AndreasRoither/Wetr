
using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace Wetr.Cockpit.Wpf.Views
{
    /// <summary>
    /// Interaction logic for MainContentView.xaml
    /// </summary>
    public partial class MainContentView : UserControl
    {
        public MainContentView()
        {
            InitializeComponent();
        }

        private void HamburgerMenuControl_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            HamburgerMenuControl.Content = e.InvokedItem;
        }
    }
}
