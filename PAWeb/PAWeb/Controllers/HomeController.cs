using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAWeb.Entities;
using PAWeb.Handler;
using PAWeb.Models;

namespace PAWeb.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly DatabaseContext context;
        private readonly DataHandler dataHandler;

        public HomeController(DatabaseContext context, DataHandler dataHandler)
        {
            this.context = context;
            this.dataHandler = dataHandler;
        }

        [HttpGet, Route("getallprojects")]
        public IActionResult GetAllProjects()
        {
            var allProjects = context.Projects.ToArray();
            return Ok(allProjects);
        }


        [HttpPost, Route("sendemail")]
        public IActionResult SendEmail(Email email)
        {
            if (string.IsNullOrEmpty(email.CheckSpam))
            {
                dataHandler.ConvertEmailToMailKitAndSendByGmail(email);
                return Ok("Mail sent");
            }
            else
                return BadRequest();
        }
    }
}
