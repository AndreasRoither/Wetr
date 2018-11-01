using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface ICountryDao
    {
        Task<Country> FindByIdAsync(int countryId);
        Task<IEnumerable<Country>> FindAllAsync();
        Task<bool> UpdateAsync(Country country);
    }
}
