using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IUnitDao
    {
        Task<Unit> FindByIdAsync(int unitId);
        Task<IEnumerable<Unit>> FindAllAsync();
        Task<bool> UpdateAsync(Unit unitType);
    }
}
