using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wetr.BusinessLogic;
using Wetr.Cockpit.Wpf.Utility;
using Wetr.Domain;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// ViewModel for the WeatherStationManagementView
    /// </summary>
    /// <seealso cref="Wetr.Cockpit.Wpf.Views.WeatherStationManagementView"/>
    public class WeatherStationManagementViewModel : ViewModelBase
    {
        #region variables

        private StationManager stationManager;
        private AddressManager addressManager;
        private LoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<LoginViewModel>();
        private NotifierManager notifierManager = new NotifierManager();

        /* Stations */
        private List<Station> stations;

        public List<Station> Stations
        {
            get { return stations; }
            set
            {
                Set(ref stations, value);
            }
        }

        private Station selectedStation;

        public Station SelectedStation
        {
            get { return selectedStation; }
            set
            {
                Set(ref selectedStation, value);

                if (value == null)
                    return;

                UpdateDropdowns();
                UpdateFields();
                DeleteStation.RaiseCanExecuteChanged();
            }
        }

        /* Address */
        private String addressString;

        public String AddressString
        {
            get { return addressString; }
            set
            {
                Set(ref addressString, value);
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

        /* Staiton Name */
        private String stationName;

        public String StationName
        {
            get { return stationName; }
            set
            {
                Set(ref stationName, value);
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

        private StationType selectedStationType;

        public StationType SelectedStationType
        {
            get { return selectedStationType; }
            set
            {
                Set(ref selectedStationType, value);
            }
        }

        #endregion variables

        public WeatherStationManagementViewModel()
        {
            this.stationManager = ManagerLocator.GetStationManagerInstance;
            this.addressManager = ManagerLocator.GetAddressManagerInstance;

            AddNewStation = new RelayCommand(
                ExecuteAddNewStation,
                CanExecuteAddNewStation
            );

            SaveStation = new RelayCommand(
                ExecuteSaveStation,
                CanExecuteSaveStation
            );

            DeleteStation = new RelayCommand(
                ExecuteDeleteStation,
                CanExecuteDeleteStation
            );
        }

        #region commands

        public RelayCommand AddNewStation { get; private set; }
        public RelayCommand SaveStation { get; private set; }
        public RelayCommand DeleteStation { get; private set; }

        private bool CanExecuteDeleteStation()
        {
            if (this.SelectedStation == null)
                return false;

            try
            {
                return stationManager.HasMeasurements(this.SelectedStation).Result;
            }
            catch (BusinessSqlException ex)
            {
                notifierManager.ShowError(ex.Message);
                return false;
            }
        }

        private async void ExecuteDeleteStation()
        {
            bool result = false;
            try
            {
                result = await stationManager.DeleteStation(this.selectedStation);
            }
            catch (BusinessSqlException ex)
            {
                notifierManager.ShowError(ex.Message);
            }

            if (!result)
            {
                notifierManager.ShowError("Could not delete Station");
            }
            else
            {
                notifierManager.ShowSuccess("Station deleted");
            }

            await LoadStations();
            this.SelectedStation = this.Stations.First();
            RaisePropertyChanged(nameof(SelectedStation));
            RaisePropertyChanged(nameof(Stations));
        }

        private bool CanExecuteSaveStation()
        {
            return true;
        }

        private async void ExecuteSaveStation()
        {
            Station stationToEdit = new Station();
            stationToEdit.StationId = this.selectedStation.StationId;
            stationToEdit.Name = this.StationName;
            stationToEdit.Latitude = this.Latitude;
            stationToEdit.Longitude = this.Longitude;
            stationToEdit.StationTypeId = this.SelectedStationType.StationTypeId;
            stationToEdit.AddressId = this.selectedStation.AddressId;
            stationToEdit.UserId = this.selectedStation.UserId;

            bool result = false;

            try
            {
                Address a = await addressManager.GetAddressForId(stationToEdit.AddressId);
                a.CommunityId = this.selectedCommunity.CommunityId;
                a.Location = this.addressString;
                result = await addressManager.UpdateAddress(a);
            }
            catch (BusinessSqlException ex)
            {
                notifierManager.ShowError(ex.Message);
            }

            if (!result)
            {
                notifierManager.ShowError("Failed to update Station!");
            }
            else
            {
                notifierManager.ShowSuccess("Update successful");
            }

            try
            {
                result = await stationManager.UpdateStation(stationToEdit);
            }
            catch (BusinessSqlException ex)
            {
                notifierManager.ShowError(ex.Message);
            }

            if (!result)
            {
                notifierManager.ShowError("Failed to update Station!");
            }

            /* Reload station data */

            await LoadStations();
            this.SelectedStation = this.Stations.Where(s => s.StationId == stationToEdit.StationId).Single();
            RaisePropertyChanged(nameof(SelectedStation));
            RaisePropertyChanged(nameof(Stations));
        }

        private bool CanExecuteAddNewStation()
        {
            return true;
        }

        private async void ExecuteAddNewStation()
        {
            Station newStation = new Station();

            newStation.Name = this.StationName;
            newStation.Latitude = this.Latitude;
            newStation.Longitude = this.Longitude;
            newStation.StationTypeId = this.SelectedStationType.StationTypeId;
            newStation.UserId = this.loginViewModel.loggedInUser.UserId;

            Address a = new Address();
            a.CommunityId = this.selectedCommunity.CommunityId;
            a.Location = this.addressString;

            int newId = -1;
            try
            {
                newId = unchecked((int)await addressManager.AddNewAddress(a));
            }
            catch (BusinessSqlException ex)
            {
                notifierManager.ShowError(ex.Message);
                return;
            }

            if (newId == -1)
            {
                notifierManager.ShowSuccess("Failed to create address");
            }
            else
            {
                notifierManager.ShowSuccess("Address creation successful");
            }

            newStation.AddressId = newId;

            bool result = false;
            try
            {
                result = await stationManager.AddStation(newStation);
            }
            catch (BusinessSqlException ex)
            {
                notifierManager.ShowError(ex.Message);
                return;
            }

            if (!result)
            {
                notifierManager.ShowError("Failed to create station");
            }
            else
            {
                notifierManager.ShowSuccess("Station creation successful");
            }

            /* Reload station data */
            await LoadStations();
            this.SelectedStation = this.Stations.Last();
            RaisePropertyChanged(nameof(SelectedStation));
            RaisePropertyChanged(nameof(Stations));
        }

        #endregion commands

        #region functions

        private async void UpdateFields()
        {
            this.Longitude = SelectedStation.Longitude;
            this.Latitude = SelectedStation.Latitude;
            this.StationName = SelectedStation.Name;

            try
            {
                this.AddressString = await addressManager.GetAddressStringByAddressId(SelectedStation.AddressId);
            }
            catch (BusinessSqlException ex)
            {
                notifierManager.ShowError(ex.Message);
                return;
            }

            RaisePropertyChanged(nameof(Longitude));
            RaisePropertyChanged(nameof(Latitude));
            RaisePropertyChanged(nameof(StationName));
            RaisePropertyChanged(nameof(AddressString));
        }

        public async void UpdateDropdowns()
        {
            try
            {
                this.SelectedCountry = await this.addressManager.GetCountryForAddressId(selectedStation.AddressId);
                this.SelectedProvince = await this.addressManager.GetProvinceForAddressId(selectedStation.AddressId);
                this.SelectedDistrict = await this.addressManager.GetDistrictForAddressId(selectedStation.AddressId);
                this.SelectedCommunity = await this.addressManager.GetCommunityForAddressId(selectedStation.AddressId);
                this.SelectedStationType = await this.stationManager.GetStationTypesForStationTypeId(selectedStation.StationTypeId);
            }
            catch (BusinessSqlException ex)
            {
                notifierManager.ShowError(ex.Message);
                return;
            }

            RaisePropertyChanged(nameof(SelectedCountry));
            RaisePropertyChanged(nameof(SelectedCommunity));
            RaisePropertyChanged(nameof(SelectedProvince));
            RaisePropertyChanged(nameof(SelectedDistrict));
            RaisePropertyChanged(nameof(SelectedStationType));
        }

        public async Task LoadStations()
        {
            try
            {
                this.Stations = (await stationManager.GetStationsForUser(loginViewModel.loggedInUser.UserId)).ToList();
            }
            catch (BusinessSqlException ex)
            {
                notifierManager.ShowError(ex.Message);
            }
        }

        public async Task InitDropdowns()
        {
            try
            {
                this.Countries = (await addressManager.GetAllCountries()).ToList();
                this.Provinces = (await addressManager.GetAllProvinces()).ToList();
                this.Districts = (await addressManager.GetAllDistricts()).ToList();
                this.Communities = (await addressManager.GetAllCommunities()).ToList();
                this.StationTypes = (await stationManager.GetStationTypes()).ToList();
            }
            catch (BusinessSqlException ex)
            {
                notifierManager.ShowError(ex.Message);
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