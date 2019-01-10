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
            if (measurement.MeasurementId == 0)
            {
                return await this.template.ExecuteAsync(
                @"insert into measurement (stationId, measurementTypeId, unitId, value, timestamp) VALUES(@stationId, @measurementTypeId, @unitId, @value, @timestamp)",
                new Parameter("@stationId", measurement.StationId),
                new Parameter("@measurementTypeId", measurement.MeasurementTypeId),
                new Parameter("@unitId", measurement.UnitId),
                new Parameter("@value", measurement.Value),
                new Parameter("@timestamp", measurement.TimesStamp)) == 1;
            }
            else
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
            double result = (long)await this.template.ScalarAsync<double>("SELECT COUNT(*) FROM measurement");
            return (long)result;
        }

        public async Task<long> GetNumberOfMeasurementsFromTheLastXDaysAsync(int days)
        {
            double result = (long)await this.template.ScalarAsync<double>("SELECT COUNT(*) FROM measurement WHERE timestamp <= @to and timestamp >= @from",
                new Parameter("from", DateTime.Now.AddDays(-days)),
                new Parameter("to", DateTime.Now)
                );
            return (long)result;
        }

        private string GetReductionString(int reductionTypeId)
        {
            string reductionString = "";
            switch (reductionTypeId)
            {
                case 0:
                    reductionString = "AVG(value)"; break;
                case 1:
                    reductionString = "MIN(value)"; break;
                case 2:
                    reductionString = "MAX(value)"; break;
                case 3:
                    reductionString = "SUM(value)"; break;
            }
            return reductionString;
        }

        private string GetGroupingString(int groupingTypeId)
        {
            string groupingString = "";
            switch (groupingTypeId)
            {
                case 0:
                    // Day
                    groupingString = "DATE(timestamp)"; break;
                case 1:
                    // Week
                    groupingString = "WEEK(timestamp)"; break;
                case 2:
                    // Month
                    groupingString = "MONTH(timestamp)"; break;
                case 3:
                    // Year
                    groupingString = "YEAR(timestamp)"; break;
                case 4:
                    // Hour
                    groupingString = "HOUR(timestamp)"; break;
            }

            return groupingString;
        }

        private string GetStationFilter(List<Station> stations)
        {
            string stationFilter = "";
            if (stations.Count > 0)
            {
                stationFilter += "stationId IN (" + stations[0].StationId;
                for (int i = 1; i < stations.Count(); ++i)
                {
                    stationFilter += "," + stations[i].StationId;
                }
                stationFilter += ") and ";
            }

            return stationFilter;
        }

        public async Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, Community community)
        {

            string reductionString = GetReductionString(reductionTypeId);
            string groupingString = GetGroupingString(groupingTypeId);
            string stationFilter = GetStationFilter(stations);


            string sql = "select " + reductionString + " as value from measurement " +
                "INNER JOIN station using (stationId) " +
                "INNER JOIN address USING(addressId) where communityId = @communityId and measurementTypeId = @type and " + stationFilter + " timestamp >= @from and timestamp <= @to GROUP BY " + groupingString;

            var result = await this.template.QueryAsync(
                  sql,
                  MapToDouble,
                  new Parameter("@type", measurementTypeId),
                  new Parameter("@from", start),
                  new Parameter("@communityId", community.CommunityId),
                  new Parameter("@to", end));


            return result.ToArray();
        }

        public async Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, District district)
        {

            string reductionString = GetReductionString(reductionTypeId);
            string groupingString = GetGroupingString(groupingTypeId);
            string stationFilter = GetStationFilter(stations);


            string sql = "select " + reductionString + " as value from measurement " +
                "INNER JOIN station using (stationId) " +
                "INNER JOIN address USING(addressId) " +
                "INNER JOIN community using (communityId) where districtId = @districtId and measurementTypeId = @type and " + stationFilter + " timestamp >= @from and timestamp <= @to GROUP BY " + groupingString;

            var result = await this.template.QueryAsync(
                  sql,
                  MapToDouble,
                  new Parameter("@type", measurementTypeId),
                  new Parameter("@from", start),
                  new Parameter("@districtId", district.DistrictId),
                  new Parameter("@to", end));


            return result.ToArray();
        }

        public async Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, Province province)
        {

            string reductionString = GetReductionString(reductionTypeId);
            string groupingString = GetGroupingString(groupingTypeId);
            string stationFilter = GetStationFilter(stations);


            string sql = "select " + reductionString + " as value from measurement " +
                "INNER JOIN station using (stationId) " +
                "INNER JOIN address USING(addressId) " +
                "INNER JOIN community using (communityId) " +
                "INNER JOIN district using (districtId) where provinceId = @provinceId and measurementTypeId = @type and " + stationFilter + " timestamp >= @from and timestamp <= @to GROUP BY " + groupingString;

            var result = await this.template.QueryAsync(
                  sql,
                  MapToDouble,
                  new Parameter("@type", measurementTypeId),
                  new Parameter("@from", start),
                  new Parameter("@provinceId", province.ProvinceId),
                  new Parameter("@to", end));


            return result.ToArray();
        }

        public async Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations)
        {

            string reductionString = GetReductionString(reductionTypeId);
            string groupingString = GetGroupingString(groupingTypeId);
            string stationFilter = GetStationFilter(stations);


            string sql = "select " + reductionString + " as value from measurement " +
                "INNER JOIN station using (stationId) WHERE measurementTypeId = @type and " + stationFilter + " timestamp >= @from and timestamp <= @to GROUP BY " + groupingString;

            var result = await this.template.QueryAsync(
                  sql,
                  MapToDouble,
                  new Parameter("@type", measurementTypeId),
                  new Parameter("@from", start),
                  new Parameter("@to", end));


            return result.ToArray();
        }


        public async Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, decimal lat, decimal lon, int radius)
        {
            string reductionString = GetReductionString(reductionTypeId);
            string groupingString = GetGroupingString(groupingTypeId);
            string stationFilter = GetStationFilter(stations);


            string sql = "select " + reductionString + " as value from measurement " +
                "INNER JOIN station using (stationId) " +
                "where ( 6371 *  acos( cos( radians( @lat ) ) * cos( radians( latitude ) ) *  cos( radians( longitude ) - radians( @lon ) ) +  sin( radians( @lat ) ) *  sin( radians( latitude ) ) ) <= @rad ) and measurementTypeId = @type and " + stationFilter + " timestamp >= @from and timestamp <= @to GROUP BY " + groupingString;

            var result = await this.template.QueryAsync(
                  sql,
                  MapToDouble,
                  new Parameter("@type", measurementTypeId),
                  new Parameter("@from", start),
                  new Parameter("@lat", lat),
                  new Parameter("@lon", lon),
                  new Parameter("@rad", radius),

                  new Parameter("@to", end));


            return result.ToArray();
        }

    }
}