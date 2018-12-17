using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class MeasurementManager
    {
        IMeasurementDao measurementDao;

        public MeasurementManager(string databaseName)
        {
            measurementDao = AdoFactory.Instance.GetMeasurementDao(databaseName);
        }

        #region functions

        public async Task<IEnumerable<Measurement>> GetAllMeasurements()
        {
            return await measurementDao.FindAllAsync();
        }

        public async Task<IEnumerable<Measurement>> GetAllMeasurementsForStation(int stationId)
        {
            if (stationId < 0) return null;
            return await measurementDao.FindByStationIdAsync(stationId);
        }

        #endregion functions
    }
}