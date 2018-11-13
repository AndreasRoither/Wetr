using Common.Dal.Ado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<bool> AddForUserId(int permissionId, int userId)
        {
            // no id since it's set to auto increment
            return await this.template.ExecuteAsync(
                @"insert into hasPermission (permissionId, userId) VALUES (@permissionId, @userId)",
                new Parameter("@userId", userId),
                new Parameter("@permissionId", permissionId)) == 1;
        }

        public async Task<bool> DeleteAsync(int permissionId)
        {
            return await this.template.ExecuteAsync(
                @"delete from permission where permissionId = @permissionId",
                new Parameter("@permissionId", permissionId)) == 1;
        }

        public async Task<IEnumerable<Permission>> FindAllAsync()
        {
            return await this.template.QueryAsync("select * from permission", MapRow);
        }

        public async Task<Permission> FindByIdAsync(int permissionId)
        {
            var result = await this.template.QueryAsync(
               "select * from permission where permissionId = @permissionId",
               MapRow,
               new Parameter("@permissionId", permissionId));

            return result.SingleOrDefault();
        }

        public async Task<IEnumerable<Permission>> FindForUserId(int userId)
        {
            var result = await this.template.QueryAsync(
               "select permissionId, name, description from hasPermission INNER JOIN permission USING (permissionId) where userId = @userId ",
               MapRow,
               new Parameter("@userId", userId));

            return result;
        }

        public async Task<bool> InsertAsync(Permission permission)
        {
            return await this.template.ExecuteAsync(
                @"insert into permission (permissionId, name, description) VALUES (@permissionId, @name, @description)",
                new Parameter("@permissionId", permission.PermissionId),
                new Parameter("@name", permission.Name),
                new Parameter("@description", permission.Description)) == 1;
        }

        public async Task<bool> DeleteForUserId(int permissionId, int userId)
        {
            return await this.template.ExecuteAsync(
                @"delete from hasPermission where permissionId = @permissionId AND userId = @userId",
                new Parameter("@permissionId", permissionId),
                new Parameter("@userId", userId)) == 1;
        }

        public async Task<bool> UpdateAsync(Permission permission)
        {
            return await this.template.ExecuteAsync(
               @"update permission set name = @name, description = @description where permissionId = @permissionId",
               new Parameter("@permissionId", permission.PermissionId),
               new Parameter("@name", permission.Name),
               new Parameter("@description", permission.Description)) == 1;
        }
    }
}