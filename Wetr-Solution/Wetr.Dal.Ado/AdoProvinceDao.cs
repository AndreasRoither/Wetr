using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoProvinceDao : IProvinceDao
    {
        private readonly AdoTemplate template;

        public AdoProvinceDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static Province MapRow(IDataRecord row)
        {
            return new Province()
            {
                ProvinceId = (int)row["ProvinceId"],
                Name = (string)row["name"],
                CountryId = (int)row["countryId"],
            };
        }

        public Task<bool> DeleteAsync(int provinceId)
        {
            throw new NotImplementedException();
        }

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

        public Task<bool> InsertAsync(Province obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Province province)
        {
            throw new NotImplementedException();
        }
    }
}