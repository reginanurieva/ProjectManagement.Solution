using System.Collections.Generic;
using ProjectManagement;
using MySql.Data.MySqlClient;
using System;

namespace ProjectManagement.Tests
{
  [TestClass]
  CreateProjectTests : IDisposable
  
    public CreateProjectTests()
    {
    DB.Configuration.ConnectionString = "server=localhost;user id=root; port=8889; database=project_management_test; Allow User Variables=True;";
    }
    [TestMethod]
    public void GetAll_CreateProjectListEmpty_0()
    {
      //Arrange, Act
      int result = CreateProject.GetAll().Count;
      //Assert
      Assert.AreEqual(0, result);
      
    }
  }
}