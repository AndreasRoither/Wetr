using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Data;
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

        private static StationType MapRow(IDataRecord row)
        {
            return new StationType()
            {
                StationTypeId = (int)row["stationTypeId"],
                Name = (string)row["name"],
            };
        }

        public Task<bool> DeleteAsync(int stationTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StationType>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StationType> FindByIdAsync(int stationTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(StationType obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(StationType stationType)
        {
            throw new NotImplementedException();
        }
    }
}