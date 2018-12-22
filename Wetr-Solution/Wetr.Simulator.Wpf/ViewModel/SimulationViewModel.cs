using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using Wetr.BusinessLogic;
using Wetr.Dal.Factory;
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
    public class SimulationViewModel : ViewModelBase, IWetrViewModelBase, IDisposable
    {
        #region variables

        private int MaxChartValues = 60;
        private Timer secondTimer, minuteTimer, hourTimer, dayTimer, weekTimer;
        private PresetCreationViewModel presetCreationViewModel = ServiceLocator.Current.GetInstance<PresetCreationViewModel>();
        private readonly Wetr.BusinessLogic.Generator generator;



        public SeriesCollection SeriesCollection { get; set; }
        public ObservableCollection<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private int maxSliderValue;

        public int MaxSliderValue
        {
            get { return maxSliderValue; }
            set
            {
                if (maxSliderValue != value)
                    Set(ref maxSliderValue, value);
            }
        }

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
                Set(ref selectedPreset, value);
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
                    Set(ref graphEnabled, value);
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
                    Set(ref speedFactor, value);

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

                    if (SimulationRunning)
                    {
                        this.secondTimer.Start();
                        this.minuteTimer.Start();
                        this.hourTimer.Start();
                        this.dayTimer.Start();
                        this.weekTimer.Start();
                    }
                }
            }
        }

        public RelayCommand StartSimulation { get; private set; }
        public RelayCommand StopSimulation { get; private set; }
        public RelayCommand ComboBoxChanged { get; private set; }

        public bool SimulationRunning { get; set; }

        #endregion variables

        public SimulationViewModel()
        {

            this.generator = new BusinessLogic.Generator(AdoFactory.Instance.GetMeasurementDao("wetr"));

            SeriesCollection = new SeriesCollection();
            Labels = new ObservableCollection<string>();
            MaxSliderValue = 60;

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

            ComboBoxChanged = new RelayCommand(
                ExecuteSelectionChanged,
                CanExecuteSelectionChanged
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

        #region functions

        private bool CanExecuteSelectionChanged()
        {
            return true;
        }

        /// <summary>
        /// Set MaxSlider Value Depending on the Frequency used
        /// </summary>
        /// <seealso cref="Frequency"/>
        private void ExecuteSelectionChanged()
        {
            if (SelectedPreset != null)
            {
                switch (selectedPreset.Frequency)
                {
                    case Frequency.Second:
                        MaxSliderValue = 10;
                        break;

                    case Frequency.Minute:
                        MaxSliderValue = 600;
                        break;

                    case Frequency.Hour:
                        MaxSliderValue = 36000;
                        break;

                    case Frequency.Day:
                        MaxSliderValue = 864000;
                        break;

                    case Frequency.Week:
                        MaxSliderValue = 6048000;
                        break;
                }

                if (SpeedFactor > MaxSliderValue)
                {
                    SpeedFactor = MaxSliderValue / 10;
                }

                ResetChart();
            }
        }

        private bool CanExecuteStartSimulation()
        {
            return this.SimulationRunning == false;
        }

        private bool CanExecuteStopSimulation()
        {
            return this.SimulationRunning == true;
        }

        /// <summary>
        /// Starts the simulation
        /// </summary>
        private void ExecuteStartSimulation()
        {
            Console.WriteLine("Starting Simulation");
            this.SimulationRunning = true;

            ResetChart();

            this.secondTimer.Start();
            this.minuteTimer.Start();
            this.hourTimer.Start();
            this.dayTimer.Start();
            this.weekTimer.Start();

            this.StopSimulation.RaiseCanExecuteChanged();
            this.StartSimulation.RaiseCanExecuteChanged();
        }




        /// <summary>
        /// Stops the simulation
        /// </summary>
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


        /// <summary>
        /// Called when second Timer.Elapsed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SecondTick(object sender, EventArgs e)
        {
           this.generator.Generate(this.Presets, Frequency.Second);
            if (SelectedPreset != null && SelectedPreset.Frequency == Frequency.Second)
                UpdateChart();
        }

        /// <summary>
        /// Called when minute Timer.Elapsed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MinuteTick(object sender, EventArgs e)
        {
            this.generator.Generate(this.Presets, Frequency.Minute);
            if (SelectedPreset != null && SelectedPreset.Frequency == Frequency.Minute)
                UpdateChart();
        }

        /// <summary>
        /// Called when hour Timer.Elapsed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HourTick(object sender, EventArgs e)
        {
            this.generator.Generate(this.Presets, Frequency.Hour);
            if (SelectedPreset != null && SelectedPreset.Frequency == Frequency.Hour)
                UpdateChart();
        }

        /// <summary>
        /// Called when day Timer.Elapsed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DayTick(object sender, EventArgs e)
        {
            this.generator.Generate(this.Presets, Frequency.Day);
            if (SelectedPreset != null && SelectedPreset.Frequency == Frequency.Day)
                UpdateChart();
        }

        /// <summary>
        /// Called when week Timer.Elapsed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WeekTick(object sender, EventArgs e)
        {
            this.generator.Generate(this.Presets, Frequency.Week);
            if (SelectedPreset != null && SelectedPreset.Frequency == Frequency.Week)
                UpdateChart();
        }

        /// <summary>
        /// Update Chart with new values
        /// <para>Calls ClearChart to reduce lag in the chart</para>
        /// </summary>
        public void UpdateChart()
        {
            if (SelectedPreset != null && GraphEnabled)
            {
                int count = 0;
                foreach (Station s in this.SelectedPreset.Stations)
                {
                    List<Measurement> tempList = SelectedPreset.GeneratedData[s];
                    Measurement m = tempList[tempList.Count - 1];
                    if (SeriesCollection[count].Values.Count > MaxChartValues)
                    {
                        ClearChart(1);
                    }
                    SeriesCollection[count].Values.Add(m.Value);
                    count++;
                }
            }
        }

        /// <summary>
        /// Removes values from a SeriesCollection LineChart
        /// </summary>
        /// <param name="range">How many values should be removed</param>
        private void ClearChart(int range)
        {
            int count = 0;
            foreach (Station s in this.SelectedPreset.Stations)
            {
                for (int i = 0; i < range && i < SeriesCollection[count].Values.Count - 1; ++i)
                    SeriesCollection[count].Values.RemoveAt(i);
                count++;
            }
        }

        private void ResetChart()
        {
            if (SelectedPreset != null)
            {
                Labels.Clear();
                SeriesCollection.Clear();

                List<string> tempLabels = new List<string>();
                foreach (Station s in this.SelectedPreset.Stations)
                {
                    tempLabels.Add(s.Name);
                    SeriesCollection.Add(new LineSeries
                    {
                        Title = s.Name,
                        LineSmoothness = 1,
                        Values = new ChartValues<double>()
                    });
                }
                Labels = new ObservableCollection<string>(tempLabels);
            }
        }

        public void CleanUp()
        {
            base.Cleanup();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            secondTimer.Dispose();
            minuteTimer.Dispose();
            hourTimer.Dispose();
            dayTimer.Dispose();
            weekTimer.Dispose();
        }

        #endregion functions
    }
}