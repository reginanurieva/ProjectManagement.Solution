using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectManagement.Models;
// using ProjectManagement.Models.CreateViewModels;
using ProjectManagement.Services;
using MySql.Data.MySqlClient;

namespace ProjectManagement.Controllers
{
  public class CreateController : Controller
  {
    [HttpGet]
    public ActionResult Index()
    {
      return View();
    }
    
    [HttpPost]
    public ActionResult CreateProject(string returnUrl = null, string Name, string Content, DateTime DueDate)
    {
      var user = await GetCurrentUserAsync();

      if (user == null)
      {
          return View("Error");
      }

      Project newProject = new Project(Name, Content, DueDate, "Undone");
      newProject.Save();
      ProjectManagement.Models.User currentUser = ProjectManagement.Models.User.Find(user.UserName);
      newProject.AddUser(currentUser);
      return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    private Task<ApplicationUser> GetCurrentUserAsync()
    {
      return _userManager.GetUserAsync(HttpContext.User);
    }
  }
}