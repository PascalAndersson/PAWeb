using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PAWeb.Entities;
using PAWeb.Handler;
using PAWeb.Models;

namespace PAWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly DatabaseContext context;
        private readonly DataHandler dataHandler;
        private readonly UserHandler userHandler;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(DatabaseContext context, DataHandler dataHandler, UserHandler userHandler, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.dataHandler = dataHandler;
            this.userHandler = userHandler;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet, Route("checkifclientisauthenticated")]
        public IActionResult CheckifClientIsAuthenticated()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Ok(true);
            else
                return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost, Route("signinadmin")]
        async public Task<IActionResult> SignInAdmin(string userName, string password)
        {
            //Eventuellt refactor till en annan metod som kollar isSignedIn..
            try
            {
                bool isAdminSignedIn = await userHandler.TrySignInAdmin(userName, password);

                if (isAdminSignedIn)
                    return Ok(true);
                else
                    return Ok(false);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost, Route("signoutadmin")]
        async public Task<IActionResult> SignOutAdmin()
        {
            await userHandler.SignOutAdmin(HttpContext.User.Identity.Name);
            return Ok("signed out.");
        }

        [AllowAnonymous]
        [HttpGet, Route("createadmin")]
        async public Task<IActionResult> CreateAdmin()
        {
            await userHandler.CreateAdmin();
            return Ok("Admin created!");
        }

        [HttpPost, Route("addproject")]
        public IActionResult AddProject(Project project)
        {
            dataHandler.AddProjectToDb(project);
            return Ok($"{project.Title} added.");
        }

        [HttpDelete, Route("removeproject")]
        public IActionResult RemoveProject(int id)
        {
            dataHandler.RemoveProjectFromDb(id);

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
