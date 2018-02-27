using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using PAWeb.Entities;
using PAWeb.Extensions;
using PAWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PAWeb.Handler
{
    public class DataHandler
    {
        private readonly StringExtensions stringExtensions;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly DatabaseContext databaseContext;

        public DataHandler(StringExtensions stringExtensions, IHostingEnvironment hostingEnvironment, DatabaseContext databaseContext)
        {
            this.stringExtensions = stringExtensions;
            this.hostingEnvironment = hostingEnvironment;
            this.databaseContext = databaseContext;

        }

        public void AddProjectToDb(Project projectToAdd)
        {
            databaseContext.Projects.Add(projectToAdd);
            databaseContext.SaveChanges();
        }

        public Project GetProjectById(int id)
        {
            var project = databaseContext.Projects.SingleOrDefault(p => p.Id == id);
            return project;
        }

        public void AddImageUrlToDbProperty(int id, string fileName)
        {
            var project = GetProjectById(id);
            project.ImageUrl = fileName;

            databaseContext.Update(project);
            databaseContext.SaveChanges();
        }

        public void ConvertEmailToMailKitAndSendByGmail(Email email)
        {
            string receiver = "anderssonpascal@gmail.com";

            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToShortTimeString();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("PascalAndersson.com", email.Sender));
            message.To.Add(new MailboxAddress("Pascal", receiver));
            message.Subject = email.Subject;

            message.Body = new TextPart("html")
            {
                Text = $"<h3>Message from: " + email.Sender + "</h3> <br/> <p>" + email.Message + "</p>"

            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("anderssonpascal@gmail.com", "ndejxusjoribkolr");

                client.Send(message);
                client.Disconnect(true);
            }
        }

        public void SaveProjectImage(IFormFile image)
        {
            int id = stringExtensions.GetIdFromFileName(image);

            var uploads = Path.Combine(hostingEnvironment.WebRootPath, @"img\project_images");
            if (image.Length > 0)
            {
                var filePath = Path.Combine(uploads, $"{id}{Path.GetExtension(image.FileName)}");
                DeleteImageIfAlreadyExists(id, uploads);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                AddImageUrlToDbProperty(id, image.FileName);
            }
        }

        public void DeleteImageIfAlreadyExists(int id, string uploads)
        {
            var projectToCheck = GetProjectById(id);

            if (projectToCheck.ImageUrl != null)
            {
                var files = Directory.GetFiles(uploads).ToList();

                foreach (var file in files)
                {
                    if (file.Contains(id.ToString()))
                    {
                        File.Delete(file);
                        break;
                    }
                }
            }
                
        } 
    }
}
