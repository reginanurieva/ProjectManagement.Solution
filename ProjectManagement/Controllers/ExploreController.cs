using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    public class ExploreController : Controller
    {
        [HttpGet("/explore")]
        public ActionResult Index()
        {
            List<Project> allProjects = Project.GetAll();
            return View(allProjects);
        }

        [HttpPost("/explore/projects/{projectId}/users/{userId}/join")]
        public ActionResult JoinProject(int projectId, int userId, string returnUrl = null)
        {
            Project foundProject = Project.Find(projectId);
            ProjectManagement.Models.User foundUser = ProjectManagement.Models.User.Find(userId);
            foundProject.AddUser(foundUser);
            return RedirectToAction(nameof(ExploreController.Index));
        }
    }
}
