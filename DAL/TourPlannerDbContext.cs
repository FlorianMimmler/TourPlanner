using DAL.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TourPlannerDbContext: DbContext
    {

        public TourPlannerDbContext(DbContextOptions<TourPlannerDbContext> options)
            : base(options)
        {
        }

        public DbSet<TourDto> Tours { get; set; }

        public DbSet<TourLogDto> TourLogs { get; set; }
    }
}
