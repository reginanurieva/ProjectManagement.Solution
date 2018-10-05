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
  [Authorize]
  public class CreateController : Controller
  {
    private UserManager<ApplicationUser> _userManager;

    public CreateController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(string Name, string Content, DateTime DueDate, string Tags, string returnUrl = null)
    {
      var user = await GetCurrentUserAsync();

      if (user == null)
      {
          return View("Error");
      }

      Project newProject = new Project(Name, Content, DueDate, "In Progress");
      newProject.Save();
      ProjectManagement.Models.User currentUser = ProjectManagement.Models.User.Find(user.UserName);
      newProject.AddUser(currentUser);
      newProject.AssignOwner(currentUser);

      if (Tags != "emptyTag")
      {
        Tags = Tags.Trim();
        string [] tags = Tags.Split(' ');
        foreach (string tag in tags)
        {
          Tag foundTag;
          if (!Tag.Exist(tag))
          {
            foundTag = new Tag(tag);
            foundTag.Save();
          }
          else {
            foundTag = Tag.Find(tag);
          }
          newProject.AddTag(foundTag);
        }
      }

      return RedirectToAction(nameof(ProjectController.Index), "Project");
    }

    private Task<ApplicationUser> GetCurrentUserAsync()
    {
      return _userManager.GetUserAsync(HttpContext.User);
    }
  }
}
