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
            Tag.DeleteAll();
            Project.DeleteAll();

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
    }
}