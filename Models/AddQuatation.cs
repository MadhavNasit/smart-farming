using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class AddQuatation
    {
        public int seed_id { get; set; }
        public int dealer_id { get; set; }
        public string contactNo { get; set; }
        public int quantity { get; set; }
        public string noOfSeed { get; set; }
    }
}