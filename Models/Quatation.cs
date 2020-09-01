using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class Quatation
    {
        public List<QuatationSeed> seedlist { get; set; }
        public List<QuatationPesticide> pesticidelist { get; set; }
        public List<QuatationFertilizer> fertilizerlist { get; set; }
    }
    public class QuatationSeed
    {
        public int id { get; set; }
        public int seed_id { get; set; }
        public string SeedName { get; set; }
        public int dealer_id { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string ContactNo { get; set; }
    }

    public class QuatationPesticide
    {
        public int id { get; set; }
        public int pesticide_id { get; set; }
        public string PesticideName { get; set; }
        public int dealer_id { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string ContactNo { get; set; }
    }

    public class QuatationFertilizer
    {
        public int id { get; set; }
        public int fertilizer_id { get; set; }
        public string FertilizerName { get; set; }
        public int dealer_id { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string ContactNo { get; set; }
    }
}