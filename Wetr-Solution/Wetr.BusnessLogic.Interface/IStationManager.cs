using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.BusinessLogic.Interface
{
    public interface IStationManager
    {
        Task<IEnumerable<Station>> GetAllStations();

        Task<bool> HasMeasurements(Station station);

        Task<IEnumerable<StationType>> GetStationTypes();

        Task<StationType> GetStationTypesForStationTypeId(int stationTypeId);

        Task<IEnumerable<Station>> GetStationsForUser(int userId);

        Task<bool> UpdateStation(Station updatedStation);

        Task<bool> AddStation(Station newStation);

        Task<long> GetNumberOfStations();

        bool CheckStation(Station station);
    }
}