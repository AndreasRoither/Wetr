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
    public class AdoUnitDao : IUnitDao
    {
        private readonly AdoTemplate template;

        public AdoUnitDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static Unit MapRow(IDataRecord row)
        {
            return new Unit()
            {
                UnitId = (int)row["unitId"],
                Name = (string)row["name"],
            };
        }

        public async Task<bool> DeleteAsync(int unitId)
        {
            return await this.template.ExecuteAsync(
                @"delete from unit" +
                    "where unitId = @unitId",
                new Parameter("@unitId", unitId)) == 1;
        }

        public async Task<IEnumerable<Unit>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from unit", MapRow);
        }

        public async Task<Unit> FindByIdAsync(int unitId)
        {
            var result = await this.template.QueryAsync(
                "select * from unit where unitId = @unitId",
                MapRow,
                new Parameter("@unitId", unitId));

            return result.SingleOrDefault();
        }

        public async Task<bool> InsertAsync(Unit unit)
        {
            // no id since it's set to auto increment
            return await this.template.ExecuteAsync(
                @"insert into unit (name) VALUES" +
                    "(@name)",
                new Parameter("@name", unit.Name)) == 1;
        }

        public async Task<bool> UpdateAsync(Unit unitType)
        {
            return await this.template.ExecuteAsync(
                @"update unit set" +
                    "name = @name," +
                "where unitId = @unitId",
                new Parameter("@unitId", unitType.UnitId),
                new Parameter("@name", unitType.Name)) == 1;
        }
    }
}