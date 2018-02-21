using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PAWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace PAWeb.Entities
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> context) : base(context)
        {
            Database.EnsureCreated();
        }

        public void AddProjectToDb(Project projectToAdd)
        {
            Projects.Add(projectToAdd);
            SaveChanges();
        }

        public Project GetProjectById(int id)
        {
            var project = Projects.SingleOrDefault(p => p.Id == id);
            return project;
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
    }
}
