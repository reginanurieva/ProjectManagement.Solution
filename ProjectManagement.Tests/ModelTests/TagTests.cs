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
        public void Equals_ReturnsTrueIfTagsAreTheSame_Tag()
        {
            // Arrange, Act
            Tag firstTag = new Tag("#Planner", 1);
            Tag secondTag = new Tag("#Planner", 1);

            // Assert
            Assert.AreEqual(firstTag, secondTag);
        }

        [TestMethod]
        public void Save_SaveTag()
        {
            //Arrange
            Tag newTag = new Tag("#Skye");
            
            //Act
            newTag.Save();
            List <Tag> expectedTags = new List <Tag> {newTag};

            //Assert
            CollectionAssert.AreEqual(expectedTags, Tag.GetAll());
        }

        [TestMethod]
        public void Find_FindTagWithId_Tag()
        {
            //Arrange
            Tag newTag = new Tag("#Skye");
            newTag.Save();

            //Act
            Tag foundTag = Tag.Find(newTag.Id);

            //Assert
            Assert.AreEqual(newTag, foundTag);
        }

        [TestMethod]
        public void Find_FindTagWithName_Tag()
        {
            //Arrange
            Tag newTag = new Tag("#Skye");
            newTag.Save();

            //Act
            Tag foundTag = Tag.Find(newTag.Name);

            //Assert
            Assert.AreEqual(newTag, foundTag);
        }

        [TestMethod]
        public void Exist_CheckTagExistence_Tag()
        {
            //Arrange
            Tag newTag = new Tag("#Skye");
            newTag.Save();

            //Act
            bool resultTrue = Tag.Exist(newTag.Name);
            bool resultFalse = Tag.Exist("#Hyewon");

            //Assert
            Assert.AreEqual(true, resultTrue);
            Assert.AreEqual(false, resultFalse);
        }

        [TestMethod]
        public void Update_UpdateTag_Tag()
        {
            Tag newTag = new Tag("#Skye");
            newTag.Save();
            Tag editTag = new Tag("#Meria");
            newTag.Update(editTag);
            editTag.Id = newTag.Id;

            Assert.AreEqual(editTag, newTag);
        }

        [TestMethod]
        public void Delete_DeleteATag()
        {
            //Arrange
            Tag newTag = new Tag("#Skye");
            newTag.Save();
            Tag newTag2 = new Tag("#Meria");
            newTag2.Save();
            List <Tag> expectedTags = new List <Tag> {newTag2};

            //Act
            newTag.Delete();
            List <Tag> tags = Tag.GetAll();

            //Assert
            CollectionAssert.AreEqual(expectedTags, tags);
        }

        [TestMethod]
        public void DeleteAll_DeleteAllTags()
        {
            //Arrange
            Tag newTag = new Tag("#Skye");
            newTag.Save();
            Tag newTag2 = new Tag("#Meria");
            newTag2.Save();
            List <Tag> expectedTags = new List <Tag> {};

            //Act
            Tag.DeleteAll();
            List <Tag> tags = Tag.GetAll();

            //Assert
            CollectionAssert.AreEqual(expectedTags, tags);
        }

        [TestMethod]
        public void GetProjects_GetAllProjects_List()
        {
            //Arrange
            Tag newTag = new Tag("#Skye");
            newTag.Save();
            DateTime newDateTime = new DateTime(1234, 12, 23);
            Project firstProject = new Project("Planner", "content", newDateTime, "Done");
            firstProject.Save();
            Project secondProject = new Project("Wedding", "content", newDateTime, "Done");
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
            DateTime newDateTime = new DateTime(1234, 12, 23);
            Project firstProject = new Project("Planner", "content", newDateTime, "Done");
            firstProject.Save();
            Project secondProject = new Project("Wedding", "content", newDateTime, "Done");
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