using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;
using Wetr.BusinessLogic.Interface;

namespace Wetr.BusinessLogic
{
    public class MeasurementManager : IMeasurementManager
    {
        IMeasurementDao measurementDao;

        public MeasurementManager(string databaseName)
        {
            measurementDao = AdoFactory.Instance.GetMeasurementDao(databaseName);
        }

        #region functions

        public async Task<IEnumerable<Measurement>> GetAllMeasurementsAsync()
        {
            return await measurementDao.FindAllAsync();
        }

        public async Task<IEnumerable<Measurement>> GetAllMeasurementsForStationAsync(int stationId)
        {
            if (stationId < 0) return null;
            return await measurementDao.FindByStationIdAsync(stationId);
        }

       
        public async Task<double[]> GetDashbardTemperaturesAsync()
        {
            return await measurementDao.GetDayAverageOfLastXDaysAsync((int)EMeasurementType.Temperature, 7);
        }

        public async Task<double[]> GetDashboardRainValuesAsync()
        {
            return await measurementDao.GetDayAverageOfLastXDaysAsync((int)EMeasurementType.Rain, 7);
        }


        public async Task<long> GetNumberOfMeasurementsAsync()
        {
            return await measurementDao.GetTotalNumberOfMeasurementsAsync();
        }

        public async Task<long> GetNumberOfMeasurementsOfWeekAsync()
        {
            return await measurementDao.GetNumberOfMeasurementsFromTheLastXDaysAsync(7);
        }




        #endregion functions
    }
}