using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class CropDetails
    {
        public int id { get; set; }
        public string CropName { get; set; }
        public string CropDesc { get; set; }
        public string LandPreparation { get; set; }
        public string SowingMethod { get; set; }
        public string Harvesting { get; set; }
        public bool Kharif { get; set; }
        public bool Rabi { get; set; }
        public bool Zaid { get; set; }
        public string Season { get; set; }
        public string CropType { get; set; }
        public string CropImage { get; set; }
        public string Temp { get; set; }
        public string RainFall { get; set; }
        public string SoilType { get; set; }
        public string HighestProducers { get; set; }
        public List<string> MajorProducers { get; set; }
    }
    public class MajorProducerState
    {
        public string StateName { get; set; }
    }
}