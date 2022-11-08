using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePassword
{
    public class LoginManager
    {
        private DatabaseHandler databaseHandler;

        public LoginManager()
        {
            databaseHandler = new DatabaseHandler();
        }

        public bool Login(string username, string password)
        {
            User user = databaseHandler.GetUser(username);
            if (user == null)
            {
                return false;
            }
            if (!CrypterService.VerifyPassword(password, user.Password, user.Salt))
            {
                return false;
            }
            HashSalt newHashSalt = CrypterService.SaltPassword(user.Password);
            databaseHandler.UpdateUser(user.Name, newHashSalt);
            return true;
        }

        public void CreateAccount(string username, string password)
        {
            HashSalt hashSalt = CrypterService.SaltPassword(password);
            databaseHandler.CreateUser(new User(username, hashSalt.Hash, hashSalt.Salt));
        }
    }
}
