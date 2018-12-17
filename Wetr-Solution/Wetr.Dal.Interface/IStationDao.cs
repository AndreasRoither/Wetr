using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IStationDao : IDaoBase<Station>
    {
        Task<IEnumerable<Station>> FindByUserIdAsync(int userId);

        Task<IEnumerable<Station>> FindByStationTypeIdAsync(int stationTypeId);

        Task<IEnumerable<Station>> FindByAddressIdAsync(int addressId);

        Task<long> GetTotalNumberOfStationsAsync();
    }
}