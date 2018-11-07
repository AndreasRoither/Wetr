using Common.Dal.Ado;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<bool> DeleteAsync(int countryId)
        {
            return await this.template.ExecuteAsync(
                @"delete from country where countryId = @countryId",
                new Parameter("@countryId", countryId)) == 1;
        }

        public async Task<IEnumerable<Country>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from country", MapRow);
        }

        public async Task<Country> FindByIdAsync(int countryId)
        {
            var result = await this.template.QueryAsync(
                "select * from country where countryId = @countryId",
                MapRow,
                new Parameter("@countryId", countryId));

            return result.SingleOrDefault();
        }

        public async Task<bool> InsertAsync(Country country)
        {
            return await this.template.ExecuteAsync(
                @"insert into country (countryId, name) VALUES (@countryId, @name)",
                new Parameter("@countryId", country.CountryId),
                new Parameter("@name", country.Name)) == 1;
        }

        public async Task<bool> UpdateAsync(Country country)
        {
            return await this.template.ExecuteAsync(
                @"update country (name) set name = @name where countryId = @countryId",
                new Parameter("@countryId", country.CountryId),
                new Parameter("@name", country.Name)) == 1;
        }
    }
}