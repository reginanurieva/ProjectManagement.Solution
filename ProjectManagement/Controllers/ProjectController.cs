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
            Project foundProject = Project.Find(int.Parse(projectId));
            foundProject.AddTodo(newTodo);
            return RedirectToAction("Index");
        }

        [HttpGet("/projects/deleteTodo/{todoId}")]
        public IActionResult DeleteTask(int todoId)
        {
            Todo foundTodo = Todo.Find(todoId);
            foundTodo.Delete();
            return RedirectToAction("Index");
        }


        [HttpPost("/projects/updateStatus")]
        public IActionResult UpdateStatus(string newStatus, string todoId)
        {
            Todo foundTodo = Todo.Find(int.Parse(todoId));
            Todo newTodo = new Todo(foundTodo.Name, newStatus);
            foundTodo.Update(newTodo);
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
        public ActionResult EditProject(int projectId, string Name, string Content, DateTime DueDate, string Status, string Tags, string returnUrl = null)
        {
            Project foundProject = Project.Find(projectId);
            Project newProject = new Project(Name, Content, DueDate, Status);
            foundProject.Update(newProject);
            List <Tag> newTags = new List<Tag> {};
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
                newTags.Add(foundTag);
              }
            }

            foundProject.UpdateTags(newTags);
            return RedirectToAction(nameof(ProjectController.Details), projectId);
        }

        [HttpPost("/projects/leave")]
        public ActionResult LeaveProject(int projectId, int userId)
        {
            Project foundProject = Project.Find(projectId);
            ProjectManagement.Models.User foundUser = ProjectManagement.Models.User.Find(userId);
            foundUser.DeleteProject(foundProject);
            ProjectManagement.Models.User nextOwnerUser = foundProject.GetUsers()[0];
            foundProject.AssignOwner(nextOwnerUser);
            return RedirectToAction(nameof(ProjectController.Index));
        }

        [HttpPost("/projects/delete")]
        public ActionResult DeleteProject(int projectId)
        {
            Project foundProject = Project.Find(projectId);
            foundProject.Delete();
            return RedirectToAction(nameof(ProjectController.Index));
        }
    }
}
