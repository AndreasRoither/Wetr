using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IProvinceDao : IDaoBase<Province>
    {
        Task<IEnumerable<Province>> FindByCountryIdAsync(int provinceId);
    }
}