using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.BusinessLogic.Interface;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class AddressManager : IAddressManager
    {
        private ICountryDao countryDao = null;
        private IProvinceDao provinceDao = null;
        private IDistrictDao districtDao = null;
        private ICommunityDao communityDao = null;
        private IAddressDao addressDao = null;

        public AddressManager(ICountryDao countryDao,
            IProvinceDao provinceDao,
            IDistrictDao districtDao,
            ICommunityDao communityDao,
            IAddressDao addressDao
            )
        {
            this.countryDao = countryDao;
            this.communityDao = communityDao;
            this.provinceDao = provinceDao;
            this.districtDao = districtDao;
            this.addressDao = addressDao;
        }

     

        #region functions

        public async Task<Country> GetCountryForAddressId(int addressId)
        {
            try
            {
                Address a = await addressDao.FindByIdAsync(addressId);
                Community c = await communityDao.FindByIdAsync(a.CommunityId);
                District d = await districtDao.FindByIdAsync(c.DistrictId);
                Province p = await provinceDao.FindByIdAsync(d.ProvinceId);
                return await countryDao.FindByIdAsync(p.CountryId);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<Province> GetProvinceForAddressId(int addressId)
        {
            try
            {
                Address a = await addressDao.FindByIdAsync(addressId);
                Community c = await communityDao.FindByIdAsync(a.CommunityId);
                District d = await districtDao.FindByIdAsync(c.DistrictId);
                return await provinceDao.FindByIdAsync(d.ProvinceId);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<District> GetDistrictForAddressId(int addressId)
        {
            try
            {
                Address a = await addressDao.FindByIdAsync(addressId);
                Community c = await communityDao.FindByIdAsync(a.CommunityId);
                return await districtDao.FindByIdAsync(c.DistrictId);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<Community> GetCommunityForAddressId(int addressId)
        {
            try
            {
                Address a = await addressDao.FindByIdAsync(addressId);
                return await communityDao.FindByIdAsync(a.CommunityId);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            try
            {
                return await countryDao.FindAllAsync();
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<IEnumerable<Province>> GetAllProvinces()
        {
            try
            {
                return await provinceDao.FindAllAsync();
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<IEnumerable<District>> GetAllDistricts()
        {
            try
            {
                return await districtDao.FindAllAsync();
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<IEnumerable<Community>> GetAllCommunities()
        {
            try
            {
                return await communityDao.FindAllAsync();
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<IEnumerable<Address>> GetAllAddresses()
        {
            try
            {
                return await addressDao.FindAllAsync();
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<long> AddNewAddress(Address address)
        {
            if (!CheckAddress(address))
                return -1;
            if (!await addressDao.InsertAsync(address))
                return -1;

            try
            {
                return await addressDao.GetNextId() - 1;
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<string> GetAddressStringByAddressId(int addressId)
        {
            try
            {
                Address a = await addressDao.FindByIdAsync(addressId);
                return a.Location;
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<Address> GetAddressForId(int addressId)
        {
            try
            {
                return await addressDao.FindByIdAsync(addressId);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<bool> UpdateAddress(Address updatedAddress)
        {
            if (!CheckAddress(updatedAddress)) return false;
            try
            {
                return await addressDao.UpdateAsync(updatedAddress);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public bool CheckAddress(Address address)
        {
            if (string.IsNullOrEmpty(address.Location)) return false;
            if (address.AddressId < 0) return false;
            if (address.CommunityId < 0) return false;
            return true;
        }

        #endregion functions
    }
}