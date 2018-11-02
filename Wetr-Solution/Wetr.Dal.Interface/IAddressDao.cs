using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IAddressDao
    {
        Task<Address> FindByIdAsync(int addressId);
        Task<IEnumerable<Address>> FindAllAsync();
        Task<bool> UpdateAsync(Address address);
        Task<bool> DeleteAsync(int addressId);

        Task<IEnumerable<Address>> FindByCommunityIdAsync(int addresscommunityId);

    }
}
