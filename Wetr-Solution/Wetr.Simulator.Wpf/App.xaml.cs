#pragma warning disable 0436

using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace Wetr.Cockpit.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
