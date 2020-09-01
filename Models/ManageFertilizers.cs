using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class ManageFertilizers
    {
        public int id { get; set; }
        public string FertilizerName { get; set; }
        public string Image { get; set; }
        public string Material { get; set; }
        public string CropName { get; set; }
        public decimal Price { get; set; }
    }
}