using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using ProjectManagement.Models;

namespace ProjectManagement.Tests
{
    [TestClass]
    public class UserTests : IDisposable
    {
        public void Dispose()
        {
            User.DeleteAll();
            Project.DeleteAll();
        }

        public UserTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=project_management_test;";
        }

        [TestMethod]
        public void Equal_ReturnsTrueIfUsersAreTheSame_User()
        {
            // Arrange, Act
            User newUser = new User("Skye Nguyen","Skye","skye@gmail.com", 1);
            User newUser2 = new User("Skye Nguyen","Skye","skye@gmail.com", 1);

            // Assert
            Assert.AreEqual(newUser, newUser2);
        }

        [TestMethod]
        public void GetAll_DBStartsEmpty_0()
        {
            //Arrange
            //Act
            int result = User.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SaveUser()
        {
            //Arrange
            User newUser = new User("Skye","Skye","skye@gmail.com");

            //Act
            newUser.Save();
            List <User> expectedUsers = new List <User> {newUser};
            List <User> users = User.GetAll();

            //Assert
            CollectionAssert.AreEqual(expectedUsers, users);
        }

        [TestMethod]
        public void Find_FindUserById_User()
        {
            //Arrange
            User newUser = new User("Skye","Skye","skye@gmail.com");
            newUser.Save();

            //Act
            User foundUser = User.Find(newUser.Id);

            //Assert
            Assert.AreEqual(newUser,foundUser);
        }

        [TestMethod]
        public void Find_FindUserByUserName_User()
        {
            //Arrange
            User newUser = new User("Skye","Skye","skye@gmail.com");
            newUser.Save();

            //Act
            User foundUser = User.Find(newUser.Username);
            
            //Assert
            Assert.AreEqual(newUser,foundUser);
        }

        [TestMethod]
        public void GetAll_GetAllUsers_List()
        {
            //Arrange
            User newUser = new User("Skye Nguyen","Skye","skye@gmail.com");
            newUser.Save();
            User newUser2 = new User("Pierre Herme","PH","ph@gmail.com");
            newUser2.Save();
            List <User> expectedUsers = new List<User> {newUser, newUser2};

            //Act
            List <User> users = User.GetAll();

            //Assert
            CollectionAssert.AreEqual(expectedUsers, users);
        }

        [TestMethod]
        public void Exist_CheckUserExistanceByUserName_bool()
        {
            //Arrange
            User newUser = new User("Skye Nguyen","Skye","skye@gmail.com");
            newUser.Save();

            //Act
            bool resultTrue = User.Exist("Skye");
            bool resultFalse = User.Exist("Pierre Herme");

            //Assert
            Assert.AreEqual(true, resultTrue);
            Assert.AreEqual(false, resultFalse);
        }

        [TestMethod]
        public void Update_EditUserInfo()
        {
            //Arrange
            User newUser = new User("Skye","Skye","skye@gmail.com");
            newUser.Save();
            User expectedUser = new User("Pierre Herme","PH","ph@gmail.com");
            expectedUser.Save();

            //Act
            newUser.Update(expectedUser);
            expectedUser.Id = newUser.Id;

            //Assert
            Assert.AreEqual(expectedUser, newUser);
        }

        [TestMethod]
        public void DeleteUser_DeleteAUser()
        {
            //Arrange
            User newUser = new User("Skye","Skye","skye@gmail.com");
            newUser.Save();
            User expectedUser = new User("Pierre Herme","PH","ph@gmail.com");
            expectedUser.Save();
            List <User> expectedUsers = new List<User> {expectedUser};

            //Act
            newUser.Delete();
            List <User> users = User.GetAll();

            //Assert
            CollectionAssert.AreEqual(expectedUsers, users);
        }

        [TestMethod]
        public void DeleteAll_DeleteAllUser()
        {
            //Arrange
            User newUser = new User("Skye","Skye","skye@gmail.com");
            newUser.Save();
            User expectedUser = new User("Pierre Herme","PH","ph@gmail.com");
            expectedUser.Save();
            List <User> expectedUsers = new List<User> {};

            //Act
            User.DeleteAll();
            List <User> users = User.GetAll();

            //Assert
            CollectionAssert.AreEqual(expectedUsers, users);
        }

        [TestMethod]
        public void AddProjects_SaveAndGetProject()
        {
            //Arrange
            User newUser = new User("Skye","Skye","skye@gmail.com");
            newUser.Save();
            DateTime newDateTime = new DateTime(1234, 12, 23);
            Project testProject = new Project("Planner", "content", newDateTime, "done");
            testProject.Save();
            List <Project> expectedProjects = new List<Project> {testProject};

            //Act
            newUser.AddProject(testProject);
            List<Project> actualProjects = newUser.GetProjects();

            //Assert
            CollectionAssert.AreEqual(expectedProjects, actualProjects);
        }

        [TestMethod]
        public void GetProjects_GetAllProjectsOfThisUser_List()
        {
            //Arrange
            User newUser = new User("Skye","Skye","skye@gmail.com");
            newUser.Save();
            DateTime newDateTime = new DateTime(1234, 12, 23);
            Project testProject = new Project("Planner", "content", newDateTime, "done");
            testProject.Save();
            List <Project> expectedProjects = new List<Project> {testProject};
            newUser.AddProject(testProject);

            //Act
            List<Project> actualProjects = newUser.GetProjects();

            //Assert
            CollectionAssert.AreEqual(expectedProjects, actualProjects);
        }

        [TestMethod]
        public void DeleteProject_LeaveProject()
        {
            //Arrange
            User newUser = new User("Skye","Skye","skye@gmail.com");
            newUser.Save();
            DateTime newDateTime = new DateTime(1234, 12, 23);
            Project testProject = new Project("Planner", "content", newDateTime, "done");
            testProject.Save();
            List <Project> expectedProjects = new List<Project> {};
            newUser.AddProject(testProject);

            //Act
            newUser.DeleteProject(testProject);
            List <Project> projects = newUser.GetProjects();

            //Assert
            CollectionAssert.AreEqual(expectedProjects, projects);
        }

        [TestMethod]
        public void SetOwner_SetUserAsOwnerOfProject()
        {
            //Arrange
            User newUser1 = new User("Skye","Skye","skye@gmail.com");
            newUser1.Save();
            DateTime newDateTime = new DateTime(1234, 12, 23);
            Project testProject1 = new Project("Planner1", "content", newDateTime, "done");
            testProject1.Save();
            Project testProject2 = new Project("Planner2", "content", newDateTime, "done");
            testProject2.Save();
            List <Project> expectedProjects = new List<Project> {testProject1};

            //Act
            newUser1.AddProject(testProject1);
            newUser1.AddProject(testProject2);
            newUser1.SetOwner(testProject1);
            List<Project> projects = newUser1.GetOwnerProjects();

            //Assert
            CollectionAssert.AreEqual(expectedProjects, projects);
        }

        [TestMethod]
        public void GetOwnerProjects_GetProjectsWhereUserIsOwner_List()
        {
            //Arrange
            User newUser1 = new User("Skye","Skye","skye@gmail.com");
            newUser1.Save();
            DateTime newDateTime = new DateTime(1234, 12, 23);
            Project testProject1 = new Project("Planner1", "content", newDateTime, "done");
            testProject1.Save();
            Project testProject2 = new Project("Planner2", "content", newDateTime, "done");
            testProject2.Save();
            List <Project> expectedProjects = new List<Project> {testProject1};
            newUser1.AddProject(testProject1);
            newUser1.AddProject(testProject2);
            newUser1.SetOwner(testProject1);

            //Act
            List<Project> projects = newUser1.GetOwnerProjects();

            //Assert
            CollectionAssert.AreEqual(expectedProjects, projects);
        }
    }
}
