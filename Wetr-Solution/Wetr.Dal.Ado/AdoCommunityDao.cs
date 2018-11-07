using Common.Dal.Ado;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoCommunityDao : ICommunityDao
    {
        private readonly AdoTemplate template;

        public AdoCommunityDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static Community MapRow(IDataRecord row)
        {
            return new Community()
            {
                CommunityId = (int)row["communityId"],
                DistrictId = (int)row["districtId"],
                Name = (string)row["name"],
            };
        }

        public async Task<bool> DeleteAsync(int communityId)
        {
            return await this.template.ExecuteAsync(
                @"delete from community" +
                    "where communityId = @communityId",
                new Parameter("@communityId", communityId)) == 1;
        }

        public async Task<IEnumerable<Community>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from community", MapRow);
        }

        public async Task<Community> FindByIdAsync(int communityId)
        {
            var result = await this.template.QueryAsync(
                "select * from community where communityId = @communityId",
                MapRow,
                new Parameter("@communityId", communityId));

            return result.SingleOrDefault();
        }

        public async Task<IEnumerable<Community>> FindByDistrictIdAsync(int districtId)
        {
            var result = await this.template.QueryAsync(
                "select * from community where districtId = @districtId",
                MapRow,
                new Parameter("@districtId", districtId));

            return result;
        }

        public async Task<bool> InsertAsync(Community community)
        {
            // no id since it's set to auto increment
            return await this.template.ExecuteAsync(
                @"insert into community (communityId, name, districtId) VALUES" +
                    "(@communityId, @name, @districtId)",
                new Parameter("@communityId", community.CommunityId),
                new Parameter("@name", community.Name),
                new Parameter("@districtId", community.DistrictId)) == 1;
        }

        public async Task<bool> UpdateAsync(Community community)
        {
            return await this.template.ExecuteAsync(
                @"update community set" +
                     "name = @name, " +
                     "districtId = @districtId" +
                 "where communityId = @communityId",
                 new Parameter("@communityId", community.CommunityId),
                 new Parameter("@name", community.Name),
                 new Parameter("@districtId", community.DistrictId)) == 1;
        }
    }
}