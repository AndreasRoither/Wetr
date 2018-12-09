using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Timers;
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
                {
                    this.speedFactor = value;

                    this.secondTimer.Stop();
                    this.minuteTimer.Stop();
                    this.hourTimer.Stop();
                    this.dayTimer.Stop();
                    this.weekTimer.Stop();

                    this.secondTimer.Interval = 1000.0 / this.speedFactor;
                    this.minuteTimer.Interval = 1000.0 * 60.0 / this.speedFactor;
                    this.hourTimer.Interval = 1000.0 * 60.0 * 60.0 / this.speedFactor;
                    this.dayTimer.Interval = 1000.0 * 60.0 * 60.0 * 24.0 / this.speedFactor;
                    this.weekTimer.Interval = 1000.0 * 60.0 * 60.0 * 24.0 * 7.0 / this.speedFactor;

                    this.secondTimer.Start();
                    this.minuteTimer.Start();
                    this.hourTimer.Start();
                    this.dayTimer.Start();
                    this.weekTimer.Start();

                }
            }
        }

        public RelayCommand StartSimulation { get; private set; }
        public RelayCommand StopSimulation { get; private set; }

        public bool SimulationRunning { get; set; }

        private bool CanExecuteStartSimulation()
        {
            return this.SimulationRunning == false;
        }

        private bool CanExecuteStopSimulation()
        {
            return this.SimulationRunning == true;
        }

        private Timer secondTimer, minuteTimer, hourTimer, dayTimer, weekTimer;

        private void ExecuteStartSimulation()
        {
            Console.WriteLine("Starting Simulation");
            this.SimulationRunning = true;

            this.secondTimer.Start();
            this.minuteTimer.Start();
            this.hourTimer.Start();
            this.dayTimer.Start();
            this.weekTimer.Start();

            this.StopSimulation.RaiseCanExecuteChanged();
            this.StartSimulation.RaiseCanExecuteChanged();

        }

        private void ExecuteStopSimulation()
        {
            Console.WriteLine("Stopping Simulation");
            this.SimulationRunning = false;

            this.secondTimer.Stop();
            this.minuteTimer.Stop();
            this.hourTimer.Stop();
            this.dayTimer.Stop();
            this.weekTimer.Stop();

            this.StopSimulation.RaiseCanExecuteChanged();
            this.StartSimulation.RaiseCanExecuteChanged();
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

            this.secondTimer = new Timer();
            this.minuteTimer = new Timer();
            this.hourTimer = new Timer();
            this.dayTimer = new Timer();
            this.weekTimer = new Timer();

            this.secondTimer.Interval = 1000;
            this.minuteTimer.Interval = 1000 * 60;
            this.hourTimer.Interval = 1000 * 60 * 60;
            this.dayTimer.Interval = 1000 * 60 * 60 * 24;
            this.weekTimer.Interval = 1000 * 60 * 60 * 24 * 7;

            this.secondTimer.Elapsed += SecondTick;
            this.minuteTimer.Elapsed += MinuteTick;
            this.hourTimer.Elapsed += HourTick;
            this.dayTimer.Elapsed += DayTick;
            this.weekTimer.Elapsed += WeekTick;

        }


        public void SecondTick(object sender, EventArgs e)
        {
            Console.WriteLine("SecondTick");
        }

        public void MinuteTick(object sender, EventArgs e)
        {
            Console.WriteLine("MinuteTick");
        }

        public void HourTick(object sender, EventArgs e)
        {
            Console.WriteLine("HourTick");
        }

        public void DayTick(object sender, EventArgs e)
        {
            Console.WriteLine("DayTick");
        }

        public void WeekTick(object sender, EventArgs e)
        {
            Console.WriteLine("WeekTick");
        }
        public void CleanUp()
        {
            throw new System.NotImplementedException();
        }
    }
}