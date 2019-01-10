using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Wetr.ApiManager;
using Wetr.BusinessLogic;
using Wetr.Domain;
using Wetr.Simulator.Wpf.Interface;
using Wetr.Simulator.Wpf.Utility;

namespace Wetr.Simulator.Wpf.ViewModel
{
    /// <summary>
    /// This class contains Properties that the StationSelectionView can bind to
    /// </summary>
    /// <seealso cref="Wetr.Simulator.Wpf.Views.StationSelectionView"/>
    /// <seealso cref="Wetr.Simulator.Wpf.Interface.IWetrViewModelBase"/>
    public class StationSelectionViewModel : ViewModelBase, IWetrViewModelBase
    {
        #region variables

        private StationManager stationManager;
        private NotifierManager notifierManager = new NotifierManager();
        private WetrApiManager wetrApiManager = new WetrApiManager();

        private String availableStationsFilter;

        public String AvailableStationsFilter
        {
            get { return availableStationsFilter; }
            set
            {
                if (availableStationsFilter != value)
                {
                    Set(ref availableStationsFilter, value);
                    this.AvailableStations.Refresh();
                }
            }
        }

        private String selectedStationsFilter;

        public String SelectedStationsFilter
        {
            get { return selectedStationsFilter; }
            set
            {
                if (selectedStationsFilter != value)
                {
                    Set(ref selectedStationsFilter, value);
                    this.SelectedStations.Refresh();
                }
            }
        }

        private Station selectedAvailableStation;

        public Station SelectedAvailableStation
        {
            get { return selectedAvailableStation; }
            set
            {
                if (selectedAvailableStation != value)
                {
                    Set(ref selectedAvailableStation, value);
                }
            }
        }

        private Station selectedSelectedStation;

        public Station SelectedSelectedStation
        {
            get { return selectedSelectedStation; }
            set
            {
                if (selectedSelectedStation != value)
                {
                    Set(ref selectedSelectedStation, value);
                }
            }
        }

        private CollectionViewSource availableStationsCollection;
        private ObservableCollection<Station> availableStations;

        public ICollectionView AvailableStations
        {
            get { return availableStationsCollection.View; }
        }

        private CollectionViewSource selectedStationsCollection;
        public ObservableCollection<Station> selectedStations;

        public ICollectionView SelectedStations
        {
            get { return selectedStationsCollection.View; }
        }

        #endregion variables

        #region commands

        public RelayCommand AddStation { get; private set; }
        public RelayCommand RemoveStation { get; private set; }
        public RelayCommand ClearStations { get; private set; }
        public RelayCommand StationSelectionViewLoadedCommand { get; set; }

        /// <summary>
        /// Add station to selected stations
        /// </summary>
        private void ExecuteAddStation()
        {
            if (SelectedAvailableStation != null)
            {
                Station newStation = this.SelectedAvailableStation;
                if (!this.SelectedStations.Contains(newStation))
                {
                    this.selectedStations.Add(newStation);
                    this.RemoveStation.RaiseCanExecuteChanged();
                    this.ClearStations.RaiseCanExecuteChanged();
                }
            }
        }

        private bool CanExecuteAddStation()
        {
            /* Maybe not if the simulation is running */
            return true;
        }

        /// <summary>
        /// Remove Station from the selected stations
        /// </summary>
        private void ExecuteRemoveStation()
        {
            if (this.SelectedSelectedStation == null)
                return;
            this.selectedStations.Remove(this.SelectedSelectedStation);
            this.RemoveStation.RaiseCanExecuteChanged();
            this.ClearStations.RaiseCanExecuteChanged();
        }

        private bool CanExecuteRemoveStation()
        {
            return this.selectedStations.Count > 0;
        }

        /// <summary>
        /// Remove all selected stations
        /// </summary>
        private void ExecuteClearStation()
        {
            this.selectedStations.Clear();
            this.ClearStations.RaiseCanExecuteChanged();
            this.RemoveStation.RaiseCanExecuteChanged();
        }

        private bool CanExecuteClearStation()
        {
            return this.selectedStations.Count > 0;
        }

        /// <summary>
        /// Load all Stations from the DB
        /// <para>Called when View loaded</para>
        /// </summary>
        /// <see cref="Wetr.BusinessLogic.StationManager"/>
        private async void ExecuteViewLoaded()
        {
            try
            {
                // new version with api
                var stations = await wetrApiManager.GetStations();

                // old version with sql manager
                // var stations = await Task.Run(() => stationManager.GetAllStations());

                foreach (Station s in stations)
                {
                    availableStations.Add(s);
                }
            }
            catch (BusinessSqlException ex)
            {
                //notifierManager.ShowError(ex.Message);
            }
        }

        public bool CanExecuteViewLoaded()
        {
            return true;
        }

        #endregion commands

        public StationSelectionViewModel()
        {
            stationManager = ManagerLocator.GetStationManagerInstance;

            /* Init properties */
            this.selectedStations = new ObservableCollection<Station>();
            this.availableStations = new ObservableCollection<Station>();

            this.selectedStationsCollection = new CollectionViewSource();
            selectedStationsCollection.Source = this.selectedStations;
            selectedStationsCollection.Filter += FilterSelectedStations;

            this.availableStationsCollection = new CollectionViewSource();
            availableStationsCollection.Source = this.availableStations;
            availableStationsCollection.Filter += FilterAvailableStations;

            /* Add command */
            AddStation = new RelayCommand(
                ExecuteAddStation,
                CanExecuteAddStation);

            /* Remove command */
            RemoveStation = new RelayCommand(
                ExecuteRemoveStation,
                CanExecuteRemoveStation);

            /* Clear command */
            ClearStations = new RelayCommand(
                ExecuteClearStation,
                CanExecuteClearStation);

            StationSelectionViewLoadedCommand = new RelayCommand(
                ExecuteViewLoaded,
                CanExecuteViewLoaded);
        }

        /// <summary>
        /// Filters available stations according to the search term
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterAvailableStations(object sender, FilterEventArgs e)
        {
            Station station = (Station)e.Item;

            if (string.IsNullOrWhiteSpace(this.AvailableStationsFilter) || this.AvailableStationsFilter.Length == 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = station.Name.ToLower().Contains(this.AvailableStationsFilter.ToLower());
            }
        }

        /// <summary>
        /// Filters selected stations according to the search term
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterSelectedStations(object sender, FilterEventArgs e)
        {
            Station station = (Station)e.Item;

            if (string.IsNullOrWhiteSpace(this.SelectedStationsFilter) || this.SelectedStationsFilter.Length == 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = station.Name.ToLower().Contains(this.SelectedStationsFilter.ToLower());
            }
        }

        public void CleanUp()
        {
            base.Cleanup();
            notifierManager.Dispose();
        }
    }
}