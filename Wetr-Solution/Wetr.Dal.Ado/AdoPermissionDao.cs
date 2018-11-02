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
    public class AdoPermissionDao : IPermissionDao
    {
        private readonly AdoTemplate template;

        public AdoPermissionDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        public Task<IEnumerable<Permission>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Permission> FindByIdAsync(int permissionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Permission permission)
        {
            throw new NotImplementedException();
        }
    }
}
