using TourPlanner.DAL.DTOs;
using Microsoft.EntityFrameworkCore;

namespace TourPlanner.DAL
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
