using System;
using System.Collections.Generic;
using ProjectManagement;
using MySql.Data.MySqlClient;

namespace ProjectManagement.Models
{
    public class User : ICRUDMethods<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public User(string newName, string newUsername, string newEmail, int id = 0)
        {
            Name = newName;
            Username = newUsername;
            Email = newEmail;
            Id = id;
        }

        public override bool Equals(System.Object otherUser)
        {
            if (!(otherUser is User))
            {
                return false;
            }
            else
            {
                User newUser = (User)otherUser;
                bool idEquality = (this.Id == newUser.Id);
                bool nameEquality = (this.Name == newUser.Name);
                bool usernameEquality = (this.Username == newUser.Username);
                bool emailEquality = (this.Email == newUser.Email);
                return (nameEquality && usernameEquality && emailEquality && idEquality);
            }
        }

        public override int GetHashCode()
        {
            string allHash = this.Name.ToString() + this.Username.ToString() + this.Email.ToString();

            return allHash.GetHashCode();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO users (name, username, email) VALUES (@newName, @newUsername, @newEmail);";


            cmd.Parameters.AddWithValue("@newName", this.Name);
            cmd.Parameters.AddWithValue("@newUsername", this.Username);
            cmd.Parameters.AddWithValue("@newEmail", this.Email);

            cmd.ExecuteNonQuery();
            this.Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<User> GetAll()
        {
            List<User> allUsers = new List<User> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM users;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string username = rdr.GetString(2);
                string email = rdr.GetString(3);
                User newUser = new User(name, username, email, id);
                allUsers.Add(newUser);
            }

            conn.Close();
            if (conn != null)
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

            User foundUser = new User("", "", "", 0);
            while (rdr.Read())
            {
                int actualId = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string username = rdr.GetString(2);
                string email = rdr.GetString(3);
                foundUser = new User(name, username, email, actualId);
            }

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
            return foundUser;
        }

        public static User Find(string username)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM users WHERE username = @username;";

            cmd.Parameters.AddWithValue("@username", username);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            User foundUser = new User("", "", "", 0);
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string email = rdr.GetString(3);
                foundUser = new User(name, username, email, id);
            }

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
            return foundUser;
        }

        public void Update(User newUser)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE users SET name = @newName, username = @newUsername, email= @newEmail WHERE id = @searchId;";

            cmd.Parameters.AddWithValue("@newName", newUser.Name);
            cmd.Parameters.AddWithValue("@newUsername", newUser.Username);
            cmd.Parameters.AddWithValue("@newEmail", newUser.Email);
            cmd.Parameters.AddWithValue("@searchId", newUser.Id);

            cmd.ExecuteNonQuery();

            this.Name = newUser.Name;
            this.Username = newUser.Username;
            this.Email = newUser.Email;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM users WHERE id = @searchid;";

            cmd.Parameters.AddWithValue("@searchid", this.Id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddProject(Project newProject)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO projects_users (project_id, user_id) VALUES (@projectId, @userId);";

            cmd.Parameters.AddWithValue("@projectId", newProject.Id);
            cmd.Parameters.AddWithValue("@userId", this.Id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Project> GetProjects()
        {
            List<Project> allProjects = new List<Project> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT projects.* FROM users JOIN projects_users ON (users.id = projects_users.user_id) JOIN projects ON (projects_users.project_id = projects.id) WHERE users.id = @searchId;";

            cmd.Parameters.AddWithValue("@searchId", this.Id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string content = rdr.GetString(2);
                DateTime duedate = rdr.GetDateTime(3);
                string status = rdr.GetString(4);
                Project newProject = new Project(name, content, duedate, status, this.Id);
                allProjects.Add(newProject);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allProjects;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"TRUNCATE TABLE users;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}