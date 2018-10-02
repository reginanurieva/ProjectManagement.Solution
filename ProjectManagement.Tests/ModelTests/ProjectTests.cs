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
      Todo.DeleteAll();
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
    public void Find_FindsProjectInDB_Project()
    {
      //Arrange
      DateTime newDateTime = new DateTime(11/11/1111);
      Project testProject = new Project("Planner", "content", newDateTime, "done", 1);
      testProject.Save();

      //Act
      Project foundProject = Project.Find(testProject.Id);

      //Assert
      Assert.AreEqual(testProject.Name, foundProject.Name);
    }

    [TestMethod]
    public void Update_UpdatesProjectInDB_String()
    {
      //Arrange
      DateTime newDateTime = new DateTime(11/11/1111);
      Project testProject = new Project("Planner", "content", newDateTime, "done");
      testProject.Save();
      testProject.Name = "Wedding Planner";

      //Act
      testProject.Update(testProject);

      string result = Project.Find(testProject.Id).Name;

      //Assert
      Assert.AreEqual("Wedding Planner", result);
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

    [TestMethod]
    public void AddTodo_MakeNewTodo()
    {
      //Arrange
      Todo todo1 = new Todo("todo1", "done");
      todo1.Save();
      Todo todo2 = new Todo("todo2", "done");
      todo2.Save();
      Project currentProject = new Project("Current Project", "Project content", DateTime.Now, "done");
      currentProject.Save();
      List <Todo> expectedTodos = new List<Todo>{todo1, todo2};

      //Act
      currentProject.AddTodo(todo1);
      currentProject.AddTodo(todo2);
      List <Todo> todos = currentProject.GetTodos();

      //Assert
      CollectionAssert.AreEqual(expectedTodos, todos);
    }

    [TestMethod]
    public void GetTodos_GetAllTodosInProject_List()
    {
      //Arrange
      Todo todo1 = new Todo("todo1", "done");
      todo1.Save();
      Todo todo2 = new Todo("todo2", "done");
      todo2.Save();
      Project currentProject = new Project("Current Project", "Project content", DateTime.Now, "done");
      currentProject.Save();
      List <Todo> expectedTodos = new List<Todo>{todo1, todo2};
      currentProject.AddTodo(todo1);
      currentProject.AddTodo(todo2);

      //Act
      List <Todo> todos = currentProject.GetTodos();

      //Assert
      CollectionAssert.AreEqual(expectedTodos, todos);

    }
  }
}
