using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.Dal.Ado
{
    public class AdoUserDao : IUserDao
    {
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
