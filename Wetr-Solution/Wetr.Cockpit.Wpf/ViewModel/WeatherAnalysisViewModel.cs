using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using Wetr.BusinessLogic;
using Wetr.Cockpit.Wpf.Utility;
using Wetr.Domain;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// ViewModel for WeatherAnalysisView
    /// </summary>
    /// <seealso cref="Wetr.Cockpit.Wpf.Views.WeatherAnalysisView"/>
    public class WeatherAnalysisViewModel : ViewModelBase
    {
        #region variables

        private LoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<LoginViewModel>();
        private NotifierManager notifierManager = new NotifierManager();
        private StationManager stationManager;
        private AddressManager addressManager;

        /* Stations */
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

        private Station selectedStationAggregate;

        public Station SelectedStationAggregate
        {
            get { return selectedStationAggregate; }
            set
            {
                Set(ref selectedStationAggregate, value);
            }
        }

        private Station selectedStationAvailable;

        public Station SelectedStationAvailable
        {
            get { return selectedStationAvailable; }
            set
            {
                Set(ref selectedStationAvailable, value);
            }
        }

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

        /* StationType */
        private List<StationType> stationTypes;

        public List<StationType> StationTypes
        {
            get { return stationTypes; }
            set
            {
                Set(ref stationTypes, value);
            }
        }

        /* Countries */
        private List<Country> countries;

        public List<Country> Countries
        {
            get { return countries; }
            set
            {
                Set(ref countries, value);
            }
        }

        private Country selectedCountry;

        public Country SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                Set(ref selectedCountry, value);
            }
        }

        /* Provinces */
        private List<Province> provinces;

        public List<Province> Provinces
        {
            get { return provinces; }
            set
            {
                Set(ref provinces, value);
            }
        }

        private Province selectedProvince;

        public Province SelectedProvince
        {
            get { return selectedProvince; }
            set
            {
                Set(ref selectedProvince, value);
            }
        }

        /* District */
        private List<District> districts;

        public List<District> Districts
        {
            get { return districts; }
            set
            {
                Set(ref districts, value);
            }
        }

        private District selectedDistrict;

        public District SelectedDistrict
        {
            get { return selectedDistrict; }
            set
            {
                Set(ref selectedDistrict, value);
            }
        }

        /* Community */
        private List<Community> communities;

        public List<Community> Communities
        {
            get { return communities; }
            set
            {
                Set(ref communities, value);
            }
        }

        private Community selectedCommunity;

        public Community SelectedCommunity
        {
            get { return selectedCommunity; }
            set
            {
                Set(ref selectedCommunity, value);
            }
        }

        /* Long */
        private decimal longitude;

        public decimal Longitude
        {
            get { return longitude; }
            set
            {
                Set(ref longitude, value);
            }
        }

        /* Long */
        private decimal latitude;

        public decimal Latitude
        {
            get { return latitude; }
            set
            {
                Set(ref latitude, value);
            }
        }

        private int radius;

        public int Radius
        {
            get { return radius; }
            set
            {
                Set(ref radius, value);
            }
        }

        #endregion variables

        public WeatherAnalysisViewModel()
        {
            this.selectedStations = new ObservableCollection<Station>();
            this.availableStations = new ObservableCollection<Station>();

            this.selectedStationsCollection = new CollectionViewSource();
            selectedStationsCollection.Source = this.selectedStations;
            selectedStationsCollection.Filter += FilterSelectedStations;

            this.availableStationsCollection = new CollectionViewSource();
            availableStationsCollection.Source = this.availableStations;
            availableStationsCollection.Filter += FilterAvailableStations;

            stationManager = ManagerLocator.GetStationManagerInstance;
            addressManager = ManagerLocator.GetAddressManagerInstance;

            AddCommand = new RelayCommand(
                ExecuteAddCommand,
                CanExecuteAddCommand);

            RemoveCommand = new RelayCommand(
                ExecuteRemoveCommand,
                CanExecuteRemoveCommand);
        }

        #region functions

        public RelayCommand AddCommand { get; }
        public RelayCommand RemoveCommand { get; }

        public bool CanExecuteAddCommand()
        {
            return true;
        }

        public void ExecuteAddCommand()
        {
            if (SelectedStationAvailable != null && !SelectedStations.Contains(SelectedStationAvailable))
            {
                selectedStations.Add(SelectedStationAvailable);
            }
        }

        public bool CanExecuteRemoveCommand()
        {
            return true;
        }

        public void ExecuteRemoveCommand()
        {
            if (SelectedStationAggregate != null)
            {
                selectedStations.Remove(SelectedStationAggregate);
            }
        }

        public async Task Load()
        {
            try
            {
                this.availableStations = new ObservableCollection<Station>((await stationManager.GetAllStations()).ToList());
                availableStationsCollection.Source = this.availableStations;
                this.Countries = (await addressManager.GetAllCountries()).ToList();
                this.Provinces = (await addressManager.GetAllProvinces()).ToList();
                this.Districts = (await addressManager.GetAllDistricts()).ToList();
                this.Communities = (await addressManager.GetAllCommunities()).ToList();
                this.StationTypes = (await stationManager.GetStationTypes()).ToList();
                RaisePropertyChanged(nameof(AvailableStations));
            }
            catch (BusinessSqlException ex)
            {
                notifierManager.ShowError(ex.Message);
            }
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

        public override void Cleanup()
        {
            base.Cleanup();
            notifierManager.Dispose();
        }

        #endregion functions
    }
}