using Common.Dal.Ado;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        private static District MapRow(IDataRecord row)
        {
            return new District()
            {
                DistrictId = (int)row["districtId"],
                ProvinceId = (int)row["provinceId"],
                Name = (string)row["name"],
            };
        }

        public async Task<bool> DeleteAsync(int districtId)
        {
            return await this.template.ExecuteAsync(
                @"delete from district where districtId = @districtId",
                new Parameter("@districtId", districtId)) == 1;
        }

        public async Task<IEnumerable<District>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from district", MapRow);
        }

        public async Task<District> FindByIdAsync(int districtId)
        {
            var result = await this.template.QueryAsync(
                "select * from district where districtId = @districtId",
                MapRow,
                new Parameter("@districtId", districtId));

            return result.SingleOrDefault();
        }

        public async Task<IEnumerable<District>> FindByProvinceIdAsync(int provinceId)
        {
            var result = await this.template.QueryAsync(
                "select * from district where provinceId = @provinceId",
                MapRow,
                new Parameter("@provinceId", provinceId));

            return result;
        }

        public async Task<bool> InsertAsync(District district)
        {
            return await this.template.ExecuteAsync(
                @"insert into district (districtId, name, provinceId) VALUES (@districtId, @name, @provinceId)",
                new Parameter("@districtId", district.DistrictId),
                new Parameter("@name", district.Name),
                new Parameter("@provinceId", district.ProvinceId)) == 1;
        }

        public async Task<bool> UpdateAsync(District district)
        {
            return await this.template.ExecuteAsync(
                @"update district set name = @name, provinceId = @provinceId where districtId = @districtId",
                new Parameter("@districtId", district.DistrictId),
                new Parameter("@name", district.Name),
                new Parameter("@provinceId", district.ProvinceId)) == 1;
        }
    }
}