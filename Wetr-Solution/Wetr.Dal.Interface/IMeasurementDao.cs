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
    }
}