
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using Wetr.Cockpit.Wpf.Interface;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// DashboardViewModel for the DashboardView
    /// </summary>
    /// <seealso cref="Wetr.Cockpit.Wpf.Views.DashboardView"/>
    /// <seealso cref="Wetr.Cockpit.Wpf.Interface.IWetrViewModelBase"/>
    class DashboardViewModel : ViewModelBase, IWetrViewModelBase
    {
        #region variabls
        #endregion variables

        #region commands

        public RelayCommand StationClickCommand { get; private set; }
        public RelayCommand MeasurementClickCommand { get; private set; }

        private bool CanExecuteStationClickCommand()
        {
            /* TODO: Add constraint */
            return true;
        }

        private void ExecuteStationClickCommand()
        {

        }

        private bool CanExecuteMeasurementClickCommand()
        {
            /* TODO: Add constraint */
            return true;
        }

        private void ExecuteMeasurementClickCommand()
        {

        }

        #endregion commands

        public DashboardViewModel()
        {
            StationClickCommand = new RelayCommand(
                ExecuteStationClickCommand,
                CanExecuteMeasurementClickCommand
            );

            MeasurementClickCommand = new RelayCommand(
                ExecuteMeasurementClickCommand,
                CanExecuteMeasurementClickCommand
            );
        }

        public void CleanUp()
        {
            throw new NotImplementedException();
        }
    }
}
