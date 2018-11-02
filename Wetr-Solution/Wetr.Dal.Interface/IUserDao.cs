using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.Dal.Interface
{
    public interface IUserDao : IDaoBase<User>
    {
        Task<User> FindByEmailAsync(string email);
    }
}