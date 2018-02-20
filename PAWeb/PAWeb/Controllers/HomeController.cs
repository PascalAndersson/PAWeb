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
            var allProjects = context.Projects.ToArray();
            return Ok(allProjects);
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

        [HttpPost, Route("sendemail")]
        public IActionResult SendEmail(Email email)
        {
            context.ConvertEmailToMailKitAndSendByGmail(email);
            return Ok("Mail sent");
        }
    }
}
