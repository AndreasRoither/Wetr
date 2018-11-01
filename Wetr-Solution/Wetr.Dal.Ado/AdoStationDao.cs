using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoStationDao : IStationDao
    {
        public Task<IEnumerable<Station>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Station>> FindByAddressIdAsync(int addressId)
        {
            throw new NotImplementedException();
        }

        public Task<Station> FindByIdAsync(int stationId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Station>> FindByStationTypeIdAsync(int stationTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Station>> FindByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Station station)
        {
            throw new NotImplementedException();
        }
    }
}
