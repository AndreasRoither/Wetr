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
    public class AdoUserDao : IUserDao
    {
        private readonly AdoTemplate template;

        public AdoUserDao(IConnectionFactory connetionFactory)
        {
            this.template = new AdoTemplate(connetionFactory);
        }

        private static User MapRow(IDataRecord row)
        {
            return new User()
            {
                UserId = (int)row["userId"],
                FirstName = (string)row["firstName"],
                LastName = (string)row["lastName"],
                Password = (string)row["password"],
                Email = (string)row["email"],
            };
        }

        public Task<IEnumerable<User>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
