using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using ProjectManagement.Models;

namespace ProjectManagement.Tests
{
    [TestClass]
    public class TagTests : IDisposable
    {
        public void Dispose()
        {
            Project.DeleteAll();
            Tag.DeleteAll();
        }
        public TagTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=project_management_test;";
        }
        [TestMethod]
        public void GetAll_DBStartsEmpty_0()
        {
            //Arrange
            //Act
            int result = Tag.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Save_SaveTag()
        {
            //Arrange
            //Act
            Tag newTag = new Tag("Skye");
            newTag.Save();

            //Assert
            Assert.AreEqual(newTag, Tag.GetAll()[0]);
        }
        [TestMethod]
        public void Find_FindTag_Tag()
        {
            //Arrange
            Tag newTag = new Tag("Skye");
            newTag.Save();

            //Act
            Tag foundTag = Tag.Find(newTag.Id);

            //Assert
            Assert.AreEqual(newTag, foundTag);
        }

        [TestMethod]
        public void Update_UpdateTag_Tag()
        {
            Tag newTag = new Tag("Skye");
            newTag.Save();

            Tag editTag = new Tag("Meria");
            newTag.Update(editTag);
            editTag.Id = newTag.Id;

            Assert.AreEqual(newTag, editTag);
        }
        [TestMethod]
        public void DeleteClient_DeleteAClient()
        {
            //Arrange
            Tag newTag = new Tag("Skye");
            newTag.Save();
        
            //Act
            newTag.Delete();
            int actualCount = User.GetAll().Count;

            //Assert
            Assert.AreEqual(0, actualCount);
        }

        [TestMethod]
        public void GetProjects_GetAllProjects_List()
        {
            //Arrange
            Tag newTag = new Tag("Skye");
            newTag.Save();
            DateTime newDateTime = new DateTime(11/11/1111);
            Project firstProject = new Project("Planner", "content", newDateTime, "done", 1);
            firstProject.Save();
            Project secondProject = new Project("Wedding", "content", newDateTime, "done", 1);
            secondProject.Save();
            newTag.AddProject(firstProject);
            newTag.AddProject(secondProject);
            List <Project> allProjects = new List<Project> {firstProject, secondProject};

            //Act
            List<Project> projects = newTag.GetProjects();

            //Assert
            CollectionAssert.AreEqual(allProjects, projects);
        }
        
        [TestMethod]
        public void AddProject_AddNewProjectToTag()
        {            
            //Arrange
            Tag newTag = new Tag("Skye");
            newTag.Save();
            DateTime newDateTime = new DateTime(11/11/1111);
            Project firstProject = new Project("Planner", "content", newDateTime, "done", 1);
            firstProject.Save();
            Project secondProject = new Project("Wedding", "content", newDateTime, "done", 1);
            secondProject.Save();
            List <Project> allProjects = new List<Project> {firstProject, secondProject};

            //Act
            newTag.AddProject(firstProject);
            newTag.AddProject(secondProject);
            List<Project> projects = newTag.GetProjects();

            //Assert
            CollectionAssert.AreEqual(allProjects, projects);
        }
    }
}