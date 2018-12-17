using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using Wetr.Domain;
using Wetr.Simulator.Wpf.Interface;
using Wetr.Simulator.Wpf.Model;

namespace Wetr.Simulator.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the PresetAssignmentView can bind to
    /// </summary>
    /// <seealso cref="Wetr.Simulator.Wpf.Views.PresetAssignmentView"/>
    /// <seealso cref="Wetr.Simulator.Wpf.Interface.IWetrViewModelBase"/>
    public class PresetsAssignmentViewModel : ViewModelBase, IWetrViewModelBase
    {
        #region variables

        private StationSelectionViewModel stationSelectionViewModel = ServiceLocator.Current.GetInstance<StationSelectionViewModel>();
        private PresetCreationViewModel presetCreationViewModel = ServiceLocator.Current.GetInstance<PresetCreationViewModel>();

        public ObservableCollection<Preset> Presets
        {
            get
            {
                if (presetCreationViewModel != null)
                    return presetCreationViewModel.PresetList;
                return null;
            }
        }

        public ObservableCollection<Station> SelectedStations
        {
            get
            {
                if (stationSelectionViewModel != null)
                    return stationSelectionViewModel.selectedStations;
                return null;
            }
        }

        public ObservableCollection<Station> PresetStations
        {
            get
            {
                if (SelectedPreset != null)
                    return SelectedPreset.Stations;
                return null;
            }
        }

        private Station selectedStation;

        public Station SelectedStation
        {
            get { return selectedStation; }
            set { selectedStation = value; }
        }

        private Station selectedStationPreset;

        public Station SelectedPresetStation
        {
            get { return selectedStationPreset; }
            set { selectedStationPreset = value; }
        }

        private Preset selectedPreset;

        public Preset SelectedPreset
        {
            get
            {
                return selectedPreset;
            }

            set
            {
                if (selectedPreset != value)
                {
                    selectedPreset = value;
                    RaisePropertyChanged(nameof(PresetStations));
                }
            }
        }

        #endregion variables

        #region commands

        public RelayCommand AddPreset { get; private set; }
        public RelayCommand DeletePreset { get; private set; }
        public RelayCommand ClearPreset { get; private set; }

        private bool CanExecuteAddPreset()
        {
            return true;
        }

        private bool CanExecuteDeletePreset()
        {
            return true;
        }

        private bool CanExecuteClear()
        {
            return true;
        }

        /// <summary>
        /// Add Selected Station to the Selected PReset
        /// </summary>
        private void ExecuteAddPreset()
        {
            if (SelectedPreset != null && SelectedStation != null && !SelectedPreset.Stations.Contains(SelectedStation))
            {
                SelectedPreset.Stations.Add(SelectedStation);
                RaisePropertyChanged(nameof(PresetStations));
                this.AddPreset.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Removes a station from the selected preset
        /// </summary>
        private void ExecuteDeletePreset()
        {
            if (SelectedPreset != null && SelectedStation != null)
            {
                SelectedPreset.Stations.Remove(SelectedPresetStation);
                this.DeletePreset.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Removes all stations from the selected preset
        /// </summary>
        public void ExecuteClearPreset()
        {
            if (SelectedPreset != null)
            {
                SelectedPreset.Stations.Clear();
                this.ClearPreset.RaiseCanExecuteChanged();
            }
        }

        #endregion commands

        public PresetsAssignmentViewModel()
        {
            /* Add preset command */
            AddPreset = new RelayCommand(
                ExecuteAddPreset,
                CanExecuteAddPreset
            );

            /* Remove preset command */
            DeletePreset = new RelayCommand(
                ExecuteDeletePreset,
                CanExecuteDeletePreset
            );

            /* Clear preset command */
            ClearPreset = new RelayCommand(
                ExecuteClearPreset,
                CanExecuteClear
            );
        }

        public void CleanUp()
        {
            base.Cleanup();
        }
    }
}