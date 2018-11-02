using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IDistrictDao : IDaoBase<District>
    {
        Task<IEnumerable<District>> FindByProvinceIdAsync(int provinceId);
    }
}