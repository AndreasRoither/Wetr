using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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

        public Task<bool> DeleteAsync(int measurementId)
        {
            throw new NotImplementedException();
        }

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
