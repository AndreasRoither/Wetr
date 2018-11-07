using Common.Dal.Ado;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<bool> DeleteAsync(int stationId)
        {
            return await this.template.ExecuteAsync(
                @"delete from station where stationId = @stationId",
                new Parameter("@stationId", stationId)) == 1;
        }

        public async Task<IEnumerable<Station>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from person", MapRow);
        }

        public async Task<IEnumerable<Station>> FindByAddressIdAsync(int addressId)
        {
            var result = await this.template.QueryAsync(
                "select * from station where addressId = @addressId",
                MapRow,
                new Parameter("@addressId", addressId));

            return result;
        }

        public async Task<Station> FindByIdAsync(int stationId)
        {
            var result = await this.template.QueryAsync(
                "select * from station where stationId = @stationId",
                MapRow,
                new Parameter("@stationId", stationId));

            return result.SingleOrDefault();
        }

        public async Task<IEnumerable<Station>> FindByStationTypeIdAsync(int stationTypeId)
        {
            var result = await this.template.QueryAsync(
                "select * from station where stationTypeId = @stationTypeId",
                MapRow,
                new Parameter("@stationTypeId", stationTypeId));

            return result;
        }

        public async Task<IEnumerable<Station>> FindByUserIdAsync(int userId)
        {
            var result = await this.template.QueryAsync(
                "select * from station where stationTypeId = @userId",
                MapRow,
                new Parameter("@userId", userId));

            return result;
        }

        public async Task<bool> InsertAsync(Station station)
        {
            return await this.template.ExecuteAsync(
                @"insert into station (stationId, name, longitude, latitude, stationTypeId, addressId, userId) VALUES (@stationId, @name, @longitude, @latitude, @stationTypeId, @addressId, @userId)",
                new Parameter("@stationId", station.StationId),
                new Parameter("@name", station.Name),
                new Parameter("@longitude", station.Longitude),
                new Parameter("@latitude", station.Latitude),
                new Parameter("@stationTypeId", station.StationTypeId),
                new Parameter("@addressId", station.AddressId),
                new Parameter("@userId", station.UserId)) == 1;
        }

        public async Task<bool> UpdateAsync(Station station)
        {
            return await this.template.ExecuteAsync(
                @"update station set name = @name, longitude = @longitude, latitude = @latitude, stationTypeId = @stationTypeId, addressId = @addressId, userId = @userId where id = @stationId",
                new Parameter("@stationId", station.StationId),
                new Parameter("@name", station.Name),
                new Parameter("@longitude", station.Longitude),
                new Parameter("@latitude", station.Latitude),
                new Parameter("@stationTypeId", station.StationTypeId),
                new Parameter("@addressId", station.AddressId),
                new Parameter("@userId", station.UserId)) == 1;
        }
    }
}