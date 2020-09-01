using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class AddOrEditSeed
    {
        public int id { get; set; }
        public int crop_id { get; set; }
        public int dealer_id { get; set; }
        public List<CropList> CropList { get; set; }
        public string SeedName { get; set; }
        public string Season { get; set; }
        public string PestTolerance { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public int MinQuentity { get; set; }
        public string Description { get; set; }
        public string PacketSize { get; set; }
        public string GrowthHabit { get; set; }
        public string PositiveFactor { get; set; }
        public string NegativeFactor { get; set; }
        public string Temp { get; set; }
        public string Fertilizer { get; set; }
        public string Height { get; set; }
        public int SeedsPerPacket { get; set; }
        public HttpPostedFileBase[] Images { get; set; }
        public List<string> SeedImages { get; set; }
    }

    public class CropList
    {
        public int id { get; set; }
        public string Name { get; set; }
    }

}