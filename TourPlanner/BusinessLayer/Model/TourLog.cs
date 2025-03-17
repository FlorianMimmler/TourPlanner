using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BusinessLayer.Model
{
    public class TourLog
    {
        public string Date { get; set; }
        public string Duration { get; set; }
        public double Distance { get; set; }
        public int Id { get; set; }
        public int TourId { get; set; }
      
    }
}
