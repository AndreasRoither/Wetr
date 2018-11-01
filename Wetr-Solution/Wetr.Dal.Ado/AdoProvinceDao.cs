using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoProvinceDao : IProvinceDao
    {
        public Task<IEnumerable<Province>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Province>> FindByCountryIdAsync(int provinceId)
        {
            throw new NotImplementedException();
        }

        public Task<Province> FindByIdAsync(int provinceId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Province province)
        {
            throw new NotImplementedException();
        }
    }
}
