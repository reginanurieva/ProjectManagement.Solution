using System.Collections.Generic;
using ProjectManagement;
using MySql.Data.MySqlClient;
using System;

namespace ProjectManagement.Models
{
  public class CreateProject
  {
    public int Id { get; set; }
    public int NumberUser { get; set; }
    public string ProjectName { get; set; }
    public string UserName { get; set; }
    public string Tags { get; set; }


    public CreateProject(string projectName, string userName, string tags, int numberUser, int id = 0)
    {
      ProjectName = projectName;
      UserName = userName;
      Tags = tags;
      NumberUser = numberUser;
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
        bool numberEquality = this.NumberUser == newCreate.NumberUser;
        bool nameOneEquality = this.ProjectName == newCreate.ProjectName;
        bool nameTwoEquality = this.UserName == newCreate.UserName;
        return (idEquality && numberEquality && nameOneEquality && nameTwoEquality);
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
      cmd.CommandText = @"INSERT INTO createproject (projectName, userName, numberUser, tags) VALUES (@projectName, @userName, @numberUser, @tags;";

      cmd.Parameters.AddWithValue("@projectName", this.ProjectName);
      cmd.Parameters.AddWithValue("@userName", this.UserName);
      cmd.Parameters.AddWithValue("@numberUser", this.NumberUser);
      cmd.Parameters.AddWithValue("@tags", this.Tags);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
