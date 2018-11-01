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
        Task<Address> FindByIdAsync(int communityId);
        Task<IEnumerable<Address>> FindAllAsync();
        Task<bool> UpdateAsync(Address address);

        Task<IEnumerable<Address>> FindByCommunityIdAsync(int addresscommunityId);

    }
}
