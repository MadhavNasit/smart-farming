using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class ContactUs
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public string Subject { get; set; }
    }
}