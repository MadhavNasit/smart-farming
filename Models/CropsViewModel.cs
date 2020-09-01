using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class CropsViewModel
    {
        public IEnumerable<CropDetails> FoodCrops { get; set; }
        public IEnumerable<CropDetails> CashCrops { get; set; }
        public IEnumerable<CropDetails> PlantationCrops { get; set; }
        public IEnumerable<CropDetails> HorticultureCrops { get; set; }
    }
}