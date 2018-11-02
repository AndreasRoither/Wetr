using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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

        public Task<IEnumerable<Address>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Address>> FindByCommunityIdAsync(int addresscommunityId)
        {
            throw new NotImplementedException();
        }

        public Task<Address> FindByIdAsync(int communityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
