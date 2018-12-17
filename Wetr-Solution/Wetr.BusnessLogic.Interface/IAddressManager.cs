using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.BusnessLogic.Interface
{
    public interface IAddressManager
    {
        Task<IEnumerable<Country>> GetAllCountries();

        Task<IEnumerable<Province>> GetAllProvinces();

        Task<IEnumerable<District>> GetAllDistricts();

        Task<IEnumerable<Community>> GetAllCommunities();

        Task<IEnumerable<Address>> GetAllAddresses();

        Task<bool> AddNewAddress(Address address);

        bool CheckAddress(Address address);
    }
}
