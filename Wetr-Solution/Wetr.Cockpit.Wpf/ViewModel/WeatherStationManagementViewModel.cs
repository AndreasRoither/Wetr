using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wetr.BusinessLogic;
using Wetr.Cockpit.Wpf.Interface;
using Wetr.Domain;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    public class WeatherStationManagementViewModel : ViewModelBase, IWetrViewModelBase
    {

        private StationManager stationManager;
        private AddressManager addressManager;
        private LoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<LoginViewModel>();

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

            }
        }

        private async void UpdateFields()
        {
            this.Longitude = SelectedStation.Longitude;
            this.Latitude = SelectedStation.Latitude;
            this.StationName = SelectedStation.Name;
            this.AddressString = await addressManager.GetAddressStringByAddressId(SelectedStation.AddressId);

            RaisePropertyChanged(nameof(Longitude));
            RaisePropertyChanged(nameof(Latitude));
            RaisePropertyChanged(nameof(StationName));
            RaisePropertyChanged(nameof(AddressString));


        }

        private async void UpdateDropdowns()
        {
            this.SelectedCountry = await this.addressManager.GetCountryForAddressId(selectedStation.AddressId);
            this.SelectedProvince = await this.addressManager.GetProvinceForAddressId(selectedStation.AddressId);
            this.SelectedDistrict = await this.addressManager.GetDistrictForAddressId(selectedStation.AddressId);
            this.SelectedCommunity = await this.addressManager.GetCommunityForAddressId(selectedStation.AddressId);
            this.SelectedStationType = await this.stationManager.GetStationTypesForStationTypeId(selectedStation.StationTypeId);

            RaisePropertyChanged(nameof(SelectedCountry));
            RaisePropertyChanged(nameof(SelectedCommunity));
            RaisePropertyChanged(nameof(SelectedProvince));
            RaisePropertyChanged(nameof(SelectedDistrict));
            RaisePropertyChanged(nameof(SelectedStationType));

        }

        /* Address */
        private String addressString;

        public String AddressString
        {
            get { return addressString; }
            set { addressString = value;
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
                longitude = value;
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
                latitude = value;
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
                stationName = value;
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
                countries = value;
                Set(ref countries, value);
            }
        }

        private Country selectedCountry;
        public Country SelectedCountry
        {
            get { return selectedCountry; }
            set { selectedCountry = value;
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
                provinces = value;
                Set(ref provinces, value);
            }
        }

        private Province selectedProvince;
        public Province SelectedProvince
        {
            get { return selectedProvince; }
            set
            {
                selectedProvince = value;
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
                districts = value;
                Set(ref districts, value);
            }
        }

        private District selectedDistrict;
        public District SelectedDistrict
        {
            get { return selectedDistrict; }
            set
            {
                selectedDistrict = value;
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
                communities = value;
                Set(ref communities, value);
            }
        }

        private Community selectedCommunity;
        public Community SelectedCommunity
        {
            get { return selectedCommunity; }
            set
            {
                selectedCommunity = value;
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
                stationTypes = value;
                Set(ref stationTypes, value);
            }
        }

        private StationType selectedStationType;
        public StationType SelectedStationType
        {
            get { return selectedStationType; }
            set
            {
                selectedStationType = value;
                Set(ref selectedStationType, value);
            }
        }

        public RelayCommand AddNewStation { get; private set; }
        public RelayCommand SaveStation { get; private set; }


        public WeatherStationManagementViewModel()
        {
            this.stationManager = ManagerLocator.GetStationManagerInstance;
            this.addressManager = ManagerLocator.GetAddressManagerInstance;

            InitDropdowns();
            LoadStations();

            AddNewStation = new RelayCommand(
                ExecuteAddNewStation,
                CanExecuteAddNewStation
            );

            SaveStation = new RelayCommand(
                ExecuteSaveStation,
                CanExecuteSaveStation
            );

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

            Address a = await addressManager.GetAddressForId(stationToEdit.AddressId);
            a.CommunityId = this.selectedCommunity.CommunityId;
            a.Location = this.addressString;

            bool result = await addressManager.UpdateAddress(a);
            if (!result)
            {
                //TODO: Error message;
                Console.WriteLine("Failed to update address!");
            }
            // TODO Update Address


            result = await stationManager.UpdateStation(stationToEdit);
            if (!result)
            {
                //TODO: Error message;
                Console.WriteLine("Failed to update station!");
            }

            /* Reload station data */

            LoadStations();
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

            int newId = unchecked((int)await addressManager.AddNewAddress(a));
            if (newId == -1)
            {
                //TODO: Error message;
                Console.WriteLine("Failed to add address!");
            }

            newStation.AddressId = newId;

            bool result = await stationManager.AddStation(newStation);
            if (!result)
            {
                //TODO: Error message;
                Console.WriteLine("Failed to add station!");
            }

            /* Reload station data */

            LoadStations();
            this.SelectedStation = this.Stations.Last();
            RaisePropertyChanged(nameof(SelectedStation));
            RaisePropertyChanged(nameof(Stations));
        }

        private async void LoadStations()
        {
            this.Stations = (await stationManager.GetStationsForUser(loginViewModel.loggedInUser.UserId)).ToList();
        }

        private async void InitDropdowns()
        {
            this.Countries = (await addressManager.GetAllCountries()).ToList();
            this.Provinces = (await addressManager.GetAllProvinces()).ToList();
            this.Districts = (await addressManager.GetAllDistricts()).ToList();
            this.Communities = (await addressManager.GetAllCommunities()).ToList();
            this.StationTypes = (await stationManager.GetStationTypes()).ToList();

        }

        public void CleanUp()
        {
            throw new NotImplementedException();
        }
    }
}
