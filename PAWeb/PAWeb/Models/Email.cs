using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAWeb.Models
{
    public class Email
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Phone { get; set; }
    }
}
