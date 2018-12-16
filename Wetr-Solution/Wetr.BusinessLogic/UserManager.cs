
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

        public bool UserCredentialValidation(string email, string password)
        {
            User user = userDao.FindByEmailAsync(email).Result;
            return BCrypt.Net.BCrypt.Verify(password, user.Password);

            // hash and save a password
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // check a password
            //bool validPassword = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            // BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool RegisterUser(User user)
        {
            if (!CheckUser(user)) return false;
            return userDao.InsertAsync(user).Result;
        }

        public bool CheckUser(User user)
        {
            if (user.Email.Equals(string.Empty)) return false;
            if (user.FirstName.Equals(string.Empty)) return false;
            if (user.LastName.Equals(string.Empty)) return false;
            if (user.Password.Equals(string.Empty)) return false;
            return true;
        }

        #endregion functions
    }
}