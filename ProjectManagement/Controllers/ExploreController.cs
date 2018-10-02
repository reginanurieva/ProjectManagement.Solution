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
        public IActionResult Index()
        {
            List<Project> allProjects = Project.GetAll();
            return View(allProjects);
        }
    }
}
