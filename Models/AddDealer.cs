using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class AddDealer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int role_id { get; set; }
        public List<Role> Roles { get; set; }
    }
}