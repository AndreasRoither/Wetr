using Common.Dal.Ado;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<bool> DeleteAsync(int provinceId)
        {
            return await this.template.ExecuteAsync(
                @"delete from province" +
                    "where provinceId = @provinceId",
                new Parameter("@provinceId", provinceId)) == 1;
        }

        public async Task<IEnumerable<Province>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from province", MapRow);
        }

        public async Task<Province> FindByIdAsync(int provinceId)
        {
            var result = await this.template.QueryAsync(
                "select * from province where provinceId = @provinceId",
                MapRow,
                new Parameter("@provinceId", provinceId));

            return result.SingleOrDefault();
        }

        public async Task<IEnumerable<Province>> FindByCountryIdAsync(int countryId)
        {
            var result = await this.template.QueryAsync(
                "select * from province where countryId = @countryId",
                MapRow,
                new Parameter("@countryId", countryId));

            return result;
        }

        public async Task<bool> InsertAsync(Province province)
        {
            // no id since it's set to auto increment
            return await this.template.ExecuteAsync(
                @"insert into province (name, countryId) VALUES" +
                    "(@name, @countryId)",
                new Parameter("@name", province.Name),
                new Parameter("@countryId", province.CountryId)) == 1;
        }

        public async Task<bool> UpdateAsync(Province province)
        {
            return await this.template.ExecuteAsync(
                @"update province set" +
                    "name = @name," +
                    "countryId = @countryId" +
                "where provinceId = @provinceId",
                new Parameter("@provinceId", province.ProvinceId),
                new Parameter("@name", province.Name),
                new Parameter("@countryId", province.CountryId)) == 1;
        }
    }
}