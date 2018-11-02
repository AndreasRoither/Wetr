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
    public class AdoMeasurementTypeDao : IMeasurementTypeDao
    {
        private readonly AdoTemplate template;

        public AdoMeasurementTypeDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        public Task<IEnumerable<MeasurementType>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MeasurementType> FindByIdAsync(int measurementTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(MeasurementType measurementType)
        {
            throw new NotImplementedException();
        }
    }
}
