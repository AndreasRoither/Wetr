using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IDistrictDao
    {
        Task<District> FindByIdAsync(int districtId);
        Task<IEnumerable<District>> FindAllAsync();
        Task<bool> UpdateAsync(District district);

        Task<IEnumerable<District>> FindByProvinceIdAsync(int provinceId);

    }
}
