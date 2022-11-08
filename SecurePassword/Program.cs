using SecurePassword;
using System.Security.Cryptography;

LoginManager login = new LoginManager();

while (true)
{
    Console.Clear();
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Create Account");

    int input = Convert.ToInt32(Console.ReadLine());

    if (input == 1)
    {
        int retries = 5;
        Console.WriteLine("Type in username");
        string usernameInput = Console.ReadLine();
        while (retries > 0)
        {
            Console.WriteLine("Type in password");
            string passwordInput = Console.ReadLine();
            if (!login.Login(usernameInput, passwordInput))
            {
                retries--;
                Console.WriteLine("Password diddnt match.. you have " + retries + " attempts left");
                if (retries == 0)
                {
                    DateTime cooldownTime = DateTime.Now.AddMinutes(3);
                    while (DateTime.Now != cooldownTime)
                    {
                        Console.WriteLine("Max attemps reached!");
                        TimeSpan time = cooldownTime - DateTime.Now;
                        Console.WriteLine("Time left: " + String.Format("{0:0#}", time.ToString()));
                        Console.Read();
                        Console.Clear();
                    }
                    retries = 5;
                }
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Successfully logged in!");
                retries = 0;
            }
        }
    }
    else if (input == 2)
    {
        Console.Clear();
        Console.WriteLine("Type in username");
        string usernameInput = Console.ReadLine();
        Console.WriteLine("Type in password");
        string passwordInput = Console.ReadLine();
        Console.WriteLine("Type in password again");
        string secondPasswordInput = Console.ReadLine();
        while (passwordInput != secondPasswordInput)
        {
            Console.WriteLine("Passwords diddnt match!");
            Console.WriteLine("Type in password");
            passwordInput = Console.ReadLine();
            Console.WriteLine("Type in password again");
            secondPasswordInput = Console.ReadLine();
        }
        login.CreateAccount(usernameInput, passwordInput);
        Console.WriteLine("Successfully created your account!");
    }
    Console.ReadLine();
}