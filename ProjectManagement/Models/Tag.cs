using System;
using System.Collections.Generic;
using ProjectManagement;
using MySql.Data.MySqlClient;

namespace ProjectManagement.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Tag(string newName, int id = 0)
        {
            Name = newName;
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
                return (nameEquality && idEquality);
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO tags (name) VALUES (@newName);";

            cmd.Parameters.AddWithValue("@newName", this.Name);

            cmd.ExecuteNonQuery();
            this.Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Tag> GetAll()
        {
            List<Tag> allTags = new List<Tag> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tags;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Tag newTag = new Tag(name, id);
                allTags.Add(newTag);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allTags;
        }

        public static Tag Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tags WHERE id = @searchId;";

            cmd.Parameters.AddWithValue("@searchId", id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            Tag foundTag = new Tag("", 0);
            while (rdr.Read())
            {
                int actualId = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                foundTag = new Tag(name, actualId);
            }

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
            return foundTag;
        }

        public void Update(User newUser)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE tags SET name = @newName, WHERE id = @searchId;";

            cmd.Parameters.AddWithValue("@newName", newUser.Name);
            cmd.Parameters.AddWithValue("@searchId", newUser.Id);

            cmd.ExecuteNonQuery();

            this.Name = newUser.Name;
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
            cmd.CommandText = @"DELETE FROM tags WHERE id = @searchid;";

            cmd.Parameters.AddWithValue("@searchid", this.Id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"TRUNCATE TABLE tags;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

    }

}