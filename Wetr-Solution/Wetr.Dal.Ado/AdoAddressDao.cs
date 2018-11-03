using Common.Dal.Ado;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoAddressDao : IAddressDao
    {
        private readonly AdoTemplate template;

        public AdoAddressDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static Address MapRow(IDataRecord row)
        {
            return new Address()
            {
                AddressId = (int)row["addressId"],
                CommunityId = (int)row["communityId"],
                Location = (string)row["location"],
                Zip = (string)row["zip"]
            };
        }

        public async Task<bool> DeleteAsync(int addressId)
        {
            return await this.template.ExecuteAsync(
                @"delete from address" +
                    "where addressId = @addressId",
                new Parameter("@addressId", addressId)) == 1;
        }

        public async Task<IEnumerable<Address>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from address", MapRow);
        }

        public async Task<Address> FindByIdAsync(int addressId)
        {
            var result = await this.template.QueryAsync(
                "select * from address where addressId = @addressId",
                MapRow,
                new Parameter("@addressId", addressId));

            return result.SingleOrDefault();
        }

        
        public async Task<bool> InsertAsync(Address address)
        {
            // no id since it's set to auto increment
            return await this.template.ExecuteAsync(
                @"insert into address (location, zip, communityId) VALUES" +
                    "(@location, @zip, @communityId)",
                new Parameter("@location", address.Location),
                new Parameter("@zip", address.Zip),
                new Parameter("@communityId", address.CommunityId)) == 1;
        }

        public async Task<bool> UpdateAsync(Address address)
        {
            return await this.template.ExecuteAsync(
                @"update address set" +
                    "location = @location," +
                    "zip = @zip," +
                    "communityId = @communityId" +
                "where addressId = @addressId",
                new Parameter("@addressId", address.AddressId),
                new Parameter("@location", address.Location),
                new Parameter("@zip", address.Zip),
                new Parameter("@communityId", address.CommunityId)) == 1;
        }

        public async Task<IEnumerable<Address>> FindByCommunityIdAsync(int communityId)
        {
            var result = await this.template.QueryAsync(
               "select * from address where communityId = @communityId",
               MapRow,
               new Parameter("@communityId", communityId));

            return result;
        }

    }
}