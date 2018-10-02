using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace ProjectManagement.Models
{
  public class Todo : ICRUDMethods <Todo>
  {
    public int Id {get; set;}
    public string Name {get; set; }
    public string Status {get; set;}

    public Todo(string name, string status = "Todo", int id = 0)
    {
      Name = name;
      Status = status;
      Id = id;
    }

    public override bool Equals(System.Object otherTodo)
    {
      if(!(otherTodo is Todo))
      {
        return false;
      }
      else
      {
        Todo newTodo = (Todo) otherTodo;
        bool idEquality = this.Id == newTodo.Id;
        bool nameEquality = this.Name == newTodo.Name;
        bool statusEquality = this.Status == newTodo.Status;
        return (idEquality && nameEquality && statusEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public void Save()
    {
     MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO todos (name, status) VALUES (@name, @status);";

      cmd.Parameters.AddWithValue("@name", this.Name);
      cmd.Parameters.AddWithValue("@status", this.Status);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Todo> GetAll()
    {
      List<Todo> allTodos = new List<Todo> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM todos;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string status = rdr.GetString(2);

        Todo newTodo = new Todo(name, status, id);
        allTodos.Add(newTodo);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allTodos;
    }

    public static Todo Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM todos WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@searchId", id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      Todo newTodo = new Todo("", "", 0);

      while (rdr.Read())
      {
        newTodo.Id = rdr.GetInt32(0);
        newTodo.Name = rdr.GetString(1);
        newTodo.Status = rdr.GetString(2);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newTodo;
    }

    public void Update (Todo newTodo)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE todos SET name = @newName, status = @newStatus WHERE id = @searchId;";
      cmd.Parameters.AddWithValue("@searchId", this.Id);
      cmd.Parameters.AddWithValue("@newName", newTodo.Name);
      cmd.Parameters.AddWithValue("@newStatus", newTodo.Status);

      cmd.ExecuteNonQuery();
      this.Name = newTodo.Name;
      this.Status = newTodo.Status;

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
    cmd.CommandText = @"INSERT INTO projects_todos (project_id, todo_id) VALUES (@ProjectId, @TodoId);";

    MySqlParameter project_id = new MySqlParameter();
    project_id.ParameterName = "@ProjectId";
    project_id.Value = newProject.Id;
    cmd.Parameters.Add(project_id);

    MySqlParameter todo_id = new MySqlParameter();
    todo_id.ParameterName = "@TodoId";
    todo_id.Value = this.Id;
    cmd.Parameters.Add(todo_id);

    cmd.ExecuteNonQuery();
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
  }


  public Project GetProject()
{
  MySqlConnection conn = DB.Connection();
  conn.Open();
  MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
  cmd.CommandText = @"SELECT projects.* FROM todos
  JOIN projects_todos ON (todos.id = projects_todos.todo_id)
  JOIN projects ON (projects_todos.project_id = projects.id)
  WHERE todos.id = @TodoId;";

  MySqlParameter todoIdParameter = new MySqlParameter();
  todoIdParameter.ParameterName = "@TodoId";
  todoIdParameter.Value = this.Id;
  cmd.Parameters.Add(todoIdParameter);

  MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
  Project newProject = new Project("", "", DateTime.Now, "");
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
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM todos WHERE id = @todoId;";
      cmd.Parameters.AddWithValue("@todoId", this.Id);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM todos;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
