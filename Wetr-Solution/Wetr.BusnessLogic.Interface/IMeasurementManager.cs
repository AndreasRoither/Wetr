using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.BusinessLogic.Interface
{
    public interface IMeasurementManager
    {

        Task<double[]> GetDashbardTemperaturesAsync();

        Task<double[]> GetDashboardRainValuesAsync();

        Task<long> GetNumberOfMeasurementsAsync();

        Task<long> GetNumberOfMeasurementsOfWeekAsync();

        Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, Community community);
        Task<double[]> GetQueryResult(DateTime start, DateTime end, int measurementTypeId, int reductionTypeId, int groupingTypeId, List<Station> stations, decimal lat, decimal lon, int radius);

        Task<bool> AddMeasurement(Measurement m);

    }
}