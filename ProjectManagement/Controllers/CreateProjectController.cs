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
  public class CreateProjectController : Controller
  {
    [HttpGet("/createprojects")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet ("/createprojects/new")]
    public ActionResult CreateForm() 
    {
      return View();
    }

    [HttpPost ("/createprojects")]
    public ActionResult CreateProject (string projectName) 
    {
      CreateProject newCreateProject = new CreateProject(projectName);
      newCreateProject.Save();
      List<CreateProject> allCreateProjects = CreateProject.GetAll();
      return RedirectToAction("Index", allCreateProjects);
    }
  }
}
