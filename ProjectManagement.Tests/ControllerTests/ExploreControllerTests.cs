using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProjectManagement.Controllers;
using ProjectManagement.Models;
using System;


namespace ProjectManagement.Tests
{
  [TestClass]
  public class ExploreControllerTest : IDisposable
  {
    public ExploreControllerTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=project_management_test;";
    }

    public void Dispose()
    {
      Project.DeleteAll();
      User.DeleteAll();
      Todo.DeleteAll();
      Tag.DeleteAll();
    }

    [TestMethod]
    public void Index_TestIndexView_ViewResult()
    {
      //Arrange
      ExploreController controller = new ExploreController();

      //Act
      ViewResult result = controller.Index() as ViewResult;

      //Assert
      Assert.AreEqual("Index", result.ViewName);
    }

    [TestMethod]
    public void Index_TestIndexData_ViewResult()
    {
      //Arrange
      ExploreController controller = new ExploreController();
      DateTime testDateTime = new DateTime(1234, 12, 23);
      Project testProject = new Project("Planner", "content", testDateTime, "Done");
      testProject.Save();
      DateTime newDateTime = new DateTime(2018, 6, 7);
      Project newProject = new Project("Wedding Planner", "new content", newDateTime, "In Progress");
      newProject.Save();
      List <Project> expectedProjects = new List<Project> {testProject, newProject};

      //Act
      ViewResult result = controller.Index() as ViewResult;
      List <Project> allProjects = result.ViewData.Model as List <Project>;

      //Assert
      CollectionAssert.AreEqual(expectedProjects, allProjects);
    }
  }
}