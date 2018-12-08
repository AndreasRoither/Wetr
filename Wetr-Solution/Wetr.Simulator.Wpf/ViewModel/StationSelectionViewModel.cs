using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;
using Wetr.Simulator.Wpf.Interface;

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

        private String availableStationsFilter;

        public String AvailableStationsFilter
        {
            get { return availableStationsFilter; }
            set
            {
                if (availableStationsFilter != value)
                {
                    Set(ref availableStationsFilter, value);
                    UpdateAvailableStationsFilter();
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
                    UpdateSelectedStationsFilter();
                }
            }
        }

        private void UpdateAvailableStationsFilter()
        {
            this.AvailableStations.Refresh();
        }

        private void UpdateSelectedStationsFilter()
        {
            this.SelectedStations.Refresh();
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
        private ObservableCollection<Station> selectedStations;

        public ICollectionView SelectedStations
        {
            get { return selectedStationsCollection.View; }
        }

        #endregion variables

        #region commands


        public RelayCommand AddStation { get; private set; }
        public RelayCommand RemoveStation { get; private set; }
        public RelayCommand ClearStations { get; private set; }

        /* Add Station Command */
        private void ExecuteAddStation()
        {
            Station newStation = this.SelectedAvailableStation;
            if (!this.SelectedStations.Contains(newStation))
            {
                this.selectedStations.Add(newStation);
                this.RemoveStation.RaiseCanExecuteChanged();
                this.ClearStations.RaiseCanExecuteChanged();
            }
        }

        private bool CanExecuteAddStation()
        {
            /* Maybe not if the simulation is running */
            return true;
        }

        /* Remove Station Command */
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

        /* Clear Stations Command */
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


        #endregion commands


        public StationSelectionViewModel()
        {
            /* Init properties */
            this.selectedStations = new ObservableCollection<Station>();
            this.availableStations = new ObservableCollection<Station>();

            this.selectedStationsCollection = new CollectionViewSource();
            selectedStationsCollection.Source = this.selectedStations;
            selectedStationsCollection.Filter += Filter_SelectedStations;

            this.availableStationsCollection = new CollectionViewSource();
            availableStationsCollection.Source = this.availableStations;
            availableStationsCollection.Filter += Filter_AvailableStations;

            /* Loading stations from db */
            IStationDao stationDao = AdoFactory.Instance.GetStationDao("wetr");
            IEnumerable<Station> stations = stationDao.FindAllAsync().Result;
            foreach (Station s in stations)
            {
                this.availableStations.Add(s);
            }

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

        }

        private void Filter_AvailableStations(object sender, FilterEventArgs e)
        {
            Station station = (Station)e.Item;

            if (string.IsNullOrWhiteSpace(this.AvailableStationsFilter) || this.AvailableStationsFilter.Length == 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = station.Name.Contains(this.AvailableStationsFilter);
            }
        }

        private void Filter_SelectedStations(object sender, FilterEventArgs e)
        {
            Station station = (Station)e.Item;

            if (string.IsNullOrWhiteSpace(this.SelectedStationsFilter) || this.SelectedStationsFilter.Length == 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = station.Name.Contains(this.SelectedStationsFilter);
            }
        }

        public void CleanUp()
        {
            throw new System.NotImplementedException();
        }
    }
}