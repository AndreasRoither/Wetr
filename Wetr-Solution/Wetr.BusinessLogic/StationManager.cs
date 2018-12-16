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
            return stationDao.UpdateAsync(updatedStation).Result;
        }

        public bool AddStation(Station newStation)
        {
            return stationDao.InsertAsync(newStation).Result;
        }

        #endregion functions
    }
}