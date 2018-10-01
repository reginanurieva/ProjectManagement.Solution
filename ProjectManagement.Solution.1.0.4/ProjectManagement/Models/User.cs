using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ProjectManagement;

namespace ProjectManagement.Models
{
    public class User
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
        public string Email {get; set;}

        public User (string newName, string newUsername, string newPassword, string newEmail, int id = 0)
        {
            Name = newName;
            Username = newUsername;
            Password = newPassword;
            Email = newEmail;
            Id = id;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO users (name, username, password, email) VALUES (@newName, @newUsername, @newPassword, @newEmail);";


            cmd.Parameters.AddWithValue("@newName", this.Name);
            cmd.Parameters.AddWithValue("@newUsername", this.Username);
            cmd.Parameters.AddWithValue("@newPassword", this.Password);
            cmd.Parameters.AddWithValue("@newEmail", this.Email);

            cmd.ExecuteNonQuery();
            this.Id = (int)cmd.LastInsertedId;

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<User> GetAll()
        {
            List<User> allUsers = new List<User>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM users;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string username = rdr.GetString(2);
                string password = rdr.GetString(3)
                string email = rdr.GetString(4);
                User newUser = new User(name, username, password, email, id);
                User.Add(newUser);
            }

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
            return allUsers;
        }

        public static User Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM users WHERE id = @searchId;";

            cmd.Parameters.AddWithValue("@searchId", id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            User foundUser =  new User("","","","",0);
            while(rdr.Read())
            {
                int thisid = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string username = rdr.GetString(2);
                string password = rdr.GetString(3);
                string email = rdr.GetString(4);
                foundUser = new User(name, username, password, email, id);

            }

            conn.Close();

            if(conn != null)
            {
                conn.Dispose();
            }
            return foundUser;
        }

        public void Update(string newName, string newUsername, string newPassword, string newEmail)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE users SET name = @newName, username = @newUsername, password = @newPassword, email= @newEmail WHERE id = @searchId;";

            cmd.Parameters.AddWithValue("@newName", this.Name);
            cmd.Parameters.AddWithValue("@newUsername", this.Username);
            cmd.Parameters.AddWithValue("@newPassword", this.Password);
            cmd.Parameters.AddWithValue("@newEmail", this.Email);
            cmd.Parameters.AddWithValue("@searchId", this.Id);

            this.Name = newName;
            this.Username = newUsername;
            this.Password = newPassword;
            this.Email = newEmail;
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
    }
}