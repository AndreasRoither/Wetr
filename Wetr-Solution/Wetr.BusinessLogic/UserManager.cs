
using System.Threading.Tasks;
using Wetr.BusnessLogic.Interface;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class UserManager : IUserManager
    {
        IUserDao userDao;

        public UserManager(string databaseName)
        {
            userDao = AdoFactory.Instance.GetUserDao(databaseName);
        }

        #region functions

        public async Task<User> UserCredentialValidation(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return null;

            User user = await userDao.FindByEmailAsync(email);

            if (user == null) return null;

            // will cause invalid salt version exception if not hashed with BCrypt
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                user.Password = string.Empty;
                return user;
            }

            return null;
        }

        public async Task<bool> RegisterUser(User user)
        {
            if (!CheckUser(user)) return false;
            return await userDao.InsertAsync(user);
        }

        public bool CheckUser(User user)
        {
            if (string.IsNullOrEmpty(user.Email)) return false;
            if (string.IsNullOrEmpty(user.FirstName)) return false;
            if (string.IsNullOrEmpty(user.LastName)) return false;
            if (string.IsNullOrEmpty(user.Password)) return false;
            return true;
        }

        #endregion functions
    }
}