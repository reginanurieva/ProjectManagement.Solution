using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProjectManagement.Controllers;
using ProjectManagement.Models;


namespace ProjectManagement.Tests
{
  [TestClass]
  public class CreateControllerTest
  {
    [TestMethod]
    public void Index_TestIndexView_ViewResult()
    {
      //Arrange
      CreateController controller = new CreateController();

      //Act
      ViewResult result = controller.Index() as ViewResult;

      //Assert
      Assert.AreEqual("Index", result.ViewName);
    }
  }
}