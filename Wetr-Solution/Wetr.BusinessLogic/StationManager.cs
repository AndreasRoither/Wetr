using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wetr.BusnessLogic.Interface;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class StationManager : IStationManager
    {
        IStationDao stationDao;
        IStationTypeDao stationTypeDao;
        IMeasurementDao measurementDao;

        public StationManager(string databaseName)
        {
            stationDao = AdoFactory.Instance.GetStationDao(databaseName);
            stationTypeDao = AdoFactory.Instance.GetStationTypeDao(databaseName);
            measurementDao = AdoFactory.Instance.GetMeasurementDao(databaseName);
        }

        #region functions

        public async Task<IEnumerable<Station>> GetAllStations()
        {
            return await stationDao.FindAllAsync();
        }

        public async Task<IEnumerable<Station>> GetStationsForUser(int userId)
        {
            return await stationDao.FindByUserIdAsync(userId);
        }

        public async Task<bool> UpdateStation(Station updatedStation)
        {
            if (!CheckStation(updatedStation))
                return false;

            return await stationDao.UpdateAsync(updatedStation);
        }

        public async Task<bool> DeleteStation(Station station)
        {
            return await stationDao.DeleteAsync(station.StationId);
        }

        public async Task<bool> HasMeasurements(Station station)
        {
            return (await measurementDao.FindByStationIdAsync(station.StationId)).Count() == 0;
        }

        public async Task<bool> AddStation(Station newStation)
        {
            if (!CheckStation(newStation)) return false;
            return await stationDao.InsertAsync(newStation);
        }

        public async Task<long> GetNumberOfStations()
        {
            return await stationDao.GetTotalNumberOfStationsAsync();
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

        public async Task<IEnumerable<StationType>> GetStationTypes()
        {
            return await stationTypeDao.FindAllAsync();
        }

        public async Task<StationType> GetStationTypesForStationTypeId(int stationTypeId)
        {
            return await stationTypeDao.FindByIdAsync(stationTypeId);
        }

        #endregion functions
    }
}