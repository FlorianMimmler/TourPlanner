using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TourPlannerDbContextFactory
    {

        private readonly DbContextOptions<TourPlannerDbContext> _options;

        public TourPlannerDbContextFactory(DbContextOptions<TourPlannerDbContext> options)
        {
            _options = options;
        }

        public TourPlannerDbContext Create()
        {
            return new TourPlannerDbContext(_options);
        }

    }
}
