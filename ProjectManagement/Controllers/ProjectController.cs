using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ProjectManagement.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        [HttpGet("/projects")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/projects/newTodo")]
        public IActionResult CreateTask(string todoName, string projectId)
        {
            Todo newTodo = new Todo(todoName);
            newTodo.Save();
            // Project foundProject = Project.Find(int.Parse(projectId));
            // foundProject.AddTodo(newTodo.Id);
            return RedirectToAction("Index");
        }

        [HttpGet("/projects/{projectId}")]
        public ActionResult Details(int projectId)
        {
            Project foundProject = Project.Find(projectId);
            return View(foundProject);
        }

        [HttpGet("/projects/update/{projectId}")]
        public ActionResult Edit(int projectId)
        {
            Project foundProject = Project.Find(projectId);
            return View(foundProject);
        }

        [HttpPost("/projects/update/{projectId}")]
        public ActionResult EditProject(int projectId, string Name, string Content, DateTime DueDate, string Status, string returnUrl = null)
        {
            Project foundProject = Project.Find(projectId);
            Project newProject = new Project(Name, Content, DueDate, Status);
            foundProject.Update(newProject);
            return RedirectToAction(nameof(ProjectController.Details), projectId);
        }

        [HttpPost("/projects/leave")]
        public ActionResult LeaveProject(int projectId, int userId)
        {
            Project foundProject = Project.Find(projectId);
            ProjectManagement.Models.User foundUser = ProjectManagement.Models.User.Find(userId);
            foundUser.DeleteProject(foundProject);
            return RedirectToAction(nameof(ProjectController.Index));
        }
    }
}
