using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Wetr.BusinessLogic;
using Wetr.Cockpit.Wpf.Interface;
using Wetr.Domain;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// DashboardViewModel for the DashboardView
    /// </summary>
    /// <seealso cref="Wetr.Cockpit.Wpf.Views.DashboardView"/>
    /// <seealso cref="Wetr.Cockpit.Wpf.Interface.IWetrViewModelBase"/>
    public class DashboardViewModel : ViewModelBase, IWetrViewModelBase
    {
        #region variabls

        private StationManager stationManager;
        private MeasurementManager measurementManager;

        private LoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<LoginViewModel>();

        public Collection<Station> userStations { get; set; }

        public SeriesCollection SeriesCollectionAverageTemperature { get; set; }
        public SeriesCollection SeriesCollectionAverageRain { get; set; }

        public ObservableCollection<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private int stationCount;

        public int StationCount
        {
            get { return stationCount; }
            set
            {
                if (stationCount != value)
                    Set(ref stationCount, value);
            }
        }

        private int measurementCount;

        public int MeasurementCount
        {
            get { return measurementCount; }
            set
            {
                if (measurementCount != value)
                    Set(ref measurementCount, value);
            }
        }

        private int weeklyMeasurementCount;

        public int WeeklyMeasurementCount
        {
            get { return weeklyMeasurementCount; }
            set
            {
                if (weeklyMeasurementCount != value)
                    Set(ref weeklyMeasurementCount, value);
            }
        }

        #endregion variabls

        #region commands

        public RelayCommand StationClickCommand { get; private set; }
        public RelayCommand MeasurementClickCommand { get; private set; }
        public RelayCommand DashboardViewLoadedCommand { get; private set; }

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

        private bool CanExecuteDashboardViewLoadedCommand()
        {
            return true;
        }

        private async void ExecuteDashboardViewLoadedCommand()
        {
            await Task.Run(LoadDashboardValues);
        }

        #endregion commands

        public DashboardViewModel()
        {
            stationManager = ManagerLocator.GetStationManagerInstance;
            measurementManager = ManagerLocator.GetMeasurementManagerInstance;

            SeriesCollectionAverageTemperature = new SeriesCollection();
            SeriesCollectionAverageRain = new SeriesCollection();
            Labels = new ObservableCollection<string>();
            YFormatter = value => value.ToString("");

            SeriesCollectionAverageTemperature.Add(new LineSeries
            {
                Title = "Temperature",
                LineSmoothness = 1,
                Values = new ChartValues<double>()
            });

            SeriesCollectionAverageRain.Add(new LineSeries
            {
                Title = "Rain Amount",
                LineSmoothness = 1,
                Values = new ChartValues<double>()
            });

            Labels.Add(DateTime.Now.AddDays(-6).DayOfWeek.ToString());
            Labels.Add(DateTime.Now.AddDays(-5).DayOfWeek.ToString());
            Labels.Add(DateTime.Now.AddDays(-4).DayOfWeek.ToString());
            Labels.Add(DateTime.Now.AddDays(-3).DayOfWeek.ToString());
            Labels.Add(DateTime.Now.AddDays(-2).DayOfWeek.ToString());
            Labels.Add(DateTime.Now.AddDays(-1).DayOfWeek.ToString());
            Labels.Add(DateTime.Now.DayOfWeek.ToString());

            StationCount = 0;
            MeasurementCount = 0;
            WeeklyMeasurementCount = 0;

            StationClickCommand = new RelayCommand(
                ExecuteStationClickCommand,
                CanExecuteMeasurementClickCommand);

            MeasurementClickCommand = new RelayCommand(
                ExecuteMeasurementClickCommand,
                CanExecuteMeasurementClickCommand);

            DashboardViewLoadedCommand = new RelayCommand(
                ExecuteDashboardViewLoadedCommand,
                CanExecuteDashboardViewLoadedCommand);
        }

        public async Task LoadDashboardValues()
        {
            var stations = await stationManager.GetStationsForUser(loginViewModel.loggedInUser.UserId);
            userStations = new Collection<Station>(stations.ToList());

            StationCount = userStations.Count;

            double[] allTemp = new double[7];
            double[] allTempCount = new double[7];

            double[] allRain = new double[7];
            double[] allRainCount = new double[7];

            foreach (Station s in userStations)
            {
                var measurements = await measurementManager.GetAllMeasurementsForStation(s.StationId);
                Collection<Measurement> stationMeasurements = new Collection<Measurement>(measurements.ToList());

                MeasurementCount += stationMeasurements.Count;

                foreach (Measurement m in stationMeasurements)
                {
                    if (m.TimesStamp.Date >= DateTime.Now.AddDays(-6).Date)
                    {
                        weeklyMeasurementCount += 1;

                        switch (m.MeasurementTypeId)
                        {
                            // temperature
                            case 1:
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-6).Day)
                                {
                                    allTemp[0] += m.Value;
                                    allTempCount[0] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-5).Day)
                                {
                                    allTemp[1] += m.Value;
                                    allTempCount[1] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-4).Day)
                                {
                                    allTemp[2] += m.Value;
                                    allTempCount[2] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-3).Day)
                                {
                                    allTemp[3] += m.Value;
                                    allTempCount[3] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-2).Day)
                                {
                                    allTemp[4] += m.Value;
                                    allTempCount[4] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-1).Day)
                                {
                                    allTemp[5] += m.Value;
                                    allTempCount[5] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.Day)
                                {
                                    allTemp[6] += m.Value;
                                    allTempCount[6] += 1;
                                }
                                break;
                            // rain amount
                            case 3:
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-6).Day)
                                {
                                    allRain[0] += m.Value;
                                    allRainCount[0] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-5).Day)
                                {
                                    allRain[1] += m.Value;
                                    allRainCount[1] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-4).Day)
                                {
                                    allRain[2] += m.Value;
                                    allRainCount[2] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-3).Day)
                                {
                                    allRain[3] += m.Value;
                                    allRainCount[3] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-2).Day)
                                {
                                    allRain[4] += m.Value;
                                    allRainCount[4] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(-1).Day)
                                {
                                    allRain[5] += m.Value;
                                    allRainCount[5] += 1;
                                }
                                if (m.TimesStamp.Day == DateTime.Now.AddDays(0).Day)
                                {
                                    allRain[6] += m.Value;
                                    allRainCount[6] += 1;
                                }
                                break;
                        }
                    }
                }
            }

            if (allTempCount[0] == 0 || allTemp[0] == 0)
                SeriesCollectionAverageTemperature[0].Values.Add(0d);
            else
                SeriesCollectionAverageTemperature[0].Values.Add(allTemp[0] / allTempCount[0]);

            if (allTempCount[1] == 0 || allTemp[1] == 0)
                SeriesCollectionAverageTemperature[0].Values.Add(0d);
            else
                SeriesCollectionAverageTemperature[0].Values.Add(allTemp[1] / allTempCount[1]);

            if (allTempCount[2] == 0 || allTemp[2] == 0)
                SeriesCollectionAverageTemperature[0].Values.Add(0d);
            else
                SeriesCollectionAverageTemperature[0].Values.Add(allTemp[2] / allTempCount[2]);

            if (allTempCount[3] == 0 || allTemp[3] == 0)
                SeriesCollectionAverageTemperature[0].Values.Add(0d);
            else
                SeriesCollectionAverageTemperature[0].Values.Add(allTemp[3] / allTempCount[3]);

            if (allTempCount[4] == 0 || allTemp[4] == 0)
                SeriesCollectionAverageTemperature[0].Values.Add(0d);
            else
                SeriesCollectionAverageTemperature[0].Values.Add(allTemp[4] / allTempCount[4]);

            if (allTempCount[5] == 0 || allTemp[5] == 0)
                SeriesCollectionAverageTemperature[0].Values.Add(0d);
            else
                SeriesCollectionAverageTemperature[0].Values.Add(allTemp[5] / allTempCount[5]);

            if (allTempCount[6] == 0 || allTemp[6] == 0)
                SeriesCollectionAverageTemperature[0].Values.Add(0d);
            else
                SeriesCollectionAverageTemperature[0].Values.Add(allTemp[6] / allTempCount[6]);


            if (allRainCount[0] == 0 || allRain[0] == 0)
                SeriesCollectionAverageRain[0].Values.Add(0d);
            else
                SeriesCollectionAverageTemperature[0].Values.Add(allRain[0] / allRainCount[0]);

            if (allRainCount[1] == 0 || allRain[1] == 0)
                SeriesCollectionAverageRain[0].Values.Add(0d);
            else
                SeriesCollectionAverageRain[0].Values.Add(allRain[1] / allRainCount[1]);

            if (allRainCount[2] == 0 || allRain[2] == 0)
                SeriesCollectionAverageRain[0].Values.Add(0d);
            else
                SeriesCollectionAverageRain[0].Values.Add(allRain[2] / allRainCount[2]);

            if (allRainCount[3] == 0 || allRain[3] == 0)
                SeriesCollectionAverageRain[0].Values.Add(0d);
            else
                SeriesCollectionAverageRain[0].Values.Add(allRain[3] / allRainCount[3]);

            if (allRainCount[4] == 0 || allRain[4] == 0)
                SeriesCollectionAverageRain[0].Values.Add(0d);
            else
                SeriesCollectionAverageRain[0].Values.Add(allRain[4] / allRainCount[4]);

            if (allRainCount[5] == 0 || allRain[5] == 0)
                SeriesCollectionAverageRain[0].Values.Add(0d);
            else
                SeriesCollectionAverageRain[0].Values.Add(allRain[5] / allRainCount[5]);

            if (allRainCount[6] == 0 || allRain[6] == 0)
                SeriesCollectionAverageRain[0].Values.Add(0d);
            else
                SeriesCollectionAverageRain[0].Values.Add(allRain[6] / allRainCount[6]);
        }

        public void CleanUp()
        {
            base.Cleanup();
        }
    }
}