// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System;
// using MySql.Data.MySqlClient;
// using System.Collections.Generic;
// using ProjectManagement.Models;
// 
// namespace ProjectManagement.Tests
// {
//     [TestClass]
//     public class UserTests : IDisposable
//     {
//         public void Dispose()
//         {
//             User.DeleteAll();
//             Project.DeleteAll();
//         }
//         public UserTests()
//         {
//             DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=project_management_test;";
//         }
//         [TestMethod]
//         public void GetAll_DBStartsEmpty_0()
//         {
//             //Arrange
//             //Act
//             int result = User.GetAll().Count;
// 
//             //Assert
//             Assert.AreEqual(0, result);
//         }
//         [TestMethod]
//         public void Save_SaveUser()
//         {
//             //Arrange
//             //Act
//             User newUser = new User("Skye","Skye","skye@gmail.com");
//             newUser.Save();
// 
//             //Assert
//             Assert.AreEqual(newUser, User.GetAll()[0]);
//         }
//         [TestMethod]
//         public void Find_FindUser_User()
//         {
//             User newUser = new User("Skye","Skye","skye@gmail.com");
//             newUser.Save();
// 
//             User foundUser = User.Find(newUser.Id);
// 
//             Assert.AreEqual(newUser,foundUser);
//         }
//         [TestMethod]
//         public void Edit_EditUserInfo()
//         {
//             //Arrange
//             User newUser = new User("Skye","Skye","skye@gmail.com");
//             newUser.Save();
//             User expectedUser = new User("Pierre Herme","PH","ph@gmail.com");
// 
//             //Act
//             newUser.Update(expectedUser);
//             expectedUser.Id = newUser.Id;
// 
//             //Assert
//             Assert.AreEqual(expectedUser, newUser);
//         }
//         [TestMethod]
//         public void DeleteClient_DeleteAClient()
//         {
//             //Arrange
//             User newUser = new User("Skye","Skye","skye@gmail.com");
//             newUser.Save();
// 
//             //Act
//             newUser.Delete();
//             int actualCount = User.GetAll().Count;
// 
//             //Assert
//             Assert.AreEqual(0, actualCount);
//         }
//         [TestMethod]
//         public void AddProjects_SaveAndGetProject_List()
//         {
//             User newUser = new User("Skye","Skye","skye@gmail.com");
//             newUser.Save();
//             DateTime newDateTime = new DateTime(11/11/1111);
//             Project testProject = new Project("Planner", "content", newDateTime, "done");
//             testProject.Save();
//             List <Project> expectedProjects = new List<Project> {testProject};
// 
//             newUser.AddProject(testProject);
// 
//             List<Project> actualProjects = newUser.GetProjects();
// 
//             CollectionAssert.AreEqual(expectedProjects, actualProjects);
// 
//         }
//     }
// }
