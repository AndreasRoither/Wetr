using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoCountryDao : ICountryDao
    {
        private readonly AdoTemplate template;

        public AdoCountryDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static Country MapRow(IDataRecord row)
        {
            return new Country()
            {
                CountryId = (int)row["countryId"],
                Name = (string)row["name"],
            };
        }

        public Task<bool> DeleteAsync(int countryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Country>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Country> FindByIdAsync(int countryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
