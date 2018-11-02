using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoStationDao : IStationDao
    {
        private readonly AdoTemplate template;

        public AdoStationDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static Station MapRow(IDataRecord row)
        {
            return new Station()
            {
                StationId = (int)row["stationId"],
                StationTypeId = (int)row["stationTypeId"],
                AddressId = (int)row["addressId"],
                UserId = (int)row["userId"],
                Name = (string)row["name"],
                Longitude = (double)row["longitude"],
                Latitude = (double)row["latitude"],
            };
        }

        public Task<bool> DeleteAsync(int stationId)
        {
            throw new NotImplementedException();
        }

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

        public Task<bool> InsertAsync(Station obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Station station)
        {
            throw new NotImplementedException();
        }
    }
}