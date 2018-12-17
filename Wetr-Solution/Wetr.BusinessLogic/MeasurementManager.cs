using System;
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

       
        public async Task<double[]> GetDashbardTemperatures()
        {
            return await measurementDao.GetDayAverageOfLastXDaysAsync((int)EMeasurementType.Temperature, 7);
        }

        public async Task<double[]> GetDashboardRainValues()
        {
            return await measurementDao.GetDayAverageOfLastXDaysAsync((int)EMeasurementType.Rain, 7);
        }


        public async Task<long> GetNumberOfMeasurements()
        {
            return await measurementDao.GetTotalNumberOfMeasurementsAsync();
        }

        public async Task<long> GetNumberOfMeasurementsOfWeek()
        {
            return await measurementDao.GetNumberOfMeasurementsFromTheLastXDaysAsync(7);
        }




        #endregion functions
    }
}