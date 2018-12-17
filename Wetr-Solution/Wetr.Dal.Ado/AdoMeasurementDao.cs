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
    public class AdoMeasurementDao : IMeasurementDao
    {
        private readonly AdoTemplate template;

        public AdoMeasurementDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static Measurement MapRow(IDataRecord row)
        {
            return new Measurement()
            {
                MeasurementId = (int)row["measurementId"],
                StationId = (int)row["stationId"],
                MeasurementTypeId = (int)row["measurementTypeId"],
                UnitId = (int)row["unitId"],
                Value = (double)row["value"],
                TimesStamp = (DateTime)row["timestamp"]
            };
        }

        private static double MapToDouble(IDataRecord row)
        {
            return (double)row["value"];
            
        }

        public async Task<bool> DeleteAsync(int measurementId)
        {
            return await this.template.ExecuteAsync(
                @"delete from measurement where measurementId = @measurementId",
                new Parameter("@measurementId", measurementId)) == 1;
        }

        public async Task<IEnumerable<Measurement>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from measurement", MapRow);
        }

        public async Task<Measurement> FindByIdAsync(int measurementId)
        {
            var result = await this.template.QueryAsync(
                "select * from measurement where measurementId = @measurementId",
                MapRow,
                new Parameter("@measurementId", measurementId));

            return result.SingleOrDefault();
        }


        public async Task<bool> InsertAsync(Measurement measurement)
        {
            return await this.template.ExecuteAsync(
                @"insert into measurement (measurementId, stationId, measurementTypeId, unitId, value, timestamp) VALUES(@measurementId, @stationId, @measurementTypeId, @unitId, @value, @timestamp)",
                new Parameter("@measurementId", measurement.MeasurementId),
                new Parameter("@stationId", measurement.StationId),
                new Parameter("@measurementTypeId", measurement.MeasurementTypeId),
                new Parameter("@unitId", measurement.UnitId),
                new Parameter("@value", measurement.Value),
                new Parameter("@timestamp", measurement.TimesStamp)) == 1;
        }

        public async Task<bool> UpdateAsync(Measurement measurement)
        {
            return await this.template.ExecuteAsync(
                @"update measurement set stationId = @stationId, measurementTypeId = @measurementTypeId, unitId = @unitId, value = @value, timestamp = @timestamp where measurementId = @measurementId",
                new Parameter("@measurementId", measurement.MeasurementId),
                new Parameter("@stationId", measurement.StationId),
                new Parameter("@measurementTypeId", measurement.MeasurementTypeId),
                new Parameter("@unitId", measurement.UnitId),
                new Parameter("@value", measurement.Value),
                new Parameter("@timestamp", measurement.TimesStamp)) == 1;
        }



        public async Task<IEnumerable<Measurement>> FindByMeasurementTypeIdAsync(int measurementTypeId)
        {
            var result = await this.template.QueryAsync(
               "select * from measurement where measurementTypeId = @measurementTypeId",
               MapRow,
               new Parameter("@measurementTypeId", measurementTypeId));

            return result;
        }

        public async Task<IEnumerable<Measurement>> FindByStationIdAsync(int stationId)
        {
            var result = await this.template.QueryAsync(
               "select * from measurement where stationId = @stationId",
               MapRow,
               new Parameter("@stationId", stationId));

            return result;
        }

        public async Task<IEnumerable<Measurement>> FindByUnitIdAsync(int unitId)
        {
            var result = await this.template.QueryAsync(
                "select * from measurement where unitId = @unitId",
                MapRow,
                new Parameter("@unitId", unitId));

            return result;
        }

        public async Task<double[]> GetDayAverageOfLastXDaysAsync(int type, int numDays)
        {
            // SELECT AVG(value) "value" FROM measurement GROUP BY DATE(timestamp)
            var result = await this.template.QueryAsync(
               "select AVG(value) as value from measurement where measurementTypeId = @type and timestamp >= @from and timestamp <= @to GROUP BY DATE(timestamp)",
               MapToDouble,
               new Parameter("@type", type),
               new Parameter("@from", DateTime.Now.AddDays(-numDays)),
               new Parameter("@to", DateTime.Now));

            return result.ToArray();
        }
        

        public async Task<long> GetTotalNumberOfMeasurementsAsync()
        {
            double result = await this.template.ScalarAsync<double>("SELECT COUNT(*) FROM measurement");
            return (long) result;
        }

        public async Task<long> GetNumberOfMeasurementsFromTheLastXDaysAsync(int days)
        {
            double result = await this.template.ScalarAsync<double>("SELECT COUNT(*) FROM measurement WHERE timestamp <= @to and timestamp >= @from",
                new Parameter("from", DateTime.Now.AddDays(-days)),
                new Parameter("to", DateTime.Now)
                );
            return (long)result;
        }


    }
}