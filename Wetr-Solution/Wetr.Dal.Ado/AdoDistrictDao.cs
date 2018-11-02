using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoDistrictDao : IDistrictDao
    {
        private readonly AdoTemplate template;

        public AdoDistrictDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        public Task<IEnumerable<District>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<District> FindByIdAsync(int districtId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<District>> FindByProvinceIdAsync(int provinceId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(District district)
        {
            throw new NotImplementedException();
        }
    }
}
