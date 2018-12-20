
using CommonServiceLocator;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using Wetr.Cockpit.Wpf.ViewModel;

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

        private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DashboardViewModel vm =                 ServiceLocator.Current.GetInstance<DashboardViewModel>();
            WeatherStationManagementViewModel vm2 = ServiceLocator.Current.GetInstance<WeatherStationManagementViewModel>();
            WeatherAnalysisViewModel vm3 =          ServiceLocator.Current.GetInstance<WeatherAnalysisViewModel>();

            if (vm != null)
            {
                await vm.LoadDashboardValues();
            }

            if (vm2 != null)
            {
                await vm2.InitDropdowns();
                await vm2.LoadStations();
            }

            if (vm3 != null)
            {
                await vm3.Load();
            }
        }
    }
}
