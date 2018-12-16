using System;
using System.Collections.Generic;
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

        public IEnumerable<Station> GetAllStations()
        {
            return stationDao.FindAllAsync().Result;
        }

        public IEnumerable<Station> GetStationsForUser(int userId)
        {
            return stationDao.FindByUserIdAsync(userId).Result;
        }

        public bool UpdateStation(Station updatedStation)
        {
            if (!CheckStation(updatedStation)) return false;
            return stationDao.UpdateAsync(updatedStation).Result;
        }

        public bool AddStation(Station newStation)
        {
            if (!CheckStation(newStation)) return false;
            return stationDao.InsertAsync(newStation).Result;
        }
       
        private bool CheckStation(Station station)
        {
            if (station.Name.Equals(string.Empty)) return false;
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