using System.Collections.Generic;
using ProjectManagement;
using MySql.Data.MySqlClient;
using System;

namespace ProjectManagement.Models
{
  public class Project : ICRUDMethods<Project>
  {
    public string Name {get; set;}
    public string Content {get; set;}
    public DateTime DueDate {get; set;}
    public string Status {get; set;}
    public int Id {get; set;}

    public Project(string newName, string newContent, DateTime newDueDate, string newStatus, int id = 0)
    {
      this.Name = newName;
      this.Content = newContent;
      this.DueDate = newDueDate;
      this.Status = newStatus;
      this.Id = id;
    }

    public Project(string newName, int id = 0)
    {
      this.Name = newName;
      this.Id = id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO projects (name, content, dueDate, status) VALUES (@name, @content, @dueDate, @status);";

      cmd.Parameters.AddWithValue("@name", this.Name);
      cmd.Parameters.AddWithValue("@content", this.Content);
      cmd.Parameters.AddWithValue("@dueDate", this.DueDate);
      cmd.Parameters.AddWithValue("@status", this.Status);

      cmd.ExecuteNonQuery();
      this.Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Project> GetAll()
    {
      List<Project> allProjects = new List<Project> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM projects;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
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

    public static Project Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM projects WHERE id = @searchId;";

      cmd.Parameters.AddWithValue("@searchId", id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      Project newProject = new Project("", "", DateTime.Now, "", id);

      while(rdr.Read())
      {
        newProject.Id = rdr.GetInt32(0);
        newProject.Name = rdr.GetString(1);
        newProject.Content = rdr.GetString(2);
        newProject.DueDate = rdr.GetDateTime(3);
        newProject.Status = rdr.GetString(4);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return newProject;
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM projects_users WHERE project_id = @ProjectId; DELETE FROM projects_tags WHERE project_id = @ProjectId; DELETE FROM projects_todos WHERE project_id = @ProjectId; DELETE FROM projects_forums WHERE project_id = @ProjectId; DELETE FROM projects_owners WHERE project_id = @ProjectId; DELETE FROM projects WHERE id = @ProjectId;";
      cmd.Parameters.AddWithValue("@ProjectId", this.Id);

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
      cmd.CommandText = @"DELETE FROM projects_users; DELETE FROM projects_todos; DELETE FROM projects_tags; DELETE FROM projects_owners; DELETE FROM projects;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Update(Project newProject)
    {
      //string newName, string newContent, DateTime newDueDate, string newStatus
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE projects SET name = @newName, content = @newContent, duedate = @newDueDate, status = @newStatus WHERE id = @searchId;";

      cmd.Parameters.AddWithValue("@searchId", this.Id);
      cmd.Parameters.AddWithValue("@newName", newProject.Name);
      cmd.Parameters.AddWithValue("@newContent", newProject.Content);
      cmd.Parameters.AddWithValue("@newDueDate", newProject.DueDate);
      cmd.Parameters.AddWithValue("@newStatus", newProject.Status);

      cmd.ExecuteNonQuery();
      this.Name = newProject.Name;
      this.Content = newProject.Content;
      this.DueDate = newProject.DueDate;
      this.Status = newProject.Status;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherProject)
    {
      if (!(otherProject is Project))
      {
        return false;
      }
      else
      {

        Project newProject = (Project) otherProject;
        bool idEquality = (this.Id == newProject.Id);
        bool nameEquality = (this.Name == newProject.Name);
        bool contentEquality = (this.Content == newProject.Content);
        bool duedateEquality = (this.DueDate == newProject.DueDate);
        bool statusEquality = (this.Status == newProject.Status);
        return (idEquality && nameEquality && contentEquality && duedateEquality && statusEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public List <User> GetUsers()
    {
      List <User> allUsers = new List <User> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT users.* FROM projects JOIN projects_users ON projects.id = projects_users.project_id JOIN users ON projects_users.user_id = users.id WHERE project_id = @projectId;";
      cmd.Parameters.AddWithValue("@projectId", this.Id);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string username = rdr.GetString(2);
        string email = rdr.GetString(3);
        User foundUser = new User(name, username, email, id);
        allUsers.Add(foundUser);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allUsers;
    }

    public void AddUser(User newUser)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO projects_users (project_id, user_id) VALUES (@projectId, @userId);";
      cmd.Parameters.AddWithValue("@projectId", this.Id);
      cmd.Parameters.AddWithValue("@userId", newUser.Id);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Todo> GetTodos()
    {
      List <Todo> allTodos = new List <Todo> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT todos.id, todos.name, todos.status FROM projects JOIN projects_todos ON projects.id = projects_todos.project_id JOIN todos ON projects_todos.todo_id = todos.id WHERE project_id = @projectId;";
      cmd.Parameters.AddWithValue("@projectId", this.Id);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string status = rdr.GetString(2);
        Todo todo = new Todo(name, status, id);
        allTodos.Add(todo);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allTodos;
    }

    public void AddTodo(Todo newTodo)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO projects_todos (project_id, todo_id) VALUES (@projectId, @todoId);";
      cmd.Parameters.AddWithValue("@projectId", this.Id);
      cmd.Parameters.AddWithValue("@todoId", newTodo.Id);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Tag> GetTags()
    {
      List <Tag> allTags = new List <Tag> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT tags.* FROM projects JOIN projects_tags ON projects.id = projects_tags.project_id JOIN tags ON projects_tags.tag_id = tags.id WHERE project_id = @projectId;";
      cmd.Parameters.AddWithValue("@projectId", this.Id);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
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

    public void AddTag(Tag newTag)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO projects_tags (project_id, tag_id) VALUES (@projectId, @tagId);";
      cmd.Parameters.AddWithValue("@projectId", this.Id);
      cmd.Parameters.AddWithValue("@tagId", newTag.Id);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void DeleteTags()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM projects_tags WHERE project_id = @projectId;";
      cmd.Parameters.AddWithValue("@projectId", this.Id);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void UpdateTags(List<Tag> newTags)
    {
      this.DeleteTags();
      foreach(Tag newTag in newTags)
      {
        this.AddTag(newTag);
      }
    }

    public void AssignOwner(User user)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO projects_owners (project_id, user_id) VALUES (@projectId, @userId);";
      cmd.Parameters.AddWithValue("@projectId", this.Id);
      cmd.Parameters.AddWithValue("@userId", user.Id);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public User GetOwner()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT users.* FROM projects JOIN projects_owners ON projects.id = projects_owners.project_id JOIN users ON projects_owners.user_id = users.id WHERE project_id = @projectId;";
      cmd.Parameters.AddWithValue("@projectId", this.Id);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

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
  }
}
