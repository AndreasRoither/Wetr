using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface ICommunityDao : IDaoBase<Community>
    {
        Task<IEnumerable<Community>> FindByDistrictIdAsync(int districtId);
    }
}