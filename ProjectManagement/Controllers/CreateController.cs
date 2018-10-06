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
using ProjectManagement.Services;
using MySql.Data.MySqlClient;

namespace ProjectManagement.Controllers
{
  [Authorize]
  public class CreateController : Controller
  {
    [HttpGet]
    public ActionResult Index()
    {
      return View("Index");
    }

    [HttpPost]
    public ActionResult CreateProject(string Name, string Content, DateTime DueDate, string Tags, string UserName, string returnUrl = null)
    {
      Project newProject = new Project(Name, Content, DueDate, "In Progress");
      newProject.Save();
      ProjectManagement.Models.User currentUser = ProjectManagement.Models.User.Find(UserName);
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
  }
}
