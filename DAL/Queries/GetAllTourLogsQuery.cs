using Microsoft.EntityFrameworkCore;
using TourPlanner.Domain.Model;
using DAL.QueryInterfaces;

namespace DAL.Queries
{
    public class GetAllTourLogsQuery : DbQueryBase, IGetAllTourLogsQuery
    {

        public GetAllTourLogsQuery(TourPlannerDbContextFactory contextFactory) : base(contextFactory) { }

        public async Task<IEnumerable<TourLog>> ExecuteAsync()
        {
            using var context = ContextFactory.Create();

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
