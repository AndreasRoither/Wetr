using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class AddressManager
    {
        ICountryDao countryDao;
        IProvinceDao provinceDao;
        IDistrictDao districtDao;
        ICommunityDao communityDao;
        IAddressDao addressDao;
        
        public AddressManager(string databaseName)
        {
            countryDao = AdoFactory.Instance.GetCountryDao(databaseName);
            provinceDao = AdoFactory.Instance.GetProvinceDao(databaseName);
            districtDao = AdoFactory.Instance.GetDistrictDao(databaseName);
            communityDao = AdoFactory.Instance.GetCommunityDao(databaseName);
            addressDao = AdoFactory.Instance.GetAddressDao(databaseName);
        }

        #region functions

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await countryDao.FindAllAsync();
        }

        public async Task<IEnumerable<Province>> GetAllProvinces()
        {
            return await provinceDao.FindAllAsync();
        }

        public async Task<IEnumerable<District>> GetAllDistricts()
        {
            return await districtDao.FindAllAsync();
        }

        public async Task<IEnumerable<Community>> GetAllCommunities()
        {
            return await communityDao.FindAllAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAddresses()
        {
            return await addressDao.FindAllAsync();
        }

        public async Task<bool> AddNewAddress(Address address)
        {
            if (!CheckAddress(address)) return false;
            return await addressDao.InsertAsync(address);
        }

        public bool CheckAddress(Address address)
        {
            if (string.IsNullOrEmpty(address.Zip)) return false;
            if (string.IsNullOrEmpty(address.Location)) return false;
            if (address.AddressId < 0) return false;
            if (address.CommunityId < 0) return false;
            return true;
        }

        #endregion functions
    }
}