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

            StationCount = (int) await stationManager.GetNumberOfStations();
            MeasurementCount = (int) await measurementManager.GetNumberOfMeasurementsAsync();
            WeeklyMeasurementCount = (int)await measurementManager.GetNumberOfMeasurementsOfWeekAsync();

            var temperatureValues = await measurementManager.GetDashbardTemperaturesAsync();
            foreach (double d in temperatureValues)
                SeriesCollectionAverageTemperature[0].Values.Add(d);

            var rainValues = await measurementManager.GetDashboardRainValuesAsync();
            foreach (double d in rainValues)
                SeriesCollectionAverageRain[0].Values.Add(d);







        }

        public void CleanUp()
        {
            base.Cleanup();
        }
    }
}