using MahApps.Metro.Controls;
using System.Windows.Controls;
using Wetr.Cockpit.Wpf.ViewModel;
using Wetr.Cockpit.Wpf.Views;

namespace Wetr.Cockpit.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private static ContentControl contentControl;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            contentControl = this.ContentControl;
            // deactivated for testing purposes
            //contentControl.Content = new LoginView();
            contentControl.Content = new LoginView();
        }

        public static void SetContentControl(UserControl control)
        {
            contentControl.Content = control;
        }
    }
}