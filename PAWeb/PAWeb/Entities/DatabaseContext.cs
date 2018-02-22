using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PAWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using PAWeb.Extensions;
using PAWeb.Handler;

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

        public void AddImageUrlToDbProperty(int id, string fileName)
        {
            var project = GetProjectById(id);
            project.ImageUrl = fileName;

            Update(project);
            SaveChanges();
        }
    }
}
