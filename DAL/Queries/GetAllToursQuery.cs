using Microsoft.EntityFrameworkCore;
using TourPlanner.Domain.Model;
using TourPlanner.DAL.QueryInterfaces;

namespace TourPlanner.DAL.Queries
{
    public class GetAllToursQuery : IGetAllToursQuery
    {

        private readonly TourPlannerDbContextFactory _contextFactory;

        public GetAllToursQuery(TourPlannerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Tour>> ExecuteAsync()
        {
            using var context = _contextFactory.Create();

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
