using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoStationTypeDao : IStationTypeDao
    {
        private readonly AdoTemplate template;

        public AdoStationTypeDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static StationType MapRow(IDataRecord row)
        {
            return new StationType()
            {
                StationTypeId = (int)row["stationTypeId"],
                Name = (string)row["name"],
            };
        }

        public async Task<bool> DeleteAsync(int stationTypeId)
        {
            return await this.template.ExecuteAsync(
                @"delete from stationType" +
                    "where stationTypeId = @stationTypeId",
                new Parameter("@stationTypeId", stationTypeId)) == 1;
        }

        public async Task<IEnumerable<StationType>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from stationType", MapRow);
        }

        public async Task<StationType> FindByIdAsync(int stationTypeId)
        {
            var result = await this.template.QueryAsync(
               "select * from stationType where stationTypeId = @stationTypeId",
               MapRow,
               new Parameter("@stationTypeId", stationTypeId));

            return result.SingleOrDefault();
        }

        public async Task<bool> InsertAsync(StationType stationType)
        {
            // no id since it's set to auto increment
            return await this.template.ExecuteAsync(
                @"insert into stationType (name) VALUES" +
                    "(@name)",
                new Parameter("@name", stationType.Name)) == 1;
        }

        public async Task<bool> UpdateAsync(StationType stationType)
        {
            return await this.template.ExecuteAsync(
                @"update stationType set" +
                    "name = @name," +
                "where stationTypeId = @stationTypeId",
                new Parameter("@stationTypeId", stationType.StationTypeId),
                new Parameter("@name", stationType.Name)) == 1;
        }
    }
}