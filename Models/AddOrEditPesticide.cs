using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class AddOrEditPesticide
    {
        public int id { get; set; }
        public int dealer_id { get; set; }
        public int crop_id { get; set; }
        public List<CropList> CropList { get; set; }
        public string Category { get; set; }
        public string PesticideName { get; set; }
        public string PesticideImage { get; set; }
        public string State { get; set; }
        public string PackingType { get; set; }
        public string Dosage { get; set; }
        public string Formulation { get; set; }
        public string PesticideDescription { get; set; }
        public decimal Price { get; set; }
    }
}