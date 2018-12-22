using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.BusinessLogic.Interface
{
    public interface IAddressManager
    {
        Task<String> GetAddressStringByAddressId(int addressId);

        Task<Country> GetCountryForAddressId(int addressId);

        Task<Province> GetProvinceForAddressId(int addressId);

        Task<District> GetDistrictForAddressId(int addressId);

        Task<Community> GetCommunityForAddressId(int addressId);

        Task<bool> UpdateAddress(Address updatedAddress);

        Task<Address> GetAddressForId(int addressId);

        Task<IEnumerable<Country>> GetAllCountries();

        Task<IEnumerable<Province>> GetAllProvinces();

        Task<IEnumerable<District>> GetAllDistricts();

        Task<IEnumerable<Community>> GetAllCommunities();

        Task<long> AddNewAddress(Address address);

        bool CheckAddress(Address address);
    }
}