using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProjectManagement.Controllers;
using ProjectManagement.Models;
using System;


namespace ProjectManagement.Tests
{
  [TestClass]
  public class ProjectControllerTest : IDisposable
  {
    public void Dispose()
    {
      Project.DeleteAll();
      User.DeleteAll();
      Todo.DeleteAll();
      Tag.DeleteAll();
    }

    public ProjectControllerTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=project_management_test;";
    }

    [TestMethod]
    public void Index_TestIndexView_ViewResult()
    {
      //Arrange
      ProjectController controller = new ProjectController();

      //Act
      ViewResult result = controller.Index() as ViewResult;

      //Assert
      Assert.AreEqual("Index", result.ViewName);
    }

    [TestMethod]
    public void DeleteTask_TestDeleteTaskView_RedirectToAction()
    {
      //Arrange
      ProjectController controller = new ProjectController();
      Todo testTodo = new Todo("Add file", "In Progress");
      testTodo.Save();
      Todo newTodo = new Todo("Add more files", "Todo");
      newTodo.Save();
      List <Todo> expectedTodos = new List <Todo> {newTodo};

      //Act
      RedirectToActionResult result = controller.DeleteTask(testTodo.Id) as RedirectToActionResult;

      //Assert
      Assert.AreEqual("Index", result.ActionName);
    }

    [TestMethod]
    public void DeleteTask_TestDeleteTaskData_RedirectToAction()
    {
      //Arrange
      ProjectController controller = new ProjectController();
      Todo testTodo = new Todo("Add file", "In Progress");
      testTodo.Save();
      Todo newTodo = new Todo("Add more files", "Todo");
      newTodo.Save();
      List <Todo> expectedTodos = new List <Todo> {newTodo};

      //Act
      RedirectToActionResult result = controller.DeleteTask(testTodo.Id) as RedirectToActionResult;
      List <Todo> todos = Todo.GetAll();

      //Assert
      CollectionAssert.AreEqual(expectedTodos, todos);
    }

    [TestMethod]
    public void Details_TestDetailsView_ViewResult()
    {
      //Arrange
      ProjectController controller = new ProjectController();
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project testProject = new Project("Planner", "content", newDateTime, "Done");
      testProject.Save();

      //Act
      ViewResult result = controller.Details(testProject.Id) as ViewResult;

      //Assert
      Assert.AreEqual("Details", result.ViewName);
    }

    [TestMethod]
    public void Details_TestDetailsData_ViewResult()
    {
      //Arrange
      ProjectController controller = new ProjectController();
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project testProject = new Project("Planner", "content", newDateTime, "Done");
      testProject.Save();

      //Act
      ViewResult result = controller.Details(testProject.Id) as ViewResult;
      Project modelProject = result.ViewData.Model as Project;

      //Assert
      Assert.AreEqual(modelProject, testProject);
    }

    [TestMethod]
    public void Edit_TestEditView_ViewResult()
    {
      //Arrange
      ProjectController controller = new ProjectController();
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project testProject = new Project("Planner", "content", newDateTime, "Done");
      testProject.Save();

      //Act
      ViewResult result = controller.Edit(testProject.Id) as ViewResult;

      //Assert
      Assert.AreEqual("Edit", result.ViewName);
    }

    [TestMethod]
    public void Edit_TestEditData_ViewResult()
    {
      //Arrange
      ProjectController controller = new ProjectController();
      DateTime newDateTime = new DateTime(1234, 12, 23);
      Project testProject = new Project("Planner", "content", newDateTime, "Done");
      testProject.Save();

      //Act
      ViewResult result = controller.Edit(testProject.Id) as ViewResult;
      Project modelProject = result.ViewData.Model as Project;

      //Assert
      Assert.AreEqual(testProject, modelProject);
    }
  }
}