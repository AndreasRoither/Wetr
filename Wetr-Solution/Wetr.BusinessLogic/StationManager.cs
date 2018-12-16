using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class StationManager
    {
        IStationDao stationDao;

        public StationManager(string databaseName)
        {
            stationDao = AdoFactory.Instance.GetStationDao(databaseName);
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
            if (!CheckStation(updatedStation)) return false;
            return await stationDao.UpdateAsync(updatedStation);
        }

        public async Task<bool> AddStation(Station newStation)
        {
            if (!CheckStation(newStation)) return false;
            return await stationDao.InsertAsync(newStation);
        }
       
        private bool CheckStation(Station station)
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