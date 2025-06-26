using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Domain.Model
{
    public class TourLog
    {
        
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Comment  { get; set; }
        public double Difficulty { get; set; }
        public double Distance { get; set; }
        public string Duration { get; set; }
        public double Rating    { get; set; }
        public int TourId { get; set; }
        

    }
}
