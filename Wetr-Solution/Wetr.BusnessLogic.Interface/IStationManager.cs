using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.BusnessLogic.Interface
{
    public interface IStationManager
    {
        Task<IEnumerable<Station>> GetAllStations();

        Task<IEnumerable<Station>> GetStationsForUser(int userId);

        Task<bool> UpdateStation(Station updatedStation);

        Task<bool> AddStation(Station newStation);

        Task<long> GetNumberOfStations();

        bool CheckStation(Station station);
    }
}
