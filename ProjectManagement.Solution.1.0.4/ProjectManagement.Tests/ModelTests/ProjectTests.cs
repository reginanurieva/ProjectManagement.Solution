using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using ProjectManagment.Models;


namespace ProjectManagment.Tests
{
  [TestClass]
  public class ProjectTests : IDisposable
  {
    public void Dispose()
    {
     Project.DeleteAll();
    }

    public ProjectTests()
     {
       DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=project_managment_test;";
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
  }
}
