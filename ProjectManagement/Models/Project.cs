using System.Collections.Generic;
using ProjectManagment;
using System;

namespace ProjectManagment.Models
{
  public class Project
  {
    public string name {get; set;}
    public string content {get; set;}
    public DateTime dueDate {get; set;}
    public string status {get; set;}
    public int id {get; set;}

    public Project(string newName, string newContent, DateTime newDueDate, string newStatus, Id = 0)
    {
      name = newName;
      content = newContent;
      dueDate = newDueDate;
      status = status;
      id = id;
    }


    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO projects (name, content, dueDate, status) VALUES (@name, @content, @dueDate, @status);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter content = new MySqlParameter();
      content.ParameterName = "@content";
      content.Value = this._content;
      cmd.Parameters.Add(content);

      MySqlParameter dueDate = new MySqlParameter();
      dueDate.ParameterName = "@dueDate";
      dueDate.Value = this._dueDate;
      cmd.Parameters.Add(dueDate);

      MySqlParameter status = new MySqlParameter();
      status.ParameterName = "@status";
      status.Value = this._status;
      cmd.Parameters.Add(status);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
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
        DateTime ProjectDueDate = rdr.GetString(3);
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
      cmd.CommandText = @"SELECT * FROM projects WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int ProjectId = 0;
      string ProjectName = "";
      string ProjectContent = "";
      DateTime ProjectDueDate = DateTime.Now;//??????
      string ProjectStatus = "";

      while(rdr.Read())
      {
        ProjectId = rdr.GetInt32(0);
        ProjectName = rdr.GetString(1);
        ProjectContent = rdr.GetString(2);
        ProjectDueDate = rdr.GetDateTime(3);
        ProjectStatus = rdr.GetString(4);
      }
      Project newProject = new Project(ProjectName,ProjectContent,ProjectDueDate, ProjectStatus, ProjectId);
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

      MySqlCommand cmd = new MySqlCommand("DELETE FROM projects WHERE id = @ProjectId; DELETE FROM projects_users WHERE project_id = @ProjectId; DELETE FROM projects_tags WHERE project_id = @ProjectId;DELETE FROM projects_todos WHERE project_id = @ProjectId;DELETE FROM projects_forums WHERE project_id = @ProjectId;", conn);
      MySqlParameter projectIdParameter = new MySqlParameter();
      projectIdParameter.ParameterName = "@ProjectId";
      projectIdParameter.Value = this.GetId();

      cmd.Parameters.Add(projectIdParameter);
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
      cmd.CommandText = @"DELETE FROM projects;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


    public void Update(string newName, string newContent, DateTime newDueDate, string newStatus)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      MySqlParameter content = new MySqlParameter();
      content.ParameterName = "@newContent";
      content.Value = newContent;
      cmd.Parameters.Add(content);

      MySqlParameter dueDate = new MySqlParameter();
      dueDate.ParameterName = "@newDueDate";
      dueDate.Value = newDueDate;
      cmd.Parameters.Add(dueDate);

      MySqlParameter status = new MySqlParameter();
      status.ParameterName = "@newStatus";
      status.Value = newStatus;
      cmd.Parameters.Add(status);


      cmd.ExecuteNonQuery();
      _name = newName;
      _content = newContent;
      _dueDate = newDueDate;
      _status = newStatus;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
