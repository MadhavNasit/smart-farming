using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class ManagePesticides
    {
        public int id { get; set; }
        public string PesticideName { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public string CropName { get; set; }
        public decimal Price { get; set; }
    }
}