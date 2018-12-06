using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Wetr.Cockpit.Wpf.Model;
using Wetr.Domain;
using Wetr.Simulator.Wpf.Interface;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the PresetAssignmentView can bind to
    /// </summary>
    /// <seealso cref="Wetr.Simulator.Wpf.Views.PresetAssignmentView"/>
    /// <seealso cref="Wetr.Simulator.Wpf.Interface.IWetrViewModelBase"/>
    public class PresetsAssignmentViewModel : ViewModelBase, IWetrViewModelBase
    {
        #region variables

        private ObservableCollection<Preset> presets;

        public ObservableCollection<Preset> Presets
        {
            get { return presets; }
            set
            {
                if (presets != value)
                    Set(ref presets, value);
            }
        }

        private ObservableCollection<StationPreset> stationPresets;

        public ObservableCollection<StationPreset> StationPresets
        {
            get { return stationPresets; }
            set
            {
                if (stationPresets != value)
                    Set(ref stationPresets, value);
            }
        }

        private ObservableCollection<Station> stations;

        public ObservableCollection<Station> Stations
        {
            get { return stations; }
            set
            {
                if (stations != value)
                    Set(ref stations, value);
            }
        }

        #endregion variables

        public PresetsAssignmentViewModel()
        {
        }

        public void CleanUp()
        {
            throw new System.NotImplementedException();
        }
    }
}