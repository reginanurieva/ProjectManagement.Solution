using System.Collections.Generic;
using ProjectManagement;
using MySql.Data.MySqlClient;
using System;

namespace ProjectManagement.Models
{
  public class CreateProject
  {
    public int Id { get; set; }
    public string ProjectName { get; set; }
    public string UserName { get; set; }
    public string Tags { get; set; }


    public CreateProject(string projectName, string userName, string tags, int id = 0)
    {
      ProjectName = projectName;
      UserName = userName;
      Tags = tags;
      Id = id;
    }

    public override bool Equals(System.Object otherCreateProject)
    {
      if(!(otherCreateProject is CreateProject))
      {
        return false;
      }
      else
      {
        CreateProject newCreate = (CreateProject) otherCreateProject;
        bool idEquality = this.Id == newCreate.Id;
        bool nameOneEquality = this.ProjectName == newCreate.ProjectName;
        bool nameTwoEquality = this.UserName == newCreate.UserName;
        bool tagsEquality = this.Tags == newCreate.Tags;
        return (idEquality && nameOneEquality && nameTwoEquality && tagsEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.ProjectName.GetHashCode();
    }


    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO createprojects (projectName, userName, tags) VALUES (@projectName, @userName, @tags;";

      cmd.Parameters.AddWithValue("@projectName", this.ProjectName);
      cmd.Parameters.AddWithValue("@userName", this.UserName);
      cmd.Parameters.AddWithValue("@tags", this.Tags);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    
    public static List<CreateProject> GetAll()
    {
      List<CreateProject> allCreateProjects = new List<CreateProject> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM createprojects;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string projectName = rdr.GetString(1);
        string userName = rdr.GetString(2);
        string tags = rdr.GetString(3);
        CreateProject newCreateProject = new CreateProject(projectName, userName, tags);
        allStylists.Add(newCreateProject);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }
  }
}
