using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class homeModel
    {
        public List<CropDetails> Crops { get; set; }
        public List<NewsView> News { get; set; }
    }
}