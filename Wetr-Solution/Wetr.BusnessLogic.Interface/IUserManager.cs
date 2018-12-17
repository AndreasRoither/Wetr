using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Domain;

namespace Wetr.BusnessLogic.Interface
{
    public interface IUserManager
    {
        Task<User> UserCredentialValidation(string email, string password);

        Task<bool> RegisterUser(User user);

        bool CheckUser(User user);
    }
}
