// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System;
// using MySql.Data.MySqlClient;
// using System.Collections.Generic;
// using ProjectManagement.Models;
// 
// namespace ProjectManagement.Tests
// {
//   [TestClass]
//   public class TodoTests : IDisposable
//   {
//     public void Dispose()
//     {
//       Todo.DeleteAll();
//       //User.DeleteAll();
//       Project.DeleteAll();
//     }
// 
//     public TodoTests()
//     {
//       DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=project_management_test;";
//     }
// 
//     [TestMethod]
//     public void GetAll_DbStartsEmpty_0()
//     {
//       //Arrange
//       //Act
//       int result = Todo.GetAll().Count;
// 
//       //Assert
//       Assert.AreEqual(0, result);
//     }
// 
// 
//     [TestMethod]
//     public void Equals_ReturnsTrueIfNamesAreTheSame_Project()
//     {
//       // Arrange, Act
//       Todo firstTodo = new Todo("Fix the bug", "in progress");
//       Todo secondTodo = new Todo("Fix the bug", "in progress");
// 
//       // Assert
//       Assert.AreEqual(firstTodo, secondTodo);
//     }
// 
//     [TestMethod]
//     public void Save_SavesToDatabase_TodosList()
//     {
//       //Arrange
//       Todo testTodo = new Todo("Add File", "in progress");
// 
// 
//       //Act
//       testTodo.Save();
//       List<Todo> result = Todo.GetAll();
//       List<Todo> testList = new List<Todo>{testTodo};
// 
//       //Assert
//       Assert.AreEqual(testTodo, Todo.GetAll()[0]);
//     }
// 
// 
//     [TestMethod]
//     public void Find_FindsTodoInDB_Todo()
//     {
//       //Arrange
//       Todo testTodo = new Todo("Add File", "in progress");
//       testTodo.Save();
// 
//       //Act
//       Todo foundTodo = Todo.Find(testTodo.Id);
// 
//       //Assert
//       Assert.AreEqual(testTodo.Name, foundTodo.Name);
//     }
// 
//     [TestMethod]
//     public void Update_UpdatesTodoInDB_String()
//     {
//       //Arrange
//       Todo testTodo = new Todo("Add file", "in progress");
//       testTodo.Save();
//       testTodo.Name = "Add more files";
// 
//       //Act
//       testTodo.Update(testTodo);
// 
//       string result = Todo.Find(testTodo.Id).Name;
// 
//       //Assert
//       Assert.AreEqual("Add more files", result);
//     }
// 
// 
//     [TestMethod]
//     public void Get_TheJointTableInDB_Todo()
//     {
//       //Arrange
//       Todo testTodo = new Todo("Add file", "in progress");
//       testTodo.Save();
//       DateTime newDateTime = new DateTime(11/11/1111);
//       Project testProject = new Project("Planner", "content", newDateTime, "done", 1);
//       testProject.Save();
// 
//       testTodo.AddProject(testProject);
// 
//       //Act
//       Project actualProject = testTodo.GetProject();
// 
//       //Assert
//       Assert.AreEqual(testProject, actualProject);
// 
//     }
//   }
// }
