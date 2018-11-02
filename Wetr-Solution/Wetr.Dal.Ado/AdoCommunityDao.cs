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
    public class AdoCommunityDao : ICommunityDao
    {
        private readonly AdoTemplate template;

        public AdoCommunityDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static Community MapRow(IDataRecord row)
        {
            return new Community()
            {
                CommunityId = (int)row["communityId"],
                DistrictId = (int)row["districtId"],
                Name = (string)row["name"],
            };
        }

        public Task<bool> DeleteAsync(int communityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Community>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Community>> FindByDistrictIdAsync(int districtId)
        {
            throw new NotImplementedException();
        }

        public Task<Community> FindByIdAsync(int communityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Community community)
        {
            throw new NotImplementedException();
        }
    }
}
