using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePassword
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public User(string name, string password, string salt)
        {
            Name = name;
            Password = password;
            Salt = salt;
        }

        public User()
        {

        }
    }
}
