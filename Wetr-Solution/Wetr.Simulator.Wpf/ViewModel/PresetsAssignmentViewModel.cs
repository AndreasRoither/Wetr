using CommonServiceLocator;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Wetr.Cockpit.Wpf.Model;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;
using Wetr.Simulator.Wpf.Interface;
using Wetr.Simulator.Wpf.ViewModel;

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

        private StationSelectionViewModel stationSelectionViewModel = ServiceLocator.Current.GetInstance<StationSelectionViewModel>();
        private PresetCreationViewModel presetCreationViewModel = ServiceLocator.Current.GetInstance<PresetCreationViewModel>();

        public ObservableCollection<Preset> Presets
        {
            get { return presetCreationViewModel.PresetList; }
        }

        public Station SelectedStation { get; set; }
        public Preset SelectedPreset { get; set; }

        public ObservableCollection<Station> SelectedStations
        {
            get { return stationSelectionViewModel.selectedStations; }
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