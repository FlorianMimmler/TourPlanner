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
    public class DeleteTourQuery : DbQueryBase, IDeleteTourQuery
    {
        public DeleteTourQuery(TourPlannerDbContextFactory contextFactory) : base(contextFactory) { }

        public async Task<bool> ExecuteAsync(Tour tour)
        {
            try
            {
                using var context = ContextFactory.Create();

                var tourDTO = new TourDto
                {
                    Id = tour.Id,
                    Name = tour.Name,
                    Description = tour.Description,
                    From = tour.From,
                    To = tour.To,
                    TransportType = TransportTypeHelper.TransportTypeToString(tour.TransportType),
                    Distance = tour.Distance,
                    EstimatedTime = tour.EstimatedTime,
                    Image = tour.Image
                };

                context.Tours.Remove(tourDTO);
                return await context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                throw new Exception($"[DeleteTourQuery] Error deleting tour: {tour.Id}. {ex.Message}");
            }

        }
    }
}
