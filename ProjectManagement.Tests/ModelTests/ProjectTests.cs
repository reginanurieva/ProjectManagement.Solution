using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using ProjectManagement.Models;

namespace ProjectManagement.Tests
{
  [TestClass]
  public class ProjectTests : IDisposable
  {
    public void Dispose()
    {
      Project.DeleteAll(); 
      User.DeleteAll();
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

      [TestMethod]
      public void GetUsers_GetAllAssignedUsers_List()
      {
        //Arrange
        User user1 = new User("Hyewon Cho", "jhng2525", "jhng2525@gmail.com");
        user1.Save();
        User user2 = new User("Hyeryun Cho", "jhng25252", "jhng25252@gmail.com");
        user2.Save();
        List <User> expectedUsers = new List<User>{user1, user2};
        Project currentProject = new Project("Current Project", "Project content", DateTime.Now, "done");
        currentProject.Save();
        currentProject.AddUser(user1);
        currentProject.AddUser(user2);

        //Act
        List <User> users = currentProject.GetUsers();

        //Assert
        CollectionAssert.AreEqual(expectedUsers, users);
      }

      [TestMethod]
      public void AddUser_AssignUserToProject()
      {
        //Arrange
        User user1 = new User("Hyewon Cho", "jhng2525", "jhng2525@gmail.com");
        user1.Save();
        User user2 = new User("Hyeryun Cho", "jhng25252", "jhng25252@gmail.com");
        user2.Save();
        List <User> expectedUsers = new List<User>{user1, user2};
        Project currentProject = new Project("Current Project", "Project content", DateTime.Now, "done");
        currentProject.Save();

        //Act
        currentProject.AddUser(user1);
        currentProject.AddUser(user2);
        List <User> users = currentProject.GetUsers();

        //Assert
        CollectionAssert.AreEqual(expectedUsers, users);
      }
  }
}
