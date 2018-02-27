using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Password { get; set; }
        public bool IsSignedIn { get; set; }
    }
}
