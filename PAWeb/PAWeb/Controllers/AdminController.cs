using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAWeb.Entities;
using PAWeb.Handler;
using PAWeb.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAWeb.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly DatabaseContext context;
        private readonly DataHandler dataHandler;

        public AdminController(DatabaseContext context, DataHandler dataHandler)
        {
            this.context = context;
            this.dataHandler = dataHandler;
        }

        [HttpPost, Route("addproject")]
        public IActionResult AddProject(Project project)
        {
            context.AddProjectToDb(project);
            return Ok($"{project.Title} added.");
        }

        [HttpDelete, Route("removeproject")]
        public IActionResult RemoveProject(int id)
        {
            var projectToRemove = context.GetProjectById(id);

            context.Remove(projectToRemove);
            context.SaveChanges();

            return Ok("Project removed.");
        }

        [HttpPut, Route("editproject")]
        public IActionResult EditProject(Project project)
        {
            context.Update(project);
            context.SaveChanges();

            return Ok($"Project: {project.Id} updated.");
        }

        [HttpPut, Route("image")]
        public IActionResult EditImage(IFormFile image)
        {
            dataHandler.SaveProjectImage(image);
            return Ok();
        }

    }
}
