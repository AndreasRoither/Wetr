using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoAddressDao : IAddressDao
    {
        public Task<IEnumerable<Address>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Address>> FindByCommunityIdAsync(int addresscommunityId)
        {
            throw new NotImplementedException();
        }

        public Task<Address> FindByIdAsync(int communityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
