using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class ManageSeeds
    {
        public int id { get; set; }
        public int crop_id { get; set; }
        public string CropName { get; set; }
        public string Image { get; set; }
        public List<CropList> Crops { get; set; }
        public int dealer_id { get; set; }
        public string SeedName { get; set; }
        public decimal Price { get; set; }
    }
}