using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.BusinessLogic.Interface
{
    public interface IUserManager
    {
        Task<User> UserCredentialValidation(string email, string password);

        Task<bool> RegisterUser(User user);

        bool CheckUser(User user);
    }
}