using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Controllers
{
    public class ProjectController : Controller
    {
        [HttpGet("/project")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
