using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using ProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;


namespace ProjectManagment.Tests
{
  public static class DBConfiguration
  {
      public static string ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=project_management;Allow User Variables=true";
  }

  [TestClass]
  public class ProjectTests : IDisposable
  {
    public void Dispose()
    {
      Project.DeleteAll();
    }

    public ProjectTests()
     {
       DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=project_management_test;";
     }

     [TestMethod]
     public void GetAll_DbStartsEmpty_0()
     {
       //Arrange
       //Act
       int result = Project.GetAll().Count;

       //Assert
       Assert.AreEqual(0, result);
     }


     [TestMethod]
     public void Equals_ReturnsTrueIfNamesAreTheSame_Project()
     {
       // Arrange, Act
       Project firstProject = new Project("Planner", 1);
       Project secondProject = new Project("Planner", 1);

       // Assert
       Assert.AreEqual(firstProject, secondProject);
     }

     [TestMethod]
      public void Save_SavesToDatabase_ProjectsList()
      {
        //Arrange
        DateTime newDateTime = new DateTime(11/11/1111);
        Project testProject = new Project("Planner", "content", newDateTime, "done", 1);


        //Act
        testProject.Save();
        List<Project> result = Project.GetAll();
        List<Project> testList = new List<Project>{testProject};

        //Assert
        Assert.AreEqual(testProject, Project.GetAll()[0]);
      }
  }
}
