using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IMeasurementTypeDao
    {
        Task<MeasurementType> FindByIdAsync(int measurementTypeId);
        Task<IEnumerable<MeasurementType>> FindAllAsync();
        Task<bool> UpdateAsync(MeasurementType measurementType);
    }
}
