using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain.Model;
using TourPlanner.DAL.QueryInterfaces;

namespace TourPlanner.DAL.Queries
{
    public class GetAllTourLogsQuery : IGetAllTourLogsQuery
    {

        private readonly TourPlannerDbContextFactory _contextFactory;

        public GetAllTourLogsQuery(TourPlannerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<TourLog>> ExecuteAsync()
        {
            using var context = _contextFactory.Create();

            var tourLogsDTOs = await context.TourLogs.ToListAsync();
            return tourLogsDTOs.Select(tourLog => new TourLog
            {
                Id = tourLog.Id,
                TourId = tourLog.TourId,
                Date = tourLog.Date,
                Duration = tourLog.Duration,
                Distance = tourLog.Distance,
                Comment = tourLog.Comment,
                Difficulty = tourLog.Difficulty,
                Rating = tourLog.Rating
            });
        }

    }
}
