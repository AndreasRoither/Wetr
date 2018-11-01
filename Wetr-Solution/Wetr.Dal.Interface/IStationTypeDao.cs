using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IStationTypeDao
    {
        Task<StationType> FindByIdAsync(int stationTypeId);
        Task<IEnumerable<StationType>> FindAllAsync();
        Task<bool> UpdateAsync(StationType stationType);
    }
}
