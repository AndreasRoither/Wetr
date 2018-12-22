using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wetr.BusinessLogic.Interface;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class StationManager : IStationManager
    {
        private IStationDao stationDao;
        private IStationTypeDao stationTypeDao;
        private IMeasurementDao measurementDao;

        public StationManager(IStationDao stationDao, IStationTypeDao stationTypeDao, IMeasurementDao measurementDao)
        {
            this.stationDao = stationDao;
            this.stationTypeDao = stationTypeDao;
            this.measurementDao = measurementDao;
        }

  
        #region functions

        public async Task<IEnumerable<Station>> GetAllStations()
        {
            try
            {
                return await stationDao.FindAllAsync();
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<IEnumerable<Station>> GetStationsForUser(int userId)
        {
            try
            {
                return await stationDao.FindByUserIdAsync(userId);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<bool> UpdateStation(Station updatedStation)
        {
            if (!CheckStation(updatedStation))
                return false;

            try
            {
                return await stationDao.UpdateAsync(updatedStation);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<bool> DeleteStation(Station station)
        {
            try
            {
                return await stationDao.DeleteAsync(station.StationId);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<bool> HasMeasurements(Station station)
        {
            try
            {
                return (await measurementDao.FindByStationIdAsync(station.StationId)).Count() != 0;
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<bool> AddStation(Station newStation)
        {
            try
            {
                if (!CheckStation(newStation)) return false;
                return await stationDao.InsertAsync(newStation);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<long> GetNumberOfStations()
        {
            try
            {
                return await stationDao.GetTotalNumberOfStationsAsync();
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<IEnumerable<StationType>> GetStationTypes()
        {
            try
            {
                return await stationTypeDao.FindAllAsync();
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public async Task<StationType> GetStationTypesForStationTypeId(int stationTypeId)
        {
            try
            {
                return await stationTypeDao.FindByIdAsync(stationTypeId);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }
        }

        public bool CheckStation(Station station)
        {
            if (string.IsNullOrEmpty(station.Name)) return false;
            if (station.AddressId < 0) return false;
            if (station.Latitude < -90) return false;
            if (station.Latitude > 90) return false;
            if (station.Longitude < -180) return false;
            if (station.Longitude > 180) return false;
            if (station.UserId < 0) return false;
            return true;
        }

        #endregion functions
    }
}