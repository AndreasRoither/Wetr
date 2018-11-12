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
    public class AdoMeasurementTypeDao : IMeasurementTypeDao
    {
        private readonly AdoTemplate template;

        public AdoMeasurementTypeDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static MeasurementType MapRow(IDataRecord row)
        {
            return new MeasurementType()
            {
                MeasurementTypeId = (int)row["measurementTypeId"],
                Name = (string)row["name"],
            };
        }

        public async Task<bool> DeleteAsync(int measurementTypeId)
        {
            return await this.template.ExecuteAsync(
                @"delete from measurementType where measurementTypeId = @measurementTypeId",
                new Parameter("@measurementTypeId", measurementTypeId)) == 1;
        }

        public async Task<IEnumerable<MeasurementType>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from measurementType", MapRow);
        }

        public async Task<MeasurementType> FindByIdAsync(int measurementTypeId)
        {
            var result = await this.template.QueryAsync(
                "select * from measurementType where measurementTypeId = @measurementTypeId",
                MapRow,
                new Parameter("@measurementTypeId", measurementTypeId));

            return result.SingleOrDefault();
        }


        public async Task<bool> InsertAsync(MeasurementType measurementType)
        {
            return await this.template.ExecuteAsync(
                @"insert into measurementType (measurementTypeId, name) VALUES (@measurementTypeId, @name)",
                new Parameter("@measurementTypeId", measurementType.MeasurementTypeId),
                new Parameter("@name", measurementType.Name)) == 1;
        }

        public async Task<bool> UpdateAsync(MeasurementType measurementType)
        {
            return await this.template.ExecuteAsync(
                @"update measurementType set name = @name where measurementTypeId = @measurementTypeId",
                new Parameter("@measurementTypeId", measurementType.MeasurementTypeId),
                new Parameter("@name", measurementType.Name)) == 1;
        }

    }
}