using TourPlanner.Domain.Model;
using DAL.DTOs;
using DAL.QueryInterfaces;

namespace DAL.Queries
{
    public class CreateTourQuery : DbQueryBase, ICreateTourQuery
    {

        public CreateTourQuery(TourPlannerDbContextFactory contextFactory): base(contextFactory) { }

        public async Task ExecuteAsync(Tour tour)
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

                context.Tours.Add(tourDTO);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CreateTourQuery] Error: {ex.Message}");
                throw;
            }

        }
    }
}
