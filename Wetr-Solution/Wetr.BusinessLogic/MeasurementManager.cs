using System.Collections.Generic;
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
            measurementDao = AdoFactory.Instance.GetMeasurementDao();
        }

        #region functions

        public IEnumerable<Measurement> GetAllMeasurements()
        {
            return measurementDao.FindAllAsync().Result;
        }

        public IEnumerable<Measurement> GetAllMeasurementsForStation(int stationId)
        {
            if (stationId < 0) return null;
            return measurementDao.FindByStationIdAsync(stationId).Result;
        }

        #endregion functions
    }
}