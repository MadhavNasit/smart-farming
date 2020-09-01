using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class ManageCrop
    {
        public int id { get; set; }
        public string CropName { get; set; }
        public bool Kharif { get; set; }
        public bool Rabi { get; set; }
        public bool Zaid { get; set; }
        public string Season { get; set; }
        public string Type { get; set; }
    }
}