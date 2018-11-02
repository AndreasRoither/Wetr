using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IStationDao
    {
        Task<Station> FindByIdAsync(int stationId);
        Task<IEnumerable<Station>> FindAllAsync();
        Task<bool> UpdateAsync(Station station);
        Task<bool> DeleteAsync(int stationId);

        Task<IEnumerable<Station>> FindByUserIdAsync(int userId);
        Task<IEnumerable<Station>> FindByStationTypeIdAsync(int stationTypeId);
        Task<IEnumerable<Station>> FindByAddressIdAsync(int addressId);
    }
}
