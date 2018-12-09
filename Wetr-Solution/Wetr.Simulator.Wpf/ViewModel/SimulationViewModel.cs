using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using Wetr.Domain;
using Wetr.Simulator.Wpf.Interface;
using Wetr.Simulator.Wpf.Model;

namespace Wetr.Simulator.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the SimulatorView can bind to
    /// </summary>
    /// <seealso cref="Wetr.Simulator.Wpf.Views.SimulationView"/>
    /// <seealso cref="Wetr.Simulator.Wpf.Interface.IWetrViewModelBase"/>
    public class SimulationViewModel : ViewModelBase, IWetrViewModelBase
    {

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

        private Preset selectedPreset;
        public Preset SelectedPreset
        {
            get { return selectedPreset; }
            set
            {
                selectedPreset = value;
                RaisePropertyChanged(nameof(PresetStations));

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

        private bool graphEnabled = true;
        public bool GraphEnabled
        {
            get
            {
                return this.graphEnabled;
            }

            set
            {
                if (value != graphEnabled)
                    this.graphEnabled = value;
            }
        }

        private double speedFactor = 1;
        public double SpeedFactor
        {
            get
            {
                return this.speedFactor;
            }

            set
            {
                if (value != speedFactor)
                    this.speedFactor = value;
            }
        }

        public RelayCommand StartSimulation { get; private set; }
        public RelayCommand StopSimulation { get; private set; }

        private bool CanExecuteStartSimulation()
        {
            return true;
        }

        private bool CanExecuteStopSimulation()
        {
            return true;
        }

        private void ExecuteStartSimulation()
        {
            Console.WriteLine("Starting Simulation");
        }

        private void ExecuteStopSimulation()
        {
            Console.WriteLine("Stopping Simulation");
        }

        public SimulationViewModel()
        {
            /* Start Simulation command */
            StartSimulation = new RelayCommand(
                ExecuteStartSimulation,
                CanExecuteStartSimulation
            );

            /* Stop Simulation command */
            StopSimulation = new RelayCommand(
                ExecuteStopSimulation,
                CanExecuteStopSimulation
            );
        }

        public void CleanUp()
        {
            throw new System.NotImplementedException();
        }
    }
}