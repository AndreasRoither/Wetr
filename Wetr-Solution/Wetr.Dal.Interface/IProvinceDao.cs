using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IProvinceDao
    {
        Task<Province> FindByIdAsync(int provinceId);
        Task<IEnumerable<Province>> FindAllAsync();
        Task<bool> UpdateAsync(Province province);
        Task<bool> DeleteAsync(int provinceId);

        Task<IEnumerable<Province>> FindByCountryIdAsync(int provinceId);

    }
}
