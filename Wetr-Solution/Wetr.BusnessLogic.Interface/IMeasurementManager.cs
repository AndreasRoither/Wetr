using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.BusinessLogic.Interface
{
    public interface IMeasurementManager
    {
        Task<IEnumerable<Measurement>> GetAllMeasurementsAsync();

        Task<IEnumerable<Measurement>> GetAllMeasurementsForStationAsync(int stationId);

        Task<double[]> GetDashbardTemperaturesAsync();

        Task<double[]> GetDashboardRainValuesAsync();

        Task<long> GetNumberOfMeasurementsAsync();

        Task<long> GetNumberOfMeasurementsOfWeekAsync();
    }
}
