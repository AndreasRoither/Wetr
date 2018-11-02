using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IUserDao
    {
        Task<User> FindByIdAsync(int userId);
        Task<IEnumerable<User>> FindAllAsync();
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int userId);

        Task<User> FindByEmailAsync(string email);
    }
}
