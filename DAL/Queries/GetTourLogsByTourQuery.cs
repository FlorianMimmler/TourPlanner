using DAL.QueryInterfaces;
using Microsoft.EntityFrameworkCore;
using TourPlanner.Domain.Model;

namespace DAL.Queries
{
    public class GetTourLogsByTourQuery : DbQueryBase, IGetTourLogsByTourQuery
    {

        public GetTourLogsByTourQuery(TourPlannerDbContextFactory contextFactory) : base(contextFactory) { }

        public async Task<IEnumerable<TourLog>> ExecuteAsync(Guid tourId)
        {
            using var context = ContextFactory.Create();

            var tourLogsDTOs = await context.TourLogs.Where(tourLog => tourLog.TourId == tourId).ToListAsync();
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
