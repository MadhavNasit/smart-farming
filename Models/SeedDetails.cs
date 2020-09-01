using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class SeedDetails
    {
        public int id { get; set; }
        public int crop_id { get; set; }
        public string CropName { get; set; }
        public int dealer_id { get; set; }
        public string DealerName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
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
        public List<string> SeedImages { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
    }
}