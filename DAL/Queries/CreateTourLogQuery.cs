using TourPlanner.Domain.Model;
using DAL.DTOs;
using DAL.QueryInterfaces;
using DAL.Queries;

namespace DAL.Queries
{
    public class CreateTourLogQuery : DbQueryBase, ICreateTourLogQuery
    {

        public CreateTourLogQuery(TourPlannerDbContextFactory contextFactory): base(contextFactory) { }

        public async Task<bool> ExecuteAsync(TourLog tourLog)
        {
            try
            {
                using var context = ContextFactory.Create();

                var tourLogDTO = new TourLogDto
                {
                    Id = tourLog.Id,
                    TourId = tourLog.TourId,
                    Date = tourLog.Date.ToUniversalTime(),
                    Duration = tourLog.Duration,
                    Distance = tourLog.Distance,
                    Comment = tourLog.Comment,
                    Difficulty = tourLog.Difficulty,
                    Rating = tourLog.Rating
                };

                context.TourLogs.Add(tourLogDTO);
                return await context.SaveChangesAsync() == 1;
            } catch (Exception ex)
            {
                throw new Exception($"[CreateTourLogQuery] Error creating tourlog. {ex.Message}");
            }

        }

    }
}
