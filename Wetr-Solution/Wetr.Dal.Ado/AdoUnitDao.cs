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
    public class AdoUnitDao : IUnitDao
    {
        private readonly AdoTemplate template;

        public AdoUnitDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        public Task<IEnumerable<Unit>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Unit> FindByIdAsync(int unitId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Unit unitType)
        {
            throw new NotImplementedException();
        }
    }
}
