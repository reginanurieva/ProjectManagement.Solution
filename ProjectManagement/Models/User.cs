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


            MySqlParameter name = new MySqlParameter();
            newName.ParameterName = "@newName";
            newName.Value = this.Name;
            cmd.Parameters.Add(newName);

            MySqlParameter newUsername = new MySqlParameter();
            newUsername.ParameterName = "@newUsername";
            newUsername.Value = this.Username;
            cmd.Parameters.Add(newUsername);

            MySqlParameter newPassword = new MySqlParameter();
            newPassword.ParameterName = "@newPassword";
            newPassword.Value = this.Password;
            cmd.Parameters.Add(newPassword);

            MySqlParameter newEmail = new MySqlParameter();
            newEmail.ParameterName = "@newEmail";
            newEmail.Value = this.Email;
            cmd.Parameters.Add(newEmail);

            cmd.ExecuteNonQuery();
            this.Id = (int)cmd.LastInsertedId;

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
    }
}