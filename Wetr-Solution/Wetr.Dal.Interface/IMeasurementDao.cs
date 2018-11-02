using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IMeasurementDao
    {
        Task<Measurement> FindByIdAsync(int measurementId);
        Task<IEnumerable<Measurement>> FindAllAsync();
        Task<bool> UpdateAsync(Measurement measurement);
        Task<bool> DeleteAsync(int measurementId);

        Task<IEnumerable<Measurement>> FindByStationIdAsync(int stationId);
        Task<IEnumerable<Measurement>> FindByMeasurementTypeIdAsync(int measurementTypeId);
        Task<IEnumerable<Measurement>> FindByUnitIdAsync(int unitId);

    }
}
