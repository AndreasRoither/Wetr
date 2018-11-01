using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IPermissionDao
    {
        Task<Permission>FindByIdAsync(int permissionId);
        Task<IEnumerable<Permission>> FindAllAsync();
        Task<bool> UpdateAsync(Permission permission);
    }
}
