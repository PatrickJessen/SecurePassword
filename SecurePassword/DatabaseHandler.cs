using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePassword
{
    public class DatabaseHandler
    {
        private SqlConnection connect;
        private SqlCommand command;
        private SqlDataReader dataReader;
        private SqlDataAdapter adapter;
        private string connectionString = "Server=PJJ-P15S-2022\\SQLEXPRESS;Database=SecurePassword;Trusted_Connection=True;";


        public void CreateUser(User user)
        {
            connect = new SqlConnection(connectionString);
            connect.Open();
            string insertCmd = $"INSERT INTO Users(username, userpass, salt) VALUES('{user.Name}', '{user.Password}', '{user.Salt}')";
            command = new SqlCommand(insertCmd, connect);
            dataReader = command.ExecuteReader();
            connect.Close();
        }

        public User GetUser(string username)
        {
            connect = new SqlConnection(connectionString);
            connect.Open();
            string insertCmd = $"SELECT * FROM Users WHERE username = @name";
            command = new SqlCommand(insertCmd, connect);
            command.Parameters.AddWithValue("@name", username);
            dataReader = command.ExecuteReader();
            User myUser = new User();
            while (dataReader.Read())
            {
                myUser.Name = dataReader["username"].ToString();
                myUser.Password = dataReader["userpass"].ToString();
                myUser.Salt = dataReader["salt"].ToString();
                //myUser = new User(dataReader["username"].ToString(), dataReader["userpass"].ToString(), dataReader["salt"].ToString());
            }
            connect.Close();
            return myUser;
        }

        public void UpdateUser(string username, HashSalt hashSalt)
        {
            connect = new SqlConnection(connectionString);
            connect.Open();
            string insertCmd = $"UPDATE Users SET userpass = '{hashSalt.Hash}', salt = '{hashSalt.Salt}' WHERE username = '{username}'";
            command = new SqlCommand(insertCmd, connect);
            dataReader = command.ExecuteReader();
            connect.Close();
        }
    }
}
