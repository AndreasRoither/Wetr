using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IMeasurementDao : IDaoBase<Measurement>
    {
        Task<IEnumerable<Measurement>> FindByStationIdAsync(int stationId);

        Task<IEnumerable<Measurement>> FindByMeasurementTypeIdAsync(int measurementTypeId);

        Task<IEnumerable<Measurement>> FindByUnitIdAsync(int unitId);

        Task<double[]> GetDayAverageOfLastXDaysAsync(int type, int numDays);

        Task<long> GetTotalNumberOfMeasurementsAsync();

        Task<long> GetNumberOfMeasurementsFromTheLastXDaysAsync(int days);

        Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, Community community);
        Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, decimal lat, decimal lon, int radius);

    }
}