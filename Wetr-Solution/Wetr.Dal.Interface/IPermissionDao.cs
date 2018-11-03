using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IPermissionDao : IDaoBase<Permission>
    {
        Task<IEnumerable<Permission>> FindForUserId(int userId);

        Task<bool> DeleteForUserId(int permissionId, int userId);

        Task<bool> AddForUserId(int permissionId, int userId);
    }
}