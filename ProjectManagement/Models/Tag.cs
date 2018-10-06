using System;
using System.Collections.Generic;
using ProjectManagement;
using MySql.Data.MySqlClient;

namespace ProjectManagement.Models
{
    public class Tag : ICRUDMethods<Tag>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Tag(string newName, int id = 0)
        {
            Name = newName;
            Id = id;
        }

        public override bool Equals(System.Object otherTag)
        {
            if (!(otherTag is Tag))
            {
                return false;
            }
            else
            {
                Tag newTag = (Tag)otherTag;
                bool idEquality = (this.Id == newTag.Id);
                bool nameEquality = (this.Name == newTag.Name);
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

        public static Tag Find(string tag)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tags WHERE name = @searchName;";

            cmd.Parameters.AddWithValue("@searchName", tag);

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

        public void Update(Tag newTag)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE tags SET name = @newName WHERE id = @searchId;";

            cmd.Parameters.AddWithValue("@newName", newTag.Name);
            cmd.Parameters.AddWithValue("@searchId", this.Id);

            cmd.ExecuteNonQuery();

            this.Name = newTag.Name;
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
            cmd.CommandText = @"DELETE FROM projects_tags WHERE tag_id = @searchId; DELETE FROM tags WHERE id = @searchId;";
            cmd.Parameters.AddWithValue("@searchId", this.Id);
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
            cmd.CommandText = @"DELETE FROM projects_tags; DELETE FROM tags;";

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

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT projects.* FROM projects JOIN projects_tags ON projects.id = projects_tags.project_id JOIN tags ON projects_tags.tag_id = tags.id WHERE projects_tags.tag_id = @tagId;";
            cmd.Parameters.AddWithValue("@tagId", this.Id);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int ProjectId = rdr.GetInt32(0);
                string ProjectName = rdr.GetString(1);
                string ProjectContent = rdr.GetString(2);
                DateTime ProjectDueDate = rdr.GetDateTime(3);
                string ProjectStatus = rdr.GetString(4);
                Project newProject = new Project(ProjectName, ProjectContent,ProjectDueDate,ProjectStatus, ProjectId);
                allProjects.Add(newProject);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allProjects;
        }

        public void AddProject(Project newProject)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO projects_tags (project_id, tag_id) VALUES (@projectId, @tagId);";
            cmd.Parameters.AddWithValue("@projectId", newProject.Id);
            cmd.Parameters.AddWithValue("@tagId", this.Id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static bool Exist(string tag)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tags WHERE name = @searchName;";

            cmd.Parameters.AddWithValue("@searchName", tag);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            Tag foundTag = new Tag("", 0);

            if (rdr.HasRows)
            {
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
                return true;
            }
            else
            {
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
                return false;
            }
        }

    }

}