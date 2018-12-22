using System;
using System.Threading.Tasks;
using Wetr.BusinessLogic.Interface;
using Wetr.Dal.Factory;
using Wetr.Dal.Interface;
using Wetr.Domain;

namespace Wetr.BusinessLogic
{
    public class UserManager : IUserManager
    {
        private IUserDao userDao;

        public UserManager(IUserDao userDao)
        {
            this.userDao = userDao;
        }


        #region functions

        public async Task<User> UserCredentialValidation(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return null;
            User user = null;
            try
            {
                user = await userDao.FindByEmailAsync(email);
            }
            catch (Common.Dal.Ado.MySqlException ex)
            {
                throw new BusinessSqlException(ex.Message, ex.InnerException);
            }

            if (user == null) return null;

            // will cause invalid salt version exception if not hashed with BCrypt
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                user.Password = string.Empty;
                return user;
            }

            return null;
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