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
            {   if (presetCreationViewModel != null)
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
            // TODO: Add Constraint
            return true;
        }

        private bool CanExecuteClear()
        {
            // TODO: Add Constraint
            return true;
        }

        /* Add Preset Command */

        private void ExecuteAddPreset()
        {
            if (!PresetStations.Contains(SelectedStation))
            {
                PresetStations.Add(SelectedStation);
                RaisePropertyChanged(nameof(PresetStations));
                this.AddPreset.RaiseCanExecuteChanged();
            }
        }

        /* Delete Preset Command */

        private void ExecuteDeletePreset()
        {
            PresetStations.Remove(SelectedPresetStation);
            this.DeletePreset.RaiseCanExecuteChanged();
        }

        /* Clear Preset Command */

        public void ExecuteClearPreset()
        {
            PresetStations.Clear();
            this.ClearPreset.RaiseCanExecuteChanged();
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
            throw new System.NotImplementedException();
        }
    }
}