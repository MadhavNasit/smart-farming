using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class AddOrEditFertilizer
    {
        public int id { get; set; }
        public int dealer_id { get; set; }
        public int crop_id { get; set; }
        public List<CropList> CropList { get; set; }
        public string Material { get; set; }
        public string FertilizerName { get; set; }
        public string FertilizerImage { get; set; }
        public string PackType { get; set; }
        public string PackSize { get; set; }
        public string Features { get; set; }
        public string Solubility { get; set; }
        public string Doses { get; set; }
        public string PHvalue { get; set; }
        public string HowToUse { get; set; }
        public decimal Price { get; set; }
    }
}