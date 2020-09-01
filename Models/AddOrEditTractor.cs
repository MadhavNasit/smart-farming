using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class AddOrEditTractor
    {
        public int id { get; set; }
        public int dealer_id { get; set; }
        public string ModelName { get; set; }
        public string TractorImage { get; set; }
        public int Capacity { get; set; }
        public string Clutch { get; set; }
        public decimal Price { get; set; }
        public int NoOfCylinder { get; set; }
        public int HpCategory { get; set; }
        public string EngineRatedRPM { get; set; }
        public string Cooling { get; set; }
        public string AirFilter { get; set; }
        public string Brake { get; set; }
        public string Battery { get; set; }
        public string GearBox { get; set; }
        public string Steering { get; set; }
        public string FuelTank { get; set; }
        public string Accessories { get; set; }
        public string Features { get; set; }
        public int Warranty { get; set; }
        public string Status { get; set; }
        public int TotalWeight { get; set; }
        public int GroundClearance { get; set; }
        public int TurningRadius { get; set; }
    }
}