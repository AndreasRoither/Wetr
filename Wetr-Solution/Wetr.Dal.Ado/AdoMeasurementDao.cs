using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoMeasurementDao : IMeasurementDao
    {
        public Task<IEnumerable<Measurement>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Measurement> FindByIdAsync(int measurementId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Measurement>> FindByMeasurementTypeIdAsync(int measurementTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Measurement>> FindByStationIdAsync(int stationId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Measurement>> FindByUnitIdAsync(int unitId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Measurement measurement)
        {
            throw new NotImplementedException();
        }
    }
}
