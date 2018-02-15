using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAWeb.Entities
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> context) : base(context)
        {
            Database.EnsureCreated();
        }

        public void AddProjectToDb()
        {
            var projectToAdd = new Project
            {
                Title = "First project",
                Description = "Long funky description",
                ImageUrl = "Null va"
            };
            
            Projects.Add(projectToAdd);
            SaveChanges();
        }

    }
}
