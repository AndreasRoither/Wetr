using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.BusnessLogic.Interface;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class AddressManager : IAddressManager
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

        public async Task<Country> GetCountryForAddressId(int addressId)
        {
            Address a = await addressDao.FindByIdAsync(addressId);
            Community c = await communityDao.FindByIdAsync(a.CommunityId);
            District d = await districtDao.FindByIdAsync(c.DistrictId);
            Province p = await provinceDao.FindByIdAsync(d.ProvinceId);
            return await countryDao.FindByIdAsync(p.CountryId);
        }

        public async Task<Province> GetProvinceForAddressId(int addressId)
        {
            Address a = await addressDao.FindByIdAsync(addressId);
            Community c = await communityDao.FindByIdAsync(a.CommunityId);
            District d = await districtDao.FindByIdAsync(c.DistrictId);
            return await provinceDao.FindByIdAsync(d.ProvinceId);
        }

        public async Task<District> GetDistrictForAddressId(int addressId)
        {
            Address a = await addressDao.FindByIdAsync(addressId);
            Community c = await communityDao.FindByIdAsync(a.CommunityId);
            return await districtDao.FindByIdAsync(c.DistrictId);
        }

        public async Task<Community> GetCommunityForAddressId(int addressId)
        {
            Address a = await addressDao.FindByIdAsync(addressId);
            return await communityDao.FindByIdAsync(a.CommunityId);
        }

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

        public async Task<long> AddNewAddress(Address address)
        {
            if (!CheckAddress(address))
                return -1;
            if (!await addressDao.InsertAsync(address))
                return -1;
            return await addressDao.GetNextId() - 1;

        }

        public bool CheckAddress(Address address)
        {
            if (string.IsNullOrEmpty(address.Location)) return false;
            if (address.AddressId < 0) return false;
            if (address.CommunityId < 0) return false;
            return true;
        }

        public async Task<string> GetAddressStringByAddressId(int addressId)
        {
            Address a = await addressDao.FindByIdAsync(addressId);
            return a.Location;
        }

        public async Task<Address> GetAddressForId(int addressId)
        {
            return await addressDao.FindByIdAsync(addressId);
        }

        public async Task<bool> UpdateAddress(Address updatedAddress)
        {
            if (!CheckAddress(updatedAddress)) return false;
            return await addressDao.UpdateAsync(updatedAddress);
        }

        #endregion functions
    }
}