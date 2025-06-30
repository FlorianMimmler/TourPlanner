using DAL.DTOs;
using DAL.QueryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain.Model;

namespace DAL.Queries
{
    public class DeleteTourLogQuery : DbQueryBase, IDeleteTourLogQuery
    {
        public DeleteTourLogQuery(TourPlannerDbContextFactory contextFactory) : base(contextFactory) { }

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

                context.TourLogs.Remove(tourLogDTO);
                return await context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                throw new Exception($"[DeleteTourLogQuery] Error deleting tourlog: {tourLog.Id}. {ex.Message}");
            }

        }
    }
}
