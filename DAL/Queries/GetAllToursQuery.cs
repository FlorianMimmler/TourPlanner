using Microsoft.EntityFrameworkCore;
using TourPlanner.Domain.Model;
using DAL.QueryInterfaces;

namespace DAL.Queries
{
    public class GetAllToursQuery : DbQueryBase, IGetAllToursQuery
    {

        public GetAllToursQuery(TourPlannerDbContextFactory contextFactory) : base(contextFactory) { }

        public async Task<IEnumerable<Tour>> ExecuteAsync()
        {
            using var context = ContextFactory.Create();

            var toursDTOs = await context.Tours.ToListAsync();
            return toursDTOs.Select(tour => new Tour
            {
                Id = tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                From = tour.From,
                To = tour.To,
                TransportType = TransportTypeHelper.StringToTransportType(tour.TransportType),
                Distance = tour.Distance,
                EstimatedTime = tour.EstimatedTime,
                Image = tour.Image
            });


        }

    }
}
