using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class TourLogDto
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Comment { get; set; }
        public double Difficulty { get; set; }
        public double Distance { get; set; }
        public string Duration { get; set; }
        public double Rating { get; set; }
        [ForeignKey("Tour")]
        public Guid TourId { get; set; }

    }
}
