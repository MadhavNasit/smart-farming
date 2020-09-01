using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class UploadSeedImages
    {
        public int id { get; set; }

        public int SeedId { get; set; }
        public List<string> Images { get; set; }
    }
}