
using System.Threading.Tasks;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class UserManager
    {
        IUserDao userDao;

        public UserManager(string databaseName)
        {
            userDao = AdoFactory.Instance.GetUserDao(databaseName);
        }

        #region functions

        public async Task<bool> UserCredentialValidation(string email, string password)
        {
            User user = await userDao.FindByEmailAsync(email);
            return BCrypt.Net.BCrypt.Verify(password, user.Password);

            // hash and save a password
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // check a password
            //bool validPassword = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            // BCrypt.Net.BCrypt.HashPassword(password);
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