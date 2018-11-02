using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface ICommunityDao
    {
        Task<Community> FindByIdAsync(int communityId);
        Task<IEnumerable<Community>> FindAllAsync();
        Task<bool> UpdateAsync(Community community);
        Task<bool> DeleteAsync(int communityId);

        Task<IEnumerable<Community>> FindByDistrictIdAsync(int districtId);

    }
}
