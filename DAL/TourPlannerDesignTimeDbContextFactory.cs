using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL;

namespace DAL
{
    class TourPlannerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TourPlannerDbContext>
    {

        public TourPlannerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TourPlannerDbContext>();

            // Replace with your actual connection string
            var connectionString = "Host=localhost;Port=5432;Database=TourPlanner;Username=admin;Password=password;";
            optionsBuilder.UseNpgsql(connectionString);

            return new TourPlannerDbContext(optionsBuilder.Options);
        }

    }
}
