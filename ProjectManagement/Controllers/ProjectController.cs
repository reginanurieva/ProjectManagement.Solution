using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPost("/projects/updateStatus")]
        public IActionResult UpdateStatus(string newStatus, string todoId)
        {
            Todo foundTodo = Todo.Find(int.Parse(todoId));
            Todo newTodo = new Todo(foundTodo.Name, newStatus);
            foundTodo.Update(newTodo);
            return RedirectToAction("Index");
        }
    }
}
