using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL
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
