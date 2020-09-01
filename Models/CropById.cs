using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class CropById
    {
        public CropDetails cropDetail { get; set; }
        public List<ManageSeeds> seedList { get; set; }
        public List<ManagePesticides> pesticideList { get; set; }
        public List<ManageFertilizers> fertilizerList { get; set; }
    }
}