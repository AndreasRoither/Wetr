using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoStationTypeDao : IStationTypeDao
    {
        private readonly AdoTemplate template;

        public AdoStationTypeDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        public Task<IEnumerable<StationType>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StationType> FindByIdAsync(int stationTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(StationType stationType)
        {
            throw new NotImplementedException();
        }
    }
}
