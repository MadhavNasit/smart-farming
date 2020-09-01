using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class ManageTractor
    {
        public int id { get; set; }
        public string ModelName { get; set; }
        public int Warrenty { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }

    }
}