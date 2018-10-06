using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using ProjectManagement.Models;

namespace ProjectManagement.Tests
{
  [TestClass]
  public class TodoTests : IDisposable
  {
    public void Dispose()
    {
      Todo.DeleteAll();
      //User.DeleteAll();
      Project.DeleteAll();
    }

    public TodoTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=project_management_test;";
    }

    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Todo.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfTodosAreTheSame_Todo()
    {
      // Arrange, Act
      Todo firstTodo = new Todo("Fix the bug", "In Progress", 1);
      Todo secondTodo = new Todo("Fix the bug", "In Progress", 1);

      // Assert
      Assert.AreEqual(firstTodo, secondTodo);
    }

    [TestMethod]
    public void Save_SavesToDatabase_TodosList()
    {
      //Arrange
      Todo testTodo = new Todo("Add File", "In Progress");

      //Act
      testTodo.Save();
      List<Todo> result = Todo.GetAll();
      List<Todo> testList = new List<Todo>{testTodo};

      //Assert
      CollectionAssert.AreEqual(testList, Todo.GetAll());
    }

    [TestMethod]
    public void Find_FindsTodoInDB_Todo()
    {
      //Arrange
      Todo testTodo = new Todo("Add File", "In Progress");
      testTodo.Save();

      //Act
      Todo foundTodo = Todo.Find(testTodo.Id);

      //Assert
      Assert.AreEqual(testTodo, foundTodo);
    }

    [TestMethod]
    public void Update_UpdatesTodoInDB_String()
    {
      //Arrange
      Todo testTodo = new Todo("Add file", "In Progress");
      testTodo.Save();
      Todo newTodo = new Todo("Add more files", "Todo");
      newTodo.Save();

      //Act
      testTodo.Update(newTodo);
      testTodo.Id = newTodo.Id;

      //Assert
      Assert.AreEqual(newTodo, testTodo);
    }

    [TestMethod]
    public void Delete()
    {
      //Arrange
      Todo testTodo = new Todo("Add file", "In Progress");
      testTodo.Save();
      Todo newTodo = new Todo("Add more files", "Todo");
      newTodo.Save();
      List <Todo> expectedTodos = new List<Todo>{newTodo};

      //Act
      testTodo.Delete();
      List <Todo> todos = Todo.GetAll();

      //Assert
      CollectionAssert.AreEqual(expectedTodos, todos);
    }

    [TestMethod]
    public void DeleteAll()
    {
      //Arrange
      Todo testTodo = new Todo("Add file", "In Progress");
      testTodo.Save();
      Todo newTodo = new Todo("Add more files", "Todo");
      newTodo.Save();
      List <Todo> expectedTodos = new List<Todo>{};

      //Act
      Todo.DeleteAll();
      List <Todo> todos = Todo.GetAll();

      //Assert
      CollectionAssert.AreEqual(expectedTodos, todos);
    }

    [TestMethod]
    public void AddProject_ConnectProjectToTodo()
    {
      //Arrange
      Todo testTodo = new Todo("Add file", "In Progress");
      testTodo.Save();
      DateTime newDateTime = new DateTime(1234, 12, 11);
      Project testProject = new Project("Planner", "content", newDateTime, "Done", 1);
      testProject.Save();

      //Act
      testTodo.AddProject(testProject);
      Project actualProject = testTodo.GetProject();

      //Assert
      Assert.AreEqual(testProject, actualProject);
    }

    [TestMethod]
    public void GetProject_TheJointTableInDB_Todo()
    {
      //Arrange
      Todo testTodo = new Todo("Add file", "In Progress");
      testTodo.Save();
      DateTime newDateTime = new DateTime(1234, 12, 11);
      Project testProject = new Project("Planner", "content", newDateTime, "Done", 1);
      testProject.Save();
      testTodo.AddProject(testProject);

      //Act
      Project actualProject = testTodo.GetProject();

      //Assert
      Assert.AreEqual(testProject, actualProject);
    }
  }
}
