using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wetr.Dal.Interface
{
    public interface IDaoBase<T>
        where T : class
    {
        Task<T> FindByIdAsync(int id);

        Task<IEnumerable<T>> FindAllAsync();

        Task<bool> UpdateAsync(T obj);

        Task<bool> DeleteAsync(int id);

        Task<bool> InsertAsync(T obj);
    }
}