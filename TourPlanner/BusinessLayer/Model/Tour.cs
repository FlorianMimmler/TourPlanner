using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BusinessLayer.Model
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string? TransportType { get; set; }
        public double Distance { get; set; }
        public double EstimatedTime { get; set; }
        public string? Image { get; set; }
    }
}
