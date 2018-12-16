
namespace Wetr.BusinessLogic
{
    public class UserManager
    {
        public UserManager(string databaseName)
        {
        }

        #region functions

        public bool UserCredentialValidation(string email, string password)
        {
            // hash and save a password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // check a password
            bool validPassword = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            // BCrypt.Net.BCrypt.HashPassword(password);
            return true;
        }

        #endregion functions
    }
}