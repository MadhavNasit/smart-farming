using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarming.Models
{
    public class NewsView
    {
        public int id { get; set; }
        public string Headline { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        private DateTime _date = DateTime.Now;
        public DateTime Date { get { return _date; } set { _date = value; } }
    }
}