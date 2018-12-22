using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.BusinessLogic.Interface
{
    public interface IUserManager
    {
        Task<User> UserCredentialValidation(string email, string password);

        bool CheckUser(User user);
    }
}