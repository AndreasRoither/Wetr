using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Wetr.Cockpit.Wpf.Model;
using Wetr.Simulator.Wpf.Interface;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the SimulatorView can bind to
    /// </summary>
    /// <seealso cref="Wetr.Simulator.Wpf.Views.SimulationView"/>
    /// <seealso cref="Wetr.Simulator.Wpf.Interface.IWetrViewModelBase"/>
    public class SimulationViewModel : ViewModelBase, IWetrViewModelBase
    {
        #region variables

        private ObservableCollection<StationPreset> stationsPreset;

        public ObservableCollection<StationPreset> StationPresets
        {
            get { return stationsPreset; }
            set
            {
                if (stationsPreset != value)
                    Set(ref stationsPreset, value);
            }
        }

        #endregion variables

        public SimulationViewModel()
        {
        }

        public void CleanUp()
        {
            throw new System.NotImplementedException();
        }
    }
}