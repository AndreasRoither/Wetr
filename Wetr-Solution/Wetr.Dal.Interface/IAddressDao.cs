using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IAddressDao : IDaoBase<Address>
    {
        Task<IEnumerable<Address>> FindByCommunityIdAsync(int communityId);

        Task<long> GetNextId();
    }
}