using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Data;
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

        private static Permission MapRow(IDataRecord row)
        {
            return new Permission()
            {
                PermissionId = (int)row["permissionId"],
                Name = (string)row["name"],
                Description = (string)row["description"],
            };
        }

        public Task<bool> AddForUserId(int permissionId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Permission> FindByIdAsync(int permissionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> FindForUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveForUserId(int permissionId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Permission permission)
        {
            throw new NotImplementedException();
        }
    }
}
