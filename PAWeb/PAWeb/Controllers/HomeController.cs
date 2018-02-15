using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PAWeb.Entities;
using PAWeb.Models;

namespace PAWeb.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly DatabaseContext context;

        public HomeController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet, Route("getallprojects")]
        public IActionResult GetAllProjects()
        {
            var allProjects = context.Projects;
            return Ok(allProjects);
        }

        [HttpPost, Route("addproject")]
        public IActionResult AddProject(Project project)
        {
            context.AddProjectToDb(project);
            return Ok(project.Title);
        }
    }
}
