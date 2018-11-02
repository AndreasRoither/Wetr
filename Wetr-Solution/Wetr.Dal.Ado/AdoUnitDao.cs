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
    public class AdoUnitDao : IUnitDao
    {
        private readonly AdoTemplate template;

        public AdoUnitDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static Unit MapRow(IDataRecord row)
        {
            return new Unit()
            {
                UnitId = (int)row["unitId"],
                Name = (string)row["name"],
            };
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
